using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FooRushHour
{
    public class BoardSolution
    {
        public Board SolvedBoard { get; set; }
        public List<Movement> Path { get; set; }
    }

    public class Movement
    {
        public string PrevNode { get; set; }
        public int BlockId { get; set; }
        public Direction Direction { get; set; }
    }

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
        public static BoardSolution TestDFSSolve()
        {
            Board solvedBoard = null;
            var boards = new Stack<Board>();

            string start = null;
            string finish = null;

            var visited = new Dictionary<string, Movement>();

            boards.Push(Board.TestBoard());

            var goal = false;
            while (boards.Count != 0 && !goal)
            {
                var currentBoard = boards.Pop();
                if (start == null)
                    start = currentBoard.ToString();

                // currentBoard.PrintMatrix();
                // Console.WriteLine(i++);

                goal = currentBoard.GoalReached();
                if (goal)
                {
                    solvedBoard = currentBoard;
                    finish = solvedBoard.ToString();
                    break;
                }

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
                                    visited[board.ToString()] = new Movement
                                    {
                                        PrevNode = currentBoard.ToString(),
                                        BlockId = block.Id,
                                        Direction = direction,
                                    };

                                    boards.Push(board);
                                }
                                // board = new Board(board.Width, board.Height, board.BlockList, board.EndPoint);
                            }
                        }
                    }
                }
            }

            boards.Clear();
            return new BoardSolution()
            {
                SolvedBoard = solvedBoard,
                Path = createPath(start, finish, visited),
            };
        }
        
        // BFS
        public static BoardSolution TestBFSSolve()
        {
            Board solvedBoard = null;
            var boards = new Queue<Board>();

            string start = null;
            string finish = null;

            var visited = new Dictionary<string, Movement>();

            boards.Enqueue(Board.TestBoard());

            var goal = false;
            while (boards.Count != 0 && !goal)
            {
                var currentBoard = boards.Dequeue();
                if (start == null)
                    start = currentBoard.ToString();

                // currentBoard.PrintMatrix();
                // Console.WriteLine(i++);

                goal = currentBoard.GoalReached();
                if (goal)
                {
                    solvedBoard = currentBoard;
                    finish = solvedBoard.ToString();
                    break;
                }

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
                                    visited[board.ToString()] = new Movement
                                    {
                                        PrevNode = currentBoard.ToString(),
                                        BlockId = block.Id,
                                        Direction = direction,
                                    };

                                    boards.Enqueue(board);
                                }
                                // board = new Board(board.Width, board.Height, board.BlockList, board.EndPoint);
                            }
                        }
                    }
                }
            }

            boards.Clear();
            return new BoardSolution() {
                SolvedBoard = solvedBoard,
                Path = createPath(start, finish, visited),
            };
        }

        private static List<Movement> createPath(string start, string finish, Dictionary<string, Movement> visited)
        {
            var path = new List<Movement>();
            var v = finish;

            path.Add(visited[v]);
            do
            {
                v = visited[v].PrevNode;
                if (v != start)
                    path.Add(visited[v]);
            } while (v != start);
            path.Reverse();

            visited.Clear();
            return path;
        }
    }
}
