﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab7
{
    public class Polygon
    {
        private List<Point> vertices = new List<Point>();

        public void AddPoint(Point p)
        {
            vertices.Add(p);
        }

        public bool HasSelfIntersections()
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Line firstEdge = new Line(vertices[i], vertices[(i + 1) % vertices.Count]);
                for (int j = i + 2; j < vertices.Count; j++)
                {
                    Line secondEdge = new Line(vertices[j], vertices[(j + 1) % vertices.Count]);
                    if (firstEdge.Intersects(secondEdge))
                        return true;
                }
            }

            return false;
        }

        public void LoadFromFile(string FileName)
        {
            string[] lines = File.ReadAllLines(FileName);
            foreach (var line in lines)
            {
                int[] coord = line.Split().Select(int.Parse).ToArray();
                vertices.Add(new Point(coord[0], coord[1]));
            }

        }
    }
}