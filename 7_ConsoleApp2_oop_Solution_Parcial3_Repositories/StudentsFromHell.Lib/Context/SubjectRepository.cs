using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System.Collections.Generic;

namespace Academy.Lib.Context
{
    public class SubjectRepository : Repository<Subject>
    {
        public static Dictionary<string, Subject> SubjectsByName { get; set; } = new Dictionary<string, Subject>();

        public override SaveResult<Subject> Add(Subject entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                SubjectsByName.Add(entity.Name, entity);
            }

            return output;
        }

        public override SaveResult<Subject> Update(Subject entity)
        {
            var output = base.Update(entity);

            if (output.IsSuccess)
            {
                SubjectsByName[entity.Name] = entity;
            }

            return output;
        }

        public Subject GetSubjectsByName(string name)
        {
            if (SubjectsByName.ContainsKey(name))
                return SubjectsByName[name];

            return null;
        }
    }
}
