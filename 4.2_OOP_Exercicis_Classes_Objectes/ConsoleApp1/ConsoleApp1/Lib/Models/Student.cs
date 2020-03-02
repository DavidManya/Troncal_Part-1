using System.Collections.Generic;

namespace ConsoleApp1.Lib.Models
{
    public class Student
    {
        //public string Dni { get; set; } serà key de Dictionary
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /*public string FullName
        {
            get
            {
                return (LastName + ", " + FirstName);
            }
        }*/

        public Student()
        {
        }
    }
}
