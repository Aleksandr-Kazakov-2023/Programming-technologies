using Lab6;

namespace Lab6Test
{
    [TestClass]
    public class Lab6Test
    {
        [TestMethod]
        public void CheckForHasNotSelfIntersections()
        {
            Polygon polygon = new Polygon();
            polygon.AddPoint(new Point(0, 0));
            polygon.AddPoint(new Point(2, 0));
            polygon.AddPoint(new Point(3, 1));
            polygon.AddPoint(new Point(2, 2));
            polygon.AddPoint(new Point(0, 2));

            Assert.IsFalse(polygon.HasSelfIntersections());
        }

        [TestMethod]
        public void CheckForHasSelfIntersections()
        {
            Polygon polygon = new Polygon();
            polygon.AddPoint(new Point(3, 6));
            polygon.AddPoint(new Point(8, 10));
            polygon.AddPoint(new Point(13, 7));
            polygon.AddPoint(new Point(8, 5));
            polygon.AddPoint(new Point(4, 10));
            polygon.AddPoint(new Point(3, 6));

            Assert.IsTrue(polygon.HasSelfIntersections());
        }
    }
}