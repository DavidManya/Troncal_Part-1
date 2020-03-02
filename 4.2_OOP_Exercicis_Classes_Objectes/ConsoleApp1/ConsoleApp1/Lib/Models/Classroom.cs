using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Lib.Models
{
    public class Classroom
    {
        //public int IdSubjectCla { get; set; } serà key de Dictionary
        //public string DniCla { get; set; } també serà key del Dictionary
        public string DateEnrolment { get; set; }
        public int ChairNumber { get; set; }

        public Classroom()
        {
        }
    }
}
