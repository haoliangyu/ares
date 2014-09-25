namespace RasterEditor.Forms
{
    partial class GoToForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoToForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rowIndexTextBox = new System.Windows.Forms.TextBox();
            this.colIndexTextBox = new System.Windows.Forms.TextBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.flashingToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addSelectionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteSelectionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Row:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Column:";
            // 
            // rowIndexTextBox
            // 
            this.rowIndexTextBox.Location = new System.Drawing.Point(38, 28);
            this.rowIndexTextBox.Name = "rowIndexTextBox";
            this.rowIndexTextBox.Size = new System.Drawing.Size(100, 20);
            this.rowIndexTextBox.TabIndex = 2;
            this.rowIndexTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colIndexTextBox
            // 
            this.colIndexTextBox.Location = new System.Drawing.Point(201, 28);
            this.colIndexTextBox.Name = "colIndexTextBox";
            this.colIndexTextBox.Size = new System.Drawing.Size(100, 20);
            this.colIndexTextBox.TabIndex = 3;
            this.colIndexTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolStrip
            // 
            this.toolStrip.CanOverflow = false;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flashingToolStripButton,
            this.toolStripSeparator1,
            this.addSelectionToolStripButton,
            this.deleteSelectionToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(308, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // flashingToolStripButton
            // 
            this.flashingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.flashingToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("flashingToolStripButton.Image")));
            this.flashingToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.flashingToolStripButton.Name = "flashingToolStripButton";
            this.flashingToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.flashingToolStripButton.Text = "toolStripButton1";
            this.flashingToolStripButton.Click += new System.EventHandler(this.flashingToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addSelectionToolStripButton
            // 
            this.addSelectionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addSelectionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("addSelectionToolStripButton.Image")));
            this.addSelectionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSelectionToolStripButton.Name = "addSelectionToolStripButton";
            this.addSelectionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.addSelectionToolStripButton.Text = "toolStripButton";
            this.addSelectionToolStripButton.Click += new System.EventHandler(this.addSelectionToolStripButton_Click);
            // 
            // deleteSelectionToolStripButton
            // 
            this.deleteSelectionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteSelectionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteSelectionToolStripButton.Image")));
            this.deleteSelectionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSelectionToolStripButton.Name = "deleteSelectionToolStripButton";
            this.deleteSelectionToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.deleteSelectionToolStripButton.Text = "toolStripButton1";
            this.deleteSelectionToolStripButton.Click += new System.EventHandler(this.deleteSelectionToolStripButton_Click);
            // 
            // GoToForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 57);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.colIndexTextBox);
            this.Controls.Add(this.rowIndexTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Go To Pixel";
            this.TopMost = true;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rowIndexTextBox;
        private System.Windows.Forms.TextBox colIndexTextBox;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton flashingToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton addSelectionToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteSelectionToolStripButton;
    }
}