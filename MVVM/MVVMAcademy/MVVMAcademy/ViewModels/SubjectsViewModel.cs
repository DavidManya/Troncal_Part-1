using MVVMAcademy.Lib.Models;
using MVVMAcademy.Lib.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMAcademy.ViewModels
{
    public class SubjectsViewModel : ViewModelBase
    {
        public SubjectsViewModel()
        {
            SaveSubjectCommand = new RouteCommand(SaveSubject);
            GetSubjectsCommand = new RouteCommand(GetSubjects);
            DelSubjectCommand = new RouteCommand(DelSubject);
            EditSubjectCommand = new RouteCommand(EditSubject);
        }




        private SubjectsCollection _listSubjects = new SubjectsCollection();
        public SubjectsCollection ListSubjects
        {
            get => _listSubjects;
            set => _listSubjects = value;
        }

        private Subjects _currentSubjects;
        public Subjects CurrentSubjects
        {
            get => _currentSubjects;
            set => _currentSubjects = value;
        }
    }
}
