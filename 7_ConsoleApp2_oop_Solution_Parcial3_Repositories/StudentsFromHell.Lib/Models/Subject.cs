using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Teacher { set; get; }

        //public Guid Guid { get; private set; }

        public void ValidateSub(ValidationResult validationResult)
        {
            var vr = ValidateSub(this.Name);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }
        public void ValidateTeacher(ValidationResult validationResult)
        {
            var vr = ValidateTeacher(this.Teacher);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }

        }

        public static ValidationResult<string> ValidateSub(string name, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido el nombre de la asignatura");
            }

            if (SubjectRepository.SubjectsByName.ContainsKey(name))
            {
                output.IsSuccess = false;
                output.Errors.Add($"Ya existe una asignatura con este nombre {name}");
            }

            #region check duplication
            var repo = new SubjectRepository();
            var entityWithName = repo.GetSubjectsByName(name);

            if (currentId == default && entityWithName != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add($"Ya existe una asignatura con ese nombre {name}");
            }
            else if (currentId != default && entityWithName.Id != currentId)
            {
                // on update
                output.IsSuccess = false;
                output.Errors.Add($"Ya existe una asignatura con ese nombre {name}");
            }
            #endregion

            if (output.IsSuccess)
            {
                output.ValidatedResult = name;
            }

            return output;
        }

        public static ValidationResult<string> ValidateExistSub(string name, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido el nombre de la asignatura");
            }

            if (!SubjectRepository.SubjectsByName.ContainsKey(name))
            {
                output.IsSuccess = false;
                output.Errors.Add($"No existe una asignatura con este nombre {name}");
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = name;
            }

            return output;
        }
        public static ValidationResult<string> ValidateTeacher(string name)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido el nombre del profesor");
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = name;
            }

            return output;
        }

        public override ValidationResult Validate()
        {
            var validationResult = new ValidationResult
            {
                IsSuccess = true
            };

            ValidateSub(validationResult);
            ValidateTeacher(validationResult);

            return validationResult;
        }

        public SaveResult<Subject> Save()
        {
            var saveResult = base.Save<Subject>();
            return saveResult.Cast<Subject>();
        }

        public override Repository<T> GetRepo<T>()
        {
            var output = new SubjectRepository();
            return output as Repository<T>;
        }
        public SubjectRepository GetSubjectRepo()
        {
            return GetRepo<Subject>() as SubjectRepository;
        }

    }
}
