//using Academy.Lib.Models;
//using System;
//using System.Collections.Generic;

//namespace Academy.Lib.Context
//{
//    static class DbContext
//    {
//        #region Repositories

//        public static Dictionary<Guid, Exam> Exams { get; set; } = new Dictionary<Guid, Exam>();
//        public static Dictionary<Guid, Student> Students { get; set; } = new Dictionary<Guid, Student>();
//        public static Dictionary<Guid, Subject> Subjects { get; set; } = new Dictionary<Guid, Subject>();
//        #endregion

//        #region Indexes 

//        public static Dictionary<string, Student> StudentsByDni { get; set; } = new Dictionary<string, Student>();
//        public static Dictionary<string, Subject> SubjectsByName { get; set; } = new Dictionary<string, Subject>();
//        #endregion

//        #region Crud Student

//        public static bool AddStudent(Student student)
//        {
//            Students.Add(student.Id, student);
//            StudentsByDni.Add(student.Dni, student);

//            return true;
//        }

//        public static bool UpdateStudent(Student student)
//        {
//            if (Students.ContainsKey(student.Id))
//            {
//                var studentInMemory = Students[student.Id];

//                if (student != studentInMemory)
//                {
//                    Students[student.Id] = student;
//                    StudentsByDni[student.Dni] = student;
//                }
//            }
//            else
//            {
//                AddStudent(student);
//            }

//            return true;
//        }

//        #endregion

//        #region Crud Subject

//        public static bool AddSubject(Subject item)
//        {
//            Subjects.Add(item.Id, item);
//            SubjectsByName.Add(item.Name, item);

//            return true;
//        }

//        public static bool UpdateSubject(Subject item)
//        {
//            if (Subjects.ContainsKey(item.Id))
//            {
//                var itemInMemory = Subjects[item.Id];

//                if (item != itemInMemory)
//                {
//                    Subjects[item.Id] = item;
//                    SubjectsByName[item.Name] = item;
//                }
//            }
//            else
//            {
//                AddSubject(item);
//            }

//            return true;
//        }

//        #endregion
//    }
//}
