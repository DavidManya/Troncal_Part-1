using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class StudentRepository : Repository<Student>
    {
        public static Dictionary<string, Student> StudentsByDni { get; set; } = new Dictionary<string, Student>();

        public override SaveResult<Student> Add(Student entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                StudentsByDni.Add(entity.Dni, entity);
            }

            return output;
        }

        public override SaveResult<Student> Update(Student entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                StudentsByDni[entity.Dni] = entity;
            }

            return output;
        }

        public override DeleteResult<Student> Delete(Student entity)
        {
            var output = base.Delete(entity);

            if (output.IsSuccess)
            {
                StudentsByDni.Remove(entity.Dni);
            }

            return output;
        }

        public Student GetStudentByDni(string dni)
        {
            if (StudentsByDni.ContainsKey(dni))
                return StudentsByDni[dni];

            return null;
        }

    }
}
