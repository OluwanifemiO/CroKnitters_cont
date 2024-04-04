using CroKnitters.Entities;
using CroKnitters.Hubs;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CroKnitters.Controllers
{
    [Route("[Controller]")]
    public class ChatsController : Controller
    {
        private CrochetAppDbContext _context;
        private IHubContext<PrivateChatHub> _chat;
        private IMemoryCache _memoryCache;

        public ChatsController(CrochetAppDbContext context, IHubContext<PrivateChatHub> chat, IMemoryCache memoryCache)
        {
            _context = context;
            _chat = chat;
            _memoryCache = memoryCache;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index(string? search)
        {
            //query the users
            var userQuery = _context.Users.AsQueryable();
            userQuery = userQuery.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search));
            Console.WriteLine(userQuery);

            if (userQuery == null) { Console.WriteLine("No User!"); }
            else Console.WriteLine("User found!");

            var currentUserId = Int32.Parse(Request.Cookies["userId"]!); // Get the current user id

            // Fetch chats involving the current user
            var chats = _context.PrivateChat.Include(pc => pc.Message)
                .Where(pc => pc.SenderId == currentUserId || pc.RecieverId == currentUserId)
                .Select(pc => new
                {
                    ChatId = pc.PChatId,
                    PartnerId = pc.SenderId == currentUserId ? pc.RecieverId : pc.SenderId,
                    pc.CreationDate,
                    pc.MessageId,
                    Partner = _context.Users.FirstOrDefault(m => m.UserId == (pc.SenderId == currentUserId ? pc.RecieverId : pc.SenderId))
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
                LastMessageDate = c.CreationDate,
                users = userQuery.Include(u => u.Image).ToList(),
                UserImageSrc = c.Partner.ImageId.HasValue ? // Check if Partner has ImageId
                              _context.Images.FirstOrDefault(i => i.ImageId == c.Partner.ImageId.Value)?.ImageSrc : // Get ImageSrc if available
                               null // Set UserImageSrc to null if Partner doesn't have a profile picture
            }).ToList();

            ViewBag.users = null;
            ViewBag.users = userQuery.Include(u => u.Image);
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
                    ReceiverId = id.ToString(),
                    Content = pc.Message.Content,
                    SentTime = pc.Message.CreationDate,
                    senderInfo = _context.Users.FirstOrDefault(u => u.UserId == pc.SenderId),
                    receiverInfo = _context.Users.FirstOrDefault(u => u.UserId == id)
                })
                .OrderBy(m => m.SentTime) // Order by sent time for chronological order
                .ToListAsync();

            //if there is no history/ no previous messages
            if (chatHistory.Count < 1)
            {
                //just save the receiver id in a viewbag
                ViewBag.receiverId = id.ToString();
            }

            return View(chatHistory); // Pass the list of messages to the view
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string senderId, string message, string receiverId)
        {
            Console.WriteLine("sent data: sender ID:" + senderId + " , message content: " + message + " receiver id:" + receiverId);
            var SenderId = int.Parse(senderId);

            //retrieve the receiverId
            var ReceiverId = int.Parse(receiverId);
            //find the current user
            var currentUser = _context.Users.Find(SenderId);
            var fullName = currentUser.FirstName + " " + currentUser.LastName;
            //find the receiver
            var receiver = _context.Users.Find(ReceiverId);

            //save the data in the message model to the db 
            var msg = new Message()
            {
                SenderId = SenderId,
                Content = message,
                CreationDate = DateTime.Now,
                Sender = currentUser
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            var chat = new PrivateChat()
            {
                SenderId = SenderId,
                RecieverId = ReceiverId,
                MessageId = msg.MessageId,
                CreationDate = msg.CreationDate,
                Sender = currentUser,
                Reciever = receiver
            };
            _context.PrivateChat.Add(chat);
            await _context.SaveChangesAsync();

            var connectionId = _memoryCache.Get<string>("ConnectionId");
            Console.WriteLine("connection ID: " + connectionId);

            //as the message is sent, let the other user receive the message on their end then return an Ok result
            await _chat.Clients.All.SendAsync("RecieveMessage", new
            {
                senderId = senderId,
                content = msg.Content,
                creationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"),
                sender = fullName
            });
            return Ok();
        }
    }
}
