using _Project1.Lib.Context.Interfaces;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project1.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Lib.Client.Context
{
    public class ClientStudentRepository : ClientRepository<Student>, IStudentRepository
    {
        
        public Student FindByDni(string dni)
        {
            // haremos la llamada web al metdo find correspondiente

            return null;
        }
    }
}
