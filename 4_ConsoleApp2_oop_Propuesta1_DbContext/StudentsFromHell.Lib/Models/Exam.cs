using System;
using System.Collections.Generic;

namespace Academy.Lib.Models
{
    public class Exam
    {
        //public int ExamID { get; set; } key del Dictionary
        public int SubjectID { get; set; }
        public string DniID { get; set; }
        public DateTime DateExam { get; set; }
        public double Note { get; set; }    
    }
}
