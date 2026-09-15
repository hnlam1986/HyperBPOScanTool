using HyperBPOScanTool.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.SettingForm
{
    public partial class WhiteBorderDialog : Form
    {
        public WhiteBorderDialog()
        {
            InitializeComponent();
        }
        public Action<WhiteBorderObject> CallPreview;
        public Action<WhiteBorderObject> CallApply;
        public Action<WhiteBorderObject> CallApplyAll;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WhiteBorderObject WhiteBorderData { get; set; }
        private void WhiteBorderDialog_Load(object sender, EventArgs e)
        {
            ucWhiteBorder1.SetWhiteBorderData(WhiteBorderData);
            ucWhiteBorder1.CallPreview = CallPreview;
            ucWhiteBorder1.CallApply = CallApply;
            ucWhiteBorder1.CallApplyAll = CallApplyAll;
        }
    }
}
