using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Nota { get; set; }
}

public class Exam
{
    public string Exams { get; set; }
}

public class Subject
{
    public string Subjects { get; set; }
}

namespace ConsoleApp1
{
    class Program
    {
        static string CurrentOption { get; set; }

        static List<double> Marks { get; set; }

        static Dictionary<string, Student> students = new Dictionary<string, Student>(StringComparer.InvariantCultureIgnoreCase);

        static void Main(string[] args)
        {
            Marks = new List<double>();

            Console.WriteLine("Bienvenidos al programa de gestión de clase");
            ShowMainMenu();

            while (true)
            {
                var option = Console.ReadKey().KeyChar;

                if (option == 'm')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "m")
                    {
                        Console.WriteLine();
                        ShowMainMenu();
                    }
                }
                else if (option == 'n')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "n")
                    {
                        Console.WriteLine();
                        ShowAddNotesMenu();
                    }
                }
                else if (option == 'c')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "c")
                    {
                        Console.WriteLine();
                        ShowStatisticsMenu();
                    }
                }
                else if (option == 'v')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "v")
                    {
                        Console.WriteLine();
                        ShowStudentMenu();
                    }
                }
                else if (option == 'e')
                {
                    ClearCurrentConsoleLine();
                    Console.Beep();
                    Environment.Exit(0);
                }
            }
        }

        static void ShowMainMenu()
        {
            CurrentOption = "m";
            Console.WriteLine("Menu de opciones principal");

            Console.WriteLine("Opciones: m - Para volver a este menú");
            Console.WriteLine("Opciones: n - Añadir datos y notas de alumnos (dni*nombre*apellido*nota)");
            Console.WriteLine("Opciones: c - Estadísticas");
            Console.WriteLine("Opciones: v - Visualizar datos alumno");
            Console.WriteLine("Opciones: e - Salir de la aplicación");
            Console.WriteLine();
        }

        static void ShowAddNotesMenu()
        {
            CurrentOption = "n";
            Console.WriteLine("Menú de añadir datos y notas. Añada datos y presione al enter");
            Console.WriteLine("Presione 'm' para acabar y volver al menú principal");
            Console.WriteLine();

            while (true)
            {
                var notaText = Console.ReadLine();
                
                if (notaText == "m")
                {
                    break;
                }
                else
                {
                    double nota;
                    char[] sep = { '*' };
                    var cadena = notaText.Split(sep);
                    var dni = cadena[0];
                    var nom = cadena[1];
                    var cognom = cadena[2];
                    var val = cadena[3].Replace(".", ",");

                    if (students.ContainsKey(($"{dni}")) == true)
                    {
                        Console.WriteLine($"Valor dni introducido [{dni}] ya existe. Repetir.");
                    }
                    else
                    {
                        
                        if (double.TryParse(val, out nota))
                        {
                            students.Add(dni, new Student { FirstName = nom, LastName = cognom, Nota = nota });
                            Marks.Add(nota);
                            //Console.WriteLine($"Alumno: {nom} {cognom}, DNI:{dni}");
                        }
                        else
                        {
                            Console.WriteLine($"Valor nota introducido [{val}] no válido. Repetir.");
                        }
                    }
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowStudentMenu()
        {
            CurrentOption = "v";

            Console.WriteLine("Menú para visualizar datos de los estudiantes. Inique DNI y presione enter");
            Console.WriteLine("Presione 'm' para acabar y volver al menú principal");
            Console.WriteLine();

            while (true)
            {
                var optiondni = Console.ReadLine();

                if (optiondni == "m")
                {
                    break;
                }

                if (students.ContainsKey(($"{optiondni}")) == false)
                {
                    Console.WriteLine($"Valor dni introducido [{optiondni}] no existe. Repetir.");
                }
                else
                {
                    Console.WriteLine("-- DATOS --");
                    Console.WriteLine($"Nombre: {students[optiondni].FirstName} {students[optiondni].LastName}");
                    Console.WriteLine($"DNI: {optiondni}");
                    Console.WriteLine($"Nota: {students[optiondni].Nota}");
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowStatisticsMenu()
        {
            CurrentOption = "c";

            Console.WriteLine("Opción de Estadísticas");
            Console.WriteLine("Presione 'm' para acabar y volver al menú principal");
            Console.WriteLine("Opciones: avg - Obtener la media de las notas de los alumnos");
            Console.WriteLine("Opciones: max - Obtener la máxima nota de los alumnos");
            Console.WriteLine("Opciones: min - Obtener la mínima nota de los alumnos");
            Console.WriteLine();

            while (true)
            {
                var optionText = Console.ReadLine();

                if (optionText == "m")
                {
                    break;
                }
                else if (optionText == "avg")
                {
                    ShowAverage();
                }
                else if (optionText == "max")
                {
                    ShowMaximum();
                }
                else if (optionText == "min")
                {
                    ShowMinimum();
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowAverage()
        {
            //var avg = GetAverage();
            Console.WriteLine($"La media actual es: {Marks.Average()}");
            Console.WriteLine();
        }

        static void ShowMinimum()
        {
            Console.WriteLine($"La nota más baja es: {Marks.Min()}");
            Console.WriteLine();
        }

        static void ShowMaximum()
        {
            Console.WriteLine($"La nota más alta es: {Marks.Max()}");
            Console.WriteLine();
        }     

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        #region Formulas

        public static double GetAverage()
        {
            var sum = 0.0;

            for (var i = 0; i < Marks.Count; i++)
            {
                sum += Marks[i];
            }

            return sum / Marks.Count;
        }


        #endregion
    }
}
