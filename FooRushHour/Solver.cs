using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooRushHour
{
    public class Solver
    {
        public static Board TestSolve()
        {
            Board solvedBoard = null;
            var boards = new Stack<Board>();

            var visited = new Dictionary<string, bool>();
            var movement = new Dictionary<string, string>();

            boards.Push(Board.TestBoard());

            var goal = false;
            var i = 0;
            while (boards.Count != 0 && !goal)
            {
                var currentBoard = boards.Pop();
                visited[currentBoard.ToString()] = true;

                //currentBoard.PrintMatrix();
                //Console.WriteLine(i++);
                goal = currentBoard.GoalReached();
                if (goal)
                    solvedBoard = currentBoard;

                foreach (var block in currentBoard.BlockList)
                {
                    if (block.ValidMove(Direction.Right))
                    {
                        var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                        board.BlockList[block.Id - 1].Move(Direction.Right);
                        if (!visited.ContainsKey(board.ToString()))
                        {
                            visited[board.ToString()] = true;
                            boards.Push(board);
                        }
                    }

                    if (block.ValidMove(Direction.Left))
                    {
                        var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                        board.BlockList[block.Id - 1].Move(Direction.Left);
                        if (!visited.ContainsKey(board.ToString()))
                        {
                            visited[board.ToString()] = true;
                            boards.Push(board);
                        }
                    }

                    if (block.ValidMove(Direction.Up))
                    {
                        var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                        board.BlockList[block.Id - 1].Move(Direction.Up);
                        if (!visited.ContainsKey(board.ToString()))
                        {
                            visited[board.ToString()] = true;
                            boards.Push(board);
                        }
                    }

                    if (block.ValidMove(Direction.Down))
                    {
                        var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                        board.BlockList[block.Id - 1].Move(Direction.Down);
                        if (!visited.ContainsKey(board.ToString()))
                        {
                            visited[board.ToString()] = true;
                            boards.Push(board);
                        }
                    }
                }
            }

            return solvedBoard;
        }
    }
}
