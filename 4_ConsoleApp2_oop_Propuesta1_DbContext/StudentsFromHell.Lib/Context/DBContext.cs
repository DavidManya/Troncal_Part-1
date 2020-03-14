
using Academy.Lib.Models;

using System;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public static class DbContext
    {
        #region Repositories
        public static Dictionary<string, Student> students = new Dictionary<string, Student>();
        public static Dictionary<int, Subject> subjects = new Dictionary<int, Subject>();
        public static Dictionary<Tuple<int, string>, Course> courses = new Dictionary<Tuple<int, string>, Course>();
        public static Dictionary<int, Exam> exams = new Dictionary<int, Exam>();
        #endregion

        #region Crud
        public static bool AddStudent(Student student)
        {
            students.Add(student.Dni, student);

            return true;
        }

        public static bool UpdateStudent(Student student)
        {
            if (student.Id != null && students.ContainsKey(student.Id))
            {
                var studentInMemory = students[student.Id];

                if (student != studentInMemory)
                {
                    students.Remove(student.Id);
                    AddStudent(student);
                }
            }
            else
            {
                AddStudent(student);
            }

            return true;
        }

        public static bool DeleteStudent(Student student)
        {
            students.Remove(student.Id);

            return true;
        }
        #endregion

        #region ValidateExist

        public static bool ExistStudent(Student student)
        {
            if (!students.ContainsKey(student.Dni))
            {
                return false;
            }

            return true;
        }

        public static bool ExistSubject(Subject subject)
        {
            if (!subjects.ContainsKey(subject.SubjectID))
            {
                return false;
            }
            
            return true;
        }
        #endregion
    }
}
