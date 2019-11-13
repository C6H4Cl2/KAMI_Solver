using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAMI_Solver.Factory;

namespace KAMI_Solver.Model.Tests
{
    [TestClass()]
    public class BoardGraphTests
    {
        private int[,] colors = { { 0, 1, 0 }, { 1, 2, 1 }, { 0, 1, 0 } };
        Board testBoard;
        BoardGraph boardGraph;

        [TestInitialize]
        public void TestInitialize()
        {
            testBoard = new Board(colors);
            boardGraph = BoardGraphFactory.createFromBoard(testBoard);
        }

        [TestMethod()]
        public void ChangeColorTest()
        {
            ColorBlock selectedColorBlock = null;
            foreach (ColorBlock cb in boardGraph.ColorBlocks)
            {
                if(cb.Color == 2)
                {
                    selectedColorBlock = cb;
                }
            }

            Assert.IsNotNull(selectedColorBlock);

            boardGraph.ChangeColor(selectedColorBlock, 1);
            Assert.AreEqual(5, boardGraph.ColorBlocks.Count);

            boardGraph.ChangeColor(selectedColorBlock, 0);
            Assert.AreEqual(1, boardGraph.ColorBlocks.Count);
        }

        [TestMethod()]
        public void GetCandidateStepsTest()
        {
            List<Step> candidateSteps = boardGraph.GetCandidateSteps();

            Assert.AreEqual(13, candidateSteps.Count);
        }

        [TestMethod()]
        public void CloneTest()
        {
            BoardGraph newBoardGraph = boardGraph.Clone();

            Assert.AreEqual(boardGraph.ColorBlocks.Count, newBoardGraph.ColorBlocks.Count);
        }
    }
}