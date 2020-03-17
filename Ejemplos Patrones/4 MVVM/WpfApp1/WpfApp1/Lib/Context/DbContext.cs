using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Lib.Models;

namespace WpfApp1.Lib.Context
{
    public static class DbContext
    {
        static Dictionary<Guid, Student> _students;
        public static Dictionary<Guid, Student> Students
        {
            get
            {
                if(_students == null)
                {
                    _students = new Dictionary<Guid, Student>();

                    var student1 = new Student();
                    student1.Id = Guid.NewGuid();
                    student1.Dni = "AAA";
                    student1.Name = "Lolo";
                    student1.ChairNumber = 1;

                    var student2 = new Student();
                    student2.Id = Guid.NewGuid();
                    student2.Dni = "BBB";
                    student2.Name = "Pepa";
                    student2.ChairNumber = 2;

                    _students.Add(student1.Id, student1);
                    _students.Add(student2.Id, student2);

                }

                return _students;
            }
        }

    }
}
