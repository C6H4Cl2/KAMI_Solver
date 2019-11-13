using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Board
    {
        public int Width { get; }
        public int Height { get; }

        private Tile[,] tiles;

        public Board(int _width, int _height)
        {
            Width = _width;
            Height = _height;
            tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    tiles[x, y] = new Tile(0, x, y);
        }

        public Board(int[,] colors)
        {
            Width = colors.GetLength(0);
            Height = colors.GetLength(1);
            tiles = new Tile[Width, Height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    tiles[x, y] = new Tile(colors[x, y], x, y);
        }

        public Tile GetTileAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return null;

            return tiles[x, y];
        }
    }
}
