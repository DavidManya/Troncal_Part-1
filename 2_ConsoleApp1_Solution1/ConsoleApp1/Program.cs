using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static string CurrentOption { get; set; }

        static List<double> Marks { get; set; }

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
                else if (option == 'e')
                {
                    Console.Beep();
                    Environment.Exit(0);
                }
            }
        }

        static void ShowMainMenu()
        {
            CurrentOption = "m";
            Console.WriteLine("Menú de opciones principal");

            Console.WriteLine("Opciones: m - Para volver a este menú");
            Console.WriteLine("Opciones: n - Añadir notas de alumnos");
            Console.WriteLine("Opciones: c - Estadísticas");
            Console.WriteLine("Opciones: e - Salir aplicación");
        }

        static void ShowAddNotesMenu()
        {
            CurrentOption = "n";
            Console.WriteLine("Menú de añadir notas. Añada notas y presione al enter");
            Console.WriteLine("Presione m para acabar y volver al menú principal");

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
                    if (double.TryParse(notaText.Replace(".", ","), out nota))
                    {
                        Marks.Add(nota);
                    }
                    else
                    {
                        Console.WriteLine($"valor introducidio [{notaText}] no válido");
                    }
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
            Console.WriteLine("Presione m para acabar y volver al menú principal");
            Console.WriteLine("Opciones: avg - obtener la media de las notas de los alumnos");
            Console.WriteLine("Opciones: max - obtener la máxima nota de los alumnos");
            Console.WriteLine("Opciones: min - obtener la mínima nota de los alumnos");

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
