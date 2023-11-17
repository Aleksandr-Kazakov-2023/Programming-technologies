using System.Collections.Generic;
using System.Globalization;

namespace Lab5;

public class Student
{
    public Student(Dictionary<string, int> points, string lastName, string firstName, string middleName, string gender, Calendar birthDate, string group, int course)
    {
        Points = points;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Gender = gender;
        BirthDate = birthDate;
        Group = group;
        Course = course;
    }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Gender { get; set; }
    public Calendar BirthDate { get; set; }
    public string Group { get; set; }
    public int Course { get; set; }
    public Dictionary<string, int> Points { get; set; }
}