using System;
using Lab7;
using NUnit.Framework;

namespace Lab7Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestTrue()
        {
            Polygon p = new Polygon();
            p.LoadFromFile("..\\..\\polygon1.pg");
            Assert.False(p.HasSelfIntersections());
        }
        [Test]
        public void TestFalse()
        {
            Polygon p = new Polygon();
            p.LoadFromFile("..\\..\\polygon2.pg");
            Assert.True(p.HasSelfIntersections());
        }
    }
}