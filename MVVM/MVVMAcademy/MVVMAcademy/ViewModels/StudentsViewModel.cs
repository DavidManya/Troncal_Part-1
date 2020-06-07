using MVVMAcademy.Lib.Models;
using MVVMAcademy.Lib.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMAcademy.ViewModels
{
    public class StudentsViewModel : ViewModelBase
    {
        private StudentsCollection _listStudents = new StudentsCollection();
        public StudentsCollection ListStudents 
        { 
            get => _listStudents; 
            set => _listStudents = value; 
        }

        private Students _currentStudents;
        public Students CurrentStudents 
        { 
            get => _currentStudents; 
            set => _currentStudents = value; 
        }
    }
}
