using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using StudentManager.Data;
using static System.Console;

namespace StudentManager;

class Program
{
    // Undertill är det som var innan Migrations
    // static string connectionString = "Server=.;Database=StudentManager_v2;Integrated Security=true;Encrypt=False";
    // static ApplicationContext context = new ApplicationContext(connectionString);

    // Undertill är det som är nu med Migrations
    static ApplicationContext context = new ApplicationContext();
    static void Main()
    {
        CursorVisible = false;
        Title = "Student Manager";

        while (true)
        {
            WriteLine("1. Registrera studerande");
            WriteLine("2. Sök studerande");
            WriteLine("3. Lista studerande");
            WriteLine("4. Avsluta");

            var keyPressed = ReadKey(intercept: true);

            Clear();

            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    RegisterStudent();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    SearchStudent();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    ListStudents();

                    break;

                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    private static void RegisterStudent()
    {
        Write("Förnamn: ");

        string firstName = ReadLine();

        Write("Efternamn: ");

        string lastName = ReadLine();

        Write("Personnummer: ");

        string socialSecurityNumber = ReadLine();

        Write("Telefonnummer: ");

        string phoneNumber = ReadLine();

        Write("E-post: ");

        string email = ReadLine();

        Write("Klass: ");

        string program = ReadLine();

        var student = new Student
        (
        firstName,
        lastName,
        socialSecurityNumber,
        phoneNumber,
        email,
        program
        );

        Clear();

        try
        {
            SaveStudent(student);

            WriteLine("Studerande sparad");
        }
        catch
        {
            WriteLine("Studerande redan registrerad");
        }

        Thread.Sleep(2000);
    }

    private static void SaveStudent(Student student)
    {
        context.Student.Add(student);

        context.SaveChanges();
    }

    private static void SearchStudent()
    {
        Write("Personnummer: ");

        string socialSecurityNumber = ReadLine();
        var student = context.Student.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);

        if (student != null)
        {
            WriteLine($"Förnamn: {student.FirstName}");
            WriteLine($"Efternamn: {student.LastName}");
            WriteLine($"Personnummer: {student.SocialSecurityNumber}");
            WriteLine($"E-post: {student.Email}");
            WriteLine($"Klass: {student.Program}");

            while (ReadKey(true).Key != ConsoleKey.Escape) ;
        }
        else
        {
            WriteLine("Studerande saknas");

            Thread.Sleep(2000);
        }
    }

    private static void ListStudents()
    {
        Write("Klass: ");

        string program = ReadLine();

        var students = FetchStudents();

        var programStudents = students.Where(student => student.Program == program);

        foreach (var student in programStudents)
        {
            WriteLine($"{student.FirstName} {student.LastName}");
        }

        while (ReadKey(true).Key != ConsoleKey.Escape) ;
    }

    private static List<Student> FetchStudents() => context.Student.ToList();
}