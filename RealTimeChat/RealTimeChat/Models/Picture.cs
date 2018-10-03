using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeChat.Models
{
    public class Picture
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public Picture(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }
    }
}
