using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooRushHour
{
    public partial class MainForm : Form
    {
        private static MainForm _instance = null;

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

        private void _mainFormInit()
        {
            // var board = Solver.TestDFSSolve().SolvedBoard;
            // var board = Solver.TestBFSSolve().SolvedBoard;
            // var board = Board.TestBoard();

            var boardSolution = Solver.TestBFSSolve();
            var board = boardSolution.SolvedBoard;
            boardSolution.Path.ForEach(p =>
                Console.WriteLine("Block-{0} Move {1}", p.BlockId, p.Direction.ToString())
            );

            var boardControl = new BoardControl(board);

            var panel = new FlowLayoutPanel();
            panel.AutoSize = true;
            panel.Padding = new Padding(10);
            panel.Controls.Add(boardControl);

            Controls.Add(panel);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }
    }
}
