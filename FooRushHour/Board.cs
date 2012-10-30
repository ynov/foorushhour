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

        public Point EndPoint
        {
            get;
            private set;
        }

        public Board(int width, int height, List<Block> blockList, Point endPoint)
        {
            Width = width;
            Height = height;

            Matrix = new int[height, width];

            BlockList = new List<Block>();
            blockList.ForEach(b => _addBlock(b));

            EndPoint = endPoint;

            RefreshMatrix();
        }

        public static Board TestBoard()
        {
            int width = 6;
            int height = 6;
            Point endPoint = new Point(4, 2);

            List<Block> blocks = new List<Block>() {
                new Block(null, Orientation.Horizontal, 2, new Point(2, 2), 1),
                // new Block(null, Orientation.Horizontal, 3, new Point(3, 0)),
                new Block(null, Orientation.Horizontal, 2, new Point(2, 1)),
                new Block(null, Orientation.Vertical, 2, new Point(4, 1)),
                // new Block(null, Orientation.Vertical, 3, new Point(0, 1)),
                // new Block(null, Orientation.Vertical, 3, new Point(4, 1)),
                // new Block(null, Orientation.Horizontal, 3, new Point(3, 4)),
                // new Block(null, Orientation.Horizontal, 3, new Point(3, 5)),
                // new Block(null, Orientation.Vertical, 2, new Point(2, 4)),
            };

            Board board = new Board(width, height, blocks, endPoint);
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
            StringBuilder str = new StringBuilder();

            BlockList.ForEach(b =>
                str.Append(string.Format("[{0},{1},{2}]", b.Id, b.Postition.X, b.Postition.Y))
            );

            return str.ToString();
        }

        public string ToText()
        {
            StringBuilder str = new StringBuilder();

            str.Append(string.Format("SIZE: ({0}, {1})\n", Width, Height));
            str.Append(string.Format("END: ({0}, {1})\n", EndPoint.X, EndPoint.Y)); 
            str.Append("BLOCKS: {\n");

            // [<id>,<orientation>,<type>,<size>|<x>,<y>]
            BlockList.ForEach(b =>
                str.Append(string.Format("  [{0},{1},{2},{3}|{4},{5}]\n", b.Id, b.Orientation.ToString(), b.Type, b.Size, b.Postition.X, b.Postition.Y))
            );

            str.Append("}\n");
            return str.ToString();
        }

        private void _addBlock(Block block)
        {
            block.Board = this;
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

            if (block.Type == 1 && block.Postition == EndPoint)
            {
                MainForm.Instance.Close();
                Console.WriteLine("====\n...\nYou win!\n...\n====\n");
                // Console.ReadLine();
            }
        }
    }
}
