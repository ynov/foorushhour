using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FooRushHour
{
    public class Block
    {
        private static int _lastId = 0;
        private Point _blockPosition;
        private Board _board;

        public int Id
        {
            get;
            private set;
        }

        public Orientation Orientation
        {
            get;
            set;
        }

        public int Size
        {
            get;
            private set;
        }

        public Point Location
        {
            get;
            set;
        }

        public Point Postition
        {
            get
            {
                return _blockPosition;
            }
            set
            {
                _blockPosition = value;
                Location = new Point(
                    _blockPosition.X * Board.BOX_SQUARE_SIZE,
                    _blockPosition.Y * Board.BOX_SQUARE_SIZE
                );
                _board.RefreshMatrix();
            }
        }

        public Block(Board board, Orientation orientation, int size, Point position)
        {
            Id = ++_lastId;

            _board = board;
            Orientation = orientation;
            Size = size;
            Postition = position;
        }

        public bool ValidMove(Direction dir)
        {
            if (Orientation == Orientation.Horizontal)
            {
                if (dir == Direction.Right)
                    return Postition.X < _board.Width - Size &&
                        _board.Matrix[Postition.Y, Postition.X + Size] == 0;
                else if (dir == Direction.Left)
                    return Postition.X > 0 &&
                        _board.Matrix[Postition.Y, Postition.X - 1] == 0;
            }
            else
            {
                if (dir == Direction.Down)
                    return Postition.Y < _board.Height - Size &&
                        _board.Matrix[Postition.Y + Size, Postition.X] == 0;
                else if (dir == Direction.Up)
                    return Postition.Y > 0 &&
                        _board.Matrix[Postition.Y - 1, Postition.X] == 0;
            }

            return false;
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Right:
                    Postition = new Point(Postition.X + 1, Postition.Y);
                    break;
                case Direction.Left:
                    Postition = new Point(Postition.X - 1, Postition.Y);
                    break;
                case Direction.Down:
                    Postition = new Point(Postition.X, Postition.Y + 1);
                    break;
                case Direction.Up:
                    Postition = new Point(Postition.X, Postition.Y - 1);
                    break;
            }
            _board.PrintMatrix();
        }
    }
}
