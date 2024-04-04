using Azure.Core;
using CroKnitters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace CroKnitters.Hubs
{
    public class PrivateChatHub : Hub
    {
        private CrochetAppDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public PrivateChatHub(CrochetAppDbContext context, IMemoryCache memoryCache)
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
    }
}
