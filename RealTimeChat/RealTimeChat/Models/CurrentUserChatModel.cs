using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeChat.Models
{
    public class CurrentUserChatModel
    {
        public string ChatParticipants { get; set; }

        public CurrentUserChatModel(string participants)
        {
            ChatParticipants = participants;
        }
    }
}
