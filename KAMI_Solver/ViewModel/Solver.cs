using KAMI_Solver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.ViewModel
{
    public class Solver
    {
        private int maxSteps;

        public Solver(int maxSteps)
        {
            this.maxSteps = maxSteps;
        }

        public List<List<Board>> solve(Board board)
        {
            State state = new State(board);
            return solveHelper(state, 0);
        }

        private List<List<Board>> solveHelper(State currentState, int steps)
        {
            List<List<Board>> solutions = new List<List<Board>>();

            var currentBoard = currentState.Board;
            var TileGroups = currentState.TileGroups;
            var ColorsInUsed = currentState.ColorsInUsed;
            var EntropyValue = currentState.EntropyValue;

            if (steps > maxSteps || ColorsInUsed.Count - 1 > maxSteps - steps)
            {
                return solutions;
            }

            if (ColorsInUsed.Count == 1)
            {
                // Done
                List<Board> solution = new List<Board>();
                solution.Add(currentBoard);
                solutions.Add(solution);
                return solutions;
            }

            List<State> nextTodoStates = new List<State>();

            foreach (Color changeToColor in ColorsInUsed)
            {
                foreach (List<Tile> tiles in TileGroups)
                {
                    Color colorInCurrentState = tiles[0].Color;
                    if (colorInCurrentState == changeToColor) continue;

                    foreach (Tile tile in tiles) tile.Color = changeToColor; // the board color will also be updated

                    State newState = new State(currentBoard);
                    // if newState has a better heuristic value, then add the state to TODO
                    if (newState.EntropyValue < EntropyValue)
                    {
                        nextTodoStates.Add(newState);
                    }

                    foreach (Tile tile in tiles) tile.Color = colorInCurrentState; // roll back to prepare for next try
                }
            }

            // sort all Heuristic Values of next todo states
            nextTodoStates = nextTodoStates.OrderBy(s => s.ColorsInUsed.Count).ThenBy(s => s.EntropyValue).ToList<State>();

            foreach (State todoState in nextTodoStates)
            {
                List<List<Board>> partialSolutions = solveHelper(todoState, steps + 1);
                if (partialSolutions.Count > 0)
                {
                    solutions = solutions.Concat<List<Board>>(partialSolutions).ToList<List<Board>>();
                    // if only need one solution
                    break;
                }
            }

            foreach (List<Board> solution in solutions)
            {
                solution.Insert(0, currentBoard);
            }

            return solutions;
        }
    }
}
