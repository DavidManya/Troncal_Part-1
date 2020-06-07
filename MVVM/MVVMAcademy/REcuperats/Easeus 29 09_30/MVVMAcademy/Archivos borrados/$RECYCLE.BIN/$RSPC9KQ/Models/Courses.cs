using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace MVVMAcademy.Lib.Models
{
    public class CoursesCollection : ObservableCollection<Courses>
    {

    }

    public class Courses : Entity
    {
        //public int NameSubject { get; set; } És el mateix
        private string _NameSubject;
        public string NameSubject 
        {
            get { return _NameSubject; }
            set { _NameSubject = value; } 
        }

        private string _DniStudent;
        public string DniStudent 
        {
            get { return _DniStudent; }
            set { _DniStudent = value; }
        }

        private DateTime _DateEnrolment;
        public DateTime DateEnrolment 
        {
            get { return _DateEnrolment; }
            set { _DateEnrolment = value; }
        }

        private int _ChairNumber;
        public int ChairNumber 
        {
            get { return _ChairNumber; }
            set { _ChairNumber = value; } 
        }

        public Courses()
        {

        }

        public Courses(string namesubject, string dnistudent, DateTime dateenrolment, int chairnumber)
        {
            NameSubject = namesubject;
            DniStudent = dnistudent;
            DateEnrolment = dateenrolment;
            ChairNumber = chairnumber;
        }
    }
}
