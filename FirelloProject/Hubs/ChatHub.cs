﻿using FirelloProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace FirelloProject.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async  Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user, message);
        }
        public override async Task<Task> OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user=_userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectionId = Context.ConnectionId;
                var result=_userManager.UpdateAsync(user).Result;
                await Clients.All.SendAsync("UserOnline", user.Id);
            }

            return base.OnConnectedAsync();
        }
        public override async  Task<Task> OnDisconnectedAsync(Exception? exception)
        {
            AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
            user.ConnectionId = null;
            var result = _userManager.UpdateAsync(user).Result;
            await Clients.All.SendAsync("UserOffline", user.Id);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
