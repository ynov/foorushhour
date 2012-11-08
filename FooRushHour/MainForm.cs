using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooRushHour
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = null;
        private Panel _mainPanel = null;
        private Board _currentBoard = null;
        private BoardControl _boardControl = null;
        private int _timeoutMs = 200;

        public static MainForm Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MainForm();
                }
                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private MainForm()
        {
            InitializeComponent();
            _mainFormInit();

            Instance = this;
        }

        private void _mainMenuInit()
        {
            Menu = new MainMenu();

            MenuItem fileMenu = new MenuItem("&Board");
            fileMenu.MenuItems.Add(new MenuItem("&Create Board"));
            fileMenu.MenuItems.Add(new MenuItem("&Create Random Board"));
            fileMenu.MenuItems.Add(new MenuItem("&Open Board from File"));
            fileMenu.MenuItems.Add(new MenuItem("&Save Board to File"));

            MenuItem solveMenu = new MenuItem("&Solve");
            solveMenu.MenuItems.Add(new MenuItem("Solve using &DFS", new EventHandler((s, e) => _solverClick(s, e, b => Solver.DFSSolve(b)))));
            solveMenu.MenuItems.Add(new MenuItem("Solve using &BFS", new EventHandler((s, e) => _solverClick(s, e, b => Solver.BFSSolve(b)))));

            MenuItem speedMenu = new MenuItem("S&peed Adjustment");
            speedMenu.MenuItems.Add(new MenuItem("100 ms", new EventHandler((s, e) => _timeoutMs = 100)));
            speedMenu.MenuItems.Add(new MenuItem("200 ms", new EventHandler((s, e) => _timeoutMs = 200)));
            speedMenu.MenuItems.Add(new MenuItem("400 ms", new EventHandler((s, e) => _timeoutMs = 400)));
            speedMenu.MenuItems.Add(new MenuItem("600 ms", new EventHandler((s, e) => _timeoutMs = 600)));
            solveMenu.MenuItems.Add(speedMenu);

            Menu.MenuItems.Add(fileMenu);
            Menu.MenuItems.Add(solveMenu);
        }

        private void _mainFormInit()
        {
            _mainMenuInit();

            _mainPanel = new FlowLayoutPanel();
            _mainPanel.AutoSize = true;
            _mainPanel.Padding = new Padding(10);

            _currentBoard = Board.TestBoard();
            _boardControl = new BoardControl(_currentBoard);
            _mainPanel.Controls.Add(_boardControl);

            Controls.Add(_mainPanel);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        private void _solverClick(object s, EventArgs e, Func<Board, BoardSolution> f)
        {
            if (_currentBoard.GoalReached() || _currentBoard.UserMovementLock)
                return;

            _currentBoard.UserMovementLock = true;
            var boardSolution = f(_currentBoard);
            Task animateGoal = new Task(() =>
            {
                boardSolution.Path.ForEach(p =>
                {
                    Console.WriteLine("Block-{0} Move {1} * {2}", p.BlockId, p.Direction.ToString(), p.Times);
                    for (int i = 0; i < p.Times; i++)
                        _currentBoard.BlockList[p.BlockId - 1].Move(p.Direction);
                    _boardControl.UpdateBlockPosition(p.BlockId);
                    Thread.Sleep(_timeoutMs);
                });
                _currentBoard.UserMovementLock = false;
            });

            animateGoal.Start();
        }
    }
}
