using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Tile
    {
        public int Color { get; }

        public int PosX { get; }

        public int PosY { get; }

        public Tile(int _color, int _posX, int _posY)
        {
            this.Color = _color;
            this.PosX = _posX;
            this.PosY = _posY;
        }
    }
}
