namespace RasterEditor.Forms
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.rasterGridView = new RasterEditor.Controls.RasterGridView();
            this.noDataValueLabel = new System.Windows.Forms.Label();
            this.editionStatusStrip = new System.Windows.Forms.StatusStrip();
            this.editionToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.optionsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSelectedPixelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSelectedEditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllEditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionButton = new RasterEditor.Controls.SplitButton();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).BeginInit();
            this.editionStatusStrip.SuspendLayout();
            this.optionsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Editing:";
            // 
            // layerNameTextBox
            // 
            this.layerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layerNameTextBox.Location = new System.Drawing.Point(55, 9);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.ReadOnly = true;
            this.layerNameTextBox.Size = new System.Drawing.Size(159, 20);
            this.layerNameTextBox.TabIndex = 1;
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.rasterGridView);
            this.groupBox.Location = new System.Drawing.Point(10, 35);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(296, 219);
            this.groupBox.TabIndex = 3;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Region of Interest";
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
            this.rasterGridView.Location = new System.Drawing.Point(3, 16);
            this.rasterGridView.Name = "rasterGridView";
            this.rasterGridView.NoDataValue = 0D;
            this.rasterGridView.Size = new System.Drawing.Size(290, 200);
            this.rasterGridView.TabIndex = 0;
            // 
            // noDataValueLabel
            // 
            this.noDataValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.noDataValueLabel.AutoSize = true;
            this.noDataValueLabel.Location = new System.Drawing.Point(10, 257);
            this.noDataValueLabel.Name = "noDataValueLabel";
            this.noDataValueLabel.Size = new System.Drawing.Size(77, 13);
            this.noDataValueLabel.TabIndex = 1;
            this.noDataValueLabel.Text = "NoData Value:";
            // 
            // editionStatusStrip
            // 
            this.editionStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editionToolStripStatusLabel});
            this.editionStatusStrip.Location = new System.Drawing.Point(0, 278);
            this.editionStatusStrip.Name = "editionStatusStrip";
            this.editionStatusStrip.Size = new System.Drawing.Size(309, 22);
            this.editionStatusStrip.TabIndex = 4;
            // 
            // editionToolStripStatusLabel
            // 
            this.editionToolStripStatusLabel.Name = "editionToolStripStatusLabel";
            this.editionToolStripStatusLabel.Size = new System.Drawing.Size(19, 17);
            this.editionToolStripStatusLabel.Text = "12";
            this.editionToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optionsMenuStrip
            // 
            this.optionsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSelectedPixelsToolStripMenuItem,
            this.editAllToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearSelectedEditsToolStripMenuItem,
            this.clearAllEditsToolStripMenuItem});
            this.optionsMenuStrip.Name = "optionsMenuStrip";
            this.optionsMenuStrip.Size = new System.Drawing.Size(177, 142);
            // 
            // editSelectedPixelsToolStripMenuItem
            // 
            this.editSelectedPixelsToolStripMenuItem.Name = "editSelectedPixelsToolStripMenuItem";
            this.editSelectedPixelsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.editSelectedPixelsToolStripMenuItem.Text = "Edit Selections...";
            this.editSelectedPixelsToolStripMenuItem.Click += new System.EventHandler(this.editSelectedPixelsToolStripMenuItem_Click);
            // 
            // editAllToolStripMenuItem
            // 
            this.editAllToolStripMenuItem.Name = "editAllToolStripMenuItem";
            this.editAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.editAllToolStripMenuItem.Text = "Edit All...";
            this.editAllToolStripMenuItem.Click += new System.EventHandler(this.editAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // clearSelectedEditsToolStripMenuItem
            // 
            this.clearSelectedEditsToolStripMenuItem.Name = "clearSelectedEditsToolStripMenuItem";
            this.clearSelectedEditsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.clearSelectedEditsToolStripMenuItem.Text = "Clear Selected Edits";
            this.clearSelectedEditsToolStripMenuItem.Click += new System.EventHandler(this.clearSelectedEditsToolStripMenuItem_Click);
            // 
            // clearAllEditsToolStripMenuItem
            // 
            this.clearAllEditsToolStripMenuItem.Name = "clearAllEditsToolStripMenuItem";
            this.clearAllEditsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.clearAllEditsToolStripMenuItem.Text = "Clear All Edits";
            this.clearAllEditsToolStripMenuItem.Click += new System.EventHandler(this.clearAllEditsToolStripMenuItem_Click);
            // 
            // optionButton
            // 
            this.optionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optionButton.AutoSize = true;
            this.optionButton.ContextMenuStrip = this.optionsMenuStrip;
            this.optionButton.Location = new System.Drawing.Point(220, 7);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(83, 23);
            this.optionButton.TabIndex = 5;
            this.optionButton.Text = "Options";
            this.optionButton.UseVisualStyleBackColor = true;
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.replaceToolStripMenuItem.Text = "Replace...";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // EditForm
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.noDataValueLabel);
            this.Controls.Add(this.optionButton);
            this.Controls.Add(this.editionStatusStrip);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.layerNameTextBox);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "EditForm";
            this.Size = new System.Drawing.Size(309, 300);
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).EndInit();
            this.editionStatusStrip.ResumeLayout(false);
            this.editionStatusStrip.PerformLayout();
            this.optionsMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.StatusStrip editionStatusStrip;
        private Controls.RasterGridView rasterGridView;
        private System.Windows.Forms.ToolStripStatusLabel editionToolStripStatusLabel;
        private Controls.SplitButton optionButton;
        private System.Windows.Forms.ContextMenuStrip optionsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllEditsToolStripMenuItem;
        private System.Windows.Forms.Label noDataValueLabel;
        private System.Windows.Forms.ToolStripMenuItem editSelectedPixelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedEditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;

    }
}
