using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;

namespace Academy.Lib.Models
{
    public class Student : Entity
    {
        public string Dni { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public static bool ValidateDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {
                return false;
            }
            else if (dni.Length != 9)
            {
                return false;
            }
            else
            {
                return true;
            }
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

            validation = ValidateName(this.FirstName);
            if (!validation)
                return false;

            validation = ValidateName(this.LastName);
            if (!validation)
                return false;

            if (this.Id == null)
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
