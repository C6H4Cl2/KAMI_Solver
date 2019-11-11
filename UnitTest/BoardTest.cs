using System;
using KAMI_Solver.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int width = 10;
            int height = 3;
            Board b = new Board(width, height);

            Assert.AreEqual(width, b.width);
            Assert.AreEqual(height, b.height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Assert.AreEqual(Color.DefaultBlankColor, b.getTileAt(x, y).Color);
        }

        [TestMethod]
        public void Constructor2Test()
        {
            int[,] colors = new int[3, 3] { { 1, 1, 1 }, { 1, 2, 1 }, { 3, 2, 3 } };
            Board b = new Board(colors);

            Assert.AreEqual(colors.GetLength(0), b.width);
            Assert.AreEqual(colors.GetLength(1), b.height);

            for (int x = 0; x < b.width; x++)
                for (int y = 0; y < b.height; y++)
                    Assert.AreEqual((Color)colors[x, y], b.getTileAt(x, y).Color);
        }
    }
}
