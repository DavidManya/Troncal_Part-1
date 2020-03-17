using Common.Lib.Core.Context;
using Common.Lib.DAL.EFCore;
using Common.Lib.Infrastructure;
using Project.Lib.Context;
using Project.Lib.DAL.EFCore.Context;
using Project.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Lib.DAL.EFCore
{
    public class SubjectsRepository : EfCoreRepository<Subject>, ISubjectsRepository
    {
        private static Dictionary<string, Subject> SubjectsByName { get; set; }

        public SubjectsRepository(ProjectDbContext dbContext)
            : base(dbContext)
        {
            if (SubjectsByName == null)
            {
                SubjectsByName = new Dictionary<string, Subject>();

                foreach (var subject in dbContext.Subjects)
                    SubjectsByName.Add(subject.Name, subject);

            }
        }

        public override SaveResult<Subject> Add(Subject entity)
        {
            var output = base.Add(entity);

            if (output.IsSuccess)
            {
                SubjectsByName.Add(entity.Name, entity);
            }

            return output;
        }

        public Subject FindByName(string name)
        {
            return SubjectsByName[name];
        }

        public SaveResult<Subject> Update(Subject entity)
        {
            throw new NotImplementedException();
        }
    }
}
