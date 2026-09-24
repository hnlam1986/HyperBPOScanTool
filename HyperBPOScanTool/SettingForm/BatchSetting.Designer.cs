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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            chkSubfolder = new CheckBox();
            label3 = new Label();
            txtIndexFormat = new TextBox();
            label1 = new Label();
            txtBatchName = new TextBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            rdSheet = new RadioButton();
            rdBlankSheet = new RadioButton();
            txtBlankValue = new TextBox();
            chkBlankPage = new CheckBox();
            rdTotalPages = new RadioButton();
            txtPages = new TextBox();
            chkBlankSheet = new CheckBox();
            tabPage3 = new TabPage();
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
            txtSeparateChar = new TextBox();
            label4 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
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
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(520, 231);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtSeparateChar);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(chkSubfolder);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(txtIndexFormat);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(txtBatchName);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(btnSelectpath);
            tabPage1.Controls.Add(lblExportPath);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(512, 203);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
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
            // tabPage2
            // 
            tabPage2.Controls.Add(rdSheet);
            tabPage2.Controls.Add(rdBlankSheet);
            tabPage2.Controls.Add(txtBlankValue);
            tabPage2.Controls.Add(chkBlankPage);
            tabPage2.Controls.Add(rdTotalPages);
            tabPage2.Controls.Add(txtPages);
            tabPage2.Controls.Add(chkBlankSheet);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(512, 187);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Separate mode";
            tabPage2.UseVisualStyleBackColor = true;
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
            // tabPage3
            // 
            tabPage3.Controls.Add(btnRollbackAll);
            tabPage3.Controls.Add(btnRollback);
            tabPage3.Controls.Add(btnApplyAll);
            tabPage3.Controls.Add(btnApply);
            tabPage3.Controls.Add(btnPreviewBorder);
            tabPage3.Controls.Add(txtBottomBorder);
            tabPage3.Controls.Add(txtRightBorder);
            tabPage3.Controls.Add(txtLeftBorder);
            tabPage3.Controls.Add(txtTopBorder);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(512, 187);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Remove black border";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnRollbackAll
            // 
            btnRollbackAll.Location = new Point(187, 73);
            btnRollbackAll.Margin = new Padding(4, 3, 4, 3);
            btnRollbackAll.Name = "btnRollbackAll";
            btnRollbackAll.Size = new Size(47, 28);
            btnRollbackAll.TabIndex = 24;
            btnRollbackAll.Text = "RbA";
            btnRollbackAll.UseVisualStyleBackColor = true;
            // 
            // btnRollback
            // 
            btnRollback.Location = new Point(187, 43);
            btnRollback.Margin = new Padding(4, 3, 4, 3);
            btnRollback.Name = "btnRollback";
            btnRollback.Size = new Size(47, 28);
            btnRollback.TabIndex = 23;
            btnRollback.Text = "Rb";
            btnRollback.UseVisualStyleBackColor = true;
            // 
            // btnApplyAll
            // 
            btnApplyAll.Location = new Point(119, 73);
            btnApplyAll.Margin = new Padding(4, 3, 4, 3);
            btnApplyAll.Name = "btnApplyAll";
            btnApplyAll.Size = new Size(63, 28);
            btnApplyAll.TabIndex = 22;
            btnApplyAll.Text = "Apply all";
            btnApplyAll.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(119, 43);
            btnApply.Margin = new Padding(4, 3, 4, 3);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(63, 28);
            btnApply.TabIndex = 21;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            // 
            // btnPreviewBorder
            // 
            btnPreviewBorder.Location = new Point(119, 13);
            btnPreviewBorder.Margin = new Padding(4, 3, 4, 3);
            btnPreviewBorder.Name = "btnPreviewBorder";
            btnPreviewBorder.Size = new Size(114, 28);
            btnPreviewBorder.TabIndex = 20;
            btnPreviewBorder.Text = "Preview";
            btnPreviewBorder.UseVisualStyleBackColor = true;
            // 
            // txtBottomBorder
            // 
            txtBottomBorder.Location = new Point(9, 77);
            txtBottomBorder.Margin = new Padding(4, 3, 4, 3);
            txtBottomBorder.Name = "txtBottomBorder";
            txtBottomBorder.Size = new Size(102, 23);
            txtBottomBorder.TabIndex = 19;
            txtBottomBorder.Text = "0";
            // 
            // txtRightBorder
            // 
            txtRightBorder.Location = new Point(64, 47);
            txtRightBorder.Margin = new Padding(4, 3, 4, 3);
            txtRightBorder.Name = "txtRightBorder";
            txtRightBorder.Size = new Size(47, 23);
            txtRightBorder.TabIndex = 18;
            txtRightBorder.Text = "0";
            // 
            // txtLeftBorder
            // 
            txtLeftBorder.Location = new Point(10, 47);
            txtLeftBorder.Margin = new Padding(4, 3, 4, 3);
            txtLeftBorder.Name = "txtLeftBorder";
            txtLeftBorder.Size = new Size(47, 23);
            txtLeftBorder.TabIndex = 17;
            txtLeftBorder.Text = "0";
            // 
            // txtTopBorder
            // 
            txtTopBorder.Location = new Point(9, 17);
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
            // BatchSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 284);
            Controls.Add(btnOK);
            Controls.Add(tabControl1);
            Name = "BatchSetting";
            Text = "Batch Setting";
            Load += BatchSetting_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblExportPath;
        private Button btnSelectpath;
        private FolderBrowserDialog folderBrowserDialog1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
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
    }
}