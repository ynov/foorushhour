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
    public partial class BlockControl : UserControl
    {
        private Board _board;
        private Block _block;
        private Button _button;

        private bool mouseDown = false;

        public int Id
        {
            get;
            private set;
        }

        public BlockControl(Board board, Block block)
        {
            Id = block.Id;

            _board = board;
            _block = block;
            BackColor = Color.Transparent;

            InitializeComponent();
            _blockControlInit();
        }

        private void _blockControlInit()
        {
            Location = _block.Location;

            _button = new Button();
            if (_block.Orientation == Orientation.Horizontal)
            {
                Size = new Size(_block.Size * Board.BOX_SQUARE_SIZE, Board.BOX_SQUARE_SIZE);
                _button.Size = new Size(_block.Size * Board.BOX_SQUARE_SIZE - 20, Board.BOX_SQUARE_SIZE - 20);
            }
            else if (_block.Orientation == Orientation.Vertical)
            {
                Size = new Size(Board.BOX_SQUARE_SIZE, _block.Size * Board.BOX_SQUARE_SIZE);
                _button.Size = new Size(Board.BOX_SQUARE_SIZE - 20, _block.Size * Board.BOX_SQUARE_SIZE - 20);
            }

            Controls.Add(_button);
            _button.Location = new Point(10, 10);

            _button.MouseDown += new MouseEventHandler((s, e) => mouseDown = true);
            _button.MouseUp += new MouseEventHandler((s, e) => mouseDown = false);
            _button.MouseMove += new MouseEventHandler((s, e) => _mouseMove(e.X, e.Y));
        }

        private void _mouseMove(int x, int y)
        {
            if (mouseDown)
            {
                if (x > _button.Width && _block.ValidMove(Direction.Right))
                    _block.Move(Direction.Right);
                else if (x < 0 && _block.ValidMove(Direction.Left))
                    _block.Move(Direction.Left);
                else if (y > _button.Height && _block.ValidMove(Direction.Down))
                    _block.Move(Direction.Down);
                else if (y < 0 && _block.ValidMove(Direction.Up))
                    _block.Move(Direction.Up);

                Location = _block.Location;
            }
        }
    }
}
