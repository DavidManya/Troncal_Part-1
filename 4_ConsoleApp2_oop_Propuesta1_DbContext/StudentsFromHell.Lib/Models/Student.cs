using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsFromHell.Lib.Models
{
    public class Student
    {
        public string DniID { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Los apellidos no pueden superar los 80 carácteres.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "El nombre no puede superar los 40 carácteres.")]
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
