using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsFromHell.Lib.Models
{
    public class Exam
    {
        public int ExamID { get; set; }
        public int SubjectID { get; set; }
        public string DniID { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Exam Date")]
        public DateTime DateStamp { get; set; }
        public double Note { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }

        public Exam(int examid, int subjectid, string dniid, DateTime datestamp, int note)
        {
            this.ExamID = examid;
            this.SubjectID = subjectid;
            this.DniID = dniid;
            this.DateStamp = datestamp;
            this.Note = note;
        }          
    }
}
