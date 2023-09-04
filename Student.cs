using System.Globalization;
using System.Text.RegularExpressions;

namespace StudentManager;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
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

    public string PhoneNumber { get; set; }
    public string Email { get; set; }
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