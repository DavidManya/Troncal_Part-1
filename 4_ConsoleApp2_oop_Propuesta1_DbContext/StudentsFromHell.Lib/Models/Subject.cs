using System.Collections.Generic;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Subject : Entity
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }

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
                output.Messages.Add("El nombre no está completo. Vuelva a escribirlo");
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }

        public bool Save()
        {
            var validation = ValidateName(this.Name);
            if (!validation.IsSuccess)
                return false;

            if (this.Id == null)
            {
                DbContext.AddSubject(this);
            }
            else
            {
                DbContext.UpdateSubject(this);
            }

            return true;
        }
    }
}