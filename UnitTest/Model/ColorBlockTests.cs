using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model.Tests
{
    [TestClass()]
    public class ColorBlockTests
    {
        [TestMethod()]
        public void EqualsTest()
        {

            ColorBlock cb1 = new ColorBlock(100, 1, 2);
            ColorBlock cb2 = new ColorBlock(200, 1, 2);
            Assert.AreEqual(cb1, cb2);

            ColorBlock cb3 = new ColorBlock(100, 1, 1);
            Assert.AreNotEqual(cb1, cb3);
        }

        [TestMethod()]
        public void HashtableTest()
        {
            HashSet<ColorBlock> testHashSet = new HashSet<ColorBlock>();
            ColorBlock cb1 = new ColorBlock(100, 1, 2);
            testHashSet.Add(cb1);

            ColorBlock cb2 = new ColorBlock(200, 1, 2);
            Assert.IsTrue(testHashSet.Contains(cb2));
        }

        [TestMethod()]
        public void GetDistanceToTheFarthestTest()
        {
            ColorBlock cb1 = new ColorBlock(100, 1, 0);
            ColorBlock cb2 = new ColorBlock(200, 1, 1);
            ColorBlock cb3 = new ColorBlock(300, 2, 2);

            cb1.AddNeighbour(cb2);
            cb2.AddNeighbour(cb1);

            cb2.AddNeighbour(cb3);
            cb3.AddNeighbour(cb2);

            int farthestDistance = cb1.GetDistanceToTheFarthest(out ColorBlock cb);

            Assert.AreEqual(farthestDistance, 2);
            Assert.AreEqual(cb3, cb);
        }

        [TestMethod()]
        public void GetDistanceToTheFarthestTest2()
        {
            ColorBlock cb1 = new ColorBlock(100, 1, 0);

            int farthestDistance = cb1.GetDistanceToTheFarthest(out ColorBlock cb);

            Assert.AreEqual(farthestDistance, 0);
            Assert.AreEqual(cb1, cb);
        }
    }
}