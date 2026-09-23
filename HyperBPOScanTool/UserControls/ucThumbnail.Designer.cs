namespace HyperBPOScanTool.UserControls
{
    partial class ucThumbnail
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
            pbThumbnail = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).BeginInit();
            SuspendLayout();
            // 
            // pbThumbnail
            // 
            pbThumbnail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pbThumbnail.BackColor = SystemColors.Control;
            pbThumbnail.Location = new Point(2, 2);
            pbThumbnail.Name = "pbThumbnail";
            pbThumbnail.Size = new Size(294, 294);
            pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            pbThumbnail.TabIndex = 0;
            pbThumbnail.TabStop = false;
            pbThumbnail.DoubleClick += pbThumbnail_DoubleClick;
            // 
            // ucThumbnail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pbThumbnail);
            Name = "ucThumbnail";
            Size = new Size(300, 300);
            Click += ucThumbnail_Click;
            ((System.ComponentModel.ISupportInitialize)pbThumbnail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbThumbnail;
    }
}
