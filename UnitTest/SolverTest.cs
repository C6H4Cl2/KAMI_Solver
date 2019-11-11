using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.Model;
using KAMI_Solver.ViewModel;

namespace UnitTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void SolveSimpleOneTest()
        {
            int[,] colors = new int[3, 3] { { 1, 1, 1 }, { 1, 2, 1 }, { 3, 2, 3 } };
            Board b = new Board(colors);


            Solver solver = new Solver(2);
            List<List<Board>> solutions = solver.solve(b);
        }
    }
}
