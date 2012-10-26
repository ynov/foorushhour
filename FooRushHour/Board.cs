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

        public List<Block> BlockList
        {
            get;
            private set;
        }

        public int[,] Matrix
        {
            get;
            set;
        }

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public Board(int width, int height, List<Block> blockList)
        {
            Width = width;
            Height = height;

            Matrix = new int[height, width];

            BlockList = new List<Block>();
            BlockList.AddRange(blockList);

            RefreshMatrix();
        }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            Matrix = new int[height, width];
            BlockList = new List<Block>();

            _addBlock(new Block(this, Orientation.Horizontal, 2, new Point(2, 2)));
            _addBlock(new Block(this, Orientation.Horizontal, 3, new Point(3, 0)));
            _addBlock(new Block(this, Orientation.Horizontal, 2, new Point(2, 1)));
            _addBlock(new Block(this, Orientation.Vertical, 3, new Point(0, 1)));
            _addBlock(new Block(this, Orientation.Vertical, 3, new Point(4, 1)));
            _addBlock(new Block(this, Orientation.Horizontal, 3, new Point(3, 4)));
            _addBlock(new Block(this, Orientation.Horizontal, 3, new Point(3, 5)));
            _addBlock(new Block(this, Orientation.Vertical, 2, new Point(2, 4)));

            RefreshMatrix();
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
            StringBuilder str = new StringBuilder();

            BlockList.ForEach(b =>
                str.Append(string.Format("[{0},{1},{2}]", b.Id, b.Postition.X, b.Postition.Y))
            );

            return str.ToString();
        }

        private void _addBlock(Block block)
        {
            BlockList.Add(block);
        }

        private void _addBlockToMatrix(Block block)
        {
            if (block.Orientation == Orientation.Horizontal)
                for (int i = 0; i < block.Size; i++)
                    Matrix[block.Postition.Y, block.Postition.X + i] = 8;
            else
                for (int i = 0; i < block.Size; i++)
                    Matrix[block.Postition.Y + i, block.Postition.X] = 8;
        }
    }
}
