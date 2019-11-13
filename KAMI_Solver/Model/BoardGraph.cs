using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    // represent the Board in Graph version
    public class BoardGraph
    {
        public HashSet<ColorBlock> ColorBlocks { get; }

        // Board to BoardGraph
        public BoardGraph(HashSet<ColorBlock> colorBlocks)
        {
            this.ColorBlocks = colorBlocks;
        }

        /// <summary>
        /// Get the number of colors left
        /// </summary>
        /// <returns></returns>
        public int GetColorLeft()
        {
            HashSet<int> colors = new HashSet<int>();
            foreach (ColorBlock cb in ColorBlocks)
            {
                colors.Add(cb.Color);
            }
            return colors.Count;
        }

        /// <summary>
        /// Change target color block to a new color
        /// </summary>
        /// <param name="target">target color block</param>
        /// <param name="newColor">new color</param>
        public void ChangeColor(ColorBlock target, int newColor)
        {
            if (target == null) return;

            // note: "target" can be from another graph, get the actual "target" which belongs to this graph
            ColorBlocks.TryGetValue(target, out ColorBlock actualTarget);
            List<ColorBlock> removedColorBlocks = actualTarget.ChangeColorAndMergeNeighbours(newColor);

            foreach (ColorBlock cb in removedColorBlocks)
            {
                ColorBlocks.Remove(cb);
            }
        }

        /// <summary>
        /// Return all candidate steps in a specific order
        /// The list is sorted in a heuristic algorithm to speed up
        /// </summary>
        /// <returns></returns>
        public List<Step> GetCandidateSteps()
        {
            List<Step> stepList = new List<Step>();

            foreach (ColorBlock cb in ColorBlocks)
            {
                var candidateColors = cb.CandidateColors.Keys;
                foreach (int candidateColor in candidateColors)
                {
                    Step step = new Step(cb, candidateColor, cb.CandidateColors[candidateColor]);
                    stepList.Add(step);
                }
            }

            // sort by connected components
            // if a component has 4 red and 1 green neighbours, then "4 red" will be sorted in front of the "1 green"
            stepList = stepList.OrderByDescending(step => step.NeighboursHaveThisNewColor).ToList();

            // concat two lists
            return stepList;
        }

        /// <summary>
        /// Clone the graph
        /// </summary>
        /// <returns>new graph</returns>
        public BoardGraph Clone()
        {
            HashSet<ColorBlock> newColorBlocks = new HashSet<ColorBlock>();
            Dictionary<ColorBlock, ColorBlock> oldNewColorblockDic = new Dictionary<ColorBlock, ColorBlock>();

            foreach (ColorBlock old in ColorBlocks)
            {
                ColorBlock newColorBlock = new ColorBlock(old);

                oldNewColorblockDic.Add(old, newColorBlock);
                newColorBlocks.Add(newColorBlock);
            }

            foreach (ColorBlock old in ColorBlocks)
            {
                IEnumerator<ColorBlock> oldNeighboursEnumerator = old.GetNeighboursEnumerator();
                ColorBlock newColorBlock = oldNewColorblockDic[old];
                while (oldNeighboursEnumerator.MoveNext())
                {
                    ColorBlock newNeighbour = oldNewColorblockDic[oldNeighboursEnumerator.Current];
                    newColorBlock.AddNeighbour(newNeighbour);
                }
            }

            BoardGraph newBoardGraph = new BoardGraph(newColorBlocks);
            return newBoardGraph;
        }
    }
}
