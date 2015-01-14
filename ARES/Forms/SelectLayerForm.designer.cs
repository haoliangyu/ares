namespace ARES.Forms
{
    partial class SelectLayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLayerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.layerListView = new System.Windows.Forms.ListView();
            this.layerColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SourceColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.okButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(393, 252);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // layerListView
            // 
            this.layerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.layerColumnHeader,
            this.SourceColumnHeader});
            this.layerListView.Location = new System.Drawing.Point(14, 44);
            this.layerListView.MultiSelect = false;
            this.layerListView.Name = "layerListView";
            this.layerListView.Size = new System.Drawing.Size(454, 202);
            this.layerListView.SmallImageList = this.imageList;
            this.layerListView.TabIndex = 6;
            this.layerListView.UseCompatibleStateImageBehavior = false;
            this.layerListView.View = System.Windows.Forms.View.Details;
            this.layerListView.SelectedIndexChanged += new System.EventHandler(this.layerListView_SelectedIndexChanged);
            // 
            // layerColumnHeader
            // 
            this.layerColumnHeader.Text = "Layer Name";
            this.layerColumnHeader.Width = 129;
            // 
            // SourceColumnHeader
            // 
            this.SourceColumnHeader.Text = "Source";
            this.SourceColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SourceColumnHeader.Width = 312;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Edit.png");
            this.imageList.Images.SetKeyName(1, "NonEdit.png");
            // 
            // SelectLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 284);
            this.Controls.Add(this.layerListView);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectLayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Editing";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectLayerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView layerListView;
        private System.Windows.Forms.ColumnHeader layerColumnHeader;
        private System.Windows.Forms.ColumnHeader SourceColumnHeader;
        private System.Windows.Forms.ImageList imageList;
    }
}