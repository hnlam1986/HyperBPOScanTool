namespace HyperBPOScanTool.SettingForm
{
    partial class BatchRename
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
            txtName = new TextBox();
            btnOK = new Button();
            SuspendLayout();
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 12);
            txtName.Name = "txtName";
            txtName.Size = new Size(359, 23);
            txtName.TabIndex = 0;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(159, 56);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // BatchRename
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(386, 91);
            Controls.Add(btnOK);
            Controls.Add(txtName);
            Name = "BatchRename";
            Text = "BatchRename";
            Load += BatchRename_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtName;
        private Button btnOK;
    }
}