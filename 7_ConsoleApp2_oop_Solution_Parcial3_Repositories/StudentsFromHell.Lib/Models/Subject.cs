using Academy.Lib.Context;
using Academy.Lib.Infrastructure;
using System;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }

        
        public void ValidateName(ValidationResult validationResult)
        {
            validationResult.IsSuccess = true;

            if (!string.IsNullOrEmpty(this.Name))
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add("el nombre de la asignatura no puede estar vacío");
            }

            if (DbContext.SubjectsByName.ContainsKey(this.Name))
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.Add($"Ya existe una asignatura que se llama {this.Name}");
            }           
        }

        public override ValidationResult Validate()
        {
            var validationResult = new ValidationResult
            {
                IsSuccess = true
            };

            ValidateName(validationResult);

            return validationResult;
        }

        public SaveResult<Subject> SaveStudent()
        {
            var saveResult = base.Save();
            return saveResult.Cast<Subject>();
        }
    }
}
