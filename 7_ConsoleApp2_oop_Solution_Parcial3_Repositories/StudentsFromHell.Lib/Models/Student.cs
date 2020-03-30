using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Student : Entity
    {
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Guid Guid { get; private set; }

        #region Domain Validations

        public void ValidateExist(ValidationResult validationResult)
        {
            var vr = ValidateExist(this.Dni);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateDni(ValidationResult validationResult)
        {
            var vr = ValidateDni(this.Dni, this.Id);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateFName(ValidationResult validationResult)
        {
            var vr = ValidateFName(this.FirstName);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public void ValidateLName(ValidationResult validationResult)
        {
            var vr = ValidateFName(this.LastName);
            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        #endregion

        #region Static Validations

        public static ValidationResult<string> ValidateDni(string dni, Guid currentId = default)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI del alumno no puede estar vacío");
            }
            
            if (dni.Length < 9)
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI del alumno no tiene un formato correcto");
            }

            #region check duplication
            var repo = new StudentRepository();
            var entityWithDni = repo.GetStudentByDni(dni);

            if (currentId == default && entityWithDni != null)
            {
                // on create
                output.IsSuccess = false;
                output.Errors.Add($"Ya existe un alumno con ese DNI {dni}");
            }
            else if (currentId != default && entityWithDni.Id != currentId)
            {
                // on update
                output.IsSuccess = false;
                output.Errors.Add($"Ya existe un alumno con ese DNI {dni}");
            }
            #endregion

            if (output.IsSuccess)
            {
                output.ValidatedResult = dni;
            }

            return output;
        }

        public static ValidationResult<string> ValidateExist(string dni)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI del alumno no puede estar vacío");
            }

            if (dni.Length < 9)
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI del alumno no tiene un formato correcto");
            }

            #region check existence
            var repo = new StudentRepository();
            var entityWithDni = repo.GetStudentByDni(dni);

            if (entityWithDni == null)
            {
                output.IsSuccess = false;
                output.Errors.Add($"No existe un alumno con ese DNI {dni}");
            }
            #endregion

            if (output.IsSuccess)
            {
                output.ValidatedResult = dni;
            }

            return output;
        }

        public static ValidationResult<string> ValidateFName(string fname)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(fname))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido el nombre");
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = fname;
            }

            return output;
        }

        public static ValidationResult<string> ValidateLName(string lname)
        {
            var output = new ValidationResult<string>()
            {
                IsSuccess = true
            };

            if (string.IsNullOrEmpty(lname))
            {
                output.IsSuccess = false;
                output.Errors.Add("No ha introducido los apellidos");
            }

            if (output.IsSuccess)
            {
                output.ValidatedResult = lname;
            }
            return output;
        }

        #endregion

        public override ValidationResult Validate()
        {
            var output = base.Validate();

            ValidateDni(output);
            ValidateFName(output);
            ValidateLName(output);

            return output;
        }

        public SaveResult<Student> Save()
        {
            var saveResult = base.Save<Student>();
            return saveResult;
        }
    }
}
