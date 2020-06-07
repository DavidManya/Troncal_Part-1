using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace MVVMAcademy.Lib.Models
{
    public class SubjectsCollection : ObservableCollection<Subjects>
    {

    }

    public class Subjects : Entity
    {
        private string _Name;
        public string Name 
        {
            get { return _Name; }
            set { _Name = value; } 
        }

        private string _Teacher;
        public string Teacher 
        {
            get { return _Teacher; }
            set { _Teacher = value; }
        }

        public Subjects()
        {

        }

        public Subjects(string name, string teacher)
        {
            Name = name;
            Teacher = teacher;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
