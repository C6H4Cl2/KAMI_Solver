using KAMI_Solver.Factory;
using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Solver
    {
        private int maxSteps = Int32.MaxValue; // init with max steps
        public int MaxSteps { set { this.maxSteps = value; } }

        public Solver() { }

        public List<Step> solve(Board board)
        {
            BoardGraph graph = BoardGraphFactory.createFromBoard(board);
            return solve(graph);
        }

        public List<Step> solve(BoardGraph graph)
        {
            List<Step> solution = solveHelper(graph, 0);
            if(solution != null) solution.Reverse();
            return solution;
        }

        private List<Step> solveHelper(BoardGraph graph, int stepCount)
        {
            if (graph.ColorBlocks.Count <= 1)
            {
                return new List<Step>(); // find solution
            }

            // heuristic algorithm - cut branch
            int colorLeft = graph.GetColorLeft();
            if (maxSteps < colorLeft - 1 + stepCount)
            {
                return null;
            }

            var candidateSteps = graph.GetCandidateSteps();
            foreach (var step in candidateSteps)
            {

                var selectedColorBlock = step.ColorBlock;

                // heuristic algorithm - cut branch
                var farthestDist = selectedColorBlock.GetDistanceToTheFarthest(out _);
                int leftSteps = maxSteps - stepCount;
                if (farthestDist > maxSteps - stepCount) continue;

                // try next step
                var newGraph = graph.Clone(); 
                newGraph.ChangeColor(selectedColorBlock, step.NewColor);
                List<Step> solution = solveHelper(newGraph, stepCount + 1);
                if (solution != null)
                {
                    solution.Add(step);
                    return solution;
                }
            }

            return null; // cannot find solution in this branch
        }
    }
}
