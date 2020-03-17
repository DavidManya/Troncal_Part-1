using Common.Lib.Core.Context;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project.Lib.Context;
using Project.Lib.DAL.EFCore;
using Project.Lib.DAL.EFCore.Context;
using Project.Lib.Models;
using System;

namespace Project.Web.App
{
    public class Bootstrapper
    {
        public Bootstrapper()
        {

        }

        public void Init(IDependencyContainer dp, Func<ProjectDbContext> dbContextConst)
        {
            RegisterRepositories(dp, dbContextConst);
        }

        void RegisterRepositories(IDependencyContainer dp, Func<ProjectDbContext> dbContextConst)
        {
            var studentRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new EfCoreRepository<Student>(dbContextConst());
            }); 
            var subjectsRepoBuilder = new Func<object[], object>((parameters) =>
            {
                return new SubjectsRepository(dbContextConst());
            });

            dp.Register<IRepository<Student>, EfCoreRepository<Student>>(studentRepoBuilder);

            dp.Register<IRepository<Subject>, SubjectsRepository>(subjectsRepoBuilder);
            dp.Register<ISubjectsRepository, SubjectsRepository>(subjectsRepoBuilder);
        }
    }
}
