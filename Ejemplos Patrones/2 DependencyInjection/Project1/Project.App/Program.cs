using _Project1.Lib.Context.Interfaces;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Common.Lib.Infrastructure;
using Project1.App;
using Project1.Lib.Models;
using System;

namespace Project.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var bootstrapper = new Bootstrapper();

            Entity.DepCon = new SimpleDependencyContainer();
            bootstrapper.Init(Entity.DepCon);

            var student = new Student();
            student.Name = "Perico";
            
            if (student.Save().IsSuccess)
            {
                Console.WriteLine("el estudiante se guardó correctamente");
            }

            var repo = Entity.DepCon.Resolve<IStudentRepository>();
            var std = repo.FindByDni("12323");
        }
    }
}
