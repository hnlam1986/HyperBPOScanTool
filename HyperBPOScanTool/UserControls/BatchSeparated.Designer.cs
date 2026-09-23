namespace HyperBPOScanTool.UserControls
{
    partial class BatchSeparated
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rdSheet = new RadioButton();
            rdBlankSheet = new RadioButton();
            txtBlankValue = new TextBox();
            chkBlankPage = new CheckBox();
            rdTotalPages = new RadioButton();
            txtPages = new TextBox();
            chkBlankSheet = new CheckBox();
            rdQR = new RadioButton();
            button1 = new Button();
            gbSeparateMode = new GroupBox();
            groupBox2 = new GroupBox();
            gbSeparateMode.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // rdSheet
            // 
            rdSheet.AutoSize = true;
            rdSheet.Checked = true;
            rdSheet.Location = new Point(16, 22);
            rdSheet.Margin = new Padding(4, 3, 4, 3);
            rdSheet.Name = "rdSheet";
            rdSheet.Size = new Size(84, 19);
            rdSheet.TabIndex = 7;
            rdSheet.TabStop = true;
            rdSheet.Text = "Every sheet";
            rdSheet.UseVisualStyleBackColor = true;
            // 
            // rdBlankSheet
            // 
            rdBlankSheet.AutoSize = true;
            rdBlankSheet.Location = new Point(16, 92);
            rdBlankSheet.Margin = new Padding(4, 3, 4, 3);
            rdBlankSheet.Name = "rdBlankSheet";
            rdBlankSheet.Size = new Size(85, 19);
            rdBlankSheet.TabIndex = 8;
            rdBlankSheet.Text = "Blank sheet";
            rdBlankSheet.UseVisualStyleBackColor = true;
            // 
            // txtBlankValue
            // 
            txtBlankValue.Location = new Point(116, 89);
            txtBlankValue.Margin = new Padding(4, 3, 4, 3);
            txtBlankValue.Name = "txtBlankValue";
            txtBlankValue.Size = new Size(111, 23);
            txtBlankValue.TabIndex = 9;
            txtBlankValue.Text = "15.0";
            // 
            // chkBlankPage
            // 
            chkBlankPage.AutoSize = true;
            chkBlankPage.Location = new Point(16, 26);
            chkBlankPage.Margin = new Padding(4, 3, 4, 3);
            chkBlankPage.Name = "chkBlankPage";
            chkBlankPage.Size = new Size(120, 19);
            chkBlankPage.TabIndex = 10;
            chkBlankPage.Text = "Delete blank page";
            chkBlankPage.UseVisualStyleBackColor = true;
            // 
            // rdTotalPages
            // 
            rdTotalPages.AutoSize = true;
            rdTotalPages.Location = new Point(16, 55);
            rdTotalPages.Margin = new Padding(4, 3, 4, 3);
            rdTotalPages.Name = "rdTotalPages";
            rdTotalPages.Size = new Size(85, 19);
            rdTotalPages.TabIndex = 11;
            rdTotalPages.Text = "Total pages";
            rdTotalPages.UseVisualStyleBackColor = true;
            // 
            // txtPages
            // 
            txtPages.Location = new Point(116, 55);
            txtPages.Margin = new Padding(4, 3, 4, 3);
            txtPages.Name = "txtPages";
            txtPages.Size = new Size(111, 23);
            txtPages.TabIndex = 12;
            txtPages.Text = "4";
            // 
            // chkBlankSheet
            // 
            chkBlankSheet.AutoSize = true;
            chkBlankSheet.Location = new Point(16, 52);
            chkBlankSheet.Margin = new Padding(4, 3, 4, 3);
            chkBlankSheet.Name = "chkBlankSheet";
            chkBlankSheet.Size = new Size(114, 19);
            chkBlankSheet.TabIndex = 13;
            chkBlankSheet.Text = "Hide blank sheet";
            chkBlankSheet.UseVisualStyleBackColor = true;
            // 
            // rdQR
            // 
            rdQR.AutoSize = true;
            rdQR.Location = new Point(16, 125);
            rdQR.Margin = new Padding(4, 3, 4, 3);
            rdQR.Name = "rdQR";
            rdQR.Size = new Size(70, 19);
            rdQR.TabIndex = 14;
            rdQR.Text = "QR code";
            rdQR.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(116, 121);
            button1.Name = "button1";
            button1.Size = new Size(111, 23);
            button1.TabIndex = 15;
            button1.Text = "Download QR code";
            button1.UseVisualStyleBackColor = true;
            // 
            // gbSeparateMode
            // 
            gbSeparateMode.Controls.Add(rdSheet);
            gbSeparateMode.Controls.Add(button1);
            gbSeparateMode.Controls.Add(txtPages);
            gbSeparateMode.Controls.Add(rdQR);
            gbSeparateMode.Controls.Add(rdTotalPages);
            gbSeparateMode.Controls.Add(txtBlankValue);
            gbSeparateMode.Controls.Add(rdBlankSheet);
            gbSeparateMode.Location = new Point(3, 3);
            gbSeparateMode.Name = "gbSeparateMode";
            gbSeparateMode.Size = new Size(257, 163);
            gbSeparateMode.TabIndex = 16;
            gbSeparateMode.TabStop = false;
            gbSeparateMode.Text = "Separate mode";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(chkBlankSheet);
            groupBox2.Controls.Add(chkBlankPage);
            groupBox2.Location = new Point(266, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(154, 163);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Hide blank";
            // 
            // BatchSeparated
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox2);
            Controls.Add(gbSeparateMode);
            Name = "BatchSeparated";
            Size = new Size(425, 172);
            gbSeparateMode.ResumeLayout(false);
            gbSeparateMode.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RadioButton rdSheet;
        private RadioButton rdBlankSheet;
        private TextBox txtBlankValue;
        private CheckBox chkBlankPage;
        private RadioButton rdTotalPages;
        private TextBox txtPages;
        private CheckBox chkBlankSheet;
        private RadioButton rdQR;
        private Button button1;
        private GroupBox gbSeparateMode;
        private GroupBox groupBox2;
    }
}
