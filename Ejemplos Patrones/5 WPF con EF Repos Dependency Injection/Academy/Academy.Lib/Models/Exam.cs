using Common.Lib.Core;
using System;

namespace Academy.Lib.Models
{
    public class Exam : Entity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public Subject Subject { get; set; }

        public Guid SubjectId { get; set; }

        public Exam()
        {

        }
    }
}
