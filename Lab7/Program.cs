using System;
using System.IO;
using System.Linq;

namespace Lab7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Polygon p = new Polygon();
            p.LoadFromFile("..\\..\\polygon.pg");
            Console.WriteLine(p.HasSelfIntersections() ? "Самопересекающийся" : "Не самопересекающийся");
        }
    }
}