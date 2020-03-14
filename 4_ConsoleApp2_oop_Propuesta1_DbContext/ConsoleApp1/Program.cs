﻿using Academy.Lib.Context;
using Academy.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static string CurrentOption { get; set; }

        static void Main(string[] args)
        {

            InitialLoadData();

            Console.WriteLine("-- Bienvenid@ al programa para gestión de Academy --");
            Console.WriteLine("Opción: l / Para ver listado de Asignaturas");
            Console.WriteLine("Opción: a / Para gestión de Asignaturas");
            Console.WriteLine("Opción: g / Para gestión de Alumnos");
            Console.WriteLine("Opción: c / Para listar Asignaturas del Alumno");
            Console.WriteLine("Opción: s / Para obtener estadísticas");
            Console.WriteLine("Opción: e / Para salir de la aplicación");
            Console.WriteLine();

            var keepdoing = true;


            while (keepdoing)
            {
                var option = char.ToLower(Console.ReadKey().KeyChar);

                if (option == 'l')
                {
                    ShowSubjectList();
                }
                if (option == 'a')
                {
                    AddSubjects();
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

            static void ShowMainMenu()
            {
                Console.WriteLine("-- Menú principal --");
                Console.WriteLine("Opción: l / Para ver listado de Asignaturas");
                Console.WriteLine("Opción: a / Para gestión de Asignaturas");
                Console.WriteLine("Opción: g / Para gestión de Alumnos");
                Console.WriteLine("Opción: c / Para listar Asignaturas del Alumno");
                Console.WriteLine("Opción: s / Para obtener estadísticas");
                Console.WriteLine("Opción: e / Para salir de la aplicación");
                Console.WriteLine();
            }

            static void ShowSubjectList()
            {
                Console.WriteLine();
                Console.WriteLine("-- Lista de Cursos (Código/Nombre/Profesor) --");

                foreach (KeyValuePair<int, Subject> subject in DbContext.subjects)
                {
                    Console.WriteLine("{0}: {1}/{2}", Convert.ToString(subject.Key), subject.Value.Name, subject.Value.Teacher);
                }
                Console.WriteLine("----------------------------------------------");
            }

            static void AddSubjects()
            {

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

            static void ShowStudentsMenu()
            {
                Console.WriteLine();
                ShowStudentsMenuOptions();

                var keepdoing = true;

                while (keepdoing)
                {
                    var option = Console.ReadLine().ToLower();

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
                    var option = Console.ReadLine().ToLower();

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
                Console.WriteLine();
                Console.WriteLine("-- Listado de Alumnos --");

                foreach (KeyValuePair<string, Student> student in DbContext.students)
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
                    else if (!Student.ValidateDni(words[0]))
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else if (words.Length == 1 || words.Length == 2)
                    {
                        Console.WriteLine("Nombre del alumno incompleto");
                    }
                    else
                    {
                        var search = DbContext.students.Where(p => p.Key.Equals(words[0]));

                        if (search.Count() > 0)
                        {
                            Console.WriteLine("El DNI introducido, ya existe");
                        }
                        else
                        {
                            var student = new Student
                            {
                                Dni = words[0],
                                FirstName = words[1],
                                LastName = words[2]
                            };

                            if (student.Save())
                            {
                                Console.WriteLine("- Alta correcta -");
                            }
                            else
                            {
                                Console.WriteLine("- Alta incorrecta -");
                            }
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
                    else if (!Student.ValidateDni(words[0]))
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
                        var search = DbContext.students.Where(p => p.Key.Equals(words[0]));
                        if (search.Count() == 0)
                        {
                            Console.WriteLine("El DNI introducido, no está guardado");
                        }
                        else
                        {
                            var student = new Student
                            {
                                Id = words[0],
                                Dni = words[1],
                                FirstName = words[2],
                                LastName = words[3]
                            };
                            if (student.Save())
                            {
                                Console.WriteLine("- Modificación correcta -");
                            }
                            else
                            {
                                Console.WriteLine("- Modificación incorrecta -");
                            }
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
                    else if (!Student.ValidateDni(text))
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        var search1 = DbContext.students.Where(p => p.Key.Equals(text));

                        if (search1.Count() == 0)
                        {
                            Console.WriteLine("El DNI introducido, no está guardado");
                        }
                        else
                        {
                            DbContext.students.Remove(text);

                            var search2 = DbContext.courses.Where(p => p.Key.Item2.Contains(text));
                            if (search2.Count() > 0)
                            {
                                foreach (var result2 in search2)
                                {
                                    DbContext.courses.Remove(result2.Key);
                                }
                            }

                            var search3 = DbContext.exams.Where(p => p.Key.Equals(text));
                            if (search3.Count() > 0)
                            {
                                foreach (var result3 in search3)
                                {
                                    DbContext.exams.Remove(result3.Key);
                                }
                            }
                        }
                        Console.WriteLine("- Baja correcta -");
                        Console.WriteLine();
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
                    else if (!Student.ValidateDni(words[1]))
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else if (!(DateTime.TryParse(words[2], out dateTime)))
                    {
                        Console.WriteLine($"Revise la fecha introducida {words[2]}");
                    }
                    else
                    {
                        var search = DbContext.courses.Where(p => p.Key.Item1.Equals(Convert.ToInt32(words[0])) && p.Key.Item2.Equals(words[1]));

                        if (search.Count() > 0)
                        {
                            Console.WriteLine("El Alumno ya está matriculado en este Curso");
                        }
                        else
                        {
                            var tuple = new Tuple<int, string>(Convert.ToInt32(words[0]), words[1]);

                            var student = new Student
                            {
                                Dni = words[1]
                            };

                            var subject = new Subject
                            {
                                SubjectID = Convert.ToInt32(words[0])
                            };

                            if (!DbContext.ExistStudent(student))
                            {
                                Console.WriteLine("El alumno introducido no está en la base de datos");
                            }
                            else if (!DbContext.ExistSubject(subject))
                            {
                                Console.WriteLine("El código de la asignatura introducido no está en la base de datos");
                            }
                            else
                            {
                                DbContext.courses.Add(tuple, new Course { DateEnrolment = Convert.ToDateTime(words[2]), ChairNumber = Convert.ToInt32(words[3]) });
                                Console.WriteLine("- Alta Curso correcta -");
                                Console.WriteLine();
                            }
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
                    
                    clau = DbContext.exams.Max(x => x.Key) + 1;

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
                    else if (!Student.ValidateDni(words[0]))
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
                        var search = DbContext.exams.Where(p => p.Value.SubjectID.Equals(words[0]) && p.Value.DniID.Equals(words[1]) && p.Value.DateExam.Equals(words[2]));

                        if (search.Count() > 0)
                        {
                            Console.WriteLine("Esta Nota ya estaba añadida");
                        }
                        else
                        {
                            DbContext.exams.Add(clau, new Exam { SubjectID = Convert.ToInt32(words[0]), DniID = words[1], DateExam = Convert.ToDateTime(words[2]), Note = nota });
                            Console.WriteLine("- Nota añadida correctamente -");
                        }
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
                    else if (!Student.ValidateDni(text))
                    {
                        Console.WriteLine("El DNI está en un formato incorrecto");
                    }
                    else
                    {
                        var search = DbContext.courses.Where(p => p.Key.Item1.Equals(text));

                        Console.WriteLine("-- Listado de Asignaturas del Alumno (Nombre/Fecha Alta) --");

                        foreach (var result in search)
                        {
                            var search2 = DbContext.subjects.Where(x => x.Key.Equals(result.Key.Item2)).Select(x => x.Value.Name);
                            var name = search2.SingleOrDefault();
                            Console.WriteLine("{0} - {1}", name, Convert.ToString(result.Value.DateEnrolment));
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
                            var search = DbContext.exams.Where(x => x.Value.SubjectID.Equals(Convert.ToInt32(words[1])));

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
                                var search2 = DbContext.subjects.Where(x => x.Value.SubjectID.Equals(words[1])).Select(x => x.Value.Name);
                                var name = search2.SingleOrDefault();
                                Console.WriteLine($"La nota media para la Asignatura {name} es {sum / quant}");
                            }
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var sum = 0.0;
                            var quant = 0;
                            var search = DbContext.exams.Where(x => x.Value.DniID.Equals(words[1]));

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
                            var search = DbContext.exams.Where(x => x.Value.SubjectID.Equals(Convert.ToInt32(words[1])));

                            foreach (var result in search)
                            {
                                if (result.Value.Note < min)
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
                                var search2 = DbContext.subjects.Where(x => x.Value.SubjectID.Equals(words[1])).Select(x => x.Value.Name);
                                var name = search2.SingleOrDefault();
                                Console.WriteLine($"La nota mínima en la Asignatura {name} es {min}");
                            }
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var min = 11.0;
                            var search = DbContext.exams.Where(x => x.Value.DniID.Equals(words[1]));

                            foreach (var result in search)
                            {
                                if (result.Value.Note < min)
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
                            var search = DbContext.exams.Where(x => x.Value.SubjectID.Equals(Convert.ToInt32(words[1])));

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
                                var search2 = DbContext.subjects.Where(x => x.Value.SubjectID.Equals(words[1])).Select(x => x.Value.Name);
                                var name = search2.SingleOrDefault();
                                Console.WriteLine($"La nota máxima en la Asignatura {name} es {max}");
                            }
                        }
                        else
                        {
                            //Cerquem per Alumne
                            var max = 0.0;
                            var search = DbContext.exams.Where(x => x.Value.DniID.Equals(words[1]));

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

            #region Carga
            static void InitialLoadData()
            {
                //Llistat d'assignatures
                DbContext.subjects.Add(1, new Subject { SubjectID = 1, Name = "Algebra", Teacher = "Karenza Lark" });
                DbContext.subjects.Add(2, new Subject { SubjectID = 2, Name = "Computer Structure", Teacher = "Jacinth Kaelyn" });
                DbContext.subjects.Add(3, new Subject { SubjectID = 3, Name = "Databases", Teacher = "Vinny Maybelle" });
                DbContext.subjects.Add(4, new Subject { SubjectID = 4, Name = "Programming Methodology", Teacher = "Gae Pamila" });
                DbContext.subjects.Add(5, new Subject { SubjectID = 5, Name = "Digital Systems Design Principles", Teacher = "Terrance Ann" });
                DbContext.subjects.Add(6, new Subject { SubjectID = 6, Name = "Mathematical Analysis", Teacher = "Branden Khariton" });
                DbContext.subjects.Add(7, new Subject { SubjectID = 7, Name = "Fundamentals of Computer Technology", Teacher = "Cybill Aldous" });
                DbContext.subjects.Add(8, new Subject { SubjectID = 8, Name = "Computer Architecture", Teacher = "Myla Praskoviya" });
                DbContext.subjects.Add(9, new Subject { SubjectID = 9, Name = "Data Structures & Algorithms", Teacher = "Sage Velma" });
                DbContext.subjects.Add(10, new Subject { SubjectID = 10, Name = "Software Engineering", Teacher = "Shevon Yasmin" });
            }
            #endregion
        }
    }
}
