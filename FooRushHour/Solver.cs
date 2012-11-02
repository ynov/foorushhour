using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooRushHour
{
    public class Solver
    {
        public static Direction[] directions = new Direction[] 
        {
            Direction.Right,
            Direction.Left,
            Direction.Up,
            Direction.Down
        };

        // DFS
        public static Board TestDFSSolve()
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

                // currentBoard.PrintMatrix();
                // Console.WriteLine(i++);
                goal = currentBoard.GoalReached();
                if (goal)
                    solvedBoard = currentBoard;

                foreach (var block in currentBoard.BlockList)
                {
                    foreach (var direction in directions)
                    {
                        if (block.ValidMove(direction))
                        {
                            var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                            while (board.BlockList[block.Id - 1].ValidMove(direction))
                            {
                                board.BlockList[block.Id - 1].Move(direction);
                                if (!visited.ContainsKey(board.ToString()))
                                {
                                    visited[board.ToString()] = true;
                                    boards.Push(board);
                                }
                                // board = new Board(board.Width, board.Height, board.BlockList, board.EndPoint);
                            }
                        }
                    }
                }
            }

            return solvedBoard;
        }
        
        // BFS
        public static Board TestBFSSolve()
        {
            Board solvedBoard = null;
            var boards = new Queue<Board>();

            var visited = new Dictionary<string, bool>();
            var movement = new Dictionary<string, string>();

            boards.Enqueue(Board.TestBoard());

            var goal = false;
            var i = 0;
            while (boards.Count != 0 && !goal)
            {
                var currentBoard = boards.Dequeue();
                visited[currentBoard.ToString()] = true;

                // currentBoard.PrintMatrix();
                // Console.WriteLine(i++);
                goal = currentBoard.GoalReached();
                if (goal)
                    solvedBoard = currentBoard;

                foreach (var block in currentBoard.BlockList)
                {
                    foreach (var direction in directions)
                    {
                        if (block.ValidMove(direction))
                        {
                            var board = new Board(currentBoard.Width, currentBoard.Height, currentBoard.BlockList, currentBoard.EndPoint);
                            while (board.BlockList[block.Id - 1].ValidMove(direction))
                            {
                                board.BlockList[block.Id - 1].Move(direction);
                                if (!visited.ContainsKey(board.ToString()))
                                {
                                    visited[board.ToString()] = true;
                                    boards.Enqueue(board);
                                }
                                // board = new Board(board.Width, board.Height, board.BlockList, board.EndPoint);
                            }
                        }
                    }
                }
            }

            return solvedBoard;
        }
    }
}
