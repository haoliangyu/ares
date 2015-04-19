namespace ARES
{
    partial class ValueSymbolForm
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
            this.valueListBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.addValueButton = new System.Windows.Forms.Button();
            this.deleteValueButton = new System.Windows.Forms.Button();
            this.valueBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionSplitButton = new ARES.Controls.SplitButton();
            this.optionsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueBoxContextMenuStrip.SuspendLayout();
            this.optionsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // valueListBox
            // 
            this.valueListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.valueListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.valueListBox.Location = new System.Drawing.Point(3, 32);
            this.valueListBox.MultiSelect = false;
            this.valueListBox.Name = "valueListBox";
            this.valueListBox.Size = new System.Drawing.Size(192, 297);
            this.valueListBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.valueListBox.TabIndex = 8;
            this.valueListBox.UseCompatibleStateImageBehavior = false;
            this.valueListBox.View = System.Windows.Forms.View.Details;
            this.valueListBox.SelectedIndexChanged += new System.EventHandler(this.ValueListBox_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Value";
            this.columnHeader1.Width = 51;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Color";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 46;
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "Painting";
            // 
            // layerNameTextBox
            // 
            this.layerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.layerNameTextBox.Location = new System.Drawing.Point(62, 6);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.ReadOnly = true;
            this.layerNameTextBox.Size = new System.Drawing.Size(133, 21);
            this.layerNameTextBox.TabIndex = 10;
            // 
            // addValueButton
            // 
            this.addValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addValueButton.Location = new System.Drawing.Point(3, 335);
            this.addValueButton.Name = "addValueButton";
            this.addValueButton.Size = new System.Drawing.Size(90, 23);
            this.addValueButton.TabIndex = 11;
            this.addValueButton.Text = "Add Values";
            this.addValueButton.UseVisualStyleBackColor = true;
            this.addValueButton.Click += new System.EventHandler(this.addValueButton_Click);
            // 
            // deleteValueButton
            // 
            this.deleteValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteValueButton.Location = new System.Drawing.Point(99, 335);
            this.deleteValueButton.Name = "deleteValueButton";
            this.deleteValueButton.Size = new System.Drawing.Size(90, 23);
            this.deleteValueButton.TabIndex = 12;
            this.deleteValueButton.Text = "Delete Values";
            this.deleteValueButton.UseVisualStyleBackColor = true;
            this.deleteValueButton.Click += new System.EventHandler(this.deleteValueButton_Click);
            // 
            // valueBoxContextMenuStrip
            // 
            this.valueBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeColorToolStripMenuItem});
            this.valueBoxContextMenuStrip.Name = "valueBoxContextMenuStrip";
            this.valueBoxContextMenuStrip.Size = new System.Drawing.Size(161, 26);
            // 
            // changeColorToolStripMenuItem
            // 
            this.changeColorToolStripMenuItem.Name = "changeColorToolStripMenuItem";
            this.changeColorToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.changeColorToolStripMenuItem.Text = "Change Color...";
            this.changeColorToolStripMenuItem.Click += new System.EventHandler(this.changeColorToolStripMenuItem_Click_1);
            // 
            // optionSplitButton
            // 
            this.optionSplitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.optionSplitButton.AutoSize = true;
            this.optionSplitButton.ContextMenuStrip = this.optionsContextMenuStrip;
            this.optionSplitButton.Location = new System.Drawing.Point(3, 364);
            this.optionSplitButton.Name = "optionSplitButton";
            this.optionSplitButton.Size = new System.Drawing.Size(90, 22);
            this.optionSplitButton.TabIndex = 13;
            this.optionSplitButton.Text = "Options";
            this.optionSplitButton.UseVisualStyleBackColor = true;
            // 
            // optionsContextMenuStrip
            // 
            this.optionsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewValueToolStripMenuItem});
            this.optionsContextMenuStrip.Name = "optionsContextMenuStrip";
            this.optionsContextMenuStrip.Size = new System.Drawing.Size(167, 48);
            // 
            // addNewValueToolStripMenuItem
            // 
            this.addNewValueToolStripMenuItem.Name = "addNewValueToolStripMenuItem";
            this.addNewValueToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addNewValueToolStripMenuItem.Text = "Add New Value...";
            this.addNewValueToolStripMenuItem.Click += new System.EventHandler(this.addNewValueToolStripMenuItem_Click);
            // 
            // ValueSymbolForm
            // 
            this.AllowDrop = true;
            this.AutoSize = true;
            this.Controls.Add(this.optionSplitButton);
            this.Controls.Add(this.addValueButton);
            this.Controls.Add(this.deleteValueButton);
            this.Controls.Add(this.layerNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valueListBox);
            this.Name = "ValueSymbolForm";
            this.Size = new System.Drawing.Size(200, 389);
            this.valueBoxContextMenuStrip.ResumeLayout(false);
            this.optionsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView valueListBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private System.Windows.Forms.Button addValueButton;
        private System.Windows.Forms.Button deleteValueButton;
        private System.Windows.Forms.ContextMenuStrip valueBoxContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem changeColorToolStripMenuItem;
        private Controls.SplitButton optionSplitButton;
        private System.Windows.Forms.ContextMenuStrip optionsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addNewValueToolStripMenuItem;

    }
}
