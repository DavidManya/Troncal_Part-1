using Microsoft.EntityFrameworkCore;

namespace StudentsFromHell.Lib.Models
{
    class ApplicationDBContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
    }
}
