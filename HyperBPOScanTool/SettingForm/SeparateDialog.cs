using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.SettingForm
{
    public partial class SeparateDialog : Form
    {
        public SeparateDialog()
        {
            InitializeComponent();
        }
        public SeparateDialog(SeparateObject separate)
        {
            SeparatedDataObject = separate;
            InitializeComponent();
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SeparateObject SeparatedDataObject { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            SeparatedDataObject = ucSeparateMode.GetSeparateData();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SeparateDialog_Load(object sender, EventArgs e)
        {
            ucSeparateMode.SetSeparateData(SeparatedDataObject);
            ucSeparateMode.SetDisableSeparateMode(DisableSeparateMode);
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DisableSeparateMode { get; set; }
    }
}
