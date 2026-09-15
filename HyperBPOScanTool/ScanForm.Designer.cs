namespace HyperBPOScanTool
{
	partial class ScanForm
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
            TreeNode treeNode1 = new TreeNode("Batch");
            saveFileDialog1 = new SaveFileDialog();
            flowLayoutPanel1 = new FlowLayoutPanel();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            tsProgressBar = new ToolStripProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            menuStrip1 = new MenuStrip();
            menuSelectSource = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel2 = new Panel();
            toolStrip3 = new ToolStrip();
            btnStartCapture = new ToolStripButton();
            btnStopScan = new ToolStripButton();
            tsSelectScanner = new ToolStripComboBox();
            btnAllSettings = new ToolStripButton();
            panel3 = new Panel();
            toolStrip1 = new ToolStrip();
            toolStripButton6 = new ToolStripButton();
            toolStripButton7 = new ToolStripButton();
            toolStripButton8 = new ToolStripButton();
            toolStripButton12 = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton9 = new ToolStripButton();
            toolStripButton10 = new ToolStripButton();
            toolStripButton11 = new ToolStripButton();
            tsWhiteBorder = new ToolStripButton();
            toolStrip2 = new ToolStrip();
            toolStripButton3 = new ToolStripButton();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            tsSeparate = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton1 = new ToolStripButton();
            btnExportBatch = new ToolStripButton();
            treeView1 = new TreeView();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            toolStrip3.SuspendLayout();
            panel3.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileName = "Test";
            saveFileDialog1.Filter = "png files|*.png";
            saveFileDialog1.Title = "Save Image";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(304, 3);
            flowLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1235, 1069);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.Fill;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, tsProgressBar });
            statusStrip1.Location = new Point(0, 1191);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1549, 30);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(44, 25);
            toolStripStatusLabel1.Text = "tsLabel";
            // 
            // tsProgressBar
            // 
            tsProgressBar.Alignment = ToolStripItemAlignment.Right;
            tsProgressBar.Name = "tsProgressBar";
            tsProgressBar.Size = new Size(300, 24);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel1.Controls.Add(statusStrip1, 0, 4);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panel3, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(1549, 1221);
            tableLayoutPanel1.TabIndex = 6;
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.Fill;
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuSelectSource, toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1549, 30);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuSelectSource
            // 
            menuSelectSource.Name = "menuSelectSource";
            menuSelectSource.Size = new Size(37, 26);
            menuSelectSource.Text = "File";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(49, 26);
            toolStripMenuItem1.Text = "Batch";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(44, 26);
            toolStripMenuItem2.Text = "View";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(61, 26);
            toolStripMenuItem3.Text = "Capture";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(75, 26);
            toolStripMenuItem4.Text = "Document";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(treeView1, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 113);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1543, 1075);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.Controls.Add(toolStrip3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 30);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1549, 40);
            panel2.TabIndex = 9;
            // 
            // toolStrip3
            // 
            toolStrip3.AutoSize = false;
            toolStrip3.Dock = DockStyle.None;
            toolStrip3.Items.AddRange(new ToolStripItem[] { btnStartCapture, btnStopScan, tsSelectScanner, btnAllSettings });
            toolStrip3.Location = new Point(1, 1);
            toolStrip3.Name = "toolStrip3";
            toolStrip3.Padding = new Padding(0);
            toolStrip3.Size = new Size(467, 37);
            toolStrip3.TabIndex = 9;
            toolStrip3.Text = "toolStrip3";
            // 
            // btnStartCapture
            // 
            btnStartCapture.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnStartCapture.Enabled = false;
            btnStartCapture.Image = Properties.Resources.start;
            btnStartCapture.ImageScaling = ToolStripItemImageScaling.None;
            btnStartCapture.ImageTransparentColor = Color.Magenta;
            btnStartCapture.Name = "btnStartCapture";
            btnStartCapture.Size = new Size(44, 34);
            btnStartCapture.Text = "Start";
            btnStartCapture.Click += btnStartCapture_Click;
            // 
            // btnStopScan
            // 
            btnStopScan.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnStopScan.Enabled = false;
            btnStopScan.Image = Properties.Resources.stop;
            btnStopScan.ImageScaling = ToolStripItemImageScaling.None;
            btnStopScan.ImageTransparentColor = Color.Magenta;
            btnStopScan.Name = "btnStopScan";
            btnStopScan.Size = new Size(44, 34);
            btnStopScan.Text = "Stop";
            btnStopScan.Click += btnStopScan_Click;
            // 
            // tsSelectScanner
            // 
            tsSelectScanner.AutoSize = false;
            tsSelectScanner.DropDownStyle = ComboBoxStyle.DropDownList;
            tsSelectScanner.Name = "tsSelectScanner";
            tsSelectScanner.Size = new Size(300, 23);
            tsSelectScanner.SelectedIndexChanged += SourceMenuItem_Click;
            // 
            // btnAllSettings
            // 
            btnAllSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAllSettings.Enabled = false;
            btnAllSettings.Image = Properties.Resources.settings;
            btnAllSettings.ImageScaling = ToolStripItemImageScaling.None;
            btnAllSettings.ImageTransparentColor = Color.Magenta;
            btnAllSettings.Name = "btnAllSettings";
            btnAllSettings.Size = new Size(44, 34);
            btnAllSettings.Text = "Setting";
            btnAllSettings.Click += btnAllSettings_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(toolStrip1);
            panel3.Controls.Add(toolStrip2);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 70);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1549, 40);
            panel3.TabIndex = 10;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.Dock = DockStyle.None;
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton6, toolStripButton7, toolStripButton8, toolStripButton12, toolStripSeparator2, toolStripButton9, toolStripButton10, toolStripButton11, tsWhiteBorder });
            toolStrip1.Location = new Point(307, 1);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(376, 37);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton6
            // 
            toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton6.Image = Properties.Resources.rotate_right;
            toolStripButton6.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(44, 34);
            toolStripButton6.Text = "Right Rotate";
            // 
            // toolStripButton7
            // 
            toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton7.Image = Properties.Resources.rotate_left;
            toolStripButton7.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(44, 34);
            toolStripButton7.Text = "Left Rotate";
            // 
            // toolStripButton8
            // 
            toolStripButton8.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton8.Image = Properties.Resources.up_down;
            toolStripButton8.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton8.ImageTransparentColor = Color.Magenta;
            toolStripButton8.Name = "toolStripButton8";
            toolStripButton8.Size = new Size(34, 34);
            toolStripButton8.Text = "Up/Down";
            // 
            // toolStripButton12
            // 
            toolStripButton12.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton12.Image = Properties.Resources.straight;
            toolStripButton12.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton12.ImageTransparentColor = Color.Magenta;
            toolStripButton12.Name = "toolStripButton12";
            toolStripButton12.Size = new Size(44, 34);
            toolStripButton12.Text = "toolStripButton12";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 37);
            // 
            // toolStripButton9
            // 
            toolStripButton9.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton9.Image = Properties.Resources.delete_page;
            toolStripButton9.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton9.ImageTransparentColor = Color.Magenta;
            toolStripButton9.Name = "toolStripButton9";
            toolStripButton9.Size = new Size(44, 34);
            toolStripButton9.Text = "Delete page";
            // 
            // toolStripButton10
            // 
            toolStripButton10.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton10.Image = Properties.Resources.eraser;
            toolStripButton10.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton10.ImageTransparentColor = Color.Magenta;
            toolStripButton10.Name = "toolStripButton10";
            toolStripButton10.Size = new Size(44, 34);
            toolStripButton10.Text = "Erase area";
            // 
            // toolStripButton11
            // 
            toolStripButton11.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton11.Image = Properties.Resources.scan;
            toolStripButton11.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton11.ImageTransparentColor = Color.Magenta;
            toolStripButton11.Name = "toolStripButton11";
            toolStripButton11.Size = new Size(44, 34);
            toolStripButton11.Text = "Re-Scan";
            // 
            // tsWhiteBorder
            // 
            tsWhiteBorder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsWhiteBorder.Image = Properties.Resources.border_small;
            tsWhiteBorder.ImageScaling = ToolStripItemImageScaling.None;
            tsWhiteBorder.ImageTransparentColor = Color.Magenta;
            tsWhiteBorder.Name = "tsWhiteBorder";
            tsWhiteBorder.Size = new Size(44, 34);
            tsWhiteBorder.Text = "Remove back border";
            tsWhiteBorder.Click += tsWhiteBorder_Click;
            // 
            // toolStrip2
            // 
            toolStrip2.AutoSize = false;
            toolStrip2.Dock = DockStyle.None;
            toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripButton3, toolStripButton4, toolStripButton5, tsSeparate, toolStripSeparator1, toolStripButton1, btnExportBatch });
            toolStrip2.Location = new Point(1, 1);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Padding = new Padding(0);
            toolStrip2.Size = new Size(294, 37);
            toolStrip2.TabIndex = 7;
            toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton3.Image = Properties.Resources.new_batch;
            toolStripButton3.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(44, 34);
            toolStripButton3.Text = "New batch";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = Properties.Resources.open_batch;
            toolStripButton4.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(44, 34);
            toolStripButton4.Text = "Open batch";
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = Properties.Resources.close_batch;
            toolStripButton5.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(44, 34);
            toolStripButton5.Text = "Close batch";
            // 
            // tsSeparate
            // 
            tsSeparate.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsSeparate.Image = Properties.Resources.separate;
            tsSeparate.ImageScaling = ToolStripItemImageScaling.None;
            tsSeparate.ImageTransparentColor = Color.Magenta;
            tsSeparate.Name = "tsSeparate";
            tsSeparate.Size = new Size(44, 34);
            tsSeparate.Text = "toolStripButton13";
            tsSeparate.Click += tsSeparate_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 37);
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = Properties.Resources.clean;
            toolStripButton1.ImageScaling = ToolStripItemImageScaling.None;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(44, 34);
            toolStripButton1.Text = "Clear batch";
            toolStripButton1.Click += btnResetScan_Click;
            // 
            // btnExportBatch
            // 
            btnExportBatch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExportBatch.Image = Properties.Resources.export;
            btnExportBatch.ImageScaling = ToolStripItemImageScaling.None;
            btnExportBatch.ImageTransparentColor = Color.Magenta;
            btnExportBatch.Name = "btnExportBatch";
            btnExportBatch.Size = new Size(44, 34);
            btnExportBatch.Text = "Export";
            btnExportBatch.Click += btnExportBatch_Click;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(4, 3);
            treeView1.Margin = new Padding(4, 3, 4, 3);
            treeView1.Name = "treeView1";
            treeNode1.Name = "packNode";
            treeNode1.Tag = "root";
            treeNode1.Text = "Batch";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode1 });
            treeView1.Size = new Size(292, 1069);
            treeView1.TabIndex = 8;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // ScanForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1549, 1221);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ScanForm";
            Text = "HyperBPO Scanning Tool";
            Load += TestForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            toolStrip3.ResumeLayout(false);
            toolStrip3.PerformLayout();
            panel3.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton btnStartCapture;
        private System.Windows.Forms.ToolStripButton btnStopScan;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar tsProgressBar;
        private TableLayoutPanel tableLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuSelectSource;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;


        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton3;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnExportBatch;
        private TableLayoutPanel tableLayoutPanel2;
        private ToolStrip toolStrip3;
        //private ToolStripButton btnStartCapture;
        private ToolStripButton tsWhiteBorder;
        private ToolStripComboBox tsSelectScanner;
        private ToolStripButton btnAllSettings;
        private ToolStripButton toolStripButton1;
        private Panel panel2;
        private Panel panel3;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
        private ToolStripButton toolStripButton9;
        private ToolStripButton toolStripButton10;
        private ToolStripButton toolStripButton11;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton12;
        private ToolStripButton tsSeparate;
        private TreeView treeView1;
    }
}

