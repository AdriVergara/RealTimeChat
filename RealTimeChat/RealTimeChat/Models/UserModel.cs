﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeChat.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }

        public UserModel(int id, string name)
        {
            Id = id;
            UserName = name;
        }
    }
}
