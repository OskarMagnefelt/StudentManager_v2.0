using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using static System.Console;

namespace StudentManager;

class Program
{
    // static List<Student> students = new List<Student>();

    static string connectionString = "Server=.;Database=StudentManager_v2;Integrated Security=true;Encrypt=False";
    // static string connectionString = "localhost";
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

                    // SearchStudent();

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

        // YYYYMMDD-XXXX


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
        // if (students.Exists(x => x.SocialSecurityNumber == student.SocialSecurityNumber))
        //     throw new Exception("Student already registered");


        string sql = @"
        INSERT INTO Student (
            FirstName,
            LastName,
            SocialSecurityNumber,
            PhoneNumber,
            Email,
            Program
            ) VALUES (
                @FirstName,
                @LastName,
                @SocialSecurityNumber,
                @PhoneNumber,
                @Email,
                @Program)
        ";

        // SqlConnectionn används för att upprätthålla kommmunikationen med databasen.
        using var connection = new SqlConnection(connectionString);

        using var command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@FirstName", student.FirstName);
        command.Parameters.AddWithValue("@LastName", student.LastName);
        command.Parameters.AddWithValue("@SocialSecurityNumber", student.SocialSecurityNumber);
        command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
        command.Parameters.AddWithValue("@Email", student.Email);
        command.Parameters.AddWithValue("@Program", student.Program);


        connection.Open();

        // command.ExecuteNonQuery();
        int rowsAffected = command.ExecuteNonQuery();

        connection.Close();

        if (rowsAffected == 1)
        {
            Console.WriteLine("Student inserted successfully.");
        }
        else
        {
            Console.WriteLine("Failed to insert student.");
        }
    }

    // private static void SearchStudent()
    // {

    //     Write("Personnummer: ");

    //     string socialSecurityNumber = ReadLine();

    //     Clear();

    //     var students = FetchStudents();

    //     var student = students.Find(student => student.SocialSecurityNumber == socialSecurityNumber);

    //     if (student != null)
    //     {
    //         WriteLine($"Förnamn: {student.FirstName}");
    //         WriteLine($"Efternamn: {student.LastName}");
    //         WriteLine($"Personnummer: {student.SocialSecurityNumber}");
    //         WriteLine($"E-post: {student.Email}");
    //         WriteLine($"Klass: {student.Program}");

    //         while (ReadKey(true).Key != ConsoleKey.Escape) ;
    //     }
    //     else
    //     {
    //         WriteLine("Studerande saknas");

    //         Thread.Sleep(2000);
    //     }
    // }

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

    private static IEnumerable<Student> FetchStudents()
    {
        var sql = @"
            SELECT FirstName, 
                   LastName, 
                   SocialSecurityNumber, 
                   PhoneNumber,
                   Email,
                   Program   
            FROM Student
        ";

        using var connection = new SqlConnection(connectionString);
        using var commmand = new SqlCommand(sql, connection);

        connection.Open();
        var reader = commmand.ExecuteReader();

        var students = new List<Student>();

        // while (reader.Read())
        // {
        //     var student = new Student()
        //     {
        //         FirstName = reader["FirstName"].ToString(),
        //         LastName = reader["LastName"].ToString(),
        //         SocialSecurityNumber = reader["SocialSecurityNumber"].ToString(),
        //         PhoneNumber = reader["PhoneNumber"].ToString(),
        //         Email = reader["Email"].ToString(),
        //         Program = reader["Program"].ToString(),
        //     };
        //     students.Add(student);
        // }

        while (reader.Read())
        {
            var student = new Student(
                reader["FirstName"].ToString(),
                reader["LastName"].ToString(),
                reader["SocialSecurityNumber"].ToString(),
                reader["PhoneNumber"].ToString(),
                reader["Email"].ToString(),
                reader["Program"].ToString()
            );
            students.Add(student);
        }

        connection.Close();

        return students;
    }


}