using System;

namespace ConsoleApp1.Lib.Models
{
    public class Exam
    {
        //public int IdExam { get; set; } serà key de Dictionary
        public int IdSubjectExa { get; set; }
        public string DniExa { get; set; }
        public string DateExam { get; set; }
        public double Note { get; set; }

        public Exam()
        {
        }
    }
}
