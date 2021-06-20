using ChatApp.DTO;
using ChatApp.DTO.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp2.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(UserChatMessage message)
        {
            message.TimeStamp = System.DateTime.Now;
            await Clients.All.SendAsync(Consts.RECEIVE_MESSAGE, message);
        }
    }
}
