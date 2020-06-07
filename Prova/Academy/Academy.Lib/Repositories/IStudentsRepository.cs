using Academy.Lib.Models;
using Common.Lib.Core.Context;

namespace Academy.Lib.Repositories
{
    public interface IStudentsRepository : IRepository<Student>
    {
        Student FindByDni(string dni);
    }
}
