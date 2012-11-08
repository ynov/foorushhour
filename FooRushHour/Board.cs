using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FooRushHour
{
    public class Board
    {
        public const int BOX_SQUARE_SIZE = 80;
        private Block _blockOne = null;

        public List<Block> BlockList { get; private set;}
        public int[,] Matrix { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Point EndPoint { get; private set; }
        public bool UserMovementLock { get; set; }

        public Board(int width, int height, List<Block> blockList, Point endPoint)
        {
            Width = width;
            Height = height;

            UserMovementLock = false;

            Matrix = new int[height, width];

            Block.ResetId();
            BlockList = new List<Block>();
            blockList.ForEach(b => _addBlock(b));

            EndPoint = endPoint;

            RefreshMatrix();
        }

        public bool GoalReached()
        {
            return (_blockOne.Postition == EndPoint);
        }

        public static Board TestBoard()
        {
            var width = 6;
            var height = 6;
            var endPoint = new Point(4, 2);

            var blocks = new List<Block>() {
                new Block(null, Orientation.Horizontal, 2, new Point(2, 2), 1),
                new Block(null, Orientation.Horizontal, 3, new Point(3, 0)),
                new Block(null, Orientation.Horizontal, 2, new Point(2, 1)),
                new Block(null, Orientation.Vertical, 3, new Point(0, 1)),
                new Block(null, Orientation.Vertical, 3, new Point(4, 1)),
                new Block(null, Orientation.Horizontal, 3, new Point(3, 4)),
                new Block(null, Orientation.Horizontal, 3, new Point(3, 5)),
                new Block(null, Orientation.Vertical, 2, new Point(2, 4)),
            };

            var board = new Board(width, height, blocks, endPoint);
            return board;
        }

        public void RefreshMatrix()
        {
            for (int y = 0; y < Width; y++)
                for (int x = 0; x < Height; x++)
                    Matrix[y, x] = 0;

            BlockList.ForEach(b => _addBlockToMatrix(b));
        }

        public void PrintMatrix()
        {
            Console.WriteLine("BOARD: {0}", ToString());
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                    Console.Write("{0,2}  ", Matrix[y, x]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            BlockList.ForEach(b =>
                str.Append(string.Format("{0}{1}{2}", b.Id, b.Postition.X, b.Postition.Y))
            );

            return str.ToString();
        }

        public string ToText()
        {
            var str = new StringBuilder();

            str.Append(string.Format("SIZE: ({0}, {1})\n", Width, Height));
            str.Append(string.Format("END: ({0}, {1})\n", EndPoint.X, EndPoint.Y)); 
            str.Append("BLOCKS: {\n");

            // BlockList Format:
            // [<id>,<orientation>,<type>,<size>|<x>,<y>]
            BlockList.ForEach(b =>
                str.Append(string.Format("  [{0},{1},{2},{3}|{4},{5}]\n", b.Id, b.Orientation.ToString(), b.Type, b.Size, b.Postition.X, b.Postition.Y))
            );

            str.Append("}\n");
            return str.ToString();
        }

        private void _addBlock(Block block)
        {
            var b = new Block(this, block.Orientation, block.Size, block.Postition, block.Type);
            BlockList.Add(b);
            if (b.Type == 1)
                _blockOne = b;
        }

        private void _addBlockToMatrix(Block block)
        {
            if (block.Orientation == Orientation.Horizontal)
                for (int i = 0; i < block.Size; i++)
                    Matrix[block.Postition.Y, block.Postition.X + i] = block.Id;
            else
                for (int i = 0; i < block.Size; i++)
                    Matrix[block.Postition.Y + i, block.Postition.X] = block.Id;
        }
    }
}
