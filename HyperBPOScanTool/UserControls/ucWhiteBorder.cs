using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.UserControls
{
    public partial class ucWhiteBorder : UserControl
    {
        public ucWhiteBorder()
        {
            InitializeComponent();
        }
        public WhiteBorderObject GetWhiteBorderData()
        {
            WhiteBorderObject whiteBorderObject = new WhiteBorderObject
            {
                Top = txtTopBorder.Text == "" ? 0 : Convert.ToInt32(txtTopBorder.Text),
                Right = txtRightBorder.Text == "" ? 0 : Convert.ToInt32(txtRightBorder.Text),
                Bottom = txtBottomBorder.Text == "" ? 0 : Convert.ToInt32(txtBottomBorder.Text),
                Left = txtLeftBorder.Text == "" ? 0 : Convert.ToInt32(txtLeftBorder.Text)
            };
            return whiteBorderObject;
        }
        public Action<WhiteBorderObject> CallPreview;
        public Action<WhiteBorderObject> CallApply;
        public Action<WhiteBorderObject> CallApplyAll;
        private void btnPreviewBorder_Click(object sender, EventArgs e)
        {
            CallPreview?.Invoke(GetWhiteBorderData());
        }
        private WhiteBorderObject _whiteBorderData;
        public void SetWhiteBorderData(WhiteBorderObject data)
        {
            _whiteBorderData = data;
            txtTopBorder.Text = data.Top.ToString();
            txtRightBorder.Text = data.Right.ToString();
            txtBottomBorder.Text = data.Bottom.ToString();
            txtLeftBorder.Text = data.Left.ToString();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            CallApply?.Invoke(GetWhiteBorderData());
            this.ParentForm.Close();
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            CallApplyAll?.Invoke(GetWhiteBorderData());
            this.ParentForm.Close();
        }

        
    }
}
