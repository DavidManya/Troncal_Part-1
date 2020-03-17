using Common.Lib.Core.Context;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project1.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.App
{
    public class Bootstrapper
    {
        public Bootstrapper()
        {

        }

        public void Init(IDependencyContainer dp)
        {
            RegisterRepositories(dp);
        }

        void RegisterRepositories(IDependencyContainer dp)
        {
            dp.Register<IRepository<Student>, ClientRepository<Student>>();
        }
    }
}
