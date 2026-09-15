namespace HyperBPOScanTool.SettingForm
{
    partial class SeparateDialog
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
            ucSeparateMode = new HyperBPOScanTool.UserControls.BatchSeparated();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // ucSeparateMode
            // 
            ucSeparateMode.Location = new Point(12, 5);
            ucSeparateMode.Name = "ucSeparateMode";
            ucSeparateMode.Size = new Size(425, 172);
            ucSeparateMode.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(343, 183);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(90, 33);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(247, 183);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 33);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SeparateDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 227);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(ucSeparateMode);
            Name = "SeparateDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "SeparateDialog";
            TopMost = true;
            Load += SeparateDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserControls.BatchSeparated ucSeparateMode;
        private Button btnOK;
        private Button btnCancel;
    }
}