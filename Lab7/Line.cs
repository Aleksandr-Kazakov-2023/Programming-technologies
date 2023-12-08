namespace Lab7
{
    public class Line
    {
        private Point Start { get; }
        private Point End { get; }
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public bool Intersects(Line line)
        {
            double CCW(Point p0, Point p1, Point p2) =>
                (p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y);

            return CCW(Start, line.Start, line.End) * CCW(End, line.Start, line.End) <= 0 &&
                   CCW(line.Start, Start, End) * CCW(line.Start, Start, End) <= 0;
        }
    }
}