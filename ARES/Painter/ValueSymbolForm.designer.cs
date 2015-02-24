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
            this.colorRampComboBox = new System.Windows.Forms.ComboBox();
            this.ValueListBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.addValueButton = new System.Windows.Forms.Button();
            this.deleteValueButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.splitButton1 = new ARES.Controls.SplitButton();
            this.SuspendLayout();
            // 
            // colorRampComboBox
            // 
            this.colorRampComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorRampComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorRampComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorRampComboBox.FormattingEnabled = true;
            this.colorRampComboBox.Location = new System.Drawing.Point(5, 359);
            this.colorRampComboBox.Name = "colorRampComboBox";
            this.colorRampComboBox.Size = new System.Drawing.Size(243, 22);
            this.colorRampComboBox.TabIndex = 1;
            this.colorRampComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ValueListBox
            // 
            this.ValueListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ValueListBox.FullRowSelect = true;
            this.ValueListBox.Location = new System.Drawing.Point(5, 33);
            this.ValueListBox.MultiSelect = false;
            this.ValueListBox.Name = "ValueListBox";
            this.ValueListBox.Size = new System.Drawing.Size(243, 268);
            this.ValueListBox.TabIndex = 8;
            this.ValueListBox.UseCompatibleStateImageBehavior = false;
            this.ValueListBox.View = System.Windows.Forms.View.Details;
            this.ValueListBox.Click += new System.EventHandler(this.Color_List_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Value";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Color";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 110;
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
            this.layerNameTextBox.Size = new System.Drawing.Size(104, 21);
            this.layerNameTextBox.TabIndex = 10;
            // 
            // addValueButton
            // 
            this.addValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addValueButton.Location = new System.Drawing.Point(13, 307);
            this.addValueButton.Name = "addValueButton";
            this.addValueButton.Size = new System.Drawing.Size(110, 23);
            this.addValueButton.TabIndex = 11;
            this.addValueButton.Text = "Add Value(s)";
            this.addValueButton.UseVisualStyleBackColor = true;
            this.addValueButton.Click += new System.EventHandler(this.addValueButton_Click);
            // 
            // deleteValueButton
            // 
            this.deleteValueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteValueButton.Location = new System.Drawing.Point(129, 307);
            this.deleteValueButton.Name = "deleteValueButton";
            this.deleteValueButton.Size = new System.Drawing.Size(110, 23);
            this.deleteValueButton.TabIndex = 12;
            this.deleteValueButton.Text = "Delete Value(s)";
            this.deleteValueButton.UseVisualStyleBackColor = true;
            this.deleteValueButton.Click += new System.EventHandler(this.deleteValueButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "Color Ramp";
            // 
            // splitButton1
            // 
            this.splitButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.splitButton1.AutoSize = true;
            this.splitButton1.Location = new System.Drawing.Point(172, 6);
            this.splitButton1.Name = "splitButton1";
            this.splitButton1.Size = new System.Drawing.Size(79, 23);
            this.splitButton1.TabIndex = 13;
            this.splitButton1.Text = "Options";
            this.splitButton1.UseVisualStyleBackColor = true;
            // 
            // ValueSymbolForm
            // 
            this.AllowDrop = true;
            this.AutoSize = true;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitButton1);
            this.Controls.Add(this.deleteValueButton);
            this.Controls.Add(this.addValueButton);
            this.Controls.Add(this.layerNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ValueListBox);
            this.Controls.Add(this.colorRampComboBox);
            this.Name = "ValueSymbolForm";
            this.Size = new System.Drawing.Size(254, 389);
            this.Load += new System.EventHandler(this.Color_TOC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox colorRampComboBox;
        private System.Windows.Forms.ListView ValueListBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private System.Windows.Forms.Button addValueButton;
        private System.Windows.Forms.Button deleteValueButton;
        private Controls.SplitButton splitButton1;
        private System.Windows.Forms.Label label2;

    }
}
