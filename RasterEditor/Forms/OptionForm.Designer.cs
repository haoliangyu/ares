namespace RasterEditor.Forms
{
    partial class OptionForm
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
            this.optionsTabControl = new System.Windows.Forms.TabControl();
            this.symbolTabPage = new System.Windows.Forms.TabPage();
            this.editGroupBox = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.editCustomCheckBox = new System.Windows.Forms.CheckBox();
            this.editTranNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.editOutlineColorPaletteButton = new RasterEditor.Controls.ColorPaletteButton();
            this.editFillColorColorPaletteButton = new RasterEditor.Controls.ColorPaletteButton();
            this.label6 = new System.Windows.Forms.Label();
            this.editOutlineWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.selectionGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.selTranNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.selOutlineColorPaletteButton = new RasterEditor.Controls.ColorPaletteButton();
            this.selFillColorPaletteButton = new RasterEditor.Controls.ColorPaletteButton();
            this.selOutlineWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.defaultButton = new System.Windows.Forms.Button();
            this.optionsTabControl.SuspendLayout();
            this.symbolTabPage.SuspendLayout();
            this.editGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editTranNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editOutlineWidthNumericUpDown)).BeginInit();
            this.selectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selTranNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selOutlineWidthNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // optionsTabControl
            // 
            this.optionsTabControl.Controls.Add(this.symbolTabPage);
            this.optionsTabControl.Location = new System.Drawing.Point(12, 12);
            this.optionsTabControl.Name = "optionsTabControl";
            this.optionsTabControl.SelectedIndex = 0;
            this.optionsTabControl.Size = new System.Drawing.Size(394, 233);
            this.optionsTabControl.TabIndex = 0;
            // 
            // symbolTabPage
            // 
            this.symbolTabPage.Controls.Add(this.editGroupBox);
            this.symbolTabPage.Controls.Add(this.selectionGroupBox);
            this.symbolTabPage.Location = new System.Drawing.Point(4, 22);
            this.symbolTabPage.Name = "symbolTabPage";
            this.symbolTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.symbolTabPage.Size = new System.Drawing.Size(386, 207);
            this.symbolTabPage.TabIndex = 0;
            this.symbolTabPage.Text = "Symbol";
            this.symbolTabPage.UseVisualStyleBackColor = true;
            // 
            // editGroupBox
            // 
            this.editGroupBox.Controls.Add(this.label10);
            this.editGroupBox.Controls.Add(this.editCustomCheckBox);
            this.editGroupBox.Controls.Add(this.editTranNumericUpDown);
            this.editGroupBox.Controls.Add(this.label9);
            this.editGroupBox.Controls.Add(this.editOutlineColorPaletteButton);
            this.editGroupBox.Controls.Add(this.editFillColorColorPaletteButton);
            this.editGroupBox.Controls.Add(this.label6);
            this.editGroupBox.Controls.Add(this.editOutlineWidthNumericUpDown);
            this.editGroupBox.Controls.Add(this.label5);
            this.editGroupBox.Controls.Add(this.label4);
            this.editGroupBox.Location = new System.Drawing.Point(6, 94);
            this.editGroupBox.Name = "editGroupBox";
            this.editGroupBox.Size = new System.Drawing.Size(374, 110);
            this.editGroupBox.TabIndex = 1;
            this.editGroupBox.TabStop = false;
            this.editGroupBox.Text = "Edit";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(337, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "%";
            // 
            // editCustomCheckBox
            // 
            this.editCustomCheckBox.AutoSize = true;
            this.editCustomCheckBox.Location = new System.Drawing.Point(10, 19);
            this.editCustomCheckBox.Name = "editCustomCheckBox";
            this.editCustomCheckBox.Size = new System.Drawing.Size(148, 17);
            this.editCustomCheckBox.TabIndex = 13;
            this.editCustomCheckBox.Text = "Uses ArcMap layer render";
            this.editCustomCheckBox.UseVisualStyleBackColor = true;
            this.editCustomCheckBox.CheckedChanged += new System.EventHandler(this.editCustomCheckBox_CheckedChanged);
            // 
            // editTranNumericUpDown
            // 
            this.editTranNumericUpDown.Location = new System.Drawing.Point(262, 48);
            this.editTranNumericUpDown.Name = "editTranNumericUpDown";
            this.editTranNumericUpDown.Size = new System.Drawing.Size(68, 20);
            this.editTranNumericUpDown.TabIndex = 10;
            this.editTranNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Transparency";
            // 
            // editOutlineColorPaletteButton
            // 
            this.editOutlineColorPaletteButton.AutoSize = true;
            this.editOutlineColorPaletteButton.Location = new System.Drawing.Point(83, 76);
            this.editOutlineColorPaletteButton.Name = "editOutlineColorPaletteButton";
            this.editOutlineColorPaletteButton.Size = new System.Drawing.Size(39, 27);
            this.editOutlineColorPaletteButton.TabIndex = 12;
            this.editOutlineColorPaletteButton.UseVisualStyleBackColor = true;
            // 
            // editFillColorColorPaletteButton
            // 
            this.editFillColorColorPaletteButton.AutoSize = true;
            this.editFillColorColorPaletteButton.Location = new System.Drawing.Point(83, 43);
            this.editFillColorColorPaletteButton.Name = "editFillColorColorPaletteButton";
            this.editFillColorColorPaletteButton.Size = new System.Drawing.Size(39, 27);
            this.editFillColorColorPaletteButton.TabIndex = 11;
            this.editFillColorColorPaletteButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Fill Color";
            // 
            // editOutlineWidthNumericUpDown
            // 
            this.editOutlineWidthNumericUpDown.Location = new System.Drawing.Point(262, 76);
            this.editOutlineWidthNumericUpDown.Name = "editOutlineWidthNumericUpDown";
            this.editOutlineWidthNumericUpDown.Size = new System.Drawing.Size(68, 20);
            this.editOutlineWidthNumericUpDown.TabIndex = 10;
            this.editOutlineWidthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Outline Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Outline Color";
            // 
            // selectionGroupBox
            // 
            this.selectionGroupBox.Controls.Add(this.label8);
            this.selectionGroupBox.Controls.Add(this.selTranNumericUpDown);
            this.selectionGroupBox.Controls.Add(this.label7);
            this.selectionGroupBox.Controls.Add(this.selOutlineColorPaletteButton);
            this.selectionGroupBox.Controls.Add(this.selFillColorPaletteButton);
            this.selectionGroupBox.Controls.Add(this.selOutlineWidthNumericUpDown);
            this.selectionGroupBox.Controls.Add(this.label3);
            this.selectionGroupBox.Controls.Add(this.label2);
            this.selectionGroupBox.Controls.Add(this.label1);
            this.selectionGroupBox.Location = new System.Drawing.Point(6, 6);
            this.selectionGroupBox.Name = "selectionGroupBox";
            this.selectionGroupBox.Size = new System.Drawing.Size(374, 82);
            this.selectionGroupBox.TabIndex = 0;
            this.selectionGroupBox.TabStop = false;
            this.selectionGroupBox.Text = "Selection";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(337, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "%";
            // 
            // selTranNumericUpDown
            // 
            this.selTranNumericUpDown.Location = new System.Drawing.Point(264, 20);
            this.selTranNumericUpDown.Name = "selTranNumericUpDown";
            this.selTranNumericUpDown.Size = new System.Drawing.Size(68, 20);
            this.selTranNumericUpDown.TabIndex = 8;
            this.selTranNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(175, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Transparency";
            // 
            // selOutlineColorPaletteButton
            // 
            this.selOutlineColorPaletteButton.AutoSize = true;
            this.selOutlineColorPaletteButton.Location = new System.Drawing.Point(85, 48);
            this.selOutlineColorPaletteButton.Name = "selOutlineColorPaletteButton";
            this.selOutlineColorPaletteButton.Size = new System.Drawing.Size(39, 27);
            this.selOutlineColorPaletteButton.TabIndex = 6;
            this.selOutlineColorPaletteButton.UseVisualStyleBackColor = true;
            // 
            // selFillColorPaletteButton
            // 
            this.selFillColorPaletteButton.AutoSize = true;
            this.selFillColorPaletteButton.Location = new System.Drawing.Point(85, 15);
            this.selFillColorPaletteButton.Name = "selFillColorPaletteButton";
            this.selFillColorPaletteButton.Size = new System.Drawing.Size(39, 27);
            this.selFillColorPaletteButton.TabIndex = 5;
            this.selFillColorPaletteButton.UseVisualStyleBackColor = true;
            // 
            // selOutlineWidthNumericUpDown
            // 
            this.selOutlineWidthNumericUpDown.Location = new System.Drawing.Point(264, 49);
            this.selOutlineWidthNumericUpDown.Name = "selOutlineWidthNumericUpDown";
            this.selOutlineWidthNumericUpDown.Size = new System.Drawing.Size(68, 20);
            this.selOutlineWidthNumericUpDown.TabIndex = 4;
            this.selOutlineWidthNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Outline Color";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Outline Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fill Color";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(331, 248);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(250, 248);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // defaultButton
            // 
            this.defaultButton.Location = new System.Drawing.Point(12, 248);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(75, 23);
            this.defaultButton.TabIndex = 3;
            this.defaultButton.Text = "Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 278);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.optionsTabControl);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OptionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.optionsTabControl.ResumeLayout(false);
            this.symbolTabPage.ResumeLayout(false);
            this.editGroupBox.ResumeLayout(false);
            this.editGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editTranNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editOutlineWidthNumericUpDown)).EndInit();
            this.selectionGroupBox.ResumeLayout(false);
            this.selectionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selTranNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selOutlineWidthNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl optionsTabControl;
        private System.Windows.Forms.TabPage symbolTabPage;
        private System.Windows.Forms.GroupBox selectionGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.NumericUpDown selOutlineWidthNumericUpDown;
        private System.Windows.Forms.Label label3;
        private Controls.ColorPaletteButton selFillColorPaletteButton;
        private Controls.ColorPaletteButton selOutlineColorPaletteButton;
        private System.Windows.Forms.GroupBox editGroupBox;
        private Controls.ColorPaletteButton editOutlineColorPaletteButton;
        private Controls.ColorPaletteButton editFillColorColorPaletteButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown editOutlineWidthNumericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown editTranNumericUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown selTranNumericUpDown;
        private System.Windows.Forms.CheckBox editCustomCheckBox;
        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;

    }
}