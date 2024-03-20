using Azure.Core;
using CroKnitters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace CroKnitters.Hubs
{
    public class PrivateChatHub : Hub
    {
        private CrochetAppDbContext _context;

        public PrivateChatHub(CrochetAppDbContext context)
        {
            _context = context;
        }

        public string GetConnectionId()
        => Context.ConnectionId;


        [HttpPost("[action]")]
        public async Task SendMessage(int userId, string message)
        {
            var currentUserId = int.Parse(Context.GetHttpContext().Request.Cookies["userId"]!); // Get the current user id

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


            //as the message is sent, let the other user receive the message on their end 
            await Clients.User(userId.ToString()).SendAsync("RecieveMessage", new
            {
                SenderId = currentUserId,
                Content = msg.Content,
                CreationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"),
                Sender = currentUser
            });
        }
    }
}
