﻿using System;
using System.Collections.Generic;
using KAMI_Solver.Factory;
using KAMI_Solver.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTest
{
    [TestClass]
    public class SolveGameTest
    {
        [TestMethod]
        public void SolveTest1()
        {
            string dataPath = @"TestPuzzles\SBL9_SplattedBug.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath, out _, out int maxSteps);
                Solver solver = new Solver
                {
                    MaxSteps = maxSteps
                };
                List<Step> solutions = solver.Solve(board);

                Assert.IsNotNull(solutions);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

        }

        [TestMethod]
        public void SolveTest2()
        {
            string dataPath = @"TestPuzzles\SEL9_SevenMovesBlack.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath, out _, out int maxSteps);
                Solver solver = new Solver
                {
                    MaxSteps = maxSteps
                };
                List<Step> solutions = solver.Solve(board);

                Assert.AreNotEqual(0, solutions.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void SolveTest3()
        {
            string dataPath = @"TestPuzzles\S5C1L1_Diag1.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath, out _, out int maxSteps);
                Solver solver = new Solver
                {
                    MaxSteps = maxSteps
                };
                List<Step> solutions = solver.Solve(board);

                Assert.AreNotEqual(0, solutions.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
