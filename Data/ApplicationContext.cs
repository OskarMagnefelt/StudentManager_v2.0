using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace StudentManager.Data;

// DbContext = representerar en "session" mot databasen - det är via den
// vi kommunicerar med databasen

// Undertill är det som var innan Migrations

// public class ApplicationContext : DbContext
// {
//     private readonly string connectionString;

//     public ApplicationContext(string connectionString)
//     {
//         this.connectionString = connectionString;
//     }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(connectionString);
//     }

//     // DbSet = representerar i grund och botten en tabell

//     public DbSet<Student> Student { get; set; }
// }

// Undertill är det som skulle läggas till vid användning av Migrations

public class ApplicationContext : DbContext
{
    private string connectionString = "Server=.;Database=StudentManager;Integrated Security=true;Encrypt=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Student> Student { get; set; }
}
