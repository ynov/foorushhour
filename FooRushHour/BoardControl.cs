using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FooRushHour
{
    public partial class BoardControl : UserControl
    {
        private MainForm _mainForm;
        private Board _board;

        public ToolboxForm Toolbox { get; set; }
        public bool EditingMode { get; set; }

        public BoardControl(MainForm mainForm, Board board)
        {
            _mainForm = mainForm;
            _board = board;
            Width = Board.BOX_SQUARE_SIZE * _board.Width + 1;
            Height = Board.BOX_SQUARE_SIZE * _board.Height + 1;

            InitializeComponent();
            Paint += new PaintEventHandler(_paint);
            _boardControlInit();

            EditingMode = false;

            MouseClick += new MouseEventHandler(_mouseClick);
        }

        public void UpdateBlockPosition(int blockId)
        {
            Controls[blockId - 1].Location = _board.BlockList[blockId - 1].Location;
        }

        private void _paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var pen = new Pen(Color.Black);

            for (int x = 0; x <= _board.Width + 1; x++)
                g.DrawLine(pen, x * Board.BOX_SQUARE_SIZE, 0, x * Board.BOX_SQUARE_SIZE, _board.Height * Board.BOX_SQUARE_SIZE);
            for (int y = 0; y <= _board.Width + 1; y++)
                g.DrawLine(pen, 0, y * Board.BOX_SQUARE_SIZE, _board.Width * Board.BOX_SQUARE_SIZE, y * Board.BOX_SQUARE_SIZE);

            g.FillRectangle(new SolidBrush(Color.Gold), _board.EndPoint.X * Board.BOX_SQUARE_SIZE + 5, _board.EndPoint.Y * Board.BOX_SQUARE_SIZE + 5,
                Board.BOX_SQUARE_SIZE * 2 - 10, Board.BOX_SQUARE_SIZE - 10);
        }

        private void _boardControlInit()
        {
            _board.BlockList.ForEach(b => Controls.Add(new BlockControl(_board, b, this)));
            _board.PrintMatrix();
            Console.WriteLine(_board.ToText());
        }

        private void _mouseClick(object s, MouseEventArgs e)
        {
            if (EditingMode && e.Button == MouseButtons.Left)
            {
                var block = new Block(_board, Toolbox.Orientation, Toolbox.Length,
                    new Point(e.X / Board.BOX_SQUARE_SIZE, e.Y / Board.BOX_SQUARE_SIZE));

                _board.BlockList.Add(block);
                Controls.Add(new BlockControl(_board, block, this));
            }
        }
    }
}
