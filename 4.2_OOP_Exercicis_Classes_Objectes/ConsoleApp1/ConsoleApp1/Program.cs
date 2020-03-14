using ConsoleApp1.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static Dictionary<string, Student> students = new Dictionary<string, Student>();
        public static Dictionary<int, Subject> subjects = new Dictionary<int, Subject>();
        public static Dictionary<Tuple<int, string>, Classroom> classrooms = new Dictionary<Tuple<int, string>, Classroom>();
        public static Dictionary<int, Exam> exams = new Dictionary<int, Exam>();

        static void Main(string[] args)
        {
            InitialLoadData();

            Console.WriteLine("-- Bienvenid@ al programa para gestión de Alumnos --");
            Console.WriteLine("Opción: l / Para ver la lista de Asignaturas");
            Console.WriteLine("Opción: g / Para ir a la gestión de Alumnos");
            Console.WriteLine("Opción: c / Para listar Asignaturas del Alumno");
            Console.WriteLine("Opción: s / Para obtener estadísticas");
            Console.WriteLine("Opción: e / Para salir de la aplicación");
            Console.WriteLine();

            var keepdoing = true;

            while(keepdoing)
            {
                var option = Console.ReadKey().KeyChar;

                if (option == 'l')
                {
                    ShowSubjectList();
                }
                else if (option == 'g')
                {
                    ShowStudentsMenu();
                }
                else if (option == 'c')
                {
                    ShowSubjectStudentMenu();
                }
                else if (option == 's')
                {
                    ShowStatsMenu();
                }
                else if (option == 'e')
                {
                    ExitConsole();
                }
                else
                {
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }

            static void ShowStudentsMenuOptions()
            {
                Console.WriteLine("-- Menú gestión Alumnos --");
                Console.WriteLine("Opción: add / Para añadir un nuevo Alumno");
                Console.WriteLine("Opción: edit / Para editar un Alumno");
                Console.WriteLine("Opción: del / Para eliminar un Alumno");
                Console.WriteLine("Opción: all / Para ver todos los Alumnos");
                Console.WriteLine("Opción: sub / Para añadir Alumno a un Curso ");
                Console.WriteLine("Opción: mark / Para añadir Notas a los Alumnos");
                Console.WriteLine("Opción: m / Para volver al menú principal");
                Console.WriteLine("Opción: e / Para salir de la aplicación");
                Console.WriteLine();
            }

            static void ShowStatsMenuOptions()
            {
                Console.WriteLine();
                Console.WriteLine("-- Menú de Estadísticas --");
                Console.WriteLine("Opción: avg / Para ver la media");
                Console.WriteLine("Opción: max / Para ver la nota más alta");
                Console.WriteLine("Opción: min / Para ver la nota más baja");
                Console.WriteLine("Opción: m / Para volver al menú principal");
                Console.WriteLine();
            }

            static void ShowMainMenu()
            {
                Console.WriteLine("-- Menú principal --");
                Console.WriteLine("Opción: l / Para ver la lista de Cursos");
                Console.WriteLine("Opción: g / Para ir a la gestión de alumnos");
                Console.WriteLine("Opción: c / Para listar Asignaturas del Alumno");
                Console.WriteLine("Opción: s / Para obtener estadísticas");
                Console.WriteLine("Opción: e / Para salir de la aplicación");
                Console.WriteLine();
            }

            static void ShowSubjectList()
            {
                Console.WriteLine();
                Console.WriteLine("-- Lista de Cursos (Código/Nombre/Profesor) --");
                foreach(KeyValuePair<int, Subject> subject in subjects)
                {
                    Console.WriteLine("{0}: {1}/{2}", subject.Key, subject.Value.Name, subject.Value.Teacher);
                }
                Console.WriteLine("----------------------------------------------");
            }

            static void ShowStudentsMenu()
            {
                Console.WriteLine();
                ShowStudentsMenuOptions();

                var keepdoing = true;

                while (keepdoing)
                {
                    var option = Console.ReadLine();

                    if (option == "")
                    {
                        Console.WriteLine("No ha informado ninguna opción");
                    }
                    else if (option == "add")
                    {
                        AddNewStudent();
                    }
                    else if (option == "edit")
                    {
                        EditStudent();
                    }
                    else if (option == "del")
                    {
                        DeleteStudent();
                    }
                    else if (option == "all")
                    {
                        ShowAllStudents();
                    }
                    else if (option == "sub")
                    {
                        AddSubjectStudent();
                    }
                    else if (option == "mark")
                    {
                        AddMarkStudent();
                    }
                    else if (option == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (option == "e")
                    {
                        ExitConsole();
                    }
                    else
                    {
                        Console.WriteLine("Opción no reconocida. Introduzca una válida");
                    }

                }

                ShowMainMenu();
            }

            static void ShowStatsMenu()
            {
                Console.WriteLine();
                ShowStatsMenuOptions();

                while (true)
                {
                    var option = Console.ReadLine();

                    if (option == "m")
                    {
                        break;
                    }
                    else if (option == "avg")
                    {
                        ShowAverage();
                    }
                    else if (option == "max")
                    {
                        ShowMaximum();
                    }
                    else if (option == "min")
                    {
                        ShowMinimum();
                    }
                    else
                    {
                        Console.WriteLine("Opción no reconocida. Introduzca una válida");
                    }
                }

                ClearCurrentConsoleLine();
                Console.WriteLine();
                ShowMainMenu();
            }

            static void ShowAllStudents()
            {
                Console.WriteLine("-- Listado de Alumnos --");
                foreach (KeyValuePair<string, Student> student in students)
                {
                    Console.WriteLine("{0}: {1}, {2}", student.Key, student.Value.LastName, student.Value.FirstName);
                }
            }

            static void AddNewStudent()
            {
                Console.WriteLine();
                Console.WriteLine("-- Añadir nuevos Alumnos --");
                Console.WriteLine("Introduzca 3 campos separados por comas con el DNI, Nombre y Apellidos y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                { 
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (words[0].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else if (words.Length == 1 || words.Length == 2)
                    {
                        Console.WriteLine("Nombre del alumno incompleto");
                    }
                    else
                    {
                        if (students.ContainsKey(words[0]))
                        {
                            Console.WriteLine("El DNI introducido, ya existe");
                        }
                        else
                        {
                            students.Add(words[0], new Student { FirstName = words[1], LastName = words[2] });
                            Console.WriteLine("- Alta correcta -");
                            Console.WriteLine();
                        }
                    }
                }

                ShowStudentsMenuOptions();

            }

            static void EditStudent()
            {
                Console.WriteLine();
                Console.WriteLine("-- Editar datos Alumnos --");
                Console.WriteLine("Introduzca 4 campos separados por comas con el DNI guardado y los nuevos datos de DNI, Nombre y Apellidos, y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (words[0].Length != 9)
                    {
                        Console.WriteLine("El DNI guardado está en un formato incorrecto");
                    }
                    else if (words.Length == 1)
                    {
                        Console.WriteLine("No ha informado nuevos datos");
                    }
                    else if (words.Length == 2 || words.Length == 3)
                    {
                        Console.WriteLine("Nombre del alumno incompleto");
                    }
                    else
                    {
                        if (!students.ContainsKey(words[0]))
                        {
                            Console.WriteLine("El DNI introducido, no está guardado");
                        }
                        else
                        {
                            students.Remove(words[0]);
                            students.Add(words[1], new Student { FirstName = words[2], LastName = words[3] });
                            Console.WriteLine("- Modificación correcta -");
                            Console.WriteLine();
                        }
                    }
                }

                ShowStudentsMenuOptions();

            }

            static void DeleteStudent()
            {
                //S'ha d'eliminar d'Students, Classroom i Exam
                Console.WriteLine();
                Console.WriteLine("-- Eliminar Alumnos --");
                Console.WriteLine("Introduzca el DNI y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    var tuple = new Tuple<int, string>(0, text);
                    if (text == "")
                    {
                        Console.WriteLine("El DNI no está informado");
                    }
                    else if (text == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (text == "e")
                    {
                        ExitConsole();
                    }
                    else if (text.Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        if (!students.ContainsKey(text))
                        {
                            Console.WriteLine("El DNI introducido, no está guardado");
                        }
                        else
                        {
                            students.Remove(text);

                            foreach (KeyValuePair<Tuple<int, string>, Classroom> classroom in classrooms)
                            {
                                if (classroom.Key.Item2 == text)
                                {
                                    classrooms.Remove(classroom.Key);
                                }
                            }

                            foreach (KeyValuePair<int, Exam> exam in exams)
                            {
                                if (exam.Value.DniExa == text)
                                {
                                    exams.Remove(exam.Key);
                                }
                            }

                            Console.WriteLine("- Baja correcta -");
                        }
                    }
                }

                ShowStudentsMenuOptions();

            }

            static void AddSubjectStudent()
            {
                Console.WriteLine();
                Console.WriteLine("-- Añadir Alumnos a Cursos --");
                Console.WriteLine("Introduzca 4 campos separados por comas con el Código del Curso, DNI, Fecha alta y Asiento, y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');
                    DateTime dateTime;

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (words.Length == 1 || words.Length == 2 || words.Length == 3)
                    {
                        Console.WriteLine("No ha informado todos los datos");
                    }
                    else if (words[1].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else if (!(DateTime.TryParse(words[2], out dateTime)))
                    {
                        Console.WriteLine($"Revise la fecha introducida {words[2]}");
                    }
                    else
                    {
                        var tuple = new Tuple<int, string>(Convert.ToInt32(words[0]), words[1]);

                        if (classrooms.ContainsKey(tuple))
                        {
                            Console.WriteLine("El Alumno ya está matriculado en este Curso");
                        }
                        else
                        {
                            classrooms.Add(tuple, new Classroom { DateEnrolment = words[2], ChairNumber = Convert.ToInt32(words[3]) });
                            Console.WriteLine("- Alta Curso correcta -");
                        }
                    }
                }

                ShowStudentsMenuOptions();

            }

            static void AddMarkStudent()
            {
                Console.WriteLine();
                Console.WriteLine("-- Añadir notas a Alumnos --");
                Console.WriteLine("Introduzca 4 campos separados por comas con el Código de Asignatura, DNI del Alumno, fecha del exámen (aaaa-mm-dd) y la nota (0.0), y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    DateTime data;
                    Double nota;
                    var clau = 0;
                    if (exams.Count == 0)
                    {
                        clau = 1;
                    }
                    else
                    {
                        clau = exams.Max(x => x.Key) + 1;
                    }
                    
                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (words.Length == 1 || words.Length == 2 || words.Length == 3)
                    {
                        Console.WriteLine("No ha informado todos los datos");
                    }
                    else if (words[1].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else if (!DateTime.TryParse(words[2], out data))
                    {
                        Console.WriteLine($"Fecha introducida {words[2]} incorrecta");
                    }
                    else if (!Double.TryParse(words[3].Replace(".", ","), out nota))
                    {
                        Console.WriteLine($"Nota introducida {words[3]} incorrecta");
                    }
                    else
                    {
                        exams.Add(clau, new Exam { IdSubjectExa = Convert.ToInt32(words[0]), DniExa = words[1], DateExam = words[2], Note = nota });
                        Console.WriteLine("- Nota añadida correctamente -");
                    }
                }

                ShowStudentsMenuOptions();

            }

            static void ShowSubjectStudentMenu()
            {
                Console.WriteLine();
                Console.WriteLine("-- Ver Cursos del Alumno --");
                Console.WriteLine("Introduzca el DNI y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    if (text == "")
                    {
                        Console.WriteLine("El DNI no está informado");
                    }
                    else if (text == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (text == "e")
                    {
                        ExitConsole();
                    }
                    else if (text.Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        var search = classrooms.Where(p => p.Key.Item2.Contains(text));

                        Console.WriteLine("-- Listado de Asignaturas del Alumno (Nombre/Fecha Alta) --");

                        foreach (var result in search)
                        {
                            Console.WriteLine("{0} - {1}", subjects[result.Key.Item1].Name, result.Value.DateEnrolment);
                        }
                    }
                }

                ShowMainMenu();

            }

            #region Formulas
            static void ShowAverage()
            {
                Console.WriteLine();
                Console.WriteLine("-- Nota media --");
                Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
                Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el Código de Asignatura, y pulse enter");
                Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (!(words[0] == "1" || words[0] == "2"))
                    {
                        Console.WriteLine("Indique correctamente la opción");
                    }
                    else if (words.Length == 1 || words[1] == "")
                    {
                        Console.WriteLine("Falta la clave de búsqueda");
                    }
                    else if (words[0] == "2" && words[1].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        if (words[0] == "1")
                        {
                            //Cerquem per Asignatura
                            var sum = 0.0;
                            var quant = 0;
                            var search = exams.Where(x => x.Value.IdSubjectExa.Equals(Convert.ToInt32(words[1])));

                            foreach (var result in search)
                            {
                                sum += result.Value.Note;
                                quant += 1;
                            }
                            
                            if (sum == 0.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota media para la Asignatura {subjects[Convert.ToInt32(words[1])].Name} es {sum / quant}");
                            }  
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var sum = 0.0;
                            var quant = 0;
                            var search = exams.Where(x => x.Value.DniExa.Equals(words[1]));

                            foreach (var result in search)
                            {
                                sum += result.Value.Note;
                                quant += 1;
                            }
                            if (sum == 0.0)
                            {
                                Console.WriteLine("No hay notas para este Alumno");
                            }
                            else
                            {
                                Console.WriteLine($"La nota media para el Alumno {words[1]} es {sum / quant}");
                            }  
                        }
                    }
                }

                ShowStatsMenuOptions();
            }

            static void ShowMinimum()
            {
                Console.WriteLine();
                Console.WriteLine("-- Nota más baja --");
                Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
                Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el Código de Asignatura, y pulse enter");
                Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (!(words[0] == "1" || words[0] == "2"))
                    {
                        Console.WriteLine("Indique correctamente la opción");
                    }
                    else if (words.Length == 1 || words[1] == "")
                    {
                        Console.WriteLine("Falta la clave de búsqueda");
                    }
                    else if (words[0] == "2" && words[1].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        if (words[0] == "1")
                        {
                            //Cerquem per Asignatura
                            var min = 11.0;
                            var search = exams.Where(x => x.Value.IdSubjectExa.Equals(Convert.ToInt32(words[1])));

                            foreach (var result in search)
                            {
                                if(result.Value.Note < min)
                                {
                                    min = result.Value.Note;
                                }
                            }

                            if (min == 11.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota mínima en la Asignatura {subjects[Convert.ToInt32(words[1])].Name} es {min}");
                            }
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var min = 11.0;
                            var search = exams.Where(x => x.Value.DniExa.Equals(words[1]));

                            foreach (var result in search)
                            {
                                if(result.Value.Note < min)
                                {
                                    min = result.Value.Note;
                                }
                            }
                            if (min == 11.0)
                            {
                                Console.WriteLine("No hay notas para este Alumno");
                            }
                            else
                            {
                                Console.WriteLine($"La nota mínima del Alumno {words[1]} es {min}");
                            }
                        }
                    }
                }

                ShowStatsMenuOptions();
            }

            static void ShowMaximum()
            {
                Console.WriteLine();
                Console.WriteLine("-- Nota más alta --");
                Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
                Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el Código de Asignatura, y pulse enter");
                Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
                Console.WriteLine("- Para volver al menú anterior pulse m");
                Console.WriteLine();
                var keepdoing = true;

                while (keepdoing)
                {
                    var text = Console.ReadLine();
                    string[] words = text.Split(',');

                    if (words[0] == "")
                    {
                        Console.WriteLine("No ha introducido datos");
                    }
                    else if (words[0] == "m")
                    {
                        keepdoing = false;
                        break;
                    }
                    else if (words[0] == "e")
                    {
                        ExitConsole();
                    }
                    else if (!(words[0] == "1" || words[0] == "2"))
                    {
                        Console.WriteLine("Indique correctamente la opción");
                    }
                    else if (words.Length == 1 || words[1] == "")
                    {
                        Console.WriteLine("Falta la clave de búsqueda");
                    }
                    else if (words[0] == "2" && words[1].Length != 9)
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        if (words[0] == "1")
                        {
                            //Cerquem per Asignatura
                            var max = 0.0;
                            var search = exams.Where(x => x.Value.IdSubjectExa.Equals(Convert.ToInt32(words[1])));

                            foreach (var result in search)
                            {
                                if (result.Value.Note > max)
                                {
                                    max = result.Value.Note;
                                }
                            }

                            if (max == 0.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota máxima en la Asignatura {subjects[Convert.ToInt32(words[1])].Name} es {max}");
                            }
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var max = 0.0;
                            var search = exams.Where(x => x.Value.DniExa.Equals(words[1]));

                            foreach (var result in search)
                            {
                                if (result.Value.Note > max)
                                {
                                    max = result.Value.Note;
                                }
                            }
                            if (max == 0.0)
                            {
                                Console.WriteLine("No hay notas para este Alumno");
                            }
                            else
                            {
                                Console.WriteLine($"La nota máxima del Alumno {words[1]} es {max}");
                            }
                        }
                    }
                }

                ShowStatsMenuOptions();
            }

            #endregion

            static void ClearCurrentConsoleLine()
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
            }

            static void ExitConsole()
            {
                ClearCurrentConsoleLine();
                Console.Beep();
                Environment.Exit(0);
            }

            static void InitialLoadData()
            { 
                //Llistat d'assignatures
                subjects.Add(01, new Subject { Name = "Algebra", Teacher = "Karenza Lark" });
                subjects.Add(02, new Subject { Name = "Computer Structure", Teacher = "Jacinth Kaelyn" });
                subjects.Add(03, new Subject { Name = "Databases", Teacher = "Vinny Maybelle" });
                subjects.Add(04, new Subject { Name = "Programming Methodology", Teacher = "Gae Pamila" });
                subjects.Add(05, new Subject { Name = "Digital Systems Design Principles", Teacher = "Terrance Ann" });
                subjects.Add(06, new Subject { Name = "Mathematical Analysis", Teacher = "Branden Khariton" });
                subjects.Add(07, new Subject { Name = "Fundamentals of Computer Technology", Teacher = "Cybill Aldous" });
                subjects.Add(08, new Subject { Name = "Computer Architecture", Teacher = "Myla Praskoviya" });
                subjects.Add(09, new Subject { Name = "Data Structures & Algorithms", Teacher = "Sage Velma" });
                subjects.Add(10, new Subject { Name = "Software Engineering", Teacher = "Shevon Yasmin" });

                students.Add("12345678A", new Student { FirstName = "Lola", LastName = "Mento Mucho" });
                students.Add("23456789B", new Student { FirstName = "Elena", LastName = "Nito Del Bosque" });
                students.Add("34567890C", new Student { FirstName = "Dolores", LastName = "Fuertes De Barriga" });
                students.Add("45678901D", new Student { FirstName = "Armando", LastName = "Bronca Segura" });
                students.Add("56789012E", new Student { FirstName = "Matías", LastName = "Queroso Mogollón" });

                classrooms.Add(Tuple.Create(1, "12345678A"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 23 });
                classrooms.Add(Tuple.Create(2, "12345678A"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 23 });
                classrooms.Add(Tuple.Create(3, "12345678A"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 23 });
                classrooms.Add(Tuple.Create(4, "23456789B"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 22 });
                classrooms.Add(Tuple.Create(5, "23456789B"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 22 });
                classrooms.Add(Tuple.Create(6, "23456789B"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 22 });
                classrooms.Add(Tuple.Create(1, "34567890C"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 21 });
                classrooms.Add(Tuple.Create(2, "34567890C"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 21 });
                classrooms.Add(Tuple.Create(3, "34567890C"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 21 });
                classrooms.Add(Tuple.Create(4, "45678901D"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 20 });
                classrooms.Add(Tuple.Create(5, "45678901D"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 20 });
                classrooms.Add(Tuple.Create(6, "45678901D"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 20 });
                classrooms.Add(Tuple.Create(1, "56789012E"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 19 });
                classrooms.Add(Tuple.Create(2, "56789012E"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 19 });
                classrooms.Add(Tuple.Create(3, "56789012E"), new Classroom { DateEnrolment = "02-02-2020", ChairNumber = 19 });

                exams.Add(01, new Exam { IdSubjectExa = 1, DniExa = "12345678A", DateExam = "02-03-2020", Note = 7.5 });
                exams.Add(02, new Exam { IdSubjectExa = 2, DniExa = "12345678A", DateExam = "02-03-2020", Note = 7.0 });
                exams.Add(03, new Exam { IdSubjectExa = 3, DniExa = "12345678A", DateExam = "02-03-2020", Note = 8.5 });
                exams.Add(04, new Exam { IdSubjectExa = 4, DniExa = "23456789B", DateExam = "02-03-2020", Note = 6.5 });
                exams.Add(05, new Exam { IdSubjectExa = 5, DniExa = "23456789B", DateExam = "02-03-2020", Note = 9.5 });
                exams.Add(06, new Exam { IdSubjectExa = 6, DniExa = "23456789B", DateExam = "02-03-2020", Note = 6.0 });
                exams.Add(07, new Exam { IdSubjectExa = 1, DniExa = "34567890C", DateExam = "02-03-2020", Note = 5.5 });
                exams.Add(08, new Exam { IdSubjectExa = 2, DniExa = "34567890C", DateExam = "02-03-2020", Note = 8.5 });
                exams.Add(09, new Exam { IdSubjectExa = 3, DniExa = "34567890C", DateExam = "02-03-2020", Note = 7.0 });
                exams.Add(10, new Exam { IdSubjectExa = 4, DniExa = "45678901D", DateExam = "02-03-2020", Note = 7.5 });
                exams.Add(11, new Exam { IdSubjectExa = 5, DniExa = "45678901D", DateExam = "02-03-2020", Note = 8.5 });
                exams.Add(12, new Exam { IdSubjectExa = 6, DniExa = "45678901D", DateExam = "02-03-2020", Note = 9.5 });
                exams.Add(13, new Exam { IdSubjectExa = 1, DniExa = "56789012E", DateExam = "02-03-2020", Note = 5.0 });
                exams.Add(14, new Exam { IdSubjectExa = 2, DniExa = "56789012E", DateExam = "02-03-2020", Note = 5.0 });
                exams.Add(15, new Exam { IdSubjectExa = 3, DniExa = "56789012E", DateExam = "02-03-2020", Note = 4.5 });
            }
        }
    }
}
