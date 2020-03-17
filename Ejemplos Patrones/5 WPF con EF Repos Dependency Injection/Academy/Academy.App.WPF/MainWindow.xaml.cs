using Academy.Lib.DAL;
using Academy.Lib.Models;
using Academy.Lib.Repositories;
using Common.Lib.Core;
using Common.Lib.Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Academy.App.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var std = new Student();
            std.Name = "Perico";
            std.Dni = "abcd";
            var sr = std.Save<Student>();

            if (sr.IsSuccess)
            {
                var repoStudents = Entity.DepCon.Resolve<IStudentsRepository>();

                var student = repoStudents.FindByDni("abcd");
                MessageBox.Show(student.Name);
            }
        }
    }
}
