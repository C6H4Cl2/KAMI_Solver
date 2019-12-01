using KAMI_Solver.Factory;
using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class Solver : IDisposable
    {
        private int maxSteps = int.MaxValue; // init with max steps
        public int MaxSteps { set { this.maxSteps = value; } }

        private CancellationTokenSource ts;

        public Solver()
        {
            ts = new CancellationTokenSource();
        }

        public List<Step> Solve(Board board)
        {
            BoardGraph graph = BoardGraphFactory.createFromBoard(board);
            return Solve(graph);
        }

        public List<Step> Solve(BoardGraph graph)
        {
            List<Step> solution = SolveHelper(graph, 0);
            if (solution != null) solution.Reverse();
            return solution;
        }

        private List<Step> SolveHelper(BoardGraph graph, int stepCount)
        {
            // if the task is cancelled
            if (ts.Token.IsCancellationRequested) return null;

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
                List<Step> solution = SolveHelper(newGraph, stepCount + 1);
                if (solution != null)
                {
                    solution.Add(step);
                    return solution;
                }
            }

            return null; // cannot find solution in this branch
        }

        public void Cancel()
        {
            if (ts != null) ts.Cancel();
        }

        // refer: https://docs.microsoft.com/en-ca/visualstudio/code-quality/ca1063-implement-idisposable-correctly?view=vs-2015
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // CancellationTokenSource needs to implements "IDisposable"
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && ts != null)
            {
                ts.Dispose();
                ts = null;
            }
        }

        ~Solver()
        {
            Dispose(false);
        }
    }
}
