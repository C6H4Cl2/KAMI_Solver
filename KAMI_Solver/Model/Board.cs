using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Board
    {
        public int width { get; }
        public int height { get; }

        private Tile[,] tiles;

        public Board(int _width, int _height)
        {
            width = _width;
            height = _height;
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tiles[x, y] = new Tile();
        }

        public Board(int[,] colors)
        {
            width = colors.GetLength(0);
            height = colors.GetLength(1);
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y] = new Tile(colors[x, y]);
                }
        }

        public Board(Board board)
        {
            width = board.width;
            height = board.height;
            tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tiles[x, y] = new Tile(board.getTileAt(x, y));
        }

        public Tile getTileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height) return null;

            return tiles[x, y];
        }
    }
}
