namespace ARES.Forms
{
    partial class EditExtentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditExtentForm));
            this.label1 = new System.Windows.Forms.Label();
            this.inputRasterComboBox = new System.Windows.Forms.ComboBox();
            this.openInputRasterBbutton = new System.Windows.Forms.Button();
            this.outputRasterTextBox = new System.Windows.Forms.TextBox();
            this.openOutputRasterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.outputExtentComboBox = new System.Windows.Forms.ComboBox();
            this.openOutputExtentButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.leftCoorTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttomCoorTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pixelSizeTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Raster File";
            // 
            // inputRasterComboBox
            // 
            this.inputRasterComboBox.FormattingEnabled = true;
            this.inputRasterComboBox.Location = new System.Drawing.Point(11, 25);
            this.inputRasterComboBox.Name = "inputRasterComboBox";
            this.inputRasterComboBox.Size = new System.Drawing.Size(366, 21);
            this.inputRasterComboBox.TabIndex = 1;
            this.inputRasterComboBox.SelectedIndexChanged += new System.EventHandler(this.inputRasterComboBox_SelectedIndexChanged);
            // 
            // openInputRasterBbutton
            // 
            this.openInputRasterBbutton.Image = ((System.Drawing.Image)(resources.GetObject("openInputRasterBbutton.Image")));
            this.openInputRasterBbutton.Location = new System.Drawing.Point(382, 22);
            this.openInputRasterBbutton.Name = "openInputRasterBbutton";
            this.openInputRasterBbutton.Size = new System.Drawing.Size(25, 25);
            this.openInputRasterBbutton.TabIndex = 2;
            this.openInputRasterBbutton.UseVisualStyleBackColor = true;
            this.openInputRasterBbutton.Click += new System.EventHandler(this.openInputRasterBbutton_Click);
            // 
            // outputRasterTextBox
            // 
            this.outputRasterTextBox.Location = new System.Drawing.Point(11, 72);
            this.outputRasterTextBox.Name = "outputRasterTextBox";
            this.outputRasterTextBox.ReadOnly = true;
            this.outputRasterTextBox.Size = new System.Drawing.Size(366, 20);
            this.outputRasterTextBox.TabIndex = 4;
            // 
            // openOutputRasterButton
            // 
            this.openOutputRasterButton.Image = ((System.Drawing.Image)(resources.GetObject("openOutputRasterButton.Image")));
            this.openOutputRasterButton.Location = new System.Drawing.Point(382, 67);
            this.openOutputRasterButton.Name = "openOutputRasterButton";
            this.openOutputRasterButton.Size = new System.Drawing.Size(25, 25);
            this.openOutputRasterButton.TabIndex = 5;
            this.openOutputRasterButton.UseVisualStyleBackColor = true;
            this.openOutputRasterButton.Click += new System.EventHandler(this.openOutputRasterButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output Extent";
            // 
            // outputExtentComboBox
            // 
            this.outputExtentComboBox.FormattingEnabled = true;
            this.outputExtentComboBox.Location = new System.Drawing.Point(11, 119);
            this.outputExtentComboBox.Name = "outputExtentComboBox";
            this.outputExtentComboBox.Size = new System.Drawing.Size(366, 21);
            this.outputExtentComboBox.TabIndex = 7;
            this.outputExtentComboBox.SelectedIndexChanged += new System.EventHandler(this.outputExtentComboBox_SelectedIndexChanged);
            // 
            // openOutputExtentButton
            // 
            this.openOutputExtentButton.Image = ((System.Drawing.Image)(resources.GetObject("openOutputExtentButton.Image")));
            this.openOutputExtentButton.Location = new System.Drawing.Point(382, 115);
            this.openOutputExtentButton.Name = "openOutputExtentButton";
            this.openOutputExtentButton.Size = new System.Drawing.Size(25, 25);
            this.openOutputExtentButton.TabIndex = 8;
            this.openOutputExtentButton.UseVisualStyleBackColor = true;
            this.openOutputExtentButton.Click += new System.EventHandler(this.openOutputExtentButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Buttom-Left Corner";
            // 
            // leftCoorTextBox
            // 
            this.leftCoorTextBox.Location = new System.Drawing.Point(32, 161);
            this.leftCoorTextBox.Name = "leftCoorTextBox";
            this.leftCoorTextBox.Size = new System.Drawing.Size(159, 20);
            this.leftCoorTextBox.TabIndex = 13;
            this.leftCoorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "X";
            // 
            // buttomCoorTextBox
            // 
            this.buttomCoorTextBox.Location = new System.Drawing.Point(217, 161);
            this.buttomCoorTextBox.Name = "buttomCoorTextBox";
            this.buttomCoorTextBox.Size = new System.Drawing.Size(159, 20);
            this.buttomCoorTextBox.TabIndex = 16;
            this.buttomCoorTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Pixel Size";
            // 
            // pixelSizeTextBox
            // 
            this.pixelSizeTextBox.Location = new System.Drawing.Point(10, 200);
            this.pixelSizeTextBox.Name = "pixelSizeTextBox";
            this.pixelSizeTextBox.Size = new System.Drawing.Size(366, 20);
            this.pixelSizeTextBox.TabIndex = 18;
            this.pixelSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(301, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(217, 226);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 20;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(197, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Output Raster File";
            // 
            // EditExtentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 257);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.pixelSizeTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttomCoorTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.leftCoorTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.openOutputExtentButton);
            this.Controls.Add(this.outputExtentComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.openOutputRasterButton);
            this.Controls.Add(this.outputRasterTextBox);
            this.Controls.Add(this.openInputRasterBbutton);
            this.Controls.Add(this.inputRasterComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EditExtentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Raster Extent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inputRasterComboBox;
        private System.Windows.Forms.Button openInputRasterBbutton;
        private System.Windows.Forms.TextBox outputRasterTextBox;
        private System.Windows.Forms.Button openOutputRasterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox outputExtentComboBox;
        private System.Windows.Forms.Button openOutputExtentButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox leftCoorTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox buttomCoorTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox pixelSizeTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}