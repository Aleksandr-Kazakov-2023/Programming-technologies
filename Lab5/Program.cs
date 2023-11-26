using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lab5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Simulation(20);
        }

        public static void Simulation(int numberOfStudents)
        {
            List<Student> students = GenerateStudent(numberOfStudents);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < students.Count; j++)
                {
                    if (students[j].Course < 5)
                    {
                        students[j].Course++;
                    }
                    else
                    {
                        students.Remove(students[j]);
                        j--;
                    }
                }
                students.AddRange(GenerateStudent(numberOfStudents));
                Console.WriteLine("Студенты:");
                students.ForEach(Console.WriteLine);
                List<Student> namesakes = GetNamesakes(students);
                Console.WriteLine("Однофамильцы:");
                namesakes.ForEach(Console.WriteLine);
                Console.WriteLine();
            }
        }

        public class StudentEqualityComparer : IEqualityComparer<Student>
        {
            public bool Equals(Student x, Student y) => x.LastName == y.LastName;
            public int GetHashCode(Student obj) => (obj as Student).LastName.GetHashCode();
        }

        public static List<Student> GetNamesakes(List<Student> students)
        {
            List<Student> namesakes;
            List<Student> bpiStudents = students.Where(s => s.Group.StartsWith("БПИ")).ToList();
            List<Student> otherStudents = students.Except(bpiStudents).ToList();
            namesakes = bpiStudents.Intersect(otherStudents, new StudentEqualityComparer()).ToList();
            return namesakes;
        }

        public static List<Student> GenerateStudent(int count)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Student> students = new List<Student>();
            string[] lastNames =
            {
                "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов", "Новиков",
                "Федоров", "Морозов", "Волков", "Алексеев", "Лебедев", "Семенов", "Егоров", "Павлов", "Козлов",
                "Степанов", "Николаев"
            };
            string[] firstNamesMale =
            {
                "Андрей", "Давид", "Константин", "Глеб", "Максим", "Роман", "Платон", "Даниил", "Дмитрий", "Лев",
                "Павел", "Михаил"
            };
            string[] firstNamesFemale =
            {
                "Виктория", "Вероника", "София", "Ксения", "Милана", "Виктория", "Полина", "Арина"
            };
            string[] middleNamesMale =
            {
                "Егорович", "Романович", "Тимофеевич", "Максимович", "Дмитриевич", "Максимович", "Дмитриевич",
                "Данилович", "Максимович", "Филиппович", "Саввич"
            };
            string[] middleNamesFemale =
            {
                "Алексеевна", "Вячеславовна", "Александровна", "Ивановна", "Макаровна", "Михайловна",
                "Александровна", "Максимовна", "Кирилловна", "Платоновна"
            };
            string[] groups = { "БИВТ", "БИСТ", "БПИ" };
            int year, month, day;

            for (int i = 0; i < count; i++)
            {
                year = rnd.Next(1970, DateTime.Now.Year - 17);
                month = rnd.Next(12) + 1;
                day = rnd.Next(DateTime.DaysInMonth(year, month)) + 1;
                int genderNum = rnd.Next(2);
                string gender = genderNum == 0 ? "Мужской" : "Женский";
                string firstName = genderNum == 0
                    ? firstNamesMale[rnd.Next(firstNamesMale.Length)]
                    : firstNamesFemale[rnd.Next(firstNamesFemale.Length)];
                string lastName = lastNames[rnd.Next(lastNames.Length)] + (genderNum == 0 ? "" : "а");
                string middleName = genderNum == 0
                    ? middleNamesMale[rnd.Next(middleNamesMale.Length)]
                    : middleNamesFemale[rnd.Next(middleNamesFemale.Length)];
                students.Add(new Student(lastName, firstName, middleName, gender,
                    new DateTime(year, month, day),
                    $"{groups[rnd.Next(groups.Length)]}-{DateTime.Now.Year % 100}", 1,
                    new Dictionary<string, int>()
                        {{"math", rnd.Next(20, 101)}, {"russ", rnd.Next(20, 101)}, {"info", rnd.Next(20, 101)}}));
            }

            return students;
        }
    }
}