using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.Factory;
using System.Collections.Generic;

namespace KAMI_Solver.Model.Tests
{
    [TestClass()]
    public class SolverTests
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
        public void solveTest()
        {
            Solver solver = new Solver();
            solver.MaxSteps = 2;

            List<Step> solution = solver.Solve(boardGraph);
            Assert.IsNotNull(solution);
        }
    }
}