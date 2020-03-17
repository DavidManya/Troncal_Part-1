using _Project1.Lib.Context.Interfaces;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project1.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Lib.Client.Context
{
    public class ServerStudentRepository : ServerRepository<Student>, IStudentRepository
    {
        private static Dictionary<string, Student> StudentByDni { get; set; } = new Dictionary<string, Student>();

        public override SaveResult<Student> Add(Student entity)

        {
            var output = base.Add(entity);

            StudentByDni.Add(entity.Dni, entity);

            return output;
        }


        public Student FindByDni(string dni)
        {
            return StudentByDni[dni];
        }
    }
}
