using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lab5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Student> students = GenerateStudent(20);
            Console.WriteLine();
        }

        void Simulation(int numberOfYears)
        {
            List<Student> students = GenerateStudent(100);
            for (int i = 0; i < numberOfYears; i++)
            {

            }
        }

        public static List<Student> GenerateStudent(int count)
        {
            Random rnd = new Random();
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
            year = rnd.Next(1970, DateTime.Now.Year - 17);
            month = rnd.Next(12) + 1;
            day = rnd.Next(DateTime.DaysInMonth(year, month) + 1);

            for (int i = 0; i < count; i++)
            {
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