using HyperBPOScanTool.Models;
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
        public BatchSettingObject CurrentBatchSetting { get; set; }
        private void btnSelectpath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblExportPath.Text = folderBrowserDialog1.SelectedPath;

            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            //CurrentBatchSetting = GetBatchSetting();

        }

        private void BatchSetting_Load(object sender, EventArgs e)
        {
            if (File.Exists("batchsetting.json"))
            {
                string json = File.ReadAllText("batchsetting.json");
                if (json != "")
                {
                    _lstBatchSetting = System.Text.Json.JsonSerializer.Deserialize<List<BatchSettingObject>>(json);
                    RefreshListBatch();
                }
                // Load batch settings from JSON file
            }
            if (CurrentBatchSetting != null)
            {
                LoadBatchSetting(CurrentBatchSetting);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            txtLeftBorder.Text = txtRightBorder.Text = txtBottomBorder.Text = txtTopBorder.Text;
        }

        private BatchSettingObject GetBatchSetting()
        {
            BatchSettingObject batch = new BatchSettingObject
            {
                BatchId = DateTime.Now.Ticks.ToString(),
                BatchName = txtBatchName.Text,
                ExportFolder = lblExportPath.Text,
                IndexFormat = txtIndexFormat.Text,
                SeparateChar = txtSeparateChar.Text,
                HasCreateSubFolder = chkSubfolder.Checked,
                WhiteBorderTop = int.Parse(txtTopBorder.Text),
                WhiteBorderRight = int.Parse(txtRightBorder.Text),
                WhiteBorderBottom = int.Parse(txtBottomBorder.Text),
                WhiteBorderLeft = int.Parse(txtLeftBorder.Text),
                IsDiscardBlankSheets = chkBlankPage.Checked,
                SplitPageCount = int.Parse(txtPages.Text),
                BlankSheetThreshold = decimal.Parse(txtBlankValue.Text)
            };
            if (rdSheet.Checked)
            {
                batch.SeparateType = SeparateType.Persheet;// Handle sheet-specific logic
            }
            else if (rdTotalPages.Checked)
            {
                batch.SeparateType = SeparateType.NumOfPage;// Handle sheet-specific logic
            }
            else if (rdBlankSheet.Checked)
            {
                batch.SeparateType = SeparateType.BlankSheet;// Handle sheet-specific logic
            }
            return batch;
        }
        List<BatchSettingObject> _lstBatchSetting = new List<BatchSettingObject>();
        private void CallSave(bool isSaveAs = false)
        {
            BatchSettingObject batch = GetBatchSetting();
            if (CurrentBatchSetting != null && !isSaveAs)
            {
                batch.BatchId = CurrentBatchSetting.BatchId;
                CurrentBatchSetting = batch;
                _lstBatchSetting.Where(x => x.BatchId == batch.BatchId).ToList().ForEach(x =>
                {
                    x.BatchName = batch.BatchName;
                    x.ExportFolder = batch.ExportFolder;
                    x.IndexFormat = batch.IndexFormat;
                    x.SeparateChar = batch.SeparateChar;
                    x.HasCreateSubFolder = batch.HasCreateSubFolder;
                    x.WhiteBorderTop = batch.WhiteBorderTop;
                    x.WhiteBorderRight = batch.WhiteBorderRight;
                    x.WhiteBorderBottom = batch.WhiteBorderBottom;
                    x.WhiteBorderLeft = batch.WhiteBorderLeft;
                    x.IsDiscardBlankSheets = batch.IsDiscardBlankSheets;
                    x.SplitPageCount = batch.SplitPageCount;
                    x.BlankSheetThreshold = batch.BlankSheetThreshold;
                    x.SeparateType = batch.SeparateType;
                });
            }
            else
            {
                _lstBatchSetting.Add(batch);
            }
            if (_lstBatchSetting.Count > 0)
            {
                File.WriteAllText("batchsetting.json", System.Text.Json.JsonSerializer.Serialize(_lstBatchSetting));
            }
            RefreshListBatch();
            tabControlBatchSetting.SelectedIndex = 0;
            tabList.Select();
            if (isSaveAs)
            {
                lstBatchSetting.SelectedIndex = lstBatchSetting.Items.Count - 1;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            CallSave();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentBatchSetting = null;
            txtBatchName.Text = "";
            lblExportPath.Text = "";
            txtIndexFormat.Text = "";
            txtSeparateChar.Text = "";
            chkSubfolder.Checked = false;
            txtTopBorder.Text = "30";
            txtRightBorder.Text = "30";
            txtBottomBorder.Text = "30";
            txtLeftBorder.Text = "30";
            chkBlankSheet.Checked = false;
            txtPages.Text = "2";
            txtBlankValue.Text = "0.002";

        }
        private void LoadBatchSetting(BatchSettingObject setting)
        {
            if (setting != null)
            {
                txtBatchName.Text = setting.BatchName;
                txtIndexFormat.Text = setting.IndexFormat;
                lblExportPath.Text = setting.ExportFolder;
                txtSeparateChar.Text = setting.SeparateChar;
                chkSubfolder.Checked = setting.HasCreateSubFolder;
                txtTopBorder.Text = setting.WhiteBorderTop.ToString();
                txtRightBorder.Text = setting.WhiteBorderRight.ToString();
                txtBottomBorder.Text = setting.WhiteBorderBottom.ToString();
                txtLeftBorder.Text = setting.WhiteBorderLeft.ToString();
                //chkBlankSheet.Checked = setting.IsDiscardBlankSheets;
                txtPages.Text = setting.SplitPageCount.ToString();
                txtBlankValue.Text = setting.BlankSheetThreshold.ToString();
                chkBlankPage.Checked = setting.IsDiscardBlankSheets;
                switch (setting.SeparateType)
                {
                    case SeparateType.Persheet:
                        rdSheet.Checked = true;
                        break;
                    case SeparateType.NumOfPage:
                        rdTotalPages.Checked = true;
                        break;
                    case SeparateType.BlankSheet:
                        rdBlankSheet.Checked = true;
                        break;
                }
                if (lstBatchSetting.Items.Count > 0)
                {
                    //lstBatchSetting.SelectedItem = setting;
                    for (int i = 0; i < _lstBatchSetting.Count; i++)
                    {
                        if (_lstBatchSetting[i].BatchId == setting.BatchId)
                        {
                            lstBatchSetting.SelectedIndex = i;
                            break;
                        }
                    }

                }

            }
        }
        private void lstBatchSetting_DoubleClick(object sender, EventArgs e)
        {
            CurrentBatchSetting = (BatchSettingObject)lstBatchSetting.SelectedItem;
            LoadBatchSetting(CurrentBatchSetting);
            tabControlBatchSetting.SelectedIndex = 1;
            tabGeneral.Select();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            CallSave(true);
        }

        private void lstBatchSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentBatchSetting = (BatchSettingObject)lstBatchSetting.SelectedItem;
            LoadBatchSetting(CurrentBatchSetting);
        }
        private void RefreshListBatch()
        {
            lstBatchSetting.DataSource = null;
            lstBatchSetting.DataSource = _lstBatchSetting;
            lstBatchSetting.DisplayMember = "BatchName";
            lstBatchSetting.ValueMember = "BatchId";
            lstBatchSetting.Refresh();
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected batch setting?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int i = lstBatchSetting.SelectedIndex;
                _lstBatchSetting.RemoveAt(i);
                RefreshListBatch();
                if (_lstBatchSetting.Count > 0)
                {
                    File.WriteAllText("batchsetting.json", System.Text.Json.JsonSerializer.Serialize(_lstBatchSetting));
                }
            }
        }

        
    }
}
