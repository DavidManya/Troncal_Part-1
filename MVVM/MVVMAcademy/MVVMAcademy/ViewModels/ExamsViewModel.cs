using MVVMAcademy.Lib.Models;
using MVVMAcademy.Lib.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMAcademy.ViewModels
{
    class ExamsViewModel : ViewModelBase
    {
        private ExamsCollection _listExams = new ExamsCollection();
        public ExamsCollection ListExams
        {
            get => _listExams;
            set => _listExams = value;
        }

        private Exams _currentExams;
        public Exams CurrentExams
        {
            get => _currentExams;
            set => _currentExams = value;
        }
    }
}
