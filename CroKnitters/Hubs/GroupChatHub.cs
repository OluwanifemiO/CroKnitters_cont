using Azure.Core;
using CroKnitters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CroKnitters.Hubs
{
    public class GroupChatHub:Hub
    {
        private CrochetAppDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GroupChatHub(CrochetAppDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public string GetConnectionId()
        {
            var connectionId = Context.ConnectionId;

           return connectionId;
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            _memoryCache.Set<string>("ConnectionId", connectionId);
            await base.OnConnectedAsync();
        }

        public async Task SendMessageToGroup(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


        public async Task SendMessage(string senderId, string message, string groupId)
        {
            Console.WriteLine("sent data: sender ID:" + senderId + " , message content: " + message + " , group id: " + groupId);
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

            await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);

            //as the message is sent, let the other user receive the message on their end then return an Ok result
            await Clients.Group(group.GroupName).SendAsync("RecieveMessage", new
            {
                //SenderId = currentUserId,
                content = msg.Content,
                creationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"),
                //Sender = currentUser
            });
        }


        public async Task JoinGroup( int groupId)
        {
            //get user id
            var currentUserId = int.Parse(Context.GetHttpContext().Request.Cookies["userId"]!); // Get the current user id

            //find the group
            var group = await _context.Groups.FindAsync(groupId);

            //create a new group user object and add the user to it
            var groupUser = new GroupUser()
            {
                GroupId = groupId,
                UserId = currentUserId,
                Role = "Member"
            };
            _context.GroupUsers.Add(groupUser);
            await _context.SaveChangesAsync();

            await Groups.AddToGroupAsync(Context.ConnectionId, group.GroupName);

            //take them to the group chat
           // return RedirectToAction("GetChat", new { id = groupId });
        }

    }
}
