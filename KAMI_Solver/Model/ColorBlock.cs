using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class ColorBlock
    {

        public Position Pos { get; }

        public int Color { get; private set; }

        private HashSet<ColorBlock> Neighbours;

        public readonly Dictionary<int, int> CandidateColors; // <color code | # of neighbours have this color code>


        public ColorBlock(int color, int x, int y)
        {
            Color = color;
            Pos = new Position(x, y);
            Neighbours = new HashSet<ColorBlock>();
            CandidateColors = new Dictionary<int, int>();
        }

        public ColorBlock(ColorBlock cb) : this(cb.Color, cb.Pos.X, cb.Pos.Y) { }

        public IEnumerator<ColorBlock> GetNeighboursEnumerator()
        {
            return Neighbours.GetEnumerator();
        }

        public void AddNeighbour(ColorBlock cb)
        {
            if (Neighbours.Add(cb) && !this.Equals(cb))
            {
                int color = cb.Color;
                if (CandidateColors.ContainsKey(color))
                {
                    CandidateColors[color]++;
                }
                else
                {
                    CandidateColors.Add(color, 1);
                }
            }
        }

        public void RemoveNeighbour(ColorBlock cb)
        {
            if (Neighbours.Remove(cb))
            {
                int color = cb.Color;
                if (CandidateColors.ContainsKey(color))
                {
                    CandidateColors[color]--;
                    if(CandidateColors[color] <= 0) CandidateColors.Remove(color);
                }
            }
        }

        /// <summary>
        /// change a color block's color
        /// </summary>
        /// <param name="newColor"></param>
        /// <returns>removed neighbours</returns>
        public List<ColorBlock> ChangeColorAndMergeNeighbours(int newColor)
        {
            if (newColor == Color) return new List<ColorBlock>(0);

            this.Color = newColor;

            List<ColorBlock> mergedNeighbours = new List<ColorBlock>(Neighbours.Count);
            foreach (ColorBlock cb in Neighbours)
            {
                if (cb.Color == newColor)
                {
                    mergedNeighbours.Add(cb);
                }
            }

            foreach (ColorBlock mergedNeighbour in mergedNeighbours)
            {
                this.RemoveNeighbour(mergedNeighbour);

                HashSet<ColorBlock> mergedNeighbourNeighbours = mergedNeighbour.Neighbours;
                foreach (ColorBlock mergedNeighbourNeighbour in mergedNeighbourNeighbours)
                {
                    if (mergedNeighbourNeighbour.Equals(this)) continue;
                    mergedNeighbourNeighbour.RemoveNeighbour(mergedNeighbour);

                    this.AddNeighbour(mergedNeighbourNeighbour);
                    mergedNeighbourNeighbour.AddNeighbour(this);
                }
            }

            return mergedNeighbours;
        }

        public int GetDistanceToTheFarthest(out ColorBlock farthestColorBlock)
        {
            Queue<ColorBlock> queue = new Queue<ColorBlock>();
            queue.Enqueue(this);
            HashSet<ColorBlock> visited = new HashSet<ColorBlock>();
            visited.Add(this);

            int distance = 0;
            farthestColorBlock = null; // default value
            while (queue.Count > 0)
            {
                Queue<ColorBlock> nextLevelQueue = new Queue<ColorBlock>();
                while (queue.Count > 0)
                {
                    farthestColorBlock = queue.Dequeue();
                    foreach (ColorBlock farther in farthestColorBlock.Neighbours)
                    {
                        if (!visited.Contains(farther))
                        {
                            nextLevelQueue.Enqueue(farther);
                            visited.Add(farther);
                        }
                    }
                }
                distance++;
                queue = nextLevelQueue;
            }

            return distance-1;
            
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                ColorBlock cb = obj as ColorBlock;
                return Pos.Equals(cb.Pos);
            }
        }

        public override int GetHashCode()
        {
            return Pos.GetHashCode();
        }
    }
}
