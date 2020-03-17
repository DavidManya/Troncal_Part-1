using _Project1.Lib.Context.Interfaces;
using Common.Lib.Core.Context;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project.Lib.Client.Context;
using Project1.Lib.Models;

namespace Project1.Web.App
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
            dp.Register<IRepository<Student>, ServerStudentRepository>();
            dp.Register<IStudentRepository, ServerStudentRepository>();
        }
    }
}
