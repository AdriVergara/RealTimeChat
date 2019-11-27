using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeChat.Models
{
    public class File
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }

        public File(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }
    }
}