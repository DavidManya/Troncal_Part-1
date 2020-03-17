using Common.Lib.Core.Context;
using Project1.Lib.Models;

namespace _Project1.Lib.Context.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Student FindByDni(string dni);
    }
}
