using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.DTO.Models
{
    public class UserChatMessage
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TimeStampString => TimeStamp.ToString("DD-MM-YYYY, HH:MM:SS");
    }
}
