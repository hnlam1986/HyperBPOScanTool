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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Pack");
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSources = new System.Windows.Forms.ToolStripDropDownButton();
            this.sepSourceList = new System.Windows.Forms.ToolStripSeparator();
            this.reloadSourcesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartCapture = new System.Windows.Forms.ToolStripButton();
            this.btnStopScan = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnSaveMock = new System.Windows.Forms.ToolStripButton();
            this.btnMockData = new System.Windows.Forms.ToolStripButton();
            this.btnStartLoadFile = new System.Windows.Forms.ToolStripButton();
            this.panelOptions = new System.Windows.Forms.TableLayoutPanel();
            this.groupDuplex = new System.Windows.Forms.GroupBox();
            this.ckDuplex = new System.Windows.Forms.CheckBox();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.comboSize = new System.Windows.Forms.ComboBox();
            this.groupDepth = new System.Windows.Forms.GroupBox();
            this.comboDepth = new System.Windows.Forms.ComboBox();
            this.groupDPI = new System.Windows.Forms.GroupBox();
            this.comboDPI = new System.Windows.Forms.ComboBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btnAllSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRollbackAll = new System.Windows.Forms.Button();
            this.btnRollback = new System.Windows.Forms.Button();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnPreviewBorder = new System.Windows.Forms.Button();
            this.txtBottomBorder = new System.Windows.Forms.TextBox();
            this.txtRightBorder = new System.Windows.Forms.TextBox();
            this.txtLeftBorder = new System.Windows.Forms.TextBox();
            this.txtTopBorder = new System.Windows.Forms.TextBox();
            this.chkBlankSheet = new System.Windows.Forms.CheckBox();
            this.txtPages = new System.Windows.Forms.TextBox();
            this.rdTotalPages = new System.Windows.Forms.RadioButton();
            this.chkBlankPage = new System.Windows.Forms.CheckBox();
            this.txtBlankValue = new System.Windows.Forms.TextBox();
            this.rdBlankSheet = new System.Windows.Forms.RadioButton();
            this.rdSheet = new System.Windows.Forms.RadioButton();
            this.btnResetScan = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.groupDuplex.SuspendLayout();
            this.groupSize.SuspendLayout();
            this.groupDepth.SuspendLayout();
            this.groupDPI.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Test";
            this.saveFileDialog1.Filter = "png files|*.png";
            this.saveFileDialog1.Title = "Save Image";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSources,
            this.btnStartCapture,
            this.btnStopScan,
            this.btnExport,
            this.btnSaveMock,
            this.btnMockData,
            this.btnStartLoadFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1328, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSources
            // 
            this.btnSources.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSources.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sepSourceList,
            this.reloadSourcesListToolStripMenuItem});
            this.btnSources.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSources.Name = "btnSources";
            this.btnSources.Size = new System.Drawing.Size(94, 22);
            this.btnSources.Text = "Select &sources";
            this.btnSources.DropDownOpening += new System.EventHandler(this.btnSources_DropDownOpening);
            // 
            // sepSourceList
            // 
            this.sepSourceList.Name = "sepSourceList";
            this.sepSourceList.Size = new System.Drawing.Size(168, 6);
            // 
            // reloadSourcesListToolStripMenuItem
            // 
            this.reloadSourcesListToolStripMenuItem.Name = "reloadSourcesListToolStripMenuItem";
            this.reloadSourcesListToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.reloadSourcesListToolStripMenuItem.Text = "&Reload sources list";
            this.reloadSourcesListToolStripMenuItem.Click += new System.EventHandler(this.reloadSourcesListToolStripMenuItem_Click);
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartCapture.Enabled = false;
            this.btnStartCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(62, 22);
            this.btnStartCapture.Text = "S&tart scan";
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStopScan.Enabled = false;
            this.btnStopScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(62, 22);
            this.btnStopScan.Text = "Sto&p scan";
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(44, 22);
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSaveMock
            // 
            this.btnSaveMock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSaveMock.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveMock.Image")));
            this.btnSaveMock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveMock.Name = "btnSaveMock";
            this.btnSaveMock.Size = new System.Drawing.Size(94, 22);
            this.btnSaveMock.Text = "Save Mock data";
            this.btnSaveMock.Click += new System.EventHandler(this.btnSaveMock_Click);
            // 
            // btnMockData
            // 
            this.btnMockData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMockData.Image = ((System.Drawing.Image)(resources.GetObject("btnMockData.Image")));
            this.btnMockData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMockData.Name = "btnMockData";
            this.btnMockData.Size = new System.Drawing.Size(96, 22);
            this.btnMockData.Text = "Load Mock data";
            this.btnMockData.Click += new System.EventHandler(this.btnMockData_Click);
            // 
            // btnStartLoadFile
            // 
            this.btnStartLoadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnStartLoadFile.Image = ((System.Drawing.Image)(resources.GetObject("btnStartLoadFile.Image")));
            this.btnStartLoadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartLoadFile.Name = "btnStartLoadFile";
            this.btnStartLoadFile.Size = new System.Drawing.Size(80, 22);
            this.btnStartLoadFile.Text = "Start load file";
            this.btnStartLoadFile.Click += new System.EventHandler(this.btnStartLoadFile_Click);
            // 
            // panelOptions
            // 
            this.panelOptions.AutoScroll = true;
            this.panelOptions.ColumnCount = 1;
            this.panelOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelOptions.Controls.Add(this.groupDuplex, 0, 3);
            this.panelOptions.Controls.Add(this.groupSize, 0, 2);
            this.panelOptions.Controls.Add(this.groupDepth, 0, 1);
            this.panelOptions.Controls.Add(this.groupDPI, 0, 0);
            this.panelOptions.Controls.Add(this.treeView1, 0, 7);
            this.panelOptions.Controls.Add(this.btnAllSettings, 0, 4);
            this.panelOptions.Controls.Add(this.groupBox1, 0, 5);
            this.panelOptions.Controls.Add(this.btnResetScan, 0, 6);
            this.panelOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOptions.Location = new System.Drawing.Point(0, 25);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.RowCount = 8;
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.panelOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panelOptions.Size = new System.Drawing.Size(222, 1033);
            this.panelOptions.TabIndex = 3;
            // 
            // groupDuplex
            // 
            this.groupDuplex.Controls.Add(this.ckDuplex);
            this.groupDuplex.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupDuplex.Enabled = false;
            this.groupDuplex.Location = new System.Drawing.Point(8, 203);
            this.groupDuplex.Margin = new System.Windows.Forms.Padding(8);
            this.groupDuplex.Name = "groupDuplex";
            this.groupDuplex.Size = new System.Drawing.Size(206, 58);
            this.groupDuplex.TabIndex = 6;
            this.groupDuplex.TabStop = false;
            this.groupDuplex.Text = "Duplex";
            // 
            // ckDuplex
            // 
            this.ckDuplex.AutoSize = true;
            this.ckDuplex.Location = new System.Drawing.Point(18, 24);
            this.ckDuplex.Name = "ckDuplex";
            this.ckDuplex.Size = new System.Drawing.Size(65, 17);
            this.ckDuplex.TabIndex = 0;
            this.ckDuplex.Text = "Enabled";
            this.ckDuplex.UseVisualStyleBackColor = true;
            this.ckDuplex.CheckedChanged += new System.EventHandler(this.ckDuplex_CheckedChanged);
            // 
            // groupSize
            // 
            this.groupSize.Controls.Add(this.comboSize);
            this.groupSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSize.Enabled = false;
            this.groupSize.Location = new System.Drawing.Point(8, 138);
            this.groupSize.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(206, 54);
            this.groupSize.TabIndex = 5;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Size";
            // 
            // comboSize
            // 
            this.comboSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSize.FormattingEnabled = true;
            this.comboSize.Location = new System.Drawing.Point(18, 19);
            this.comboSize.Name = "comboSize";
            this.comboSize.Size = new System.Drawing.Size(169, 21);
            this.comboSize.TabIndex = 0;
            this.comboSize.SelectedIndexChanged += new System.EventHandler(this.comboSize_SelectedIndexChanged);
            // 
            // groupDepth
            // 
            this.groupDepth.Controls.Add(this.comboDepth);
            this.groupDepth.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupDepth.Enabled = false;
            this.groupDepth.Location = new System.Drawing.Point(8, 73);
            this.groupDepth.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDepth.Name = "groupDepth";
            this.groupDepth.Size = new System.Drawing.Size(206, 54);
            this.groupDepth.TabIndex = 4;
            this.groupDepth.TabStop = false;
            this.groupDepth.Text = "Depth";
            // 
            // comboDepth
            // 
            this.comboDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDepth.FormattingEnabled = true;
            this.comboDepth.Location = new System.Drawing.Point(18, 19);
            this.comboDepth.Name = "comboDepth";
            this.comboDepth.Size = new System.Drawing.Size(169, 21);
            this.comboDepth.TabIndex = 0;
            this.comboDepth.SelectedIndexChanged += new System.EventHandler(this.comboDepth_SelectedIndexChanged);
            // 
            // groupDPI
            // 
            this.groupDPI.Controls.Add(this.comboDPI);
            this.groupDPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupDPI.Enabled = false;
            this.groupDPI.Location = new System.Drawing.Point(8, 8);
            this.groupDPI.Margin = new System.Windows.Forms.Padding(8, 8, 8, 3);
            this.groupDPI.Name = "groupDPI";
            this.groupDPI.Size = new System.Drawing.Size(206, 54);
            this.groupDPI.TabIndex = 0;
            this.groupDPI.TabStop = false;
            this.groupDPI.Text = "DPI";
            // 
            // comboDPI
            // 
            this.comboDPI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDPI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDPI.FormattingEnabled = true;
            this.comboDPI.Location = new System.Drawing.Point(18, 19);
            this.comboDPI.Name = "comboDPI";
            this.comboDPI.Size = new System.Drawing.Size(169, 21);
            this.comboDPI.TabIndex = 0;
            this.comboDPI.SelectedIndexChanged += new System.EventHandler(this.comboDPI_SelectedIndexChanged);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 596);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "packNode";
            treeNode1.Tag = "root";
            treeNode1.Text = "Pack";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(216, 434);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // btnAllSettings
            // 
            this.btnAllSettings.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAllSettings.Enabled = false;
            this.btnAllSettings.Location = new System.Drawing.Point(34, 272);
            this.btnAllSettings.Name = "btnAllSettings";
            this.btnAllSettings.Size = new System.Drawing.Size(153, 23);
            this.btnAllSettings.TabIndex = 7;
            this.btnAllSettings.Text = "Open driver settings";
            this.btnAllSettings.UseVisualStyleBackColor = true;
            this.btnAllSettings.Click += new System.EventHandler(this.btnAllSettings_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRollbackAll);
            this.groupBox1.Controls.Add(this.btnRollback);
            this.groupBox1.Controls.Add(this.btnApplyAll);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.btnPreviewBorder);
            this.groupBox1.Controls.Add(this.txtBottomBorder);
            this.groupBox1.Controls.Add(this.txtRightBorder);
            this.groupBox1.Controls.Add(this.txtLeftBorder);
            this.groupBox1.Controls.Add(this.txtTopBorder);
            this.groupBox1.Controls.Add(this.chkBlankSheet);
            this.groupBox1.Controls.Add(this.txtPages);
            this.groupBox1.Controls.Add(this.rdTotalPages);
            this.groupBox1.Controls.Add(this.chkBlankPage);
            this.groupBox1.Controls.Add(this.txtBlankValue);
            this.groupBox1.Controls.Add(this.rdBlankSheet);
            this.groupBox1.Controls.Add(this.rdSheet);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(8, 307);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 240);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Separate mode";
            // 
            // btnRollbackAll
            // 
            this.btnRollbackAll.Location = new System.Drawing.Point(160, 206);
            this.btnRollbackAll.Name = "btnRollbackAll";
            this.btnRollbackAll.Size = new System.Drawing.Size(40, 24);
            this.btnRollbackAll.TabIndex = 15;
            this.btnRollbackAll.Text = "RbA";
            this.btnRollbackAll.UseVisualStyleBackColor = true;
            // 
            // btnRollback
            // 
            this.btnRollback.Location = new System.Drawing.Point(160, 180);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(40, 24);
            this.btnRollback.TabIndex = 14;
            this.btnRollback.Text = "Rb";
            this.btnRollback.UseVisualStyleBackColor = true;
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Location = new System.Drawing.Point(102, 206);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(54, 24);
            this.btnApplyAll.TabIndex = 13;
            this.btnApplyAll.Text = "Apply all";
            this.btnApplyAll.UseVisualStyleBackColor = true;
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(102, 180);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(54, 24);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnPreviewBorder
            // 
            this.btnPreviewBorder.Location = new System.Drawing.Point(102, 154);
            this.btnPreviewBorder.Name = "btnPreviewBorder";
            this.btnPreviewBorder.Size = new System.Drawing.Size(98, 24);
            this.btnPreviewBorder.TabIndex = 11;
            this.btnPreviewBorder.Text = "Preview";
            this.btnPreviewBorder.UseVisualStyleBackColor = true;
            this.btnPreviewBorder.Click += new System.EventHandler(this.btnPreviewBorder_Click);
            // 
            // txtBottomBorder
            // 
            this.txtBottomBorder.Location = new System.Drawing.Point(8, 210);
            this.txtBottomBorder.Name = "txtBottomBorder";
            this.txtBottomBorder.Size = new System.Drawing.Size(88, 20);
            this.txtBottomBorder.TabIndex = 10;
            this.txtBottomBorder.Text = "0";
            // 
            // txtRightBorder
            // 
            this.txtRightBorder.Location = new System.Drawing.Point(55, 184);
            this.txtRightBorder.Name = "txtRightBorder";
            this.txtRightBorder.Size = new System.Drawing.Size(41, 20);
            this.txtRightBorder.TabIndex = 9;
            this.txtRightBorder.Text = "0";
            // 
            // txtLeftBorder
            // 
            this.txtLeftBorder.Location = new System.Drawing.Point(9, 184);
            this.txtLeftBorder.Name = "txtLeftBorder";
            this.txtLeftBorder.Size = new System.Drawing.Size(41, 20);
            this.txtLeftBorder.TabIndex = 8;
            this.txtLeftBorder.Text = "0";
            // 
            // txtTopBorder
            // 
            this.txtTopBorder.Location = new System.Drawing.Point(8, 158);
            this.txtTopBorder.Name = "txtTopBorder";
            this.txtTopBorder.Size = new System.Drawing.Size(88, 20);
            this.txtTopBorder.TabIndex = 7;
            this.txtTopBorder.Text = "0";
            // 
            // chkBlankSheet
            // 
            this.chkBlankSheet.AutoSize = true;
            this.chkBlankSheet.Location = new System.Drawing.Point(6, 131);
            this.chkBlankSheet.Name = "chkBlankSheet";
            this.chkBlankSheet.Size = new System.Drawing.Size(106, 17);
            this.chkBlankSheet.TabIndex = 6;
            this.chkBlankSheet.Text = "Hide blank sheet";
            this.chkBlankSheet.UseVisualStyleBackColor = true;
            this.chkBlankSheet.CheckedChanged += new System.EventHandler(this.chkBlankSheet_CheckedChanged);
            // 
            // txtPages
            // 
            this.txtPages.Location = new System.Drawing.Point(93, 48);
            this.txtPages.Name = "txtPages";
            this.txtPages.Size = new System.Drawing.Size(96, 20);
            this.txtPages.TabIndex = 5;
            this.txtPages.Text = "4";
            // 
            // rdTotalPages
            // 
            this.rdTotalPages.AutoSize = true;
            this.rdTotalPages.Location = new System.Drawing.Point(6, 48);
            this.rdTotalPages.Name = "rdTotalPages";
            this.rdTotalPages.Size = new System.Drawing.Size(81, 17);
            this.rdTotalPages.TabIndex = 4;
            this.rdTotalPages.Text = "Total pages";
            this.rdTotalPages.UseVisualStyleBackColor = true;
            this.rdTotalPages.CheckedChanged += new System.EventHandler(this.rdTotalPages_CheckedChanged);
            // 
            // chkBlankPage
            // 
            this.chkBlankPage.AutoSize = true;
            this.chkBlankPage.Location = new System.Drawing.Point(6, 108);
            this.chkBlankPage.Name = "chkBlankPage";
            this.chkBlankPage.Size = new System.Drawing.Size(104, 17);
            this.chkBlankPage.TabIndex = 3;
            this.chkBlankPage.Text = "Hide blank page";
            this.chkBlankPage.UseVisualStyleBackColor = true;
            this.chkBlankPage.CheckedChanged += new System.EventHandler(this.chkBlankPage_CheckedChanged);
            // 
            // txtBlankValue
            // 
            this.txtBlankValue.Location = new System.Drawing.Point(93, 77);
            this.txtBlankValue.Name = "txtBlankValue";
            this.txtBlankValue.Size = new System.Drawing.Size(96, 20);
            this.txtBlankValue.TabIndex = 2;
            this.txtBlankValue.Text = "15.0";
            this.txtBlankValue.TextChanged += new System.EventHandler(this.txtBlankValue_TextChanged);
            // 
            // rdBlankSheet
            // 
            this.rdBlankSheet.AutoSize = true;
            this.rdBlankSheet.Location = new System.Drawing.Point(6, 80);
            this.rdBlankSheet.Name = "rdBlankSheet";
            this.rdBlankSheet.Size = new System.Drawing.Size(81, 17);
            this.rdBlankSheet.TabIndex = 1;
            this.rdBlankSheet.Text = "Blank sheet";
            this.rdBlankSheet.UseVisualStyleBackColor = true;
            this.rdBlankSheet.CheckedChanged += new System.EventHandler(this.rdBlankSheet_CheckedChanged);
            // 
            // rdSheet
            // 
            this.rdSheet.AutoSize = true;
            this.rdSheet.Checked = true;
            this.rdSheet.Location = new System.Drawing.Point(6, 19);
            this.rdSheet.Name = "rdSheet";
            this.rdSheet.Size = new System.Drawing.Size(81, 17);
            this.rdSheet.TabIndex = 0;
            this.rdSheet.TabStop = true;
            this.rdSheet.Text = "Every sheet";
            this.rdSheet.UseVisualStyleBackColor = true;
            this.rdSheet.CheckedChanged += new System.EventHandler(this.rdSheet_CheckedChanged);
            // 
            // btnResetScan
            // 
            this.btnResetScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnResetScan.Location = new System.Drawing.Point(3, 558);
            this.btnResetScan.Name = "btnResetScan";
            this.btnResetScan.Size = new System.Drawing.Size(216, 32);
            this.btnResetScan.TabIndex = 10;
            this.btnResetScan.Text = "Reset Scan";
            this.btnResetScan.UseVisualStyleBackColor = true;
            this.btnResetScan.Click += new System.EventHandler(this.btnResetScan_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(222, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1106, 1033);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1328, 1058);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TestForm";
            this.Text = "Test Form";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.groupDuplex.ResumeLayout(false);
            this.groupDuplex.PerformLayout();
            this.groupSize.ResumeLayout(false);
            this.groupDepth.ResumeLayout(false);
            this.groupDPI.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton btnSources;
        private System.Windows.Forms.ToolStripSeparator sepSourceList;
        private System.Windows.Forms.ToolStripMenuItem reloadSourcesListToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel panelOptions;
        private System.Windows.Forms.GroupBox groupDPI;
        private System.Windows.Forms.GroupBox groupDepth;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.GroupBox groupDuplex;
        private System.Windows.Forms.ComboBox comboDPI;
        private System.Windows.Forms.ComboBox comboSize;
        private System.Windows.Forms.ComboBox comboDepth;
        private System.Windows.Forms.ToolStripButton btnStartCapture;
        private System.Windows.Forms.ToolStripButton btnStopScan;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.CheckBox ckDuplex;
        private System.Windows.Forms.Button btnAllSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdBlankSheet;
        private System.Windows.Forms.RadioButton rdSheet;
        private System.Windows.Forms.TextBox txtBlankValue;
        private System.Windows.Forms.Button btnResetScan;
        private System.Windows.Forms.CheckBox chkBlankPage;
        private System.Windows.Forms.RadioButton rdTotalPages;
        private System.Windows.Forms.TextBox txtPages;
        private System.Windows.Forms.ToolStripButton btnMockData;
        private System.Windows.Forms.ToolStripButton btnSaveMock;
        private System.Windows.Forms.CheckBox chkBlankSheet;
        private System.Windows.Forms.ToolStripButton btnStartLoadFile;
        private System.Windows.Forms.Button btnPreviewBorder;
        private System.Windows.Forms.TextBox txtBottomBorder;
        private System.Windows.Forms.TextBox txtRightBorder;
        private System.Windows.Forms.TextBox txtLeftBorder;
        private System.Windows.Forms.TextBox txtTopBorder;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Button btnRollbackAll;
        private System.Windows.Forms.Button btnRollback;
    }
}

