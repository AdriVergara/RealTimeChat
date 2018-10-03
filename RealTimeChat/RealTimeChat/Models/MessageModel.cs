using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeChat.Models
{
    public class MessageModel
    {
        public string Title { get; set; }
        public string MessageOwner { get; set; }
        public File File { get; set; }

        public MessageModel()
        {

        }

        public MessageModel(string messageOwner, File file)
        {
            Title = null;
            MessageOwner = messageOwner;
            File = file;
        }

        public MessageModel(string title, string messageOwner, File file)
        {
            Title = title;
            MessageOwner = messageOwner;
            File = file;
        }

        public MessageModel(string title, string messageOwner)
        {
            Title = title;
            MessageOwner = messageOwner;
            File = null;
        }
    }
}
