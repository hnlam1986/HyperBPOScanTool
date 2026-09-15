namespace HyperBPOScanTool.UserControls
{
    partial class ucWhiteBorder
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
            btnRollbackAll = new Button();
            btnRollback = new Button();
            btnApplyAll = new Button();
            btnApply = new Button();
            btnPreviewBorder = new Button();
            txtBottomBorder = new TextBox();
            txtRightBorder = new TextBox();
            txtLeftBorder = new TextBox();
            txtTopBorder = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // btnRollbackAll
            // 
            btnRollbackAll.Location = new Point(292, 78);
            btnRollbackAll.Margin = new Padding(4, 3, 4, 3);
            btnRollbackAll.Name = "btnRollbackAll";
            btnRollbackAll.Size = new Size(79, 28);
            btnRollbackAll.TabIndex = 24;
            btnRollbackAll.Text = "Rollback all";
            btnRollbackAll.UseVisualStyleBackColor = true;
            // 
            // btnRollback
            // 
            btnRollback.Location = new Point(292, 48);
            btnRollback.Margin = new Padding(4, 3, 4, 3);
            btnRollback.Name = "btnRollback";
            btnRollback.Size = new Size(79, 28);
            btnRollback.TabIndex = 23;
            btnRollback.Text = "Rollback";
            btnRollback.UseVisualStyleBackColor = true;
            // 
            // btnApplyAll
            // 
            btnApplyAll.Location = new Point(212, 78);
            btnApplyAll.Margin = new Padding(4, 3, 4, 3);
            btnApplyAll.Name = "btnApplyAll";
            btnApplyAll.Size = new Size(79, 28);
            btnApplyAll.TabIndex = 22;
            btnApplyAll.Text = "Apply all";
            btnApplyAll.UseVisualStyleBackColor = true;
            btnApplyAll.Click += btnApplyAll_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(212, 48);
            btnApply.Margin = new Padding(4, 3, 4, 3);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(79, 28);
            btnApply.TabIndex = 21;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnPreviewBorder
            // 
            btnPreviewBorder.Location = new Point(212, 18);
            btnPreviewBorder.Margin = new Padding(4, 3, 4, 3);
            btnPreviewBorder.Name = "btnPreviewBorder";
            btnPreviewBorder.Size = new Size(159, 28);
            btnPreviewBorder.TabIndex = 20;
            btnPreviewBorder.Text = "Preview";
            btnPreviewBorder.UseVisualStyleBackColor = true;
            btnPreviewBorder.Click += btnPreviewBorder_Click;
            // 
            // txtBottomBorder
            // 
            txtBottomBorder.Location = new Point(38, 82);
            txtBottomBorder.Margin = new Padding(4, 3, 4, 3);
            txtBottomBorder.Name = "txtBottomBorder";
            txtBottomBorder.Size = new Size(102, 23);
            txtBottomBorder.TabIndex = 19;
            txtBottomBorder.Text = "0";
            txtBottomBorder.TextAlign = HorizontalAlignment.Center;
            // 
            // txtRightBorder
            // 
            txtRightBorder.Location = new Point(93, 52);
            txtRightBorder.Margin = new Padding(4, 3, 4, 3);
            txtRightBorder.Name = "txtRightBorder";
            txtRightBorder.Size = new Size(47, 23);
            txtRightBorder.TabIndex = 18;
            txtRightBorder.Text = "0";
            txtRightBorder.TextAlign = HorizontalAlignment.Center;
            // 
            // txtLeftBorder
            // 
            txtLeftBorder.Location = new Point(39, 52);
            txtLeftBorder.Margin = new Padding(4, 3, 4, 3);
            txtLeftBorder.Name = "txtLeftBorder";
            txtLeftBorder.Size = new Size(47, 23);
            txtLeftBorder.TabIndex = 17;
            txtLeftBorder.Text = "0";
            txtLeftBorder.TextAlign = HorizontalAlignment.Center;
            // 
            // txtTopBorder
            // 
            txtTopBorder.Location = new Point(38, 22);
            txtTopBorder.Margin = new Padding(4, 3, 4, 3);
            txtTopBorder.Name = "txtTopBorder";
            txtTopBorder.Size = new Size(102, 23);
            txtTopBorder.TabIndex = 16;
            txtTopBorder.Text = "0";
            txtTopBorder.TextAlign = HorizontalAlignment.Center;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 4);
            label1.Name = "label1";
            label1.Size = new Size(27, 15);
            label1.TabIndex = 25;
            label1.Text = "Top";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 55);
            label2.Name = "label2";
            label2.Size = new Size(27, 15);
            label2.TabIndex = 26;
            label2.Text = "Left";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(147, 55);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 27;
            label3.Text = "Right";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(63, 108);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 28;
            label4.Text = "Bottom";
            // 
            // ucWhiteBorder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRollbackAll);
            Controls.Add(btnRollback);
            Controls.Add(btnApplyAll);
            Controls.Add(btnApply);
            Controls.Add(btnPreviewBorder);
            Controls.Add(txtBottomBorder);
            Controls.Add(txtRightBorder);
            Controls.Add(txtLeftBorder);
            Controls.Add(txtTopBorder);
            Name = "ucWhiteBorder";
            Size = new Size(378, 127);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRollbackAll;
        private Button btnRollback;
        private Button btnApplyAll;
        private Button btnApply;
        private Button btnPreviewBorder;
        private TextBox txtBottomBorder;
        private TextBox txtRightBorder;
        private TextBox txtLeftBorder;
        private TextBox txtTopBorder;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
