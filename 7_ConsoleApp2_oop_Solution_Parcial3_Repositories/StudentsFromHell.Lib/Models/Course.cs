using System;
using System.Collections.Generic;
using System.Linq;
using Academy.Lib.Context;
using Academy.Lib.Infrastructure;

namespace Academy.Lib.Models
{
    public class Course : Entity
    {
        public string NameSubject { get; set; }
        //public Guid IdSubject { get; set; }
        public string DniStudent { get; set; }
        //public Guid IdStudent { get; set; }
        public DateTime DateEnrolment { get; set; }
        public int ChairNumber { get; set; }

        public void ValidateChairNumber(ValidationResult validationResult)
        {
            var vr = ValidateChairNumber(this.ChairNumber.ToString(), this.NameSubject);

            if (!vr.IsSuccess)
            {
                validationResult.IsSuccess = false;
                validationResult.Errors.AddRange(vr.Errors);
            }
        }

        public static ValidationResult<int> ValidateChairNumber(string chairNumberText, string namesubject)
        {
            var output = new ValidationResult<int>()
            {
                IsSuccess = true
            };

            var chairNumber = 0;
            var isConversionOk = false;

            #region check null or empty
            if (string.IsNullOrEmpty(chairNumberText))
            {
                output.IsSuccess = false;
                output.Errors.Add("El número de la silla no puede estar vacío");
            }
            #endregion

            #region check format conversion

            isConversionOk = int.TryParse(chairNumberText, out chairNumber);

            if (!isConversionOk)
            {
                output.IsSuccess = false;
                output.Errors.Add($"No se puede convertir {chairNumber} en número");
            }

            #endregion

            #region check if the char is already in use

            if (isConversionOk)
            {
                var repoCourse = new Repository<Course>();
                var currentStudentInChair = repoCourse.QueryAll().FirstOrDefault(s => s.ChairNumber == chairNumber && s.NameSubject == namesubject);

                if (currentStudentInChair != null)
                {
                    output.IsSuccess = false;
                    output.Errors.Add($"Ya hay un alumno en la silla {chairNumber}");
                }
            }
            #endregion

            if (output.IsSuccess)
                output.ValidatedResult = chairNumber;

            return output;
        }

        public override ValidationResult Validate()
        {
            var validationResult = new ValidationResult
            {
                IsSuccess = true
            };

            ValidateChairNumber(validationResult);

            return validationResult;
        }

        public SaveResult<Course> Save()
        {
            var saveResult = base.Save<Course>();
            return saveResult;
        }

        public override Repository<T> GetRepo<T>()
        {
            var output = new CourseRepository();
            return output as Repository<T>;
        }
        public CourseRepository GetCourseRepo()
        {
            return GetRepo<Course>() as CourseRepository;
        }
    }
}
