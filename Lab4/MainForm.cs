using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class MainForm : Form
    {
        private class Point3D
        {
            private int x;
            private int y;
            private int z;

            public Point3D(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public int X => x;

            public int Y => y;

            public int Z => z;

            public double Distance(Point3D p)
            {
                return Math.Sqrt(Math.Pow(p.x - this.x, 2) + Math.Pow(p.y - this.y, 2) + Math.Pow(p.z - this.z, 2));
            }
        }

        bool isDefectors(Point3D[] group1, Point3D[] group2, int index)
        {
            double ownMin = 300;
            double foreignMin = 300;
            for (int i = 0; i < group1.Length; i++)
            {
                if (i != index)
                    ownMin = Math.Min(ownMin, group1[i].Distance(group1[index]));
                foreignMin = Math.Min(foreignMin, group2[i].Distance(group1[index]));
            }

            return ownMin > foreignMin;
        }

        int Defectors(int n)
        {
            int seaSize = 300;
            Point3D[] group1 = new Point3D[n];
            Point3D[] group2 = new Point3D[n];
            Random rnd = new Random();
            int defectorsCount = 0;
            for (int i = 0; i < n; i++)
            {
                group1[i] = new Point3D(rnd.Next(seaSize), rnd.Next(seaSize), rnd.Next(seaSize));
                group2[i] = new Point3D(rnd.Next(seaSize), rnd.Next(seaSize), rnd.Next(seaSize));
            }

            for (int i = 0; i < n; i++)
            {
                defectorsCount += isDefectors(group1, group2, i) ? 1 : 0;
                defectorsCount += isDefectors(group2, group1, i) ? 1 : 0;
            }

            return defectorsCount;
        }

        class Student
        {
            private int[] marks = new int[20];

            public Student()
            {
                Random rnd = new Random();
                for (int i = 0; i < marks.Length; i++)
                {
                    marks[i] = rnd.Next(5) + 1;
                }
            }

            public double Average => marks.Average();
        }

        class Group
        {
            private Student[] students = new Student[30];

            public Group()
            {
                for (int i = 0; i < students.Length; i++)
                {
                    students[i] = new Student();
                }
            }

            public void Sort()
            {
                students = students.OrderBy(o => o.Average).ToArray();
            }
        }

        class University
        {
            private Group[] groups = new Group[100];

            public Group[] Groups
            {
                get => groups;
                set => groups = value;
            }

            public University()
            {
                for (int i = 0; i < groups.Length; i++)
                {
                    groups[i] = new Group();
                }
            }
        }

        public void Rating(int n)
        {
            University[] universities = new University[n];
            for (int i = 0; i < n; i++)
            {
                universities[i] = new University();
                foreach (Group group in universities[i].Groups)
                {
                    group.Sort();
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            Rating(10);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 2; i < 50; i += 1)
            {
                long x;

                stopwatch.Restart();
                Defectors(i);
                stopwatch.Stop();
                x = stopwatch.ElapsedMilliseconds;
                chart.Series[0].Points.AddXY(i, x);

                stopwatch.Restart();
                Rating(i);
                stopwatch.Stop();
                x = stopwatch.ElapsedMilliseconds;
                chart.Series[1].Points.AddXY(i, x);

            }
        }
    }
}