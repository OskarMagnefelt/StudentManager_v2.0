using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StudentManager;

public class Student
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(20)]
    public string SocialSecurityNumber
    {
        get => socialSecurityNumber;
        set
        {
            var regEx = new Regex("^\\d{8}-\\d{4}$");

            if (!regEx.IsMatch(value))
            {
                throw new ArgumentException("Invalid social security number");
            }

            var birthDatePart = value.Substring(0, 8);

            if (!DateTime.TryParseExact(birthDatePart, "yyyyMMdd", CultureInfo.InvariantCulture,
                                     DateTimeStyles.None, out DateTime result))
            {
                throw new ArgumentException("Invalid social security number");
            }

            socialSecurityNumber = value;
        }
    }

    [MaxLength(50)]
    public string PhoneNumber { get; set; }

    [MaxLength(50)]
    public string Email { get; set; }

    [MaxLength(50)]
    public string Program { get; set; }

    public Student(string firstName, string lastName, string socialSecurityNumber, string phoneNumber, string email, string program)
    {
        FirstName = firstName;
        LastName = lastName;
        SocialSecurityNumber = socialSecurityNumber;
        PhoneNumber = phoneNumber;
        Email = email;
        Program = program;
    }

    private string socialSecurityNumber;
}