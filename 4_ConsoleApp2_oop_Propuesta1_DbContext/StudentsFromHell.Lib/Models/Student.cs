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
        public string LastName { get; set; }
        public string FirstName { get; set; }

        #region Statics
        public static ValidationResult<string> ValidateDni(string dni, bool checkDniValid)
        {
            var output = new ValidationResult<string>
            {
                IsSuccess = true,
            };
            
            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI no está informado");
            }

            if (dni.Length != 9)
            {
                output.IsSuccess = false;
                output.Errors.Add("El DNI está en un formato incorrecto, vuelva a escribirlo");
            }

            return output;

        }

        public static ValidationResult<string> ValidateName(string name)
        {
            var output = new ValidationResult<string>
            {
                IsSuccess = true,
            };

            #region check format

            if (string.IsNullOrEmpty(name))
            {
                output.IsSuccess = false;
                output.Errors.Add("El nombre no está completo. Vuelva a escribirlo");
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }

        public bool Save()
        {
            var validation = ValidateDni(this.Dni, true);
            if (!validation.IsSuccess)
                return false;

            validation = ValidateName(this.FirstName);
            if (!validation.IsSuccess)
                return false;

            validation = ValidateName(this.LastName);
            if (!validation.IsSuccess)
                return false;

            if (this.Id == null)
            {
                DbContext.AddStudent(this);
            }
            else
            {
                DbContext.UpdateStudent(this);
            }

            return true;
        }
        #endregion
    }
}
