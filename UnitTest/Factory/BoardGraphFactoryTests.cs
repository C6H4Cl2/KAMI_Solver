using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAMI_Solver.Model;
using KAMI_Solver.Factory;

namespace KAMI_Solver.Model.Tests
{
    [TestClass()]
    public class BoardGraphFactoryTests
    {
        private int[,] colors = { { 0, 0, 1, 0, 0 }, { 0, 1, 0, 1, 0 }, { 0, 0, 1, 0, 0 } };
        Board testBoard;

        [TestInitialize]
        public void TestInitialize()
        {
            testBoard = new Board(colors);
        }

        [TestMethod()]
        public void ConvertTest()
        {
            BoardGraph bg = BoardGraphFactory.createFromBoard(testBoard);
            HashSet<ColorBlock> ColorBlocks = bg.ColorBlocks;

            Assert.AreEqual(ColorBlocks.Count, 7);
        }
    }
}