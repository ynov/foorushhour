using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace FooRushHour
{
    public class BoardIO
    {
        public static Board ReadFile(string filename)
        {
            var reader = new StreamReader(filename);
            string line = null;

            var sizePtrn = @"SIZE: (\d), (\d)";
            var endPtrn = @"END: (\d), (\d)";
            var blockCountPtrn = @"BLOCKS: (\d)";
            var blockPtrn = @"(\d),([a-zA-Z]+),(\d),(\d)\|(\d),(\d)";

            Size boardSize = new Size();
            Point boardEndPoint = new Point();
            int boardBlockCount = 0;
            var blocks = new List<Block>();

            int lineNum = 1;
            int headerLine = 0;
            int blockLine = 0;

            do
            {
                line = reader.ReadLine();
                if (line == null)
                    break;

                if (headerLine < 3)
                {
                    if (Regex.IsMatch(line, sizePtrn))
                    {
                        var groups = new Regex(sizePtrn).Match(line).Groups;
                        boardSize = new Size(int.Parse(groups[1].Value), int.Parse(groups[2].Value));
                        headerLine++;
                    }

                    if (Regex.IsMatch(line, endPtrn))
                    {
                        var groups = new Regex(endPtrn).Match(line).Groups;
                        boardEndPoint = new Point(int.Parse(groups[1].Value), int.Parse(groups[2].Value));
                        headerLine++;
                    }

                    if (Regex.IsMatch(line, blockCountPtrn))
                    {
                        var groups = new Regex(blockCountPtrn).Match(line).Groups;
                        boardBlockCount = int.Parse(groups[1].Value);
                        headerLine++;
                    }
                }

                // Read the blocks
                else if (blockLine < boardBlockCount)
                {
                    var groups = new Regex(blockPtrn).Match(line).Groups;

                    Orientation orientation = (groups[2].Value == "Horizontal") ? Orientation.Horizontal : Orientation.Vertical;
                    
                    var block = new Block(
                        null,
                        orientation, int.Parse(groups[4].Value),
                        new Point(int.Parse(groups[5].Value), int.Parse(groups[6].Value)),
                        int.Parse(groups[3].Value)
                    );

                    blocks.Add(block);
                    blockLine++;
                }

                lineNum++;
            } while (line != null);

            reader.Close();
            return new Board(boardSize.Width, boardSize.Height, blocks, boardEndPoint);
        }

        public static void WriteFile(Board board, string filename)
        {
            var writer = new StreamWriter(filename);
            string boardText = board.ToText();
            writer.Write(boardText);
            writer.Close();
        }
    }
}
