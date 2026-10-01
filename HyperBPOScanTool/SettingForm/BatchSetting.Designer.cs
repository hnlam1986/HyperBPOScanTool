namespace HyperBPOScanTool
{
    partial class BatchSetting
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
            lblExportPath = new Label();
            btnSelectpath = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tabControlBatchSetting = new TabControl();
            tabList = new TabPage();
            lstBatchSetting = new ListBox();
            tabGeneral = new TabPage();
            txtSeparateChar = new TextBox();
            label4 = new Label();
            chkSubfolder = new CheckBox();
            label3 = new Label();
            txtIndexFormat = new TextBox();
            label1 = new Label();
            txtBatchName = new TextBox();
            label2 = new Label();
            tabSeparate = new TabPage();
            rdSheet = new RadioButton();
            rdBlankSheet = new RadioButton();
            txtBlankValue = new TextBox();
            chkBlankPage = new CheckBox();
            rdTotalPages = new RadioButton();
            txtPages = new TextBox();
            chkBlankSheet = new CheckBox();
            tabBlankBorder = new TabPage();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            btnAll = new Button();
            btnRollbackAll = new Button();
            btnRollback = new Button();
            btnApplyAll = new Button();
            btnApply = new Button();
            btnPreviewBorder = new Button();
            txtBottomBorder = new TextBox();
            txtRightBorder = new TextBox();
            txtLeftBorder = new TextBox();
            txtTopBorder = new TextBox();
            btnOK = new Button();
            btnDel = new Button();
            btnSave = new Button();
            btnSaveAs = new Button();
            tabControlBatchSetting.SuspendLayout();
            tabList.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabSeparate.SuspendLayout();
            tabBlankBorder.SuspendLayout();
            SuspendLayout();
            // 
            // lblExportPath
            // 
            lblExportPath.AutoSize = true;
            lblExportPath.Location = new Point(106, 165);
            lblExportPath.Name = "lblExportPath";
            lblExportPath.Size = new Size(16, 15);
            lblExportPath.TabIndex = 0;
            lblExportPath.Text = "...";
            // 
            // btnSelectpath
            // 
            btnSelectpath.Location = new Point(10, 161);
            btnSelectpath.Name = "btnSelectpath";
            btnSelectpath.Size = new Size(90, 23);
            btnSelectpath.TabIndex = 1;
            btnSelectpath.Text = "Export Folder";
            btnSelectpath.UseVisualStyleBackColor = true;
            btnSelectpath.Click += btnSelectpath_Click;
            // 
            // tabControlBatchSetting
            // 
            tabControlBatchSetting.Controls.Add(tabList);
            tabControlBatchSetting.Controls.Add(tabGeneral);
            tabControlBatchSetting.Controls.Add(tabSeparate);
            tabControlBatchSetting.Controls.Add(tabBlankBorder);
            tabControlBatchSetting.Location = new Point(0, 0);
            tabControlBatchSetting.Name = "tabControlBatchSetting";
            tabControlBatchSetting.SelectedIndex = 0;
            tabControlBatchSetting.Size = new Size(520, 231);
            tabControlBatchSetting.TabIndex = 2;
            // 
            // tabList
            // 
            tabList.Controls.Add(lstBatchSetting);
            tabList.Location = new Point(4, 24);
            tabList.Name = "tabList";
            tabList.Padding = new Padding(3);
            tabList.Size = new Size(512, 203);
            tabList.TabIndex = 3;
            tabList.Text = "List Management";
            tabList.UseVisualStyleBackColor = true;
            // 
            // lstBatchSetting
            // 
            lstBatchSetting.DisplayMember = "BatchName";
            lstBatchSetting.Dock = DockStyle.Fill;
            lstBatchSetting.FormattingEnabled = true;
            lstBatchSetting.Location = new Point(3, 3);
            lstBatchSetting.Name = "lstBatchSetting";
            lstBatchSetting.Size = new Size(506, 197);
            lstBatchSetting.TabIndex = 0;
            lstBatchSetting.ValueMember = "BatchId";
            lstBatchSetting.Click += lstBatchSetting_SelectedIndexChanged;
            lstBatchSetting.DoubleClick += lstBatchSetting_DoubleClick;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(txtSeparateChar);
            tabGeneral.Controls.Add(label4);
            tabGeneral.Controls.Add(chkSubfolder);
            tabGeneral.Controls.Add(label3);
            tabGeneral.Controls.Add(txtIndexFormat);
            tabGeneral.Controls.Add(label1);
            tabGeneral.Controls.Add(txtBatchName);
            tabGeneral.Controls.Add(label2);
            tabGeneral.Controls.Add(btnSelectpath);
            tabGeneral.Controls.Add(lblExportPath);
            tabGeneral.Location = new Point(4, 24);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(3);
            tabGeneral.Size = new Size(512, 203);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtSeparateChar
            // 
            txtSeparateChar.Location = new Point(97, 70);
            txtSeparateChar.Name = "txtSeparateChar";
            txtSeparateChar.Size = new Size(384, 23);
            txtSeparateChar.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 73);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 8;
            label4.Text = "Separate char";
            // 
            // chkSubfolder
            // 
            chkSubfolder.AutoSize = true;
            chkSubfolder.Location = new Point(99, 45);
            chkSubfolder.Name = "chkSubfolder";
            chkSubfolder.Size = new Size(205, 19);
            chkSubfolder.TabIndex = 7;
            chkSubfolder.Text = "Create subfolder with batch name";
            chkSubfolder.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(97, 134);
            label3.Name = "label3";
            label3.Size = new Size(258, 15);
            label3.TabIndex = 6;
            label3.Text = "Input only 0 (ZERO). For example: 000, 00000, ....";
            // 
            // txtIndexFormat
            // 
            txtIndexFormat.Location = new Point(97, 108);
            txtIndexFormat.Name = "txtIndexFormat";
            txtIndexFormat.Size = new Size(384, 23);
            txtIndexFormat.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 111);
            label1.Name = "label1";
            label1.Size = new Size(74, 15);
            label1.TabIndex = 4;
            label1.Text = "Index format";
            // 
            // txtBatchName
            // 
            txtBatchName.Location = new Point(97, 16);
            txtBatchName.Name = "txtBatchName";
            txtBatchName.Size = new Size(384, 23);
            txtBatchName.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 19);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 2;
            label2.Text = "Batch Name";
            // 
            // tabSeparate
            // 
            tabSeparate.Controls.Add(rdSheet);
            tabSeparate.Controls.Add(rdBlankSheet);
            tabSeparate.Controls.Add(txtBlankValue);
            tabSeparate.Controls.Add(chkBlankPage);
            tabSeparate.Controls.Add(rdTotalPages);
            tabSeparate.Controls.Add(txtPages);
            tabSeparate.Controls.Add(chkBlankSheet);
            tabSeparate.Location = new Point(4, 24);
            tabSeparate.Name = "tabSeparate";
            tabSeparate.Padding = new Padding(3);
            tabSeparate.Size = new Size(512, 203);
            tabSeparate.TabIndex = 1;
            tabSeparate.Text = "Separate mode";
            tabSeparate.UseVisualStyleBackColor = true;
            // 
            // rdSheet
            // 
            rdSheet.AutoSize = true;
            rdSheet.Checked = true;
            rdSheet.Location = new Point(9, 6);
            rdSheet.Margin = new Padding(4, 3, 4, 3);
            rdSheet.Name = "rdSheet";
            rdSheet.Size = new Size(84, 19);
            rdSheet.TabIndex = 0;
            rdSheet.TabStop = true;
            rdSheet.Text = "Every sheet";
            rdSheet.UseVisualStyleBackColor = true;
            // 
            // rdBlankSheet
            // 
            rdBlankSheet.AutoSize = true;
            rdBlankSheet.Location = new Point(9, 76);
            rdBlankSheet.Margin = new Padding(4, 3, 4, 3);
            rdBlankSheet.Name = "rdBlankSheet";
            rdBlankSheet.Size = new Size(85, 19);
            rdBlankSheet.TabIndex = 1;
            rdBlankSheet.Text = "Blank sheet";
            rdBlankSheet.UseVisualStyleBackColor = true;
            // 
            // txtBlankValue
            // 
            txtBlankValue.Location = new Point(109, 73);
            txtBlankValue.Margin = new Padding(4, 3, 4, 3);
            txtBlankValue.Name = "txtBlankValue";
            txtBlankValue.Size = new Size(111, 23);
            txtBlankValue.TabIndex = 2;
            txtBlankValue.Text = "0.002";
            // 
            // chkBlankPage
            // 
            chkBlankPage.AutoSize = true;
            chkBlankPage.Location = new Point(9, 109);
            chkBlankPage.Margin = new Padding(4, 3, 4, 3);
            chkBlankPage.Name = "chkBlankPage";
            chkBlankPage.Size = new Size(126, 19);
            chkBlankPage.TabIndex = 3;
            chkBlankPage.Text = "Discard blank page";
            chkBlankPage.UseVisualStyleBackColor = true;
            // 
            // rdTotalPages
            // 
            rdTotalPages.AutoSize = true;
            rdTotalPages.Location = new Point(9, 39);
            rdTotalPages.Margin = new Padding(4, 3, 4, 3);
            rdTotalPages.Name = "rdTotalPages";
            rdTotalPages.Size = new Size(85, 19);
            rdTotalPages.TabIndex = 4;
            rdTotalPages.Text = "Total pages";
            rdTotalPages.UseVisualStyleBackColor = true;
            // 
            // txtPages
            // 
            txtPages.Location = new Point(109, 39);
            txtPages.Margin = new Padding(4, 3, 4, 3);
            txtPages.Name = "txtPages";
            txtPages.Size = new Size(111, 23);
            txtPages.TabIndex = 5;
            txtPages.Text = "4";
            // 
            // chkBlankSheet
            // 
            chkBlankSheet.AutoSize = true;
            chkBlankSheet.Location = new Point(9, 135);
            chkBlankSheet.Margin = new Padding(4, 3, 4, 3);
            chkBlankSheet.Name = "chkBlankSheet";
            chkBlankSheet.Size = new Size(114, 19);
            chkBlankSheet.TabIndex = 6;
            chkBlankSheet.Text = "Hide blank sheet";
            chkBlankSheet.UseVisualStyleBackColor = true;
            // 
            // tabBlankBorder
            // 
            tabBlankBorder.Controls.Add(label8);
            tabBlankBorder.Controls.Add(label7);
            tabBlankBorder.Controls.Add(label6);
            tabBlankBorder.Controls.Add(label5);
            tabBlankBorder.Controls.Add(btnAll);
            tabBlankBorder.Controls.Add(btnRollbackAll);
            tabBlankBorder.Controls.Add(btnRollback);
            tabBlankBorder.Controls.Add(btnApplyAll);
            tabBlankBorder.Controls.Add(btnApply);
            tabBlankBorder.Controls.Add(btnPreviewBorder);
            tabBlankBorder.Controls.Add(txtBottomBorder);
            tabBlankBorder.Controls.Add(txtRightBorder);
            tabBlankBorder.Controls.Add(txtLeftBorder);
            tabBlankBorder.Controls.Add(txtTopBorder);
            tabBlankBorder.Location = new Point(4, 24);
            tabBlankBorder.Name = "tabBlankBorder";
            tabBlankBorder.Size = new Size(512, 203);
            tabBlankBorder.TabIndex = 2;
            tabBlankBorder.Text = "Remove black border";
            tabBlankBorder.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(71, 120);
            label8.Name = "label8";
            label8.Size = new Size(47, 15);
            label8.TabIndex = 29;
            label8.Text = "Bottom";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(146, 67);
            label7.Name = "label7";
            label7.Size = new Size(35, 15);
            label7.TabIndex = 28;
            label7.Text = "Right";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 67);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 27;
            label6.Text = "Left";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(79, 16);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 26;
            label5.Text = "Top";
            // 
            // btnAll
            // 
            btnAll.Location = new Point(146, 35);
            btnAll.Name = "btnAll";
            btnAll.Size = new Size(29, 23);
            btnAll.TabIndex = 25;
            btnAll.Text = "All";
            btnAll.UseVisualStyleBackColor = true;
            btnAll.Click += btnAll_Click;
            // 
            // btnRollbackAll
            // 
            btnRollbackAll.Location = new Point(288, 91);
            btnRollbackAll.Margin = new Padding(4, 3, 4, 3);
            btnRollbackAll.Name = "btnRollbackAll";
            btnRollbackAll.Size = new Size(79, 28);
            btnRollbackAll.TabIndex = 24;
            btnRollbackAll.Text = "Rollback all";
            btnRollbackAll.UseVisualStyleBackColor = true;
            // 
            // btnRollback
            // 
            btnRollback.Location = new Point(288, 60);
            btnRollback.Margin = new Padding(4, 3, 4, 3);
            btnRollback.Name = "btnRollback";
            btnRollback.Size = new Size(79, 28);
            btnRollback.TabIndex = 23;
            btnRollback.Text = "Rollback";
            btnRollback.UseVisualStyleBackColor = true;
            // 
            // btnApplyAll
            // 
            btnApplyAll.Location = new Point(206, 91);
            btnApplyAll.Margin = new Padding(4, 3, 4, 3);
            btnApplyAll.Name = "btnApplyAll";
            btnApplyAll.Size = new Size(79, 28);
            btnApplyAll.TabIndex = 22;
            btnApplyAll.Text = "Apply all";
            btnApplyAll.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(206, 60);
            btnApply.Margin = new Padding(4, 3, 4, 3);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(79, 28);
            btnApply.TabIndex = 21;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            // 
            // btnPreviewBorder
            // 
            btnPreviewBorder.Location = new Point(206, 29);
            btnPreviewBorder.Margin = new Padding(4, 3, 4, 3);
            btnPreviewBorder.Name = "btnPreviewBorder";
            btnPreviewBorder.Size = new Size(161, 28);
            btnPreviewBorder.TabIndex = 20;
            btnPreviewBorder.Text = "Preview";
            btnPreviewBorder.UseVisualStyleBackColor = true;
            // 
            // txtBottomBorder
            // 
            txtBottomBorder.Location = new Point(37, 94);
            txtBottomBorder.Margin = new Padding(4, 3, 4, 3);
            txtBottomBorder.Name = "txtBottomBorder";
            txtBottomBorder.Size = new Size(102, 23);
            txtBottomBorder.TabIndex = 19;
            txtBottomBorder.Text = "0";
            // 
            // txtRightBorder
            // 
            txtRightBorder.Location = new Point(92, 64);
            txtRightBorder.Margin = new Padding(4, 3, 4, 3);
            txtRightBorder.Name = "txtRightBorder";
            txtRightBorder.Size = new Size(47, 23);
            txtRightBorder.TabIndex = 18;
            txtRightBorder.Text = "0";
            // 
            // txtLeftBorder
            // 
            txtLeftBorder.Location = new Point(38, 64);
            txtLeftBorder.Margin = new Padding(4, 3, 4, 3);
            txtLeftBorder.Name = "txtLeftBorder";
            txtLeftBorder.Size = new Size(47, 23);
            txtLeftBorder.TabIndex = 17;
            txtLeftBorder.Text = "0";
            // 
            // txtTopBorder
            // 
            txtTopBorder.Location = new Point(37, 34);
            txtTopBorder.Margin = new Padding(4, 3, 4, 3);
            txtTopBorder.Name = "txtTopBorder";
            txtTopBorder.Size = new Size(102, 23);
            txtTopBorder.TabIndex = 16;
            txtTopBorder.Text = "0";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(417, 237);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(92, 35);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnDel
            // 
            btnDel.Location = new Point(5, 245);
            btnDel.Name = "btnDel";
            btnDel.Size = new Size(50, 27);
            btnDel.TabIndex = 5;
            btnDel.Text = "Delete";
            btnDel.UseVisualStyleBackColor = true;
            btnDel.Click += btnDel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(61, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(56, 27);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnSaveAs
            // 
            btnSaveAs.Location = new Point(123, 245);
            btnSaveAs.Name = "btnSaveAs";
            btnSaveAs.Size = new Size(56, 27);
            btnSaveAs.TabIndex = 7;
            btnSaveAs.Text = "Save As";
            btnSaveAs.UseVisualStyleBackColor = true;
            btnSaveAs.Click += btnSaveAs_Click;
            // 
            // BatchSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 284);
            Controls.Add(btnSaveAs);
            Controls.Add(btnSave);
            Controls.Add(btnDel);
            Controls.Add(btnOK);
            Controls.Add(tabControlBatchSetting);
            Name = "BatchSetting";
            Text = "Batch Setting";
            Load += BatchSetting_Load;
            tabControlBatchSetting.ResumeLayout(false);
            tabList.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            tabGeneral.PerformLayout();
            tabSeparate.ResumeLayout(false);
            tabSeparate.PerformLayout();
            tabBlankBorder.ResumeLayout(false);
            tabBlankBorder.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblExportPath;
        private Button btnSelectpath;
        private FolderBrowserDialog folderBrowserDialog1;
        private TabControl tabControlBatchSetting;
        private TabPage tabGeneral;
        private TabPage tabSeparate;
        private TabPage tabBlankBorder;
        private RadioButton rdSheet;
        private RadioButton rdBlankSheet;
        private TextBox txtBlankValue;
        private CheckBox chkBlankPage;
        private RadioButton rdTotalPages;
        private TextBox txtPages;
        private CheckBox chkBlankSheet;
        private Button btnRollbackAll;
        private Button btnRollback;
        private Button btnApplyAll;
        private Button btnApply;
        private Button btnPreviewBorder;
        private TextBox txtBottomBorder;
        private TextBox txtRightBorder;
        private TextBox txtLeftBorder;
        private TextBox txtTopBorder;
        private TextBox txtBatchName;
        private Label label2;
        private Button btnOK;
        private Label label3;
        private TextBox txtIndexFormat;
        private Label label1;
        private CheckBox chkSubfolder;
        private TextBox txtSeparateChar;
        private Label label4;
        private TabPage tabList;
        private ListBox lstBatchSetting;
        private Button btnAll;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Button btnDel;
        private Button btnSave;
        private Button btnSaveAs;
    }
}