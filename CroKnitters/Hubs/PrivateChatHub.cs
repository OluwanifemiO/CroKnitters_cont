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


        public async Task SendMessage(string senderId, string message)
        {
            Console.WriteLine("sent data: " +senderId  + " " + message);
            //var currentUserId = int.Parse(Context.GetHttpContext().Request.Cookies["userId"]!); // Get the current user id
            var SenderId = int.Parse(senderId);
            //find the current user
            var currentUser = _context.Users.Find(SenderId);

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


            //as the message is sent, let the other user receive the message on their end 
            await Clients.All.SendAsync("ReceiveMessage", new
            {
                //SenderId = currentUserId,
                content = msg.Content,
                creationDate = msg.CreationDate.ToString("dd/MM/yyyy hh:mm:ss"),
                //Sender = currentUser
            });
        }
    }
}
