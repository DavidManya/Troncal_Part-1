using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class CourseRepository : Repository<Course>
    {
        public override SaveResult<Course> Add(Course entity)
        {
            var output = base.Add(entity);

            return output;
        }

        public override SaveResult<Course> Update(Course entity)
        {
            var output = base.Update(entity);

            return output;
        }
    }
}
