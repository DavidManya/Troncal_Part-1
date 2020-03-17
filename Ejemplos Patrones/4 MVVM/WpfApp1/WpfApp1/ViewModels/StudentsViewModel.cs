using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WpfApp1.Lib.Context;
using WpfApp1.Lib.Models;
using WpfApp1.Lib.UI;

namespace WpfApp1.Views
{
    public class StudentsViewModel : ViewModelBase
    {
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        string _email;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        string _password;

        public List<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
        List<Student> _students;


        public StudentsViewModel()
        {
            Email = "pepe@pepe.com";

            AddStudentCommand = new RouteCommand(AddStudent);
            GetStudentsCommand = new RouteCommand(GetStudents);
        }

        public void AddStudent()
        {
            var student = new Student();
            student.Id = Guid.NewGuid();
            student.Dni = "CCC" + DateTime.Now.Second;
            student.Name = "pocholo"+DateTime.Now.Second;
            student.ChairNumber = DateTime.Now.Second;

            DbContext.Students.Add(student.Id, student);
            GetStudents();
        }

        public void GetStudents()
        {
            Students = DbContext.Students.Values.ToList();
        }

        #region Commands
        public ICommand AddStudentCommand { get; set; }
        public ICommand GetStudentsCommand { get; set; }

        #endregion

    }
}
