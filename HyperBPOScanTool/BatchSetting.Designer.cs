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
            label1 = new Label();
            btnSelectpath = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            chkBlankSheet = new CheckBox();
            txtPages = new TextBox();
            rdTotalPages = new RadioButton();
            chkBlankPage = new CheckBox();
            txtBlankValue = new TextBox();
            rdBlankSheet = new RadioButton();
            rdSheet = new RadioButton();
            btnRollbackAll = new Button();
            btnRollback = new Button();
            btnApplyAll = new Button();
            btnApply = new Button();
            btnPreviewBorder = new Button();
            txtBottomBorder = new TextBox();
            txtRightBorder = new TextBox();
            txtLeftBorder = new TextBox();
            txtTopBorder = new TextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(111, 19);
            label1.Name = "label1";
            label1.Size = new Size(16, 15);
            label1.TabIndex = 0;
            label1.Text = "...";
            // 
            // btnSelectpath
            // 
            btnSelectpath.Location = new Point(15, 15);
            btnSelectpath.Name = "btnSelectpath";
            btnSelectpath.Size = new Size(90, 23);
            btnSelectpath.TabIndex = 1;
            btnSelectpath.Text = "Export Folder";
            btnSelectpath.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 730);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnSelectpath);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 422);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
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
            tabPage2.Size = new Size(792, 702);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Separate mode";
            tabPage2.UseVisualStyleBackColor = true;
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
            tabPage3.Size = new Size(792, 702);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Remove black border";
            tabPage3.UseVisualStyleBackColor = true;
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
            // txtPages
            // 
            txtPages.Location = new Point(109, 39);
            txtPages.Margin = new Padding(4, 3, 4, 3);
            txtPages.Name = "txtPages";
            txtPages.Size = new Size(111, 23);
            txtPages.TabIndex = 5;
            txtPages.Text = "4";
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
            // chkBlankPage
            // 
            chkBlankPage.AutoSize = true;
            chkBlankPage.Location = new Point(9, 109);
            chkBlankPage.Margin = new Padding(4, 3, 4, 3);
            chkBlankPage.Name = "chkBlankPage";
            chkBlankPage.Size = new Size(112, 19);
            chkBlankPage.TabIndex = 3;
            chkBlankPage.Text = "Hide blank page";
            chkBlankPage.UseVisualStyleBackColor = true;
            // 
            // txtBlankValue
            // 
            txtBlankValue.Location = new Point(109, 73);
            txtBlankValue.Margin = new Padding(4, 3, 4, 3);
            txtBlankValue.Name = "txtBlankValue";
            txtBlankValue.Size = new Size(111, 23);
            txtBlankValue.TabIndex = 2;
            txtBlankValue.Text = "15.0";
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
            // BatchSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 730);
            Controls.Add(tabControl1);
            Name = "BatchSetting";
            Text = "Batch Setting";
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

        private Label label1;
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
    }
}