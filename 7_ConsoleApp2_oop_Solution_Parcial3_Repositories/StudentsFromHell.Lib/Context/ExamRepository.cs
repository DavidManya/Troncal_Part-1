using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class ExamRepository : Repository<Exam>
    {
        public override SaveResult<Exam> Add(Exam entity)
        {
            var output = base.Add(entity);

            return output;
        }

        //public override SaveResult<Exam> Update(Exam entity)
        //{
        //    var output = base.Update(entity);

        //    return output;
        //}
    }
}
