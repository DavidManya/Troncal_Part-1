using Academy.Lib.Context;
using Academy.Lib.Infrastructure;
using System;

namespace Academy.Lib.Models
{
    public abstract class  Entity
    {
        public Guid Id { get; set; }

        public ValidationResult CurrentValidation { get; private set; }

        public virtual SaveResult<T> Save<T>() where T : Entity
        {
            var output = new SaveResult<T>();

            CurrentValidation = Validate();

            if (CurrentValidation.IsSuccess)
            {
                //var repo = new Repository<T>();
                var repo = GetRepo<T>();

                if (this.Id == Guid.Empty)
                {
                    this.Id = Guid.NewGuid();
                    output = repo.Add(this as T);
                }
                else
                {
                    output = repo.Update(this as T);
                }
            }

            output.Validation = CurrentValidation;

            return output;
        }

        public virtual DeleteResult<T> Delete<T>() where T : Entity
        {
            var output = new DeleteResult<T>();

            CurrentValidation = ValidateDel();

            if (CurrentValidation.IsSuccess)
            {
                var repo = GetRepo<T>();
                output = repo.Delete(this as T);
            }

            output.Validation = CurrentValidation;

            return output;
        }

        public virtual ValidationResult Validate()
        {
            var output = new ValidationResult()
            {
                IsSuccess = true
            };

            return output;
        }

        public virtual ValidationResult ValidateDel()
        {
            var output = new ValidationResult()
            {
                IsSuccess = true
            };

            return output;
        }

        public virtual Repository<T> GetRepo<T>() where T : Entity
        {
            var output = new Repository<T>();
            return output;
        }
    }
}
