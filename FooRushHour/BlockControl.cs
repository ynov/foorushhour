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
        private BoardControl _boardControl;
        private Block _block;
        private Button _button;
        private bool _moving = false;

        private bool mouseDown = false;

        public int Id
        {
            get;
            private set;
        }

        public BlockControl(Board board, Block block, BoardControl boardControl)
        {
            Id = block.Id;

            _board = board;
            _block = block;
            _boardControl = boardControl;
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

            if (_block.Type == 1)
                _button.BackColor = Color.Red;
            else
                _button.BackColor = Color.Gray;

            _button.MouseDown += new MouseEventHandler((s, e) => { mouseDown = true; _mouseRightDown(s, e); });
            _button.MouseUp += new MouseEventHandler((s, e) => {
                mouseDown = false;
                if (_moving)
                {
                    InfoBox.Instance.IncementMovementCount();
                    _moving = false;
                }
            });
            _button.MouseMove += new MouseEventHandler((s, e) => _mouseMove(e.X, e.Y));
        }

        private void _mouseMove(int x, int y)
        {
            if (_board.UserMovementLock)
                return;

            if (mouseDown)
            {
                if (x > _button.Width && _block.ValidMove(Direction.Right))
                {
                    _moving = true;
                    _block.Move(Direction.Right, true);
                }
                else if (x < 0 && _block.ValidMove(Direction.Left))
                {
                    _moving = true;
                    _block.Move(Direction.Left, true);
                }
                else if (y > _button.Height && _block.ValidMove(Direction.Down))
                {
                    _moving = true;
                    _block.Move(Direction.Down, true);
                }
                else if (y < 0 && _block.ValidMove(Direction.Up))
                {
                    _moving = true;
                    _block.Move(Direction.Up, true);
                }
                Location = _block.Location;
            }
        }

        private void _mouseRightDown(object s, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && _block.Type != 1 && _boardControl.EditingMode)
            {
                mouseDown = false;
                _board.BlockList.Remove(_block);
                _boardControl.Controls.Remove(this);
            }
        }
    }
}
