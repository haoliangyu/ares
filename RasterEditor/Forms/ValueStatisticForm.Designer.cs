namespace RasterEditor.Forms
{
    partial class ValueStatisticForm
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
            this.statDataGridView = new System.Windows.Forms.DataGridView();
            this.fieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.statDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statDataGridView
            // 
            this.statDataGridView.AllowUserToAddRows = false;
            this.statDataGridView.AllowUserToDeleteRows = false;
            this.statDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldColumn,
            this.valueColumn});
            this.statDataGridView.Location = new System.Drawing.Point(5, 7);
            this.statDataGridView.Name = "statDataGridView";
            this.statDataGridView.ReadOnly = true;
            this.statDataGridView.Size = new System.Drawing.Size(251, 296);
            this.statDataGridView.TabIndex = 0;
            // 
            // fieldColumn
            // 
            this.fieldColumn.HeaderText = "Field";
            this.fieldColumn.Name = "fieldColumn";
            this.fieldColumn.ReadOnly = true;
            // 
            // valueColumn
            // 
            this.valueColumn.HeaderText = "Value";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.ReadOnly = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(91, 307);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ValueStatisticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 334);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.statDataGridView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValueStatisticForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pixels Statistic";
            ((System.ComponentModel.ISupportInitialize)(this.statDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView statDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
        private System.Windows.Forms.Button okButton;
    }
}