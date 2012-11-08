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
        public int Times { get; set; }
    }

    public class Solver
    {
        public static Direction[] directions = new Direction[] 
        {
            Direction.Right, Direction.Left, Direction.Up, Direction.Down
        };

        public static BoardSolution DFSSolve(Board initialBoard)
        {
            return solve(initialBoard, Algorithm.DFS);
        }
        
        public static BoardSolution BFSSolve(Board initialBoard)
        {
            return solve(initialBoard, Algorithm.BFS);
        }

        private static BoardSolution solve(Board initialBoard, Algorithm algorithm)
        {
            Board solvedBoard = null;
            IEnumerable<Board> boards = null;

            Action<IEnumerable<Board>, Board> PushF = null;
            Func<IEnumerable<Board>, Board> PopF = null;
            Func<IEnumerable<Board>, int> CountF = null;
            Action<IEnumerable<Board>> ClearF = null;

            if (algorithm == Algorithm.DFS)
            {
                boards = new Stack<Board>();
                PushF = (bs, b) => ((Stack<Board>)bs).Push(b);
                PopF = (bs) => ((Stack<Board>)bs).Pop();
                CountF = bs => ((Stack<Board>)bs).Count;
                ClearF = bs => ((Stack<Board>)bs).Clear();
            }
            else if (algorithm == Algorithm.BFS)
            {
                boards = new Queue<Board>();
                PushF = (bs, b) => ((Queue<Board>)bs).Enqueue(b);
                PopF = (bs) => ((Queue<Board>)bs).Dequeue();
                CountF = bs => ((Queue<Board>)bs).Count;
                ClearF = bs => ((Queue<Board>)bs).Clear();
            }   

            string start = null;
            string finish = null;

            var visited = new Dictionary<string, Movement>();

            PushF(boards, initialBoard);

            // int i = 1;
            var goal = false;
            while (CountF(boards) != 0 && !goal)
            {
                var currentBoard = PopF(boards);
                
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
                            int times = 0;
                            while (board.BlockList[block.Id - 1].ValidMove(direction))
                            {
                                board.BlockList[block.Id - 1].Move(direction);
                                times++;
                                if (!visited.ContainsKey(board.ToString()))
                                {
                                    visited[board.ToString()] = new Movement
                                    {
                                        PrevNode = currentBoard.ToString(),
                                        BlockId = block.Id,
                                        Direction = direction,
                                        Times = times,
                                    };

                                    PushF(boards, board);
                                }
                            }
                        }
                    }
                }
            }

            ClearF(boards);
            return new BoardSolution()
            {
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
