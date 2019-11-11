using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Tile
    {
        public Color Color { get; set; }

        public Tile(Color _color)
        {
            this.Color = _color;
        }

        public Tile(int colorEnumIndex)
        {
            if (colorEnumIndex >= (int)Color.MaxCount)
                throw new Exception("Too many colors to initalize tiles");
            this.Color = (Color)colorEnumIndex;
        }

        public Tile() : this(Color.DefaultBlankColor) { }

        public Tile(Tile tile)
        {
            Color = tile.Color;
        }
    }
}
