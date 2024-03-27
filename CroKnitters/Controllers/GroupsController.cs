using CroKnitters.Entities;
using CroKnitters.Hubs;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CroKnitters.Controllers
{
    [Route("[Controller]")]
    public class GroupsController : Controller
    {
        private CrochetAppDbContext _context;
        private IHubContext<PrivateChatHub> _chat;

        public GroupsController(CrochetAppDbContext context, IHubContext<PrivateChatHub> chat)
        {
            _context = context;
            _chat = chat;
        }


        //find all the groups the current user is in
        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            //get user id
            var currentUserId = int.Parse(Request.Cookies["userId"]!);

            //get all groups
            var allGroups = _context.Groups.ToList();

            var userGroups = await _context.Groups
                    .Include(g => g.GroupUsers)
                    .Where(g => g.GroupUsers.Any(gu => gu.UserId == currentUserId))
                    .Select(g => new GroupsViewModel()
                    {
                        GroupId = g.GroupId,
                        GroupName = g.GroupName,
                        Description = g.Description,
                        CreationDate = g.CreationDate,
                        groups = allGroups,
                        MemberCount = g.GroupUsers.Count()
                    })
                    .ToListAsync();

            return View(userGroups);
        }


        [HttpGet("[action]/{id}")]
        public IActionResult GetChat(int id)
        {
            // Get user id
            var currentUserId = int.Parse(Request.Cookies["userId"]);
            
            // Get the group members
            var groupMembers = _context.GroupUsers
                .Include(g => g.Group)
                .Include(g => g.User)
                .ThenInclude(u => u.Image)
                .Where(g => g.GroupId == id)
                .ToList();

            // Get the chat history as MessageViewModels
            var chatHistory = _context.GroupChat
                .Include(gc => gc.Message)
                .Where(gc => gc.GroupId == id)
                .Select(gc => new MessageViewModel
                {
                    SenderId = gc.Message.SenderId.ToString(),
                    //ReceiverId = gc.Message.ReceiverId.ToString(),
                    Content = gc.Message.Content,
                    SentTime = gc.Message.CreationDate 
                })
                .ToList();

            // Prepare the view model
            var viewModel = new GroupChatViewModel
            {
                GroupId = id,
                groupMembers = groupMembers,
                viewModel = chatHistory 
            };

           

            // Pass the view model to the view
            return View("GroupChat", viewModel);
        }

        [HttpGet("[action]")] 
        public IActionResult CreateGroup()
        {
            //take them to the group form
            return View("CreateGroup");
        }


        [HttpPost("[action]")] //accept the data from the form
        public async Task<IActionResult> CreateGroup(NewGroupViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                //if the model state is valid, add the new group oject to the db and save
                var newGroup = new Group()
                {
                    GroupName = viewModel.GroupName,
                    Description = viewModel.Description,
                    CreationDate = DateTime.Now
                };

                _context.Groups.Add(newGroup);
                await _context.SaveChangesAsync();

                // Get user id
                var currentUserId = int.Parse(Request.Cookies["userId"]);

                //add the user/owner as a group user 
                var newGroupUser = new GroupUser()
                {
                    GroupId = newGroup.GroupId,
                    UserId = currentUserId,
                    Role = "Admin"
                };
                _context.GroupUsers.Add(newGroupUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // Log or inspect ModelState errors
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state.Errors)
                {
                    Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                }
            }
            //if not valid, take them to the group form
            return RedirectToAction("CreateGroup");
        }

        [HttpGet("[action]/{connectionId}/{id}")] //if a user wants to join a new group, get the id of that group
        public async Task<IActionResult> JoinGroup(string connectionId, int id)
        {
            //get user id
            var currentUserId = int.Parse(Request.Cookies["userId"]!);

            //create a new group user object and add the user to it
            var groupUser = new GroupUser()
            {
                GroupId = id,
                UserId = currentUserId,
                Role = "Member"
            };
            _context.GroupUsers.Add(groupUser);
            await _context.SaveChangesAsync();

            //take them to the group chat
            return RedirectToAction("GetChat", new {id = id});
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string senderId, string message, string groupId)
        {
            Console.WriteLine("sent data: sender ID:" + senderId + " , message content: " + message +" , group id: " + groupId);
            //var currentUserId = int.Parse(Context.GetHttpContext().Request.Cookies["userId"]!); // Get the current user id
            var SenderId = int.Parse(senderId);

            var GroupId = int.Parse(groupId);

            //find the current user
            var currentUser = _context.Users.Find(SenderId);

            //find the group
            var group = await _context.Groups.FindAsync(GroupId);
            Console.WriteLine(group.GroupName);
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

            var chat = new GroupChat()
            {
                GroupId = int.Parse(groupId),
                MessageId = msg.MessageId,
            };
            _context.GroupChat.Add(chat);
            await _context.SaveChangesAsync();

            //await _chat.Groups.AddToGroupAsync(Context.connectionId, groupName);

            //as the message is sent, let the other user receive the message on their end then return an Ok result
            await _chat.Clients.Group(group.GroupName).SendAsync("RecieveMessage", new
            {
                //SenderId = currentUserId,
                content = msg.Content,    
                creationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"),
                //Sender = currentUser
            });
            return Ok();
        }

    }
}
