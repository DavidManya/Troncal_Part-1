using System.Collections.Generic;
using Academy.Lib.Context;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }

        public static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        public bool Save()
        {
            if (this.Id == null)
            {
                DbContext.AddSubject(this);
            }
            else
            {
                DbContext.UpdateSubject(this);
            }

            return true;
        }
    }
}