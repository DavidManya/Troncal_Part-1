using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace MVVMAcademy.Lib.Models
{
    public class StudentsCollection : ObservableCollection<Students>
    {

    }

    public class Students : Entity
    {
        private string _Dni;
        public string Dni 
        {
            get { return _Dni; }
            set { _Dni = value; } 
        }
        
        private string _FirstName;
        public string FirstName 
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _Email;
        public string Email 
        {
            get { return _Email; }
            set { _Email = value; } 
        }

        public Students()
        {

        }

        public Students(string dni, string firstname, string lastname, string email)
        {
            Dni = dni;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }

        public override string ToString()
        {
            return Dni;
        }
    }
}
