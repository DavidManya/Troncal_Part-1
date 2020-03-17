using Common.Lib.Core.Context;
using Project.Lib.Models;

namespace Project.Lib.Context
{
    public interface ISubjectsRepository : IRepository<Subject>
    {
        Subject FindByName(string name);
    }
}
