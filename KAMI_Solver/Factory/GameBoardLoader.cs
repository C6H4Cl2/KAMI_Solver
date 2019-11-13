using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KAMI_Solver.Factory
{
    public class GameBoardLoader
    {
        static public Board Load(string dataPath, out int[,] colors)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dataPath);

            XmlElement root = doc.DocumentElement;

            try
            {
                int width = Convert.ToInt32(root.Attributes["width"].Value);
                int height = Convert.ToInt32(root.Attributes["height"].Value);
                string colours = root.Attributes["colours"].Value;

                colors = new int[width, height];
                for (int y = 0; y < colors.GetLength(1); y++)
                {
                    for (int x = 0; x < colors.GetLength(0); x++)
                    {
                        colors[x, y] = (int)char.GetNumericValue(colours[x + width * y]);
                    }
                }

                Board board = new Board(colors);
                return board;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        static public Board Load(string dataPath)
        {
            return Load(dataPath, out _);
        }
    }
}
