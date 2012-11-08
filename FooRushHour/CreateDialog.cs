using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FooRushHour
{
    public partial class CreateDialog : Form
    {
        public Board Board { get; set; }

        public CreateDialog()
        {
            InitializeComponent();

            var whRange = new List<int>() { 4, 5, 6, 7, 8 };
            whRange.ForEach(i => { CBWidth.Items.Add(i); CBHeight.Items.Add(i); });
            CBWidth.SelectedIndex = 2;
            CBHeight.SelectedIndex = 2;

            for (int i = 1; i <= (int)CBHeight.SelectedItem; i++)
                CBFinishRow.Items.Add(i);
            CBFinishRow.SelectedIndex = 2;

            CBHeight.SelectedIndexChanged += new EventHandler((o, e) =>
            {
                CBFinishRow.Items.Clear();
                for (int i = 1; i <= (int)CBHeight.SelectedItem; i++)
                    CBFinishRow.Items.Add(i);
                CBFinishRow.SelectedIndex = 2;
            });
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            var width = (int)CBWidth.SelectedItem;
            var height = (int)CBHeight.SelectedItem;

            var redBlockPos = new Point(width - 2, (int)CBFinishRow.SelectedItem - 1);
            var blocks = new List<Block>();

            blocks.Add(new Block(null, Orientation.Horizontal, 2, redBlockPos, 1));

            Board = new Board(width, height, blocks, redBlockPos);
            Close();
        }
    }
}
