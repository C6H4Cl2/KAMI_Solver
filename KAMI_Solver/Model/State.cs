using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Model
{
    public class State
    {
        public Board Board { get; }
        public List<List<Tile>> TileGroups { get; }

        // the colors that used in current state
        // Since the goal of the game is to draw the whole board to only one color,
        // if a color is not used in current state, then the color will not be necessary to be used in future steps
        public SortedSet<Color> ColorsInUsed { get; }

        public int EntropyValue { get; }

        public State(Board board)
        {
            Board = new Board(board); // make a copy
            TileGroups = clusteredByColor(Board);
            ColorsInUsed = findColorsInUsed(TileGroups);
            EntropyValue = computeEntropyValue(ColorsInUsed, Board);
        }

        public State(State s) : this(s.Board) { }

        private List<List<Tile>> clusteredByColor(Board board)
        {
            List<List<Tile>> result = new List<List<Tile>>();

            // devided to groups
            bool[,] visited = new bool[board.width, board.height]; // inital to false
            Color currentColor = Color.DefaultBlankColor;
            for (int x = 0; x < board.width; x++)
            {
                for (int y = 0; y < board.height; y++)
                {
                    // if a tile is not visisted
                    if(!visited[x, y])
                    {
                        // update current color
                        currentColor = board.getTileAt(x, y).Color;

                        List<Tile> cluster = clusteredByColorHelper(board, visited, currentColor, x, y);
                        result.Add(cluster);
                    }
                }
            }

            return result;
        }

        private List<Tile> clusteredByColorHelper(Board board, bool[,] visited, Color currentColor, int x, int y)
        {
            List<Tile> result = new List<Tile>();
            if (x < 0 || y < 0 || x >= board.width || y >= board.height) return result;
            if (visited[x, y]) return result;
            if (board.getTileAt(x, y).Color != currentColor) return result;

            // set the tile to be visited
            visited[x, y] = true;
            // add this tile to result b/c they have the same color
            result.Add(board.getTileAt(x, y));

            // recursively visit tiles around it
            List<Tile> rightResult = clusteredByColorHelper(board, visited, currentColor, x + 1, y);
            List<Tile> leftResult = clusteredByColorHelper(board, visited, currentColor, x - 1, y);
            List<Tile> upResult = clusteredByColorHelper(board, visited, currentColor, x, y - 1);
            List<Tile> downResult = clusteredByColorHelper(board, visited, currentColor, x, y + 1);

            // concatenate all results
            result = result.Concat(rightResult).Concat(leftResult).Concat(upResult).Concat(downResult).ToList<Tile>();

            return result;
        }

        private SortedSet<Color> findColorsInUsed(List<List<Tile>> colorGroups)
        {
            SortedSet<Color> result = new SortedSet<Color>();
            foreach (List<Tile> colorGroup in colorGroups)
            {
                result.Add(colorGroup[0].Color);
            }
            return result;
        }

        private int computeEntropyValue(SortedSet<Color> ColorsInUsed, Board board)
        {
            int result = 0;

            for (int x = 0; x < board.width; x++)
            {
                for (int y = 0; y < board.height; y++)
                {
                    Tile tile = board.getTileAt(x, y);
                    Color centerColor = tile.Color;
                    HashSet<Color> diffColorsAround = new HashSet<Color>();

                    Tile leftTile = board.getTileAt(x - 1, y);
                    if (leftTile != null && leftTile.Color != centerColor) diffColorsAround.Add(leftTile.Color);

                    Tile rightTile = board.getTileAt(x + 1, y);
                    if (rightTile != null && rightTile.Color != centerColor) diffColorsAround.Add(rightTile.Color);

                    Tile upTile = board.getTileAt(x, y - 1);
                    if (upTile != null && upTile.Color != centerColor) diffColorsAround.Add(upTile.Color);

                    Tile downTile = board.getTileAt(x, y + 1);
                    if (downTile != null && downTile.Color != centerColor) diffColorsAround.Add(downTile.Color);

                    // compute entropy
                    result += diffColorsAround.Count;
                }
            }
            return result;
        }
    }
}
