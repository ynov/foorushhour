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
        public static MainForm Instance
        {
            get;
            set;
        }

        public MainForm()
        {
            InitializeComponent();
            _mainFormInit();

            Instance = this;
        }

        private void _mainFormInit()
        {
            var boardControl = new BoardControl(Board.TestBoard());

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
