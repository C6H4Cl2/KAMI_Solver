using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KAMI_Solver.ViewModel;
using KAMI_Solver.Model;

namespace UnitTest
{
    [TestClass]
    public class GameBoardLoaderTest
    {
        [TestMethod]
        public void LoadTest()
        {
            string dataPath = @"D:\game\SteamLibrary\steamapps\common\KAMI\puzzles\Convolution\SConvL9.xml";

            try
            {
                Board board = GameBoardLoader.Load(dataPath);
                Assert.IsNotNull(board);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
