using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;

namespace Academy.Lib.Models
{
    public class Student : User
    {
        public string Dni { get; set; }

        public List<Exam> Exams
        {
            get
            {
                return DbContext.Exams.Values.Where(e => e.student.Id == this.Id).ToList();
            }
        }

        public static bool ValidateDni(string dni)
        {
            return !string.IsNullOrEmpty(dni);
        }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public bool Save()
        {
            var validation = ValidateDni(this.Dni);
            if (!validation)
                return false;

            validation = ValidateName(this.Name);
            if (!validation)
                return false;

            if (this.Id == Guid.Empty)
            {                
                DbContext.AddStudent(this);
            }
            else
            {
                DbContext.UpdateStudent(this);
            }            

            return true;
        }
    }
}
