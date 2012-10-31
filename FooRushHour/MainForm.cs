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
            
            var solvedBoard = Solver.TestSolve();
            // var boardControl = new BoardControl(Board.TestBoard());
            var boardControl = new BoardControl(solvedBoard);

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
