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
    }
}