namespace FooRushHour
{
    partial class ToolboxForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.BlockPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CBSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CBOrientation = new System.Windows.Forms.ComboBox();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MainPanel.Controls.Add(this.BlockPanel);
            this.MainPanel.Controls.Add(this.CloseButton);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.CBSize);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.CBOrientation);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(194, 361);
            this.MainPanel.TabIndex = 0;
            // 
            // BlockPanel
            // 
            this.BlockPanel.Location = new System.Drawing.Point(4, 84);
            this.BlockPanel.Name = "BlockPanel";
            this.BlockPanel.Size = new System.Drawing.Size(190, 190);
            this.BlockPanel.TabIndex = 5;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(4, 280);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(113, 29);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close and Save";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size (Length):";
            // 
            // CBSize
            // 
            this.CBSize.FormattingEnabled = true;
            this.CBSize.Location = new System.Drawing.Point(4, 57);
            this.CBSize.Name = "CBSize";
            this.CBSize.Size = new System.Drawing.Size(69, 21);
            this.CBSize.TabIndex = 2;
            this.CBSize.SelectedIndexChanged += new System.EventHandler(this.CBSize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Orientation:";
            // 
            // CBOrientation
            // 
            this.CBOrientation.FormattingEnabled = true;
            this.CBOrientation.Location = new System.Drawing.Point(4, 16);
            this.CBOrientation.Name = "CBOrientation";
            this.CBOrientation.Size = new System.Drawing.Size(113, 21);
            this.CBOrientation.TabIndex = 0;
            this.CBOrientation.SelectedIndexChanged += new System.EventHandler(this.CBOrientation_SelectedIndexChanged);
            // 
            // ToolboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 361);
            this.Controls.Add(this.MainPanel);
            this.Name = "ToolboxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToolboxForm_FormClosed);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBOrientation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBSize;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel BlockPanel;
    }
}
