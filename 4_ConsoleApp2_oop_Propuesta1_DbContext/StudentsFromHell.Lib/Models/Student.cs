using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsFromHell.Lib.Models
{
    public class Student
    {
        public string DniID { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "First name cannot be longer than 80 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "First name cannot be longer than 40 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        public Student(string dniid, string lastname, string firstname)
        {
            this.DniID = dniid;
            this.LastName = lastname;
            this.FirstName = firstname;
        }
    }
}
