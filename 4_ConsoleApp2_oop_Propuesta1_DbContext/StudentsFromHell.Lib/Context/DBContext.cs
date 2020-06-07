using Academy.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Lib.Context
{
    public static class DbContext
    {
        #region Repositories
        public static Dictionary<string, Student> students = new Dictionary<string, Student>();
        public static Dictionary<string, Subject> subjects = new Dictionary<string, Subject>();
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
            if (student.Id != null & students.ContainsKey(student.Id))
                {
                var studentInMemory = students[student.Id];

                if (student != studentInMemory)
                {
                    students[student.Dni] = student;
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
            students.Remove(student.Dni);

            return true;
        }

        public static bool AddSubject(Subject subject)
        {
            subjects.Add(subject.Id, subject);

            return true;
        }

        public static bool UpdateSubject(Subject subject)
        {
            if (subject.Id != null & subjects.ContainsKey(subject.Id))
            {
                var subjectInMemory = subjects[subject.Id];

                if (subject != subjectInMemory)
                {
                    subjects[subject.Id] = subject;
                }
            }
            else
            {
                AddSubject(subject);
            }

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
            if (!subjects.ContainsKey(subject.Id))
            {
                return false;
            }
            
            return true;
        }

        public static bool ExistChair(Tuple<int, string> tuple, Course course)
        {
            foreach (KeyValuePair<Tuple<int, string>, Course> classroom in courses)
            {
                if (classroom.Key.Item1 == tuple.Item1 & classroom.Value.ChairNumber == course.ChairNumber)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
