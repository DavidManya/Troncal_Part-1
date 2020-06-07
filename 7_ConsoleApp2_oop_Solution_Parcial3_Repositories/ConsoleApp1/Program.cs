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
            InitialLoad();

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
            Console.WriteLine("Opción: [a] / Gestión de Alumn@s");
            Console.WriteLine("Opción: [s] / Gestión de Asignaturas");
            Console.WriteLine("Opción: [n] / Añadir notas de Alumn@s");
            Console.WriteLine("Opción: [c] / Obtener Estadísticas");
            Console.WriteLine("Opción: [m] / Para volver a este menú");
            Console.WriteLine("Opción: [e] / Para salir de la aplicación");
        }

        static void ShowHandleStudentsMenu()
        {
            CurrentOption = "a";
            Console.WriteLine("- Menú para la gestión de Alumn@s -");
            Console.WriteLine("Opción: [a] / Añadir nuev@ Alumn@");
            Console.WriteLine("Opción: [n] / Información Alumn@");
            Console.WriteLine("Opción: [e] / Editar Alumn@ existente");
            Console.WriteLine("Opción: [b] / Eliminar Alumn@ existente");
            Console.WriteLine("Opción: [l] / Listado de Alumn@s");
            Console.WriteLine("Opción: [c] / Añadir cursos a Alumn@");
            Console.WriteLine("Opción: [x] / Exámenes Alumn@");
            Console.WriteLine("Opción: [s] / Asignaturas Alumn@");
            Console.WriteLine("Opción: [m] / Para acabar y volver al menú principal");
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
                    Console.WriteLine("Deberá introducir el DNI, el nombre, los apellidos y un correo electrónico");
                    Console.WriteLine("Para volver sin guardar Alumn@ escriba [*] en cualquier momento");
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

                    if (dni == "*")
                        break;
                    #endregion

                    #region read fname
                    Console.WriteLine("Escriba el nombre para Alumn@:");
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

                    if (fname == "*")
                        break;
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

                    if (lname == "*")
                        break;
                    #endregion

                    #region read email
                    Console.WriteLine("Ahora escriba el correo electrónico:");
                    var email = Console.ReadLine();

                    if (email == "*")
                        break;

                    ValidationResult<string> vrEmail;
                    while (!(vrEmail = Student.ValidateEmail(email)).IsSuccess)
                    {
                        Console.WriteLine(vrEmail.AllErrors);
                        email = Console.ReadLine();
                        if (email == "*")
                            break;
                    }

                    if (email == "*")
                        break;
                    #endregion

                    if (vrDni.IsSuccess && vrFName.IsSuccess && vrLName.IsSuccess && vrEmail.IsSuccess)
                    {
                        var student = new Student
                        {
                            Dni = vrDni.ValidatedResult,
                            FirstName = vrFName.ValidatedResult,
                            LastName = vrLName.ValidatedResult,
                            Email = vrEmail.ValidatedResult
                        };

                        var sr = student.Save();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Alumn@ guardad@ correctamente");
                            Console.WriteLine("Para añadir otr@ Alumn@ pulse [a] ó [m] para terminar");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Uno o más errores han ocurrido y Alumn@ no se ha guardado correctamente:");
                            Console.WriteLine(sr.AllErrors);
                        }
                    }
                }
                else if (option == "n" || option == "N") //Informació Alumne
                {
                    //llegim dni, validem format i existència, si NO està error i si està mostrar dades
                    Console.WriteLine("Para volver sin consultar escriba [*] en cualquier momento");
                    Console.WriteLine("Escriba el DNI para Alumn@ a consultar:");

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

                    if (dni == "*")
                        break;
                    #endregion

                    if (vrDni.IsSuccess)
                    {
                        //var repo = new Repository<Student>();
                        //var data = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);
                        //var data = repo.QueryAll().Where( s => s.Dni == dni);
                        var repo = new StudentRepository();
                        var entityWithDni = repo.GetStudentByDni(dni);

                        Console.WriteLine("- Datos Alumn@ -");
                        Console.WriteLine("{0}: {1}, {2} / {3}", entityWithDni.Dni, entityWithDni.LastName, entityWithDni.FirstName,entityWithDni.Email);
                    }

                }
                else if (option == "e" || option == "E") //Editar Alumne
                {
                    //llegim dni, validem format i existència, si NO està error i si està mostrar dades actuals i demanar noves
                    Console.WriteLine("Para volver sin modificar escriba [*] en cualquier momento");
                    Console.WriteLine("Escriba el DNI para Alumn@ a modificar:");

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

                    if (dni == "*")
                        break;
                    #endregion

                    if (vrDni.IsSuccess)
                    {
                        //var repo = new Repository<Student>();
                        //var data = repo.QueryAll().FirstOrDefault(s => s.Dni == dni);
                        //var data = repo.QueryAll().Where( s => s.Dni == dni);
                        var repo = new StudentRepository();
                        var entityWithDni = repo.GetStudentByDni(dni);
                        idmod = entityWithDni.Id;

                        Console.WriteLine();
                        Console.WriteLine("- Datos Alumn@ -");
                        Console.WriteLine("{0}: {1}, {2} / {3}", entityWithDni.Dni, entityWithDni.LastName, entityWithDni.FirstName, entityWithDni.Email);
                        Console.WriteLine();
                    }

                    #region read fname
                    Console.WriteLine("Escriba de nuevo el nombre para Alumn@:");
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

                    if (fname == "*")
                        break;
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

                    if (lname == "*")
                        break;
                    #endregion

                    #region read email
                    Console.WriteLine("Ahora escriba de nuevo el correo electrónico:");
                    var email = Console.ReadLine();

                    if (email == "*")
                        break;

                    ValidationResult<string> vrEmail;
                    while (!(vrEmail = Student.ValidateEmail(email)).IsSuccess)
                    {
                        Console.WriteLine(vrEmail.AllErrors);
                        email = Console.ReadLine();
                        if (email == "*")
                            break;
                    }

                    if (email == "*")
                        break;
                    #endregion

                    if (vrFName.IsSuccess && vrLName.IsSuccess && vrEmail.IsSuccess)
                    {
                        var student = new Student
                        {
                            Id = idmod,
                            Dni = vrDni.ValidatedResult,
                            FirstName = vrFName.ValidatedResult,
                            LastName = vrLName.ValidatedResult,
                            Email = vrEmail.ValidatedResult
                        };

                        var sr = student.Save();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine("");
                            Console.WriteLine($"Alumn@ modificad@ correctamente");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine($"Uno o más errores han ocurrido y Alumn@ no se ha modificado correctamente: {sr.AllErrors}");
                        }

                    }
                }
                else if (option == "b" || option == "B") //Eliminar Alumne
                {
                    Console.WriteLine("Escriba el DNI para Alumn@ a eliminar:");
                    #region read dni
                    var dni = Console.ReadLine();

                    ValidationResult<string> vrDni;
                    while (!(vrDni = Student.ValidateExist(dni)).IsSuccess)
                    {
                        Console.WriteLine(vrDni.AllErrors);
                        dni = Console.ReadLine();
                    }
                    #endregion

                    if (vrDni.IsSuccess)
                    {
                        var student = new Student
                        {
                            Dni = vrDni.ValidatedResult
                        };

                        var sr = student.Delete();

                        if (sr.IsSuccess)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Alumn@ eliminado correctamente");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Uno o más errores han ocurrido y Alumn@ no se ha podido eliminar:");
                            Console.WriteLine(sr.AllErrors);
                        }
                    }
                }
                else if (option == "l" || option == "L") //Llistat d'Alumnes
                {
                    var repo = new Repository<Student>();
                    var data = from result in repo.QueryAll() orderby result.LastName, result.FirstName select result;

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Alumn@s --");
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
                    Console.WriteLine("Pulse una opción del Menú para continuar");
                    Console.WriteLine();
                }
                else if (option == "c" || option == "C") //Afegir Curs
                {
                    Console.WriteLine();
                    Console.WriteLine("-- Añadir Alumn@ a Cursos --");
                    Console.WriteLine("Deberá introucir 3 campos con el Nombre de la Asignatura, DNI Alumn@ y Asiento");
                    Console.WriteLine("- Para volver al menú anterior pulse [m]");
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
                            while (!(vrName = Subject.ValidateExistSub(name)).IsSuccess)
                            {
                                Console.WriteLine(vrName.AllErrors);
                                name = Console.ReadLine();
                            }

                            Console.WriteLine("Ahora introduzca el DNI para Alumn@:");
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
                            while (!(vrChNum = Course.ValidateChairNumber(chair, name)).IsSuccess)
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
                                    Console.WriteLine("");
                                    Console.WriteLine("Asignatura añadida correctamente");
                                }
                                else
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("Uno o más errores han ocurrido y la Asignatura no se ha añadido correctamente:");
                                    Console.WriteLine(sr.AllErrors);
                                }
                            }
                        }
                    }
                }
                else if (option == "x" || option == "X") //Exàmens Alumne
                {
                    //llegir dni, validar format i existència, si NO està error i si està recuperar dades d'Exam
                    Console.WriteLine("Escriba el DNI para Alumn@ a consultar:");
                    #region read dni
                    var dni = Console.ReadLine();

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
                        Console.WriteLine("- Exámenes Alumn@ -");
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
                    Console.WriteLine("Escriba el DNI para Alumn@ a consultar:");
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

                    Console.WriteLine("- Asignaturas Alumn@ -");
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
            Console.WriteLine("Opción: [a] - Añadir una nueva Asignatura");
            Console.WriteLine("Opción: [l] - Listado de Asignaturas");
            Console.WriteLine("Opción: [s] - Ver Alumn@s por Asignatura");
            Console.WriteLine("Opción: [e] - Ver exámenes de Asignatura");
            Console.WriteLine("Opción: [m] - Para acabar y volver al menú principal");
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
                    Console.WriteLine("Deberá introducir los datos del Nombre de la Asigntura y el nombre del Profesor");
                    Console.WriteLine("Para volver sin guardar Asignatura escriba [*]");
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

                    if (name == "*")
                        break;
                    #endregion

                    #region read teacher
                    Console.WriteLine("Ahora escriba el nombre del profesor ó [*] para salir:");
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

                    if (teacher == "*")
                        break;

                    #endregion

                    if (vrName.IsSuccess && vrTeacher.IsSuccess)
                    {
                        var subject = new Subject
                        {
                            Name = name,
                            Teacher = teacher
                        };

                        var sr = subject.Save();
                        if (sr.IsSuccess)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Asignatura guardada correctamente");
                            Console.WriteLine("Pulse [a] para seguir añadiendo ó [m] para salir");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Uno o más errores han ocurrido y la Asignatura no se ha guardado correctamente");
                            Console.WriteLine(sr.AllErrors);
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
                    Console.WriteLine("Pulse una opción del Menú para continuar");
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
                    while (!(vrName = Subject.ValidateExistSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                        if (name == "*")
                            break;
                    }

                    if (name == "*")
                        break;
                    #endregion

                    var repo = new Repository<Course>();
                    var data = repo.QueryAll().Where(s => s.NameSubject.Contains(name));

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Alumn@s para la Asignatura --");
                        Console.WriteLine("DNI: Apellidos, Nombre");

                        foreach (var result in data)
                        {
                            var repo2 = new Repository<Student>();
                            var cerca = from result2 in repo2.QueryAll().Where(s => s.Dni.Contains(result.DniStudent)) orderby result2.LastName, result2.FirstName select result2;

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
                    Console.WriteLine("Pulse una opción del Menú para continuar");
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
                    while (!(vrName = Subject.ValidateExistSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                        if (name == "*")
                            break;
                    }

                    if (name == "*")
                        break;
                    #endregion

                    var repo = new Repository<Exam>();
                    var data = from result in repo.QueryAll().Where(s => s.NameSubject.Contains(name)) orderby result.DniStudent select result;

                    if (data.Count() > 0)
                    {
                        Console.WriteLine("-- Listado de Resultados para la Asignatura --");
                        Console.WriteLine("DNI: Apellidos, Nombre - Fecha Exámen - Nota");

                        foreach (var result in data)
                        {
                            var repo2 = new Repository<Student>();
                            var cerca = from result2 in repo2.QueryAll().Where(s => s.Dni.Contains(result.DniStudent)) orderby result2.LastName, result2.FirstName select result2;

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

            while (true)
            {
                var notaText = Console.ReadLine();

                if (notaText == "m" || notaText == "M" )
                {
                    break;
                }
                else if (notaText == "a" || notaText == "A" )               
                {
                    Console.WriteLine();
                    Console.WriteLine("Para añadir notas debe informar: DNI Alumn@, Nombre Asignatura, Fecha de Exámen y la Nota");
                    Console.WriteLine("Para volver sin guardar Nota escriba [*] en cualquier momento");
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

                    if (dni == "*")
                        break;
                    #endregion

                    #region read name
                    Console.WriteLine("Ahora escriba el nombre de la Asignatura:");
                    var name = Console.ReadLine();

                    if (name == "*")
                        break;

                    ValidationResult<string> vrName;
                    while (!(vrName = Subject.ValidateExistSub(name)).IsSuccess)
                    {
                        Console.WriteLine(vrName.AllErrors);
                        name = Console.ReadLine();
                        if (name == "*")
                            break;
                    }

                    if (name == "*")
                        break;
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

                    if (date == "*")
                        break;
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

                    if (mark == "*")
                        break;
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
                            Console.WriteLine("");
                            Console.WriteLine("Nota guardada correctamente");
                            Console.WriteLine("Para seguir añadiendo notas pulse [a] ó [m] para salir");
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Uno o más errores han ocurrido y la Nota no se ha guardado correctamente:");
                            Console.WriteLine(sr.AllErrors);
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
            Console.WriteLine("Opción: avg - Media de las notas de l@s Alumn@s");
            Console.WriteLine("Opción: max - Máxima nota de l@s Alumn@s");
            Console.WriteLine("Opción: min - Mínima nota de l@s Alumn@s");
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
            Console.WriteLine("Se puede buscar por Asignatura o por Alumn@:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando [1] y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumn@ introduzca 2 campos separados por comas indicando [2] y el DNI de Alumn@, y pulse enter");
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
                        if (!(vrName = Subject.ValidateExistSub(words[1])).IsSuccess)
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
                        if (!(vrDni = Student.ValidateExist(words[1])).IsSuccess)
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
                                Console.WriteLine("No hay notas para Alumn@");
                            }
                            else
                            {
                                Console.WriteLine($"La nota media para Alumn@ {words[1]} es {sum / quant}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            //ShowStatisticsMenu();
        }

        static void ShowMinimum()
        {
            Console.WriteLine();
            Console.WriteLine("-- Nota más baja --");
            Console.WriteLine("Se puede buscar por Asignatura o por Alumn@:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando [1] y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumn@ introduzca 2 campos separados por comas indicando [2] y el DNI de Alumn@, y pulse enter");
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
                        if (!(vrName = Subject.ValidateExistSub(words[1])).IsSuccess)
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
                                Console.WriteLine($"La nota más baja en la Asignatura {words[1]} es {min}");
                            }
                        }
                    }
                    else
                    {
                        //Cerquem per Alumne
                        ValidationResult<string> vrDni;
                        if (!(vrDni = Student.ValidateExist(words[1])).IsSuccess)
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
                                Console.WriteLine("No hay notas para Alumn@");
                            }
                            else
                            {
                                Console.WriteLine($"La nota más baja de Alumn@ {words[1]} es {min}");
                            }
                        }

                    }
                }
            }

            Console.WriteLine();
            //ShowStatisticsMenu();
        }

        static void ShowMaximum()
        {
            Console.WriteLine();
            Console.WriteLine("-- Nota más alta --");
            Console.WriteLine("Se puede buscar por Asignatura o por Alumn@:");
            Console.WriteLine("Si es por Asignatura introduzca 2 campos separados por comas indicando [1] y el nombre de la Asignatura, y pulse enter");
            Console.WriteLine("Si es por Alumn@ introduzca 2 campos separados por comas indicando [2] y el DNI de Alumn@, y pulse enter");
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
                        if (!(vrName = Subject.ValidateExistSub(words[1])).IsSuccess)
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
                                Console.WriteLine($"La nota más alta en la Asignatura {words[1]} es {max}");
                            }
                        }
                    }
                    else
                    {
                        //Cerquem per Alumne
                        ValidationResult<string> vrDni;
                        if (!(vrDni = Student.ValidateExist(words[1])).IsSuccess)
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
                                Console.WriteLine("No hay notas para Alumn@");
                            }
                            else
                            {
                                Console.WriteLine($"La nota más alta de Alumn@ {words[1]} es {max}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            //ShowStatisticsMenu();
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

        static void InitialLoad()
        {
            var student = new Student
            {
                Dni = "12345678A",
                FirstName = "Elena",
                LastName = "Nito del Bosque",
                Email = "elenita@gmail.com"
            };
            student.Save();

            student = new Student
            {
                Dni = "23456789B",
                FirstName = "Paqui",
                LastName = "Dermo Gris",
                Email = "lapaqui@gmail.com"
            };
            student.Save();

            student = new Student
            {
                Dni = "34567890C",
                FirstName = "Luís",
                LastName = "Térico Perdido",
                Email = "luilui@gmail.com"
            };
            student.Save();

            var subject = new Subject
            {
                Name = "Algebra",
                Teacher = "Joe Pitágoras"
            };
            subject.Save();

            subject = new Subject
            {
                Name = "Itinerari.NET",
                Teacher = "Jose Freire"
            };
            subject.Save();

            subject = new Subject
            {
                Name = "JavaScript para Dummies",
                Teacher = "Jake Petrulla"
            };
            subject.Save();

            var course = new Course
            {
                NameSubject = "Algebra",
                DniStudent = "12345678A",
                DateEnrolment = DateTime.Today,
                ChairNumber = 1
            };
            course.Save();

            course = new Course
            {
                NameSubject = "Itinerari.NET",
                DniStudent = "12345678A",
                DateEnrolment = DateTime.Today,
                ChairNumber = 2
            };
            course.Save();

            course = new Course
            {
                NameSubject = "Algebra",
                DniStudent = "23456789B",
                DateEnrolment = DateTime.Today,
                ChairNumber = 3
            };
            course.Save();

            course = new Course
            {
                NameSubject = "Itinerari.NET",
                DniStudent = "23456789B",
                DateEnrolment = DateTime.Today,
                ChairNumber = 4
            };
            course.Save();

            course = new Course
            {
                NameSubject = "JavaScript para Dummies",
                DniStudent = "34567890C",
                DateEnrolment = DateTime.Today,
                ChairNumber = 5
            };
            course.Save();

            var exam = new Exam
            {
                DniStudent = "12345678A",
                NameSubject = "Algebra",
                DateExam = DateTime.Today,
                Mark = 8.5
            };
            exam.Save();

            exam = new Exam
            {
                DniStudent = "23456789B",
                NameSubject = "Algebra",
                DateExam = DateTime.Today,
                Mark = 5.5
            };
            exam.Save();

            exam = new Exam
            {
                DniStudent = "12345678A",
                NameSubject = "Itinerari.NET",
                DateExam = DateTime.Today,
                Mark = 9.5
            };
            exam.Save();

            exam = new Exam
            {
                DniStudent = "23456789B",
                NameSubject = "Itinerari.NET",
                DateExam = DateTime.Today,
                Mark = 6.0
            };
            exam.Save();

            exam = new Exam
            {
                DniStudent = "34567890C",
                NameSubject = "JavaScript para Dummies",
                DateExam = DateTime.Today,
                Mark = 4.0
            };
            exam.Save();
        }
    }
}
