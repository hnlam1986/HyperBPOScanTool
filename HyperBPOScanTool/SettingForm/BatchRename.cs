using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.SettingForm
{
    public partial class BatchRename : Form
    {
        public BatchRename()
        {
            InitializeComponent();
        }

        private void BatchRename_Load(object sender, EventArgs e)
        {

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NewBatchName { get; set; }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            NewBatchName = txtName.Text;
        }
    }
}
