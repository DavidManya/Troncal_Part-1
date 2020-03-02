using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Lib.Models
{
    public class Subject
    {
        //public int IdSubject { get; set; } serà key de Dictionary
        public string Name { get; set; }
        public string Teacher { get; set; }

        public Subject()
        {
        }
    }
}
