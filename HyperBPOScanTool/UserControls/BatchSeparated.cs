using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HyperBPOScanTool.UserControls
{
    public partial class BatchSeparated : UserControl
    {
        public BatchSeparated()
        {
            InitializeComponent();
        }

        public SeparateObject GetSeparateData()
        {
            SeparateObject separateObject = new SeparateObject
            {
                TotalPage = txtPages.Text == "" ? 0 : Convert.ToInt32(txtPages.Text),
                BlankValue = txtBlankValue.Text == "" ? 0 : Convert.ToDecimal(txtBlankValue.Text),
                HideBlankPage = chkBlankPage.Checked,
                HideBlankSheet = chkBlankSheet.Checked
            };
            if (rdSheet.Checked)
            {
                separateObject.SeparateMode = SeparateType.Persheet;
            }
            else if (rdTotalPages.Checked)
            {
                separateObject.SeparateMode = SeparateType.NumOfPage;
            }
            else if (rdBlankSheet.Checked)
            {
                separateObject.SeparateMode = SeparateType.BlankSheet;
            }
            else if (rdQR.Checked)
            {
                separateObject.SeparateMode = SeparateType.Barcode;
            }
            return separateObject;
        }
        public void SetSeparateData(SeparateObject separate)
        {
            txtPages.Text = separate.TotalPage.ToString();
            txtBlankValue.Text = separate.BlankValue.ToString();
            chkBlankPage.Checked = separate.HideBlankPage;
            chkBlankSheet.Checked = separate.HideBlankSheet;
            switch (separate.SeparateMode)
            {
                case SeparateType.Persheet:
                    {
                        rdSheet.Checked = true;
                        rdTotalPages.Checked = false;
                        rdBlankSheet.Checked = false;
                        rdQR.Checked = false;
                        break;
                    }
                case SeparateType.NumOfPage:
                    {
                        rdTotalPages.Checked = true;
                        rdSheet.Checked = false;
                        rdBlankSheet.Checked = false;
                        rdQR.Checked = false;
                        break;
                    }
                case SeparateType.BlankSheet:
                    {
                        rdBlankSheet.Checked = true;
                        rdSheet.Checked = false;
                        rdTotalPages.Checked = false;
                        rdQR.Checked = false;
                        break;
                    }
                case SeparateType.Barcode:
                    {
                        rdQR.Checked = true;
                        rdSheet.Checked = false;
                        rdTotalPages.Checked = false;
                        rdBlankSheet.Checked = false;
                        break;
                    }
            }
        }
    }
}
