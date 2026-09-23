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
            components = new System.ComponentModel.Container();
            TreeNode treeNode2 = new TreeNode("Batch");
            saveFileDialog1 = new SaveFileDialog();
            flowLayoutPanel1 = new FlowLayoutPanel();
            statusStrip1 = new StatusStrip();
            lblStatusText = new ToolStripStatusLabel();
            tsProgressBar = new ToolStripProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            menuStrip1 = new MenuStrip();
            menuSelectSource = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            panel2 = new Panel();
            toolStrip3 = new ToolStrip();
            btnStartCapture = new ToolStripButton();
            btnStopScan = new ToolStripButton();
            tsSelectScanner = new ToolStripComboBox();
            btnAllSettings = new ToolStripButton();
            panel3 = new Panel();
            toolStrip1 = new ToolStrip();
            tsRotateRight = new ToolStripButton();
            tsRotateLeft = new ToolStripButton();
            tsUpDown = new ToolStripButton();
            tsStraigth = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsDeletePage = new ToolStripButton();
            tsClearArea = new ToolStripButton();
            tsRescan = new ToolStripButton();
            tsWhiteBorder = new ToolStripButton();
            toolStrip2 = new ToolStrip();
            tsNewBatch = new ToolStripButton();
            txOpenBatch = new ToolStripButton();
            tsCloseBatch = new ToolStripButton();
            tsClearCurrentScan = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsSeparate = new ToolStripButton();
            btnExportBatch = new ToolStripButton();
            menuTreview = new ContextMenuStrip(components);
            miIsblank = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            miAddNew = new ToolStripMenuItem();
            miDelete = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            miInsertScan = new ToolStripMenuItem();
            miRescan = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            miRename = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            toolStrip3.SuspendLayout();
            panel3.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            menuTreview.SuspendLayout();
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
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1243, 1075);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.Fill;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusText, tsProgressBar });
            statusStrip1.Location = new Point(0, 1191);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1549, 30);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusText
            // 
            lblStatusText.Name = "lblStatusText";
            lblStatusText.Size = new Size(39, 25);
            lblStatusText.Text = "Status";
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
            tableLayoutPanel1.Controls.Add(splitContainer1, 0, 3);
            tableLayoutPanel1.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel1.Controls.Add(statusStrip1, 0, 4);
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
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 113);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel1);
            splitContainer1.Size = new Size(1543, 1075);
            splitContainer1.SplitterDistance = 296;
            splitContainer1.TabIndex = 7;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Margin = new Padding(4, 3, 4, 3);
            treeView1.Name = "treeView1";
            treeNode2.Name = "packNode";
            treeNode2.Tag = "root";
            treeNode2.Text = "Batch";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode2 });
            treeView1.Size = new Size(296, 1075);
            treeView1.TabIndex = 8;
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
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
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsRotateRight, tsRotateLeft, tsUpDown, tsDeletePage, tsWhiteBorder, toolStripSeparator2, tsStraigth, tsClearArea, tsRescan });
            toolStrip1.Location = new Point(317, 1);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(376, 37);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsRotateRight
            // 
            tsRotateRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsRotateRight.Image = Properties.Resources.rotate_right;
            tsRotateRight.ImageScaling = ToolStripItemImageScaling.None;
            tsRotateRight.ImageTransparentColor = Color.Magenta;
            tsRotateRight.Name = "tsRotateRight";
            tsRotateRight.Size = new Size(44, 34);
            tsRotateRight.Text = "Right Rotate";
            tsRotateRight.Click += tsRotateRight_Click;
            // 
            // tsRotateLeft
            // 
            tsRotateLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsRotateLeft.Image = Properties.Resources.rotate_left;
            tsRotateLeft.ImageScaling = ToolStripItemImageScaling.None;
            tsRotateLeft.ImageTransparentColor = Color.Magenta;
            tsRotateLeft.Name = "tsRotateLeft";
            tsRotateLeft.Size = new Size(44, 34);
            tsRotateLeft.Text = "Left Rotate";
            tsRotateLeft.Click += tsRotateLeft_Click;
            // 
            // tsUpDown
            // 
            tsUpDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsUpDown.Image = Properties.Resources.up_down;
            tsUpDown.ImageScaling = ToolStripItemImageScaling.None;
            tsUpDown.ImageTransparentColor = Color.Magenta;
            tsUpDown.Name = "tsUpDown";
            tsUpDown.Size = new Size(34, 34);
            tsUpDown.Text = "Up/Down";
            tsUpDown.Click += tsUpDown_Click;
            // 
            // tsStraigth
            // 
            tsStraigth.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsStraigth.Enabled = false;
            tsStraigth.Image = Properties.Resources.straight;
            tsStraigth.ImageScaling = ToolStripItemImageScaling.None;
            tsStraigth.ImageTransparentColor = Color.Magenta;
            tsStraigth.Name = "tsStraigth";
            tsStraigth.Size = new Size(44, 34);
            tsStraigth.Text = "toolStripButton12";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 37);
            // 
            // tsDeletePage
            // 
            tsDeletePage.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsDeletePage.Image = Properties.Resources.delete_page;
            tsDeletePage.ImageScaling = ToolStripItemImageScaling.None;
            tsDeletePage.ImageTransparentColor = Color.Magenta;
            tsDeletePage.Name = "tsDeletePage";
            tsDeletePage.Size = new Size(44, 34);
            tsDeletePage.Text = "Delete page";
            // 
            // tsClearArea
            // 
            tsClearArea.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsClearArea.Enabled = false;
            tsClearArea.Image = Properties.Resources.eraser;
            tsClearArea.ImageScaling = ToolStripItemImageScaling.None;
            tsClearArea.ImageTransparentColor = Color.Magenta;
            tsClearArea.Name = "tsClearArea";
            tsClearArea.Size = new Size(44, 34);
            tsClearArea.Text = "Erase area";
            // 
            // tsRescan
            // 
            tsRescan.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsRescan.Enabled = false;
            tsRescan.Image = Properties.Resources.scan;
            tsRescan.ImageScaling = ToolStripItemImageScaling.None;
            tsRescan.ImageTransparentColor = Color.Magenta;
            tsRescan.Name = "tsRescan";
            tsRescan.Size = new Size(44, 34);
            tsRescan.Text = "Re-Scan";
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
            toolStrip2.Items.AddRange(new ToolStripItem[] { tsNewBatch, txOpenBatch, tsCloseBatch, tsClearCurrentScan, toolStripSeparator1, tsSeparate, btnExportBatch });
            toolStrip2.Location = new Point(1, 1);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Padding = new Padding(0);
            toolStrip2.Size = new Size(306, 37);
            toolStrip2.TabIndex = 7;
            toolStrip2.Text = "toolStrip2";
            // 
            // tsNewBatch
            // 
            tsNewBatch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsNewBatch.Image = Properties.Resources.new_batch;
            tsNewBatch.ImageScaling = ToolStripItemImageScaling.None;
            tsNewBatch.ImageTransparentColor = Color.Magenta;
            tsNewBatch.Name = "tsNewBatch";
            tsNewBatch.Size = new Size(44, 34);
            tsNewBatch.Text = "New batch";
            tsNewBatch.Click += tsNewBatch_Click;
            // 
            // txOpenBatch
            // 
            txOpenBatch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            txOpenBatch.Image = Properties.Resources.open_batch;
            txOpenBatch.ImageScaling = ToolStripItemImageScaling.None;
            txOpenBatch.ImageTransparentColor = Color.Magenta;
            txOpenBatch.Name = "txOpenBatch";
            txOpenBatch.Size = new Size(44, 34);
            txOpenBatch.Text = "Open batch";
            // 
            // tsCloseBatch
            // 
            tsCloseBatch.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsCloseBatch.Image = Properties.Resources.close_batch;
            tsCloseBatch.ImageScaling = ToolStripItemImageScaling.None;
            tsCloseBatch.ImageTransparentColor = Color.Magenta;
            tsCloseBatch.Name = "tsCloseBatch";
            tsCloseBatch.Size = new Size(44, 34);
            tsCloseBatch.Text = "Close batch";
            // 
            // tsClearCurrentScan
            // 
            tsClearCurrentScan.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsClearCurrentScan.Image = Properties.Resources.clean;
            tsClearCurrentScan.ImageScaling = ToolStripItemImageScaling.None;
            tsClearCurrentScan.ImageTransparentColor = Color.Magenta;
            tsClearCurrentScan.Name = "tsClearCurrentScan";
            tsClearCurrentScan.Size = new Size(44, 34);
            tsClearCurrentScan.Text = "Clear batch";
            tsClearCurrentScan.Click += btnResetScan_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 37);
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
            // menuTreview
            // 
            menuTreview.Items.AddRange(new ToolStripItem[] { miIsblank, toolStripSeparator3, miAddNew, miDelete, toolStripSeparator5, miInsertScan, miRescan, toolStripSeparator4, miRename });
            menuTreview.Name = "contextMenuStrip1";
            menuTreview.Size = new Size(131, 154);
            // 
            // miIsblank
            // 
            miIsblank.Enabled = false;
            miIsblank.Name = "miIsblank";
            miIsblank.Size = new Size(130, 22);
            miIsblank.Text = "Is Blank";
            miIsblank.Click += miIsblank_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(127, 6);
            // 
            // miAddNew
            // 
            miAddNew.Enabled = false;
            miAddNew.Name = "miAddNew";
            miAddNew.Size = new Size(130, 22);
            miAddNew.Text = "Add New";
            // 
            // miDelete
            // 
            miDelete.Enabled = false;
            miDelete.Name = "miDelete";
            miDelete.Size = new Size(130, 22);
            miDelete.Text = "Delete";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(127, 6);
            // 
            // miInsertScan
            // 
            miInsertScan.Enabled = false;
            miInsertScan.Name = "miInsertScan";
            miInsertScan.Size = new Size(130, 22);
            miInsertScan.Text = "Insert scan";
            // 
            // miRescan
            // 
            miRescan.Enabled = false;
            miRescan.Name = "miRescan";
            miRescan.Size = new Size(130, 22);
            miRescan.Text = "ReScan";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(127, 6);
            // 
            // miRename
            // 
            miRename.Enabled = false;
            miRename.Name = "miRename";
            miRename.Size = new Size(130, 22);
            miRename.Text = "Rename";
            miRename.Click += miRename_Click;
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
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            toolStrip3.ResumeLayout(false);
            toolStrip3.PerformLayout();
            panel3.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            menuTreview.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton btnStartCapture;
        private System.Windows.Forms.ToolStripButton btnStopScan;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusText;
        private ToolStripProgressBar tsProgressBar;
        private TableLayoutPanel tableLayoutPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuSelectSource;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;


        private ToolStrip toolStrip2;
        private ToolStripButton tsNewBatch;
        private ToolStripButton txOpenBatch;
        private ToolStripButton tsCloseBatch;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnExportBatch;
        private ToolStrip toolStrip3;
        //private ToolStripButton btnStartCapture;
        private ToolStripButton tsWhiteBorder;
        private ToolStripComboBox tsSelectScanner;
        private ToolStripButton btnAllSettings;
        private ToolStripButton tsClearCurrentScan;
        private Panel panel2;
        private Panel panel3;
        private ToolStrip toolStrip1;
        private ToolStripButton tsRotateRight;
        private ToolStripButton tsRotateLeft;
        private ToolStripButton tsUpDown;
        private ToolStripButton tsDeletePage;
        private ToolStripButton tsClearArea;
        private ToolStripButton tsRescan;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsStraigth;
        private ToolStripButton tsSeparate;
        private TreeView treeView1;
        private SplitContainer splitContainer1;
        private ContextMenuStrip menuTreview;
        private ToolStripMenuItem miIsblank;
        private ToolStripMenuItem miDelete;
        private ToolStripMenuItem miInsertScan;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem miAddNew;
        private ToolStripMenuItem miRescan;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem miRename;
        private ToolStripSeparator toolStripSeparator5;
    }
}

