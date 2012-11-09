using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FooRushHour
{
    public partial class ToolboxForm : Form
    {
        private MainForm _mainForm;

        public Orientation Orientation { get; set; }
        public int Length { get; set; }

        private Button _block;

        public ToolboxForm(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();

            _block = new Button();
            BlockPanel.Controls.Add(_block);

            CBOrientation.Items.Add("Horizontal");
            CBOrientation.Items.Add("Vertical");
            CBOrientation.SelectedIndex = 0;

            CBSize.Items.Add(2);
            CBSize.Items.Add(3);
            CBSize.SelectedIndex = 0;

            if (CBOrientation.SelectedIndex == 0)
                Orientation = Orientation.Horizontal;
            else
                Orientation = Orientation.Vertical;

            if (CBSize.SelectedIndex == 0)
                Length = 2;
            else
                Length = 3;

            _updateBlock();
        }

        private void _updateBlock()
        {
            _block.BackColor = Color.Gray;
            _block.Width = Board.BOX_SQUARE_SIZE - 20;
            _block.Height = Board.BOX_SQUARE_SIZE - 20;

            if (Orientation == Orientation.Horizontal)
                _block.Width *= Length;
            else
                _block.Height *= Length;
        }

        private void CBOrientation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBOrientation.SelectedIndex == 0)
                Orientation = Orientation.Horizontal;
            else
                Orientation = Orientation.Vertical;
            _updateBlock();
        }

        private void CBSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBSize.SelectedIndex == 0)
                Length = 2;
            else
                Length = 3;
            _updateBlock();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _mainForm.DoneEditing(this);
            Close();
        }

        private void ToolboxForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mainForm.DoneEditing(this);
        }
    }
}
