using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsFromHell.Lib.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "El nombre de la asignatura no puede superar los 80 carácteres.")]
        public string Name { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "El nombre del profesor no puede superar los 80 carácteres.")]
        public string Teacher { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }

        public Subject(int subjectid, string name, string teacher)
        {
            this.SubjectID = subjectid;
            this.Name = name;
            this.Teacher = teacher;
        }
    }
}
