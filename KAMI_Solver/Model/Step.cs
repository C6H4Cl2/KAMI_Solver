using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Step
    {
        public ColorBlock ColorBlock { get; }
        public int NewColor { get; }
        public int NeighboursHaveThisNewColor { get; }

        public Step(ColorBlock colorBlock, int newColor, int neighboursHaveThisNewColor)
        {
            ColorBlock = colorBlock;
            NewColor = newColor;
            NeighboursHaveThisNewColor = neighboursHaveThisNewColor;
        }

        public override string ToString()
        {
            return string.Format("Change pos:({0}, {1}) to color '{2}'", ColorBlock.Pos.X, ColorBlock.Pos.Y, NewColor);
        }
    }
}
