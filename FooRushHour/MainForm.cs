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

            MenuItem boardMenu = new MenuItem("&Board");
            boardMenu.Name = "boardMenu";
            boardMenu.MenuItems.Add(new MenuItem("[&Create] Board", new EventHandler(_createBoard)));
            boardMenu.MenuItems.Add(new MenuItem("&Create Random Board"));
            boardMenu.MenuItems.Add(new MenuItem("[&Open] Board from File", new EventHandler(_openBoard)));
            boardMenu.MenuItems.Add(new MenuItem("[&Save] Board to File", new EventHandler(_saveBoard)));

            MenuItem solveMenu = new MenuItem("&Solve");
            solveMenu.Name = "solveMenu";
            solveMenu.MenuItems.Add(new MenuItem("Solve using &DFS", new EventHandler((s, e) => _solverClick(s, e, b => Solver.DFSSolve(b)))));
            solveMenu.MenuItems.Add(new MenuItem("Solve using &BFS", new EventHandler((s, e) => _solverClick(s, e, b => Solver.BFSSolve(b)))));

            MenuItem speedMenu = new MenuItem("S&peed Adjustment");
            speedMenu.MenuItems.Add(new MenuItem("50 ms", new EventHandler((s, e) => _timeoutMs = 50)));
            speedMenu.MenuItems.Add(new MenuItem("100 ms", new EventHandler((s, e) => _timeoutMs = 100)));
            speedMenu.MenuItems.Add(new MenuItem("200 ms", new EventHandler((s, e) => _timeoutMs = 200)));
            speedMenu.MenuItems.Add(new MenuItem("400 ms", new EventHandler((s, e) => _timeoutMs = 400)));
            speedMenu.MenuItems.Add(new MenuItem("600 ms", new EventHandler((s, e) => _timeoutMs = 600)));
            solveMenu.MenuItems.Add(speedMenu);

            Menu.MenuItems.Add(boardMenu);
            Menu.MenuItems.Add(solveMenu);
        }

        private void _mainFormInit()
        {
            _mainMenuInit();

            _mainPanel = new FlowLayoutPanel();
            _mainPanel.AutoSize = true;
            _mainPanel.Padding = new Padding(10);

            _currentBoard = BoardIO.ReadFile("StartupDefault.txt");
            _boardControl = new BoardControl(this, _currentBoard);
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
                Console.WriteLine("Movement trace:");
                boardSolution.Path.ForEach(p =>
                {
                    Console.WriteLine("Block-{0} Move {1} * {2}", p.BlockId, p.Direction.ToString(), p.Times);
                    for (int i = 0; i < p.Times; i++)
                        _currentBoard.BlockList[p.BlockId - 1].Move(p.Direction);
                    _boardControl.Invoke(new Action<Movement>(q => _boardControl.UpdateBlockPosition(q.BlockId)), p);
                    Thread.Sleep(_timeoutMs);
                });
                Console.WriteLine("Done\n");
                _currentBoard.UserMovementLock = false;
            });

            animateGoal.Start();
        }

        private void _openBoard(object s, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Board Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            dlg.FilterIndex = 1;

            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                _currentBoard = BoardIO.ReadFile(dlg.FileName);
                _mainPanel.Controls.Remove(_boardControl);
                _boardControl = new BoardControl(this, _currentBoard);
                _mainPanel.Controls.Add(_boardControl);
            }
        }

        private void _saveBoard(object s, EventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Board Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            dlg.FilterIndex = 1;

            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                BoardIO.WriteFile(_currentBoard, dlg.FileName);
            }
        }

        private void _createBoard(object s, EventArgs e)
        {
            var createDlg = new CreateDialog();
            createDlg.ShowDialog();

            if (createDlg.Board == null)
                return;

            var toolbox = new ToolboxForm(this);
            toolbox.Location = new Point(Location.X + Width, Location.Y);
            toolbox.Show();

            _currentBoard = createDlg.Board;
            _mainPanel.Controls.Remove(_boardControl);
            _boardControl = new BoardControl(this, _currentBoard);
            _mainPanel.Controls.Add(_boardControl);

            Menu.MenuItems["boardMenu"].Enabled = false;
            Menu.MenuItems["solveMenu"].Enabled = false;
            _boardControl.Toolbox = toolbox;
            _boardControl.EditingMode = true;
        }

        public void DoneEditing(ToolboxForm toolbox)
        {
            if (!_boardControl.EditingMode)
                return;

            _saveBoard(null, null);
            _mainPanel.Controls.Remove(toolbox);
            Menu.MenuItems["boardMenu"].Enabled = true;
            Menu.MenuItems["solveMenu"].Enabled = true;
            _boardControl.Toolbox = null;
            _boardControl.EditingMode = false;
        }
    }
}
