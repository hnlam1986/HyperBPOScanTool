namespace HyperBPOScanTool.SettingForm
{
    partial class WhiteBorderDialog
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
            ucWhiteBorder1 = new HyperBPOScanTool.UserControls.ucWhiteBorder();
            SuspendLayout();
            // 
            // ucWhiteBorder1
            // 
            ucWhiteBorder1.Location = new Point(12, 12);
            ucWhiteBorder1.Name = "ucWhiteBorder1";
            ucWhiteBorder1.Size = new Size(378, 127);
            ucWhiteBorder1.TabIndex = 0;
            // 
            // WhiteBorderDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 158);
            Controls.Add(ucWhiteBorder1);
            Name = "WhiteBorderDialog";
            Text = "WhiteBorderDialog";
            Load += WhiteBorderDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserControls.ucWhiteBorder ucWhiteBorder1;
    }
}