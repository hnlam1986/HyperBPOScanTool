using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool
{
    public partial class BatchSetting : Form
    {
        public BatchSetting()
        {
            InitializeComponent();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string BatchName { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ExportPath { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IndexFormat { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public string SeparateChar { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CreateSubFolder { get; set; }

        private void btnSelectpath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblExportPath.Text = folderBrowserDialog1.SelectedPath;
                ExportPath = folderBrowserDialog1.SelectedPath;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            BatchName = txtBatchName.Text;
            IndexFormat = txtIndexFormat.Text;
            SeparateChar = txtSeparateChar.Text;
            CreateSubFolder = chkSubfolder.Checked;
        }

        private void BatchSetting_Load(object sender, EventArgs e)
        {
             txtBatchName.Text = BatchName;
             txtIndexFormat.Text = IndexFormat;
            lblExportPath.Text = ExportPath;
            txtSeparateChar.Text = SeparateChar;
            chkSubfolder.Checked = CreateSubFolder;
        }
    }
}
