namespace ARES
{
    partial class EditForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.optionsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllEditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.editionStatusStrip = new System.Windows.Forms.StatusStrip();
            this.editionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.noDataValueLabel = new System.Windows.Forms.Label();
            this.rasterGridView = new ARES.Control.RasterGridView();
            this.optionButton = new ARES.Control.SplitButton();
            this.editSelectedPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSelectedEditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuStrip.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.editionStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Editing:";
            // 
            // layerNameTextBox
            // 
            this.layerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layerNameTextBox.Location = new System.Drawing.Point(60, 6);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.ReadOnly = true;
            this.layerNameTextBox.Size = new System.Drawing.Size(149, 21);
            this.layerNameTextBox.TabIndex = 1;
            // 
            // optionsMenuStrip
            // 
            this.optionsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSelectedPixelsToolStripMenuItem,
            this.editAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearSelectedEditsToolStripMenuItem,
            this.clearAllEditsToolStripMenuItem});
            this.optionsMenuStrip.Name = "optionsMenuStrip";
            this.optionsMenuStrip.Size = new System.Drawing.Size(209, 120);
            // 
            // editAllToolStripMenuItem
            // 
            this.editAllToolStripMenuItem.Name = "editAllToolStripMenuItem";
            this.editAllToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.editAllToolStripMenuItem.Text = "Edit All Pixels...";
            this.editAllToolStripMenuItem.Click += new System.EventHandler(this.editAllToolStripMenuItem_Click);
            // 
            // clearAllEditsToolStripMenuItem
            // 
            this.clearAllEditsToolStripMenuItem.Name = "clearAllEditsToolStripMenuItem";
            this.clearAllEditsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.clearAllEditsToolStripMenuItem.Text = "Clear All Edits";
            this.clearAllEditsToolStripMenuItem.Click += new System.EventHandler(this.clearAllEditsToolStripMenuItem_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.rasterGridView);
            this.groupBox.Location = new System.Drawing.Point(8, 33);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(284, 223);
            this.groupBox.TabIndex = 3;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Region of Interest";
            // 
            // editionStatusStrip
            // 
            this.editionStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editionToolStripStatusLabel});
            this.editionStatusStrip.Location = new System.Drawing.Point(0, 278);
            this.editionStatusStrip.Name = "editionStatusStrip";
            this.editionStatusStrip.Size = new System.Drawing.Size(300, 22);
            this.editionStatusStrip.TabIndex = 4;
            this.editionStatusStrip.Text = "statusStrip1";
            // 
            // editionToolStripStatusLabel
            // 
            this.editionToolStripStatusLabel.Name = "editionToolStripStatusLabel";
            this.editionToolStripStatusLabel.Size = new System.Drawing.Size(17, 17);
            this.editionToolStripStatusLabel.Text = "12";
            // 
            // noDataValueLabel
            // 
            this.noDataValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noDataValueLabel.AutoSize = true;
            this.noDataValueLabel.Location = new System.Drawing.Point(9, 259);
            this.noDataValueLabel.Name = "noDataValueLabel";
            this.noDataValueLabel.Size = new System.Drawing.Size(83, 12);
            this.noDataValueLabel.TabIndex = 5;
            this.noDataValueLabel.Text = "NoData Value:";
            // 
            // rasterGridView
            // 
            this.rasterGridView.AllowUserToAddRows = false;
            this.rasterGridView.AllowUserToDeleteRows = false;
            this.rasterGridView.AllowUserToResizeColumns = false;
            this.rasterGridView.AllowUserToResizeRows = false;
            this.rasterGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.rasterGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.rasterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rasterGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rasterGridView.Editable = true;
            this.rasterGridView.Location = new System.Drawing.Point(3, 17);
            this.rasterGridView.Name = "rasterGridView";
            this.rasterGridView.NoDataValue = 0D;
            this.rasterGridView.RowTemplate.Height = 23;
            this.rasterGridView.Size = new System.Drawing.Size(278, 203);
            this.rasterGridView.TabIndex = 0;
            // 
            // optionButton
            // 
            this.optionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionButton.AutoSize = true;
            this.optionButton.ContextMenuStrip = this.optionsMenuStrip;
            this.optionButton.Location = new System.Drawing.Point(216, 6);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(76, 23);
            this.optionButton.TabIndex = 2;
            this.optionButton.Text = "Options";
            this.optionButton.UseVisualStyleBackColor = true;
            // 
            // editSelectedPixelsToolStripMenuItem
            // 
            this.editSelectedPixelsToolStripMenuItem.Name = "editSelectedPixelsToolStripMenuItem";
            this.editSelectedPixelsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.editSelectedPixelsToolStripMenuItem.Text = "Edit Selected Pixels...";
            this.editSelectedPixelsToolStripMenuItem.Click += new System.EventHandler(this.editSelectedPixelsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // clearSelectedEditsToolStripMenuItem
            // 
            this.clearSelectedEditsToolStripMenuItem.Name = "clearSelectedEditsToolStripMenuItem";
            this.clearSelectedEditsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.clearSelectedEditsToolStripMenuItem.Text = "Clear Selected Edits";
            this.clearSelectedEditsToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedEditsToolStripMenuItem_Click);
            // 
            // EditForm
            // 
            this.Controls.Add(this.noDataValueLabel);
            this.Controls.Add(this.editionStatusStrip);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.optionButton);
            this.Controls.Add(this.layerNameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "EditForm";
            this.Size = new System.Drawing.Size(300, 300);
            this.optionsMenuStrip.ResumeLayout(false);
            this.groupBox.ResumeLayout(false);
            this.editionStatusStrip.ResumeLayout(false);
            this.editionStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private Control.SplitButton optionButton;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.StatusStrip editionStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel editionToolStripStatusLabel;
        private System.Windows.Forms.Label noDataValueLabel;
        private System.Windows.Forms.ContextMenuStrip optionsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllEditsToolStripMenuItem;
        private Control.RasterGridView rasterGridView;
        private System.Windows.Forms.ToolStripMenuItem editSelectedPixelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedEditsToolStripMenuItem;

    }
}
