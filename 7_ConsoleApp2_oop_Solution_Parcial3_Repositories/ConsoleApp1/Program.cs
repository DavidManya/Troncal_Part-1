using Academy.Lib.Context;
using Academy.Lib.Infrastructure;
using Academy.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public static string CurrentOption { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("-- Bienvenidos al programa de gestión de Academy --");
            ShowMainMenu();

            while (true)
            {
                var option = Console.ReadKey().KeyChar;

                if (option == 'm' || option =='M')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "m")
                    {
                        Console.WriteLine();
                        ShowMainMenu();
                    }
                }
                else if (option == 'a' || option == 'A')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "a")
                    {
                        Console.WriteLine();
                        ShowHandleStudentsMenu();
                    }
                }
                else if (option == 's' || option == 'S')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "s")
                    {
                        Console.WriteLine();
                        ShowHandleSubjectsMenu();
                    }
                }
                else if( option == 'n' || option == 'N')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "n")
                    {
                        Console.WriteLine();
                        ShowAddNotesMenu();
                    }
                }
                else if(option == 'c' || option == 'C')
                {
                    ClearCurrentConsoleLine();
                    if (CurrentOption != "c")
                    {
                        Console.WriteLine();
                        ShowStatisticsMenu();
                    }
                }
                else if(option == 'e' || option == 'E')
                {
                    ExitConsole();
                }
                else
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }
        }
                
        static void ShowMainMenu()
        {
            CurrentOption = "m";
            Console.WriteLine("- Menú principal de opciones -");
            Console.WriteLine("Opción: a / Gestión de Alumnos");
            Console.WriteLine("Opción: s / Gestión de Asignaturas");
            Console.WriteLine("Opción: n / Añadir notas de alumnos");
            Console.WriteLine("Opción: c / Obtener Estadísticas");
            Console.WriteLine("Opción: m / Para volver a este menú");
            Console.WriteLine("Opción: e / Para salir de la aplicación");
        }

        static void ShowHandleStudentsMenu()
        {
            CurrentOption = "a";
            Console.WriteLine("Menu de gestionar alumnos.");
            Console.WriteLine("Opción: a / Añadir un nuevo Alumno");
            Console.WriteLine("Opción: n / Información del Alumno");
            Console.WriteLine("Opción: e / Editar un Alumno existente");
            Console.WriteLine("Opción: l / Listado de Alumnos");
            Console.WriteLine("Opción: c / Añadir cursos al Alumno");
            Console.WriteLine("Opción: x / Exámenes del Alumno");
            Console.WriteLine("Opción: s / Asignaturas del Alumno");
            Console.WriteLine("Opción: m / Para acabar y volver al menú principal");
            Console.WriteLine();

            while (true)
            {
                var option = Console.ReadLine();

                if (option == "m" || option == "M")
                {
                    break;
                }
                else if (option == "a" || option == "A")
                {
                    Console.WriteLine("Para volver sin guardar Alumno escriba * en cualquier momento");
                    Console.WriteLine("Escriba el DNI:");

                    #region read dni
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateDni(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                        if (dni == "*")
                            break;
                    }

                    #endregion

                    #region read fname
                    Console.WriteLine("Escriba el nombre del Alumno:");
                    var fname = Console.ReadLine();

                    if (fname == "*")
                        break;

                    ValidationResult<string> vrFName;
                    while (!(vrFName = Student.ValidateFName(fname)).IsSuccess)
                    {
                        Console.WriteLine(vrFName.AllErrors);
                        fname = Console.ReadLine();
                        if (fname == "*")
                            break;
                    }
                    #endregion

                    #region read lname
                    Console.WriteLine("Ahora escriba los apellidos:");
                    var lname = Console.ReadLine();

                    if (lname == "*")
                        break;

                    ValidationResult<string> vrLName;
                    while (!(vrLName = Student.ValidateLName(lname)).IsSuccess)
                    {
                        Console.WriteLine(vrLName.AllErrors);
                        lname = Console.ReadLine();
                        if (lname == "*")
                            break;
                    }
                    #endregion

                    if (vrDni.IsSuccess && vrFName.IsSuccess && vrLName.IsSuccess)
                    {
                        var student = new Student
                        {
                            Dni = vrDni.ValidatedResult,
                            FirstName = vrFName.ValidatedResult,
                            LastName = vrLName.ValidatedResult
                        };

                        var sr = student.Save();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine($"Alumno guardado correctamente");
                        }
                        else
                        {
                            Console.WriteLine($"Uno o más errores han ocurrido y el Alumno no se ha guardado correctamente: {sr.AllErrors}");
                        }
                    }
                }
                else if (option == "n" || option == "N") //Informació Alumne
                {
                    //llegim dni, validem format i existència, si NO està error i si està mostrar dades
                    Console.WriteLine("Para volver sin consultar escriba * en cualquier momento");
                    Console.WriteLine("Escriba el DNI el Alumno a consultar:");

                    #region read dni
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateExist(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                        if (dni == "*")
                            break;
                    }
                    #endregion

                    if (vrDni.IsSuccess)
                    {
                        //var repo = new Repository<Student>();
                        //var data = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);
                        //var data = repo.QueryAll().Where( s => s.Dni == dni);
                        var repo = new StudentRepository();
                        var entityWithDni = repo.GetStudentByDni(dni);

                        Console.WriteLine("- Datos del Alumno -");
                        Console.WriteLine("{0}: {1}, {2}", entityWithDni.Dni, entityWithDni.LastName, entityWithDni.FirstName);
                    }

                }
                else if (option == "e" || option == "E") //Editar Alumne
                {
                    //llegim dni, validem format i existència, si NO està error i si està mostrar dades actuals i demanar noves
                    Console.WriteLine("Para volver sin modificar escriba * en cualquier momento");
                    Console.WriteLine("Escriba el DNI del Alumno a modificar:");

                    #region read dni
                    var idmod = Guid.Empty;
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateExist(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                        if (dni == "*")
                            break;
                    }
                    #endregion

                    if (vrDni.IsSuccess)
                    {
                        //var repo = new Repository<Student>();
                        //var data = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);
                        //var data = repo.QueryAll().Where( s => s.Dni == dni);
                        var repo = new StudentRepository();
                        var entityWithDni = repo.GetStudentByDni(dni);
                        idmod = entityWithDni.Id;

                        Console.WriteLine("- Datos del Alumno -");
                        Console.WriteLine("{0}: {1}, {2}", entityWithDni.Dni, entityWithDni.LastName, entityWithDni.FirstName);
                    }

                    Console.WriteLine("Para volver sin guardar modificaciones escriba * en cualquier momento");
                    Console.WriteLine("Escriba de nuevo el DNI:");

                    #region read dni
                    var dni2 = Console.ReadLine();

                    if (dni2 == "*")
                        break;

                    ValidationResult<string> vrDni2;
                    while (!(vrDni2 = Student.ValidateDni(dni2)).IsSuccess)
                    {
                        Console.WriteLine(vrDni2.AllErrors);
                        dni2 = Console.ReadLine();
                        if (dni2 == "*")
                            break;
                    }

                    #endregion

                    #region read fname
                    Console.WriteLine("Escriba de nuevo el nombre del Alumno:");
                    var fname = Console.ReadLine();

                    if (fname == "*")
                        break;

                    ValidationResult<string> vrFName;
                    while (!(vrFName = Student.ValidateFName(fname)).IsSuccess)
                    {
                        Console.WriteLine(vrFName.AllErrors);
                        fname = Console.ReadLine();
                        if (fname == "*")
                            break;
                    }
                    #endregion

                    #region read lname
                    Console.WriteLine("Ahora escriba de nuevo los apellidos:");
                    var lname = Console.ReadLine();

                    if (lname == "*")
                        break;

                    ValidationResult<string> vrLName;
                    while (!(vrLName = Student.ValidateLName(lname)).IsSuccess)
                    {
                        Console.WriteLine(vrLName.AllErrors);
                        lname = Console.ReadLine();
                        if (lname == "*")
                            break;
                    }
                    #endregion

                    if (vrDni2.IsSuccess && vrFName.IsSuccess && vrLName.IsSuccess)
                    {
                        var student = new Student
                        {
                            Id = idmod,
                            Dni = vrDni2.ValidatedResult,
                            FirstName = vrFName.ValidatedResult,
                            LastName = vrLName.ValidatedResult
                        };

                        var sr = student.Save();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine($"Alumno modificado correctamente");
                        }
                        else
                        {
                            Console.WriteLine($"Uno o más errores han ocurrido y el Alumno no se ha modificado correctamente: {sr.AllErrors}");
                        }

                    }
                }
                else if (option == "c" || option == "C") //Afegir Curs
                {
                    Console.WriteLine();
                    Console.WriteLine("-- Añadir el Alumno a Cursos --");
                    Console.WriteLine("Deberá introucir 3 campos con el Nombre de la Asignatura, DNI el Alumno y Asiento");
                    Console.WriteLine("- Para volver al menú anterior pulse m");
                    Console.WriteLine();

                    while (true)
                    {                 
                        Console.WriteLine("Introduzca Nombre de la Asignatura:");

                        var name = Console.ReadLine();

                        if (name == "m" || name == "M")
                        {
                            break;
                        }
                        else
                        {
                            DateTime dateTime = DateTime.Today;
                            //var chair = 0;

                            ValidationResult<string> vrName;
                            while (!(vrName = Subject.ValidateSub(name)).IsSuccess)
                            {
                                Console.WriteLine(vrName.AllErrors);
                                name = Console.ReadLine();
                            }

                            Console.WriteLine("Ahora introduzca el DNI del Alumno:");
                            var dni = Console.ReadLine();

                            ValidationResult<string> vrDni;
                            while (!(vrDni = Student.ValidateDni(dni)).IsSuccess)
                            {
                                Console.WriteLine(vrDni.AllErrors);
                                dni = Console.ReadLine();
                            }

                            Console.WriteLine("Y ahora introduzca el número de asiento:");
                            var chair = Console.ReadLine();

                            ValidationResult<int> vrChNum;
                            while (!(vrChNum = Course.ValidateChairNumber(chair)).IsSuccess)
                            {
                                Console.WriteLine(vrChNum.AllErrors);
                                chair = Console.ReadLine();
                            }

                            if (vrName.IsSuccess && vrDni.IsSuccess && vrChNum.IsSuccess)
                            {
                                var course = new Course
                                {
                                    NameSubject = vrName.ValidatedResult,
                                    DniStudent = vrDni.ValidatedResult,
                                    DateEnrolment = dateTime,
                                    ChairNumber = vrChNum.ValidatedResult
                                };

                                var sr = course.Save();

                                if (sr.IsSuccess)
                                {
                                    Console.WriteLine($"Asignatura añadida correctamente");
                                }
                                else
                                {
                                    Console.WriteLine($"Uno o más errores han ocurrido y la Asignatura no se ha añadido correctamente: {sr.AllErrors}");
                                }
                            }
                        }
                    }
                }
                else if (option == "l" || option == "L") //Llistat d'Alumnes
                {
                    var repo = new Repository<Student>();
                    var data = from result in repo.QueryAll() orderby result.LastName, result.FirstName select result;

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Alumnos --");
                        Console.WriteLine("DNI: Apellidos y Nombre");

                        foreach (var result in data)
                        {
                            Console.WriteLine("{0}: {1}, {2}", result.Dni, result.LastName, result.FirstName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos todavía");
                    }

                    Console.WriteLine();
                }
                else if (option == "x" || option == "X") //Exàmens Alumne
                {
                    //llegir dni, validar format i existència, si NO està error i si està recuperar dades d'Exam
                    Console.WriteLine("Escriba el DNI del Alumno a consultar:");
                    #region read dni
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateExist(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                    }
                    #endregion

                    var repo = new Repository<Exam>();
                    var data = repo.QueryAll().Where(s => s.DniStudent.Equals(dni));

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("- Exámenes del Alumno -");
                        Console.WriteLine("Asignatura: Fecha Exámen - Nota");

                        foreach (var result in data)
                        {
                            Console.WriteLine("{0}: {1} - {2}", result.NameSubject, result.DateExam, result.Mark);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos todavía");
                    }

                    Console.WriteLine();
            
                }
                else if (option == "s" || option == "S") //Assignatures Alumne
                {
                    //llegir dni, validar format i existència, si NO està error i si està recuperar dades de Course
                    Console.WriteLine("Escriba el DNI del Alumno a consultar:");
                    #region read dni
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateExist(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                    }
                    #endregion

                    var repo = new Repository<Course>();
                    var data = repo.QueryAll().Where(s => s.DniStudent.Equals(dni));

                    Console.WriteLine("- Asignaturas del Alumno -");
                    Console.WriteLine("Fecha Alta: Asignatura - Silla");

                    if (data.Count() > 0)
                    {
                        foreach (var result in data)
                        {
                            Console.WriteLine("{0}: {1} - {2}", result.DateEnrolment, result.NameSubject, result.ChairNumber);
                        }
                    }

                    Console.WriteLine();
                }
                else
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowHandleSubjectsMenu()
        {
            CurrentOption = "s";
            Console.WriteLine("- Menú de gestión de Asignaturas -");
            Console.WriteLine("Opción: a - Añadir una nueva Asignatura");
            Console.WriteLine("Opción: l - Listado de Asignaturas");
            Console.WriteLine("Opción: s - Ver alumnos por Asignatura");
            Console.WriteLine("Opción: e - Ver exámenes de Asignatura");
            Console.WriteLine("Opción: m - Para acabar y volver al menú principal");
            Console.WriteLine();

            while (true)
            {
                var option = Console.ReadLine();

                if (option == "m" || option == "M")
                {
                    break;
                }
                else if (option == "a" || option == "A")
                {
                    Console.WriteLine("Para volver sin guardar Asignatura escriba *");
                    Console.WriteLine("Escriba el nombre para la Asignatura:");
                    Console.WriteLine();

                    #region read name
                    var name = Console.ReadLine();

                    if (name == "*")
                        break;

                    ValidationResult<string> vrName;
                    while (!(vrName = Subject.ValidateSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                        if (name == "*")
                            break;
                    }
                    #endregion

                    #region read teacher
                    Console.WriteLine("Ahora escriba el nombre del profesor:");
                    var teacher = Console.ReadLine();

                    if (teacher == "*")
                        break;

                    ValidationResult<string> vrTeacher;
                    while (!(vrTeacher = Subject.ValidateTeacher(teacher)).IsSuccess)
                    {
                        Console.WriteLine(vrTeacher.AllErrors);
                        teacher = Console.ReadLine();
                        if (teacher == "*")
                            break;
                    }

                    #endregion

                    if (vrName.IsSuccess && vrTeacher.IsSuccess)
                    {
                        var subject = new Subject
                        {
                            Name = name,
                            Teacher = teacher
                        };

                        var sr = subject.SaveSubject();
                        if (sr.IsSuccess)
                        {
                            Console.WriteLine($"Asignatura guardada correctamente");
                        }
                        else
                        {
                            Console.WriteLine($"Uno o más errores han ocurrido y la Asignatura no se ha guardado correctamente");
                        }
                    }
                }
                else if (option == "l" || option == "L")
                {
                    var repo = new Repository<Subject>();
                    var data = repo.QueryAll();

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Asignaturas --");
                        Console.WriteLine("Asignatura - Profesor");

                        foreach (var result in data)
                        {
                            Console.WriteLine("{0} - {1}", result.Name, result.Teacher);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos todavía");
                    }

                    Console.WriteLine();

                }
                else if (option == "s" || option == "S") //Alumnes de l'Assignatura
                {
                    Console.WriteLine("Escriba el nombre de la Asignatura:");
                    Console.WriteLine();

                    #region read name
                    var name = Console.ReadLine();

                    if (name == "*")
                        break;

                    ValidationResult<string> vrName;
                    while (!(vrName = Subject.ValidateSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                    }
                    #endregion

                    var repo = new Repository<Course>();
                    var data = repo.QueryAll().Where(s => s.NameSubject.Equals(name));

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Alumnos para la Asignatura --");
                        Console.WriteLine("DNI: Apellidos, Nombre");

                        foreach (var result in data)
                        {
                            var repo2 = new Repository<Student>();
                            var cerca = from result2 in repo2.QueryAll().Where(s => s.Dni.Equals(result.DniStudent)) orderby result2.LastName, result2.FirstName select result2;

                            foreach (var result2 in cerca)
                            {
                                Console.WriteLine("{0}: {1}, {2}", result2.Dni, result2.LastName, result2.FirstName);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos todavía");
                    }

                    Console.WriteLine();

                }
                else if (option == "e" || option == "E") //Exàmens de l'Assignatura
                {
                    Console.WriteLine("Escriba el nombre de la Asignatura:");
                    Console.WriteLine();

                    #region read name
                    var name = Console.ReadLine();

                    if (name == "*")
                        break;

                    ValidationResult<string> vrName;
                    while (!(vrName = Subject.ValidateSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                    }
                    #endregion

                    var repo = new Repository<Exam>();
                    var data = from result in repo.QueryAll().Where(s => s.NameSubject.Equals(name)) orderby result.DniStudent select result;

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Resultados para la Asignatura --");
                        Console.WriteLine("DNI: Apellidos, Nombre - Fecha Exámen - Nota");

                        foreach (var result in data)
                        {
                            var repo2 = new Repository<Student>();
                            var cerca = from result2 in repo2.QueryAll().Where(s => s.Dni.Equals(result.DniStudent)) orderby result2.LastName, result2.FirstName select result2;

                            foreach (var result2 in cerca)
                            {
                                Console.WriteLine("{0}: {1}, {2} - {3} - {4}", result2.Dni, result2.LastName, result2.FirstName, result.DateExam, result.Mark);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos todavía");
                    }

                    Console.WriteLine();
                }
                else
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }
        
        static void ShowAddNotesMenu()
        {
            CurrentOption = "n";
            Console.WriteLine("- Menu de gestión de Notas -");
            Console.WriteLine("Opción: a - Añadir Notas de Exámenes");
            Console.WriteLine("Opción: m - Para acabar y volver al menú principal");
            //Console.WriteLine("Añada notas [dni, asignatura, fecha exámen, nota] y presione al enter");

            while (true)
            {
                var notaText = Console.ReadLine();

                if (notaText == "m" || notaText == "M" )
                {
                    break;
                }
                else if (notaText == "a" || notaText == "A" )               
                {
                    Console.WriteLine("Para añadir notas debe informar: DNI Alumno, Nombre Asignatura, Fecha de Exámen y la Nota");
                    Console.WriteLine("Para volver sin guardar Nota escriba * en cualquier momento");
                    Console.WriteLine("Escriba el DNI:");

                    #region read dni
                    var dni = Console.ReadLine();

                    if (dni == "*")
                        break;

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateDni(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                        if (dni == "*")
                            break;
                    }

                    #endregion

                    #region read name
                    Console.WriteLine("Ahora escriba el nombre de la Asignatura:");
                    var name = Console.ReadLine();

                    if (name == "*")
                        break;

                    ValidationResult<string> vrName;
                    while (!(vrName = Subject.ValidateSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                        if (name == "*")
                            break;
                    }
                    #endregion

                    #region fecha
                    Console.WriteLine("Ahora escriba la Fecha del Exámen (dd/mm/aaaa):");
                    var date = Console.ReadLine();

                    if (date == "*")
                        break;

                    ValidationResult<DateTime> vrDate;
                    while (!(vrDate = Exam.ValidateDate(date)).IsSuccess)
                    {
                        Console.WriteLine(vrDate.AllErrors);
                        date = Console.ReadLine();
                        if (date == "*")
                            break;
                    }
                    #endregion

                    #region mark
                    Console.WriteLine("Ahora escriba la Nota del Exámen (0,0):");
                    var mark = Console.ReadLine().Replace(".", ",");

                    if (mark == "*")
                        break;

                    ValidationResult<Double> vrMark;
                    while (!(vrMark = Exam.ValidateMark(mark)).IsSuccess)
                    {
                        Console.WriteLine(vrDate.AllErrors);
                        mark = Console.ReadLine().Replace(".", ",");
                        if (mark == "*")
                            break;
                    }
                    #endregion

                    if (vrDni.IsSuccess && vrName.IsSuccess && vrDate.IsSuccess && vrMark.IsSuccess)
                    {
                        var exam = new Exam
                        {
                            DniStudent = vrDni.ValidatedResult,
                            NameSubject = vrName.ValidatedResult,
                            DateExam = vrDate.ValidatedResult,
                            Mark = vrMark.ValidatedResult
                        };

                        var sr = exam.Save();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine($"Nota guardada correctamente");
                        }
                        else
                        {
                            Console.WriteLine($"Uno o más errores han ocurrido y la Nota no se ha guardado correctamente: {sr.AllErrors}");
                        }
                    }
                }
                else
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowStatisticsMenu()
        {
            CurrentOption = "c";

            Console.WriteLine("- Menú de Estadísticas -");
            Console.WriteLine("Opción: avg - Media de las notas de los Alumnos");
            Console.WriteLine("Opción: max - Máxima nota de los Alumnos");
            Console.WriteLine("Opción: min - Mínima nota de los Alumnos");
            Console.WriteLine("Opción: m - Para acabar y volver al menú principal");
            Console.WriteLine();

            while (true)
            {
                var optionText = Console.ReadLine();

                if (optionText == "m" || optionText == "M")
                {
                    break;
                }
                else if (optionText == "avg" || optionText == "AVG")
                {
                    ShowAverage();
                }
                else if (optionText == "max" || optionText == "MAX")
                {
                    ShowMaximum();
                }
                else if (optionText == "min" || optionText == "MIN")
                {
                    ShowMinimum();
                }
                else
                {
                    ClearCurrentConsoleLine();
                    Console.WriteLine("Opción no reconocida. Introduzca una válida");
                }
            }

            ClearCurrentConsoleLine();
            Console.WriteLine();
            ShowMainMenu();
        }

        static void ShowAverage()
        {
            Console.WriteLine();
            Console.WriteLine("-- Nota media --");
            Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
            Console.WriteLine("- Para volver al menú anterior pulse m");
            Console.WriteLine();

            while (true)
            {
                var text = Console.ReadLine();
                string[] words = text.Split(',');

                if (words[0] == "")
                {
                    Console.WriteLine("No ha introducido datos");
                }
                else if (words[0] == "m")
                {
                    break;
                }
                else if (words[0] == "e" || words[0] == "E" )
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
                else
                {
                    if (words[0] == "1")
                    {
                        //Cerquem per Asignatura
                        ValidationResult<string> vrName;
                        if (!(vrName = Subject.ValidateSub(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrName.AllErrors);
                        }
                        else
                        {
                            var sum = 0.0;
                            var quant = 0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.NameSubject.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                sum += result.Mark;
                                quant += 1;
                            }

                            if (sum == 0.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota media para la Asignatura {words[1]} es {sum / quant}");
                            }
                        }
                    }
                    else
                    {
                        //Cerquem per Alumne
                        ValidationResult<string> vrDni;
                        if (!(vrDni = Student.ValidateDni(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrDni.AllErrors);
                        }
                        else
                        {
                            var sum = 0.0;
                            var quant = 0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.DniStudent.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                sum += result.Mark;
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
            }

            Console.WriteLine();
            ShowStatisticsMenu();
        }

        static void ShowMinimum()
        {
            Console.WriteLine();
            Console.WriteLine("-- Nota más baja --");
            Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
            Console.WriteLine("- Para volver al menú anterior pulse m");
            Console.WriteLine();

            while (true)
            {
                var text = Console.ReadLine();
                string[] words = text.Split(',');

                if (words[0] == "")
                {
                    Console.WriteLine("No ha introducido datos");
                }
                else if (words[0] == "m")
                {
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
                else
                {
                    if (words[0] == "1")
                    {
                        //Cerquem per Asignatura
                        ValidationResult<string> vrName;
                        if (!(vrName = Subject.ValidateSub(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrName.AllErrors);
                        }
                        else
                        {
                            var min = 11.0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.NameSubject.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                if (result.Mark < min)
                                {
                                    min = result.Mark;
                                }
                            }

                            if (min == 11.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota mínima en la Asignatura {words[1]} es {min}");
                            }
                        }
                    }
                    else
                    {
                        //Cerquem per Alumne
                        ValidationResult<string> vrDni;
                        if (!(vrDni = Student.ValidateDni(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrDni.AllErrors);
                        }
                        else
                        {
                            var min = 11.0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.DniStudent.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                if (result.Mark < min)
                                {
                                    min = result.Mark;
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
            }

            Console.WriteLine();
            ShowStatisticsMenu();
        }

        static void ShowMaximum()
        {
            Console.WriteLine();
            Console.WriteLine("-- Nota más alta --");
            Console.WriteLine("Se puede buscar por Asignatura o por Alumno:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando 1 y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumno introduzca 2 campos separados por comas indicando 2 y el DNI del Alumno, y pulse enter");
            Console.WriteLine("- Para volver al menú anterior pulse m");
            Console.WriteLine();

            while (true)
            {
                var text = Console.ReadLine();
                string[] words = text.Split(',');

                if (words[0] == "")
                {
                    Console.WriteLine("No ha introducido datos");
                }
                else if (words[0] == "m")
                {
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
                else
                {
                    if (words[0] == "1")
                    {
                        //Cerquem per Asignatura
                        ValidationResult<string> vrName;
                        if (!(vrName = Subject.ValidateSub(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrName.AllErrors);
                        }
                        else
                        {
                            var max = 0.0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.NameSubject.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                if (result.Mark > max)
                                {
                                    max = result.Mark;
                                }
                            }

                            if (max == 0.0)
                            {
                                Console.WriteLine("No se han introducido notas en esta Asignatura");
                            }
                            else
                            {
                                Console.WriteLine($"La nota máxima en la Asignatura {words[1]} es {max}");
                            }
                        }
                    }
                    else
                    {
                        //Cerquem per Alumne
                        ValidationResult<string> vrDni;
                        if (!(vrDni = Student.ValidateDni(words[1])).IsSuccess)
                        {
                            Console.WriteLine(vrDni.AllErrors);
                        }
                        else
                        {
                            var max = 0.0;
                            var repo = new Repository<Exam>();
                            var cerca = repo.QueryAll().Where(s => s.DniStudent.Equals(words[1]));

                            foreach (var result in cerca)
                            {
                                if (result.Mark > max)
                                {
                                    max = result.Mark;
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
            }

            Console.WriteLine();
            ShowStatisticsMenu();
        }     

        public static void ClearCurrentConsoleLine()
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
    }
}
