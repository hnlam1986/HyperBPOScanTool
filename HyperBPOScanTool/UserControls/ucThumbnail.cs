using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.UserControls
{
    public partial class ucThumbnail : UserControl
    {
        public event EventHandler PictureBoxDoubleClicked;
        public ucThumbnail()
        {
            InitializeComponent();
            pbThumbnail.Click += (s, e) => UserControlEventBus.PublishClick(this);
            UserControlEventBus.OnUserControlClicked += UserControlEventBus_OnUserControlClicked;
        }

        

        private void UserControlEventBus_OnUserControlClicked(object sender)
        {
            bool isCtrlPressed = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            if (sender == this)
            {
                IsSelected = !IsSelected;
                if (IsSelected)
                {
                    this.BackColor = Color.FromArgb(255, 0, 120, 215);
                }
                else
                {
                    this.BackColor = Color.Transparent;
                }
            }
            else if(isCtrlPressed==false)
            {
                this.BackColor = Color.Transparent;
                IsSelected = false;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Image
        {
            get => pbThumbnail.Image;
            set => pbThumbnail.Image = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScanPage ScanPage { get; set; }
        private void ucThumbnail_Click(object sender, EventArgs e)
        {

        }

        private void pbThumbnail_DoubleClick(object sender, EventArgs e)
        {
            PictureBoxDoubleClicked?.Invoke(this, e);
        }
    }
}
