using KAMI_Solver.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class TileTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Color colorValue = Color.Color1;
            Tile t = new Tile(colorValue);

            Assert.AreEqual(colorValue, t.Color);
        }
    }
}
