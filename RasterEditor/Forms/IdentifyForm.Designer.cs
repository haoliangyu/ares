namespace RasterEditor.Forms
{
    partial class IdentifyForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlPosTextBox = new System.Windows.Forms.TextBox();
            this.brPosTextBox = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.valueTabPage = new System.Windows.Forms.TabPage();
            this.rasterGridView = new RasterEditor.Controls.RasterGridView();
            this.layerNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.brTextBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tlTextBoxToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statTabPage = new System.Windows.Forms.TabPage();
            this.statDataGridView = new System.Windows.Forms.DataGridView();
            this.fieldColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.valueTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).BeginInit();
            this.statTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Top Left Corner";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Buttom Right Corner";
            // 
            // tlPosTextBox
            // 
            this.tlPosTextBox.Location = new System.Drawing.Point(10, 53);
            this.tlPosTextBox.Name = "tlPosTextBox";
            this.tlPosTextBox.ReadOnly = true;
            this.tlPosTextBox.Size = new System.Drawing.Size(130, 20);
            this.tlPosTextBox.TabIndex = 7;
            // 
            // brPosTextBox
            // 
            this.brPosTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.brPosTextBox.Location = new System.Drawing.Point(156, 53);
            this.brPosTextBox.Name = "brPosTextBox";
            this.brPosTextBox.ReadOnly = true;
            this.brPosTextBox.Size = new System.Drawing.Size(130, 20);
            this.brPosTextBox.TabIndex = 8;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.valueTabPage);
            this.tabControl.Controls.Add(this.statTabPage);
            this.tabControl.Location = new System.Drawing.Point(10, 79);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(280, 294);
            this.tabControl.TabIndex = 10;
            // 
            // valueTabPage
            // 
            this.valueTabPage.Controls.Add(this.rasterGridView);
            this.valueTabPage.Location = new System.Drawing.Point(4, 22);
            this.valueTabPage.Name = "valueTabPage";
            this.valueTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.valueTabPage.Size = new System.Drawing.Size(272, 268);
            this.valueTabPage.TabIndex = 0;
            this.valueTabPage.Text = "Values";
            this.valueTabPage.UseVisualStyleBackColor = true;
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
            this.rasterGridView.Location = new System.Drawing.Point(3, 3);
            this.rasterGridView.Name = "rasterGridView";
            this.rasterGridView.NoDataValue = 0D;
            this.rasterGridView.Size = new System.Drawing.Size(266, 262);
            this.rasterGridView.TabIndex = 0;
            // 
            // layerNameTextBox
            // 
            this.layerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.layerNameTextBox.Location = new System.Drawing.Point(83, 9);
            this.layerNameTextBox.Name = "layerNameTextBox";
            this.layerNameTextBox.ReadOnly = true;
            this.layerNameTextBox.Size = new System.Drawing.Size(207, 20);
            this.layerNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Identify From:";
            // 
            // brTextBoxToolTip
            // 
            this.brTextBoxToolTip.AutomaticDelay = 200;
            this.brTextBoxToolTip.AutoPopDelay = 5000;
            this.brTextBoxToolTip.InitialDelay = 200;
            this.brTextBoxToolTip.ReshowDelay = 40;
            // 
            // tlTextBoxToolTip
            // 
            this.tlTextBoxToolTip.AutomaticDelay = 200;
            this.tlTextBoxToolTip.AutoPopDelay = 5000;
            this.tlTextBoxToolTip.InitialDelay = 200;
            this.tlTextBoxToolTip.ReshowDelay = 40;
            // 
            // statTabPage
            // 
            this.statTabPage.Controls.Add(this.statDataGridView);
            this.statTabPage.Location = new System.Drawing.Point(4, 22);
            this.statTabPage.Name = "statTabPage";
            this.statTabPage.Size = new System.Drawing.Size(272, 268);
            this.statTabPage.TabIndex = 1;
            this.statTabPage.Text = "Statistic";
            this.statTabPage.UseVisualStyleBackColor = true;
            // 
            // statDataGridView
            // 
            this.statDataGridView.AllowUserToAddRows = false;
            this.statDataGridView.AllowUserToDeleteRows = false;
            this.statDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldColumn,
            this.valueColumn});
            this.statDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statDataGridView.Location = new System.Drawing.Point(0, 0);
            this.statDataGridView.Name = "statDataGridView";
            this.statDataGridView.ReadOnly = true;
            this.statDataGridView.Size = new System.Drawing.Size(272, 268);
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
            // IdentifyForm
            // 
            this.AutoSize = true;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.brPosTextBox);
            this.Controls.Add(this.tlPosTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.layerNameTextBox);
            this.Controls.Add(this.label1);
            this.Name = "IdentifyForm";
            this.Size = new System.Drawing.Size(300, 379);
            this.tabControl.ResumeLayout(false);
            this.valueTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rasterGridView)).EndInit();
            this.statTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tlPosTextBox;
        private System.Windows.Forms.TextBox brPosTextBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage valueTabPage;
        private System.Windows.Forms.TextBox layerNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip brTextBoxToolTip;
        private System.Windows.Forms.ToolTip tlTextBoxToolTip;
        private Controls.RasterGridView rasterGridView;
        private System.Windows.Forms.TabPage statTabPage;
        private System.Windows.Forms.DataGridView statDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn fieldColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;

    }
}
