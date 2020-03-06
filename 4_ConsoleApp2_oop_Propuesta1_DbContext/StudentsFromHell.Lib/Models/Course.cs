using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsFromHell.Lib.Models
{
    public class Course
    {
		public int SubjectID { get; set; }
		public string DniID { get; set; }
		public DateTime DateEnrolment { get; set; }
		public int ChairNumber { get; set; }
		
		public virtual Subject Subject { get; set; }
		public virtual Student Student { get; set; }

		public Course(int subjectid, string dniid, DateTime dateenrolment, int chairnumber)
		{
			this.SubjectID = subjectid;
			this.DniID = dniid;
			this.DateEnrolment = dateenrolment;
			this.ChairNumber = chairnumber;
		}
	}
}
