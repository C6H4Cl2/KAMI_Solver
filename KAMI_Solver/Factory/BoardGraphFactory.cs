using KAMI_Solver.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAMI_Solver.Factory
{
    public class BoardGraphFactory
    {
        static public BoardGraph createFromBoard(Board board)
        {
            bool[,] visited = new bool[board.Width, board.Height];
            List<List<Tile>> components = new List<List<Tile>>();

            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    if (visited[x, y]) continue;

                    Tile tile = board.GetTileAt(x, y);

                    List<Tile> component = FindConnectedComponent(tile, visited, board);
                    if (component.Count > 0) components.Add(component);
                }
            }

            HashSet<ColorBlock> allColorBlocks = new HashSet<ColorBlock>();

            // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?redirectedfrom=MSDN&view=netframework-4.8
            Dictionary<Tile, ColorBlock> tileBlockDic = new Dictionary<Tile, ColorBlock>();
            foreach (List<Tile> component in components)
            {
                ColorBlock cb = new ColorBlock(component[0].Color, component[0].PosX, component[0].PosY); // create a color block

                allColorBlocks.Add(cb);
                foreach (Tile tile in component)
                {
                    tileBlockDic.Add(tile, cb);
                }
            }

            // link each ColorBlocks to its neighbour
            LinkColorBlocks(tileBlockDic, board);

            // create BoardGraph
            BoardGraph bg = new BoardGraph(allColorBlocks);
            return bg;
        }

        static private List<Tile> FindConnectedComponent(Tile enterPoint, bool[,] visited, Board board)
        {
            List<Tile> component = new List<Tile>();

            if (enterPoint == null) return component;
            if (visited[enterPoint.PosX, enterPoint.PosY]) return component;

            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(enterPoint); // init queue
            int currentColor = enterPoint.Color;

            while (queue.Count > 0)
            {
                Tile currentTile = queue.Dequeue();
                if (visited[currentTile.PosX, currentTile.PosY]) continue;

                visited[currentTile.PosX, currentTile.PosY] = true;
                component.Add(currentTile);

                // Left
                Tile left = board.GetTileAt(currentTile.PosX - 1, currentTile.PosY);
                if (left != null && left.Color == currentColor && !visited[currentTile.PosX - 1, currentTile.PosY])
                    queue.Enqueue(left);

                // Right
                Tile right = board.GetTileAt(currentTile.PosX + 1, currentTile.PosY);
                if (right != null && right.Color == currentColor && !visited[currentTile.PosX + 1, currentTile.PosY])
                    queue.Enqueue(right);

                // Up
                Tile up = board.GetTileAt(currentTile.PosX, currentTile.PosY - 1);
                if (up != null && up.Color == currentColor && !visited[currentTile.PosX, currentTile.PosY - 1])
                    queue.Enqueue(up);

                // Down
                Tile down = board.GetTileAt(currentTile.PosX, currentTile.PosY + 1);
                if (down != null && down.Color == currentColor && !visited[currentTile.PosX, currentTile.PosY + 1])
                    queue.Enqueue(down);
            }

            return component;
        }

        static private void LinkColorBlocks(Dictionary<Tile, ColorBlock> tileBlockDic, Board board)
        {

            // horizontal - each pair of left and right
            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 1; x < board.Width; x++)
                {
                    var left = board.GetTileAt(x - 1, y);
                    var right = board.GetTileAt(x, y);

                    if(left != null && right != null && left.Color != right.Color) // if left and right have diff color
                    {
                        // TryGetValue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trygetvalue?view=netframework-4.8
                        tileBlockDic.TryGetValue(left, out ColorBlock cbLeft);
                        tileBlockDic.TryGetValue(right, out ColorBlock cbRight);
                        if(cbLeft != null && cbRight != null)
                        {
                            // link left to right and right to left
                            cbLeft.AddNeighbour(cbRight);
                            cbRight.AddNeighbour(cbLeft);
                        }
                    }
                }
            }

            // vertical - each pair of up and down
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 1; y < board.Height; y++)
                {
                    var up = board.GetTileAt(x, y - 1);
                    var down = board.GetTileAt(x, y);

                    if (up != null && down != null && up.Color != down.Color) // if up and down have diff color
                    {
                        // TryGetValue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trygetvalue?view=netframework-4.8
                        tileBlockDic.TryGetValue(up, out ColorBlock cbUp);
                        tileBlockDic.TryGetValue(down, out ColorBlock cbDown);
                        if (cbUp != null && cbDown != null)
                        {
                            // link left to right and right to left
                            cbUp.AddNeighbour(cbDown);
                            cbDown.AddNeighbour(cbUp);
                        }
                    }
                }
            }
        }
    }
}
