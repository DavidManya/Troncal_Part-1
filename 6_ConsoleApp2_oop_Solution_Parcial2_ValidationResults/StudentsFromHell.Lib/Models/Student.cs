using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Student : User
    {
        #region statics
        public static ValidationResult<string> ValidateDni(string dni, bool checkDniExists)
        {
            var output = new ValidationResult<string>
            {
                IsSuccess = true,
            };

            #region check format

            if (string.IsNullOrEmpty(dni))
            {
                output.IsSuccess = false;
                output.Messages.Add("el dni está en formato incorrecto, vuelva a escribirlo");
            }
            #endregion

            #region check duplicated

            if (checkDniExists && DbContext.StudentsByDni.ContainsKey(dni))
            {
                output.IsSuccess = false;
                output.Messages.Add("el dni ya existe");
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = dni;

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
                output.Messages.Add("el nombre está en formato incorrecto, vuelva a escribirlo");
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = name;

            return output;
        }

        public static ValidationResult<int> ValidateChairNumber(string chairNumberText)
        {
            var output = new ValidationResult<int>
            {
                IsSuccess = true,
            };

            var chairNumber = 0;
            var boolIsConversionOk = false;

            #region check is null or empty

            if (string.IsNullOrEmpty(chairNumberText))
            {
                output.IsSuccess = false;
                output.Messages.Add("el número de silla está vacío, vuelva a escribirlo");
            }
            #endregion

            #region check format

            boolIsConversionOk = int.TryParse(chairNumberText, out chairNumber);
            if (!boolIsConversionOk)
            {
                output.IsSuccess = false;
                output.Messages.Add($"el texto introducido [{chairNumberText}] para el formato de silla es incorrecto, vuelva a escribirlo");
            }

            #endregion

            #region check duplicated

            var estudianteSentado = DbContext.Students.Values.FirstOrDefault(s => s.ChairNumber == chairNumber);

            // esto  default(Student) es casi lo mismo que el nulo
            //if (boolIsConversionOk && DbContext.Students.Values.Any(s=>s.ChairNumber == chairNumber))
            if (boolIsConversionOk && estudianteSentado != default(Student))
            {
                output.IsSuccess = false;
                output.Messages.Add($"La silla {chairNumber} ya está ocupada por {estudianteSentado.Name}");
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = chairNumber;

            return output;
        }


        #endregion

        public string Dni { get; set; }

        public int ChairNumber { get; set; }

        public List<Exam> Exams
        {
            get
            {
                return DbContext.Exams.Values.Where(e => e.student.Id == this.Id).ToList();
            }
        }

        public bool Save()
        {
            var validation = ValidateDni(this.Dni, true);
            if (!validation.IsSuccess)
                return false;

            validation = ValidateName(this.Name);
            if (!validation.IsSuccess)
                return false;

            if (this.Id == Guid.Empty)
            {
                DbContext.AddStudent(this);
            }
            else
            {
                DbContext.UpdateStudent(this);
            }

            return true;
        }
    }
}
