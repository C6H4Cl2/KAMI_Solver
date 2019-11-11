using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.Model;

namespace UnitTest
{
    /// <summary>
    /// StateTest 的摘要说明
    /// </summary>
    [TestClass]
    public class StateTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int[,] colors = new int[3, 3] { { 1, 1, 1 }, { 1, 2, 1 }, { 3, 2, 3 } };
            Board b = new Board(colors);

            State s = new State(b);
            Assert.AreEqual(4, s.TileGroups.Count);
            Assert.AreEqual(3, s.ColorsInUsed.Count);
        }
    }
}
