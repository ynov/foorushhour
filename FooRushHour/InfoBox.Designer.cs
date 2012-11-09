namespace FooRushHour
{
    partial class InfoBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.LabelMvCount = new System.Windows.Forms.Label();
            this.LabelTimer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelGoal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Movement Count: ";
            // 
            // LabelMvCount
            // 
            this.LabelMvCount.AutoSize = true;
            this.LabelMvCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMvCount.Location = new System.Drawing.Point(103, 4);
            this.LabelMvCount.Name = "LabelMvCount";
            this.LabelMvCount.Size = new System.Drawing.Size(23, 13);
            this.LabelMvCount.TabIndex = 1;
            this.LabelMvCount.Text = "XX";
            // 
            // LabelTimer
            // 
            this.LabelTimer.AutoSize = true;
            this.LabelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTimer.Location = new System.Drawing.Point(42, 20);
            this.LabelTimer.Name = "LabelTimer";
            this.LabelTimer.Size = new System.Drawing.Size(23, 13);
            this.LabelTimer.TabIndex = 3;
            this.LabelTimer.Text = "XX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Timer: ";
            // 
            // LabelGoal
            // 
            this.LabelGoal.AutoSize = true;
            this.LabelGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelGoal.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.LabelGoal.Location = new System.Drawing.Point(3, 46);
            this.LabelGoal.Name = "LabelGoal";
            this.LabelGoal.Size = new System.Drawing.Size(11, 13);
            this.LabelGoal.TabIndex = 4;
            this.LabelGoal.Text = "-";
            // 
            // InfoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelGoal);
            this.Controls.Add(this.LabelTimer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LabelMvCount);
            this.Controls.Add(this.label1);
            this.Name = "InfoBox";
            this.Size = new System.Drawing.Size(127, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelMvCount;
        private System.Windows.Forms.Label LabelTimer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelGoal;
    }
}
