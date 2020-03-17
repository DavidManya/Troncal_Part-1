using Common.Lib.Core;
using Common.Lib.Infrastructure;

namespace Project.Lib.Models
{
    public class Student : Entity
    {
        public string Name { get; set; }

        public string Dni { get; set; }

        public SaveResult<Student> Save()
        {
            return base.Save<Student>();
        }
    }
}
