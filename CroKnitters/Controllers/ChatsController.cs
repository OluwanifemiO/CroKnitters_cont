using CroKnitters.Entities;
using CroKnitters.Hubs;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CroKnitters.Controllers
{
    [Route("[Controller]")]
    public class ChatsController : Controller
    {
        private CrochetAppDbContext _context;
        private IHubContext<PrivateChatHub> _chat;

        public ChatsController(CrochetAppDbContext context, IHubContext<PrivateChatHub> chat)
        {
            _context = context;
            _chat = chat;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var currentUserId = Int32.Parse(Request.Cookies["userId"]!); // Get the current user id

            // Fetch chats involving the current user
            var chats = _context.PrivateChat.Include(pc => pc.Message)
                .Where(pc => pc.SenderId == currentUserId || pc.RecieverId == currentUserId)
                .Select(pc => new
                {
                    ChatId = pc.PChatId,
                    PartnerId = pc.SenderId == currentUserId ? pc.RecieverId : pc.SenderId,
                    pc.CreationDate,
                    pc.MessageId // Assuming you have direct access or this can be navigated to obtain message content
                })
                .AsEnumerable() // Switch to client-side processing for distinct by PartnerId, consider performance for large datasets
                .GroupBy(pc => pc.PartnerId)
                .Select(g => g.OrderByDescending(pc => pc.CreationDate).FirstOrDefault()) // Select the most recent chat with each partner
                .ToList();

            var model = chats.Select(c => new ChatsViewModel
            {
                ChatId = c.ChatId,
                PartnerUserName = _context.Users
                .Where(u => u.UserId == c.PartnerId)
                .Select(u => u.FirstName + ' ' + u.LastName)
                .FirstOrDefault(),
                PartnerId = c.PartnerId,
                LastMessage = _context.Messages
                .Where(m => m.MessageId == c.MessageId)
                .Select(m => m.Content)
                .FirstOrDefault(), 
                LastMessageDate = c.CreationDate
            }).ToList();

            return View(model);
        }

        [HttpGet("[action]/{id}")] //collect the other user's id 
        public async Task<IActionResult> PrivateChat(int id)
        {
            var currentUserId = int.Parse(Request.Cookies["userId"]!); // Get the current user id

            //use the other user's id to get the messages sent back and forth with the current user
            var chatHistory = await _context.PrivateChat
                .Where(m => (m.SenderId == currentUserId && m.RecieverId == id) ||
                    (m.SenderId == id && m.RecieverId == currentUserId))
                    .OrderBy(m => m.CreationDate)
                .Select(pc => new MessageViewModel
                {
                    SenderId = pc.SenderId.ToString(),
                    ReceiverId = pc.RecieverId.ToString(),
                    Content = pc.Message.Content, 
                    SentTime = pc.Message.CreationDate 
                })
                .OrderBy(m => m.SentTime) // Order by sent time for chronological order
                .ToListAsync();

            return View(chatHistory); // Pass the list of messages to the view
        }


        //[HttpPost]
        //public async Task<IActionResult> SaveMessage(MessageViewModel viewModel)
        //{
        //    var currentUserId = int.Parse(Request.Cookies["userId"]!); // Get the current user id

        //    //find the current user
        //    var currentUser = _context.Users.Where(cu => cu.UserId == currentUserId);

        //    //save the data in the message model to the db 
        //    var message = _context.Messages.Select(m => new Message()
        //    {
        //        MessageId = m.MessageId,
        //        Content = viewModel.Content,

        //    });

        //    return RedirectToAction("Index"); // Pass the list of messages to the view
        //}


        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(int userId, string message )
        {
            var currentUserId = int.Parse(Request.Cookies["userId"]!); // Get the current user id

            //find the current user
            var currentUser = _context.Users.Find(currentUserId);

            //save the data in the message model to the db 
            var msg = new Message()
            {
                SenderId = currentUserId,
                Content = message,
                CreationDate = DateTime.Now,
                Sender = currentUser
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();


            //as the message is sent, let the other user receive the message on their end then return an Ok result
            await _chat.Clients.User(userId.ToString()).SendAsync("RecieveMessage", new
            {
                SenderId = currentUserId,
                Content = msg.Content,
                CreationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"), 
                Sender = currentUser
            });
            return Ok(); 
        }
    }
}
