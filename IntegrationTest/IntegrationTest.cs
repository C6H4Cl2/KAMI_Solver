using System;
using System.Collections.Generic;
using KAMI_Solver.Model;
using KAMI_Solver.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTest
{
    [TestClass]
    public class SolveGameTest
    {
        [TestMethod]
        public void solveSConvL1Test()
        {
            string dataPath = @"D:\game\SteamLibrary\steamapps\common\KAMI\puzzles\Convolution\SConvL1.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath);
                Solver solver = new Solver(7);
                List<List<Board>> solutions = solver.solve(board);

                Assert.AreNotEqual(0, solutions.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
        [TestMethod]
        public void solveSConvL9Test()
        {
            string dataPath = @"D:\game\SteamLibrary\steamapps\common\KAMI\puzzles\Convolution\SConvL9.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath);
                Solver solver = new Solver(19);
                List<List<Board>> solutions = solver.solve(board);

                Assert.AreNotEqual(0, solutions.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void solveS5C1L9_Strips3Test()
        {
            string dataPath = @"D:\game\SteamLibrary\steamapps\common\KAMI\puzzles\5Col\S5C1L9_Strips3.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath);
                Solver solver = new Solver(7);
                List<List<Board>> solutions = solver.solve(board);

                Assert.AreNotEqual(0, solutions.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
