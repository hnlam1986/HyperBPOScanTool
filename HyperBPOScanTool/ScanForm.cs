using Newtonsoft.Json;
using NTwain;
using NTwain.Data;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.GdipExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using static OpenCvSharp.ML.DTrees;

namespace HyperBPOScanTool
{
    sealed partial class ScanForm : Form
    {
        ImageCodecInfo _tiffCodecInfo;
        TwainSession _twain;
        bool _stopScan;
        bool _loadingCaps;
   

        #region setup & cleanup

        public ScanForm()
        {
            InitializeComponent();
            if (NTwain.PlatformInfo.Current.IsApp64Bit)
            {
                Text = Text + " (64bit)";
            }
            else
            {
                Text = Text + " (32bit)";
            }
            foreach (var enc in ImageCodecInfo.GetImageEncoders())
            {
                if (enc.MimeType == "image/tiff") { _tiffCodecInfo = enc; break; }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //TODO: comment for testing without Twain
            SetupTwain();

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_twain != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && _twain.State > 4)
                {
                    e.Cancel = true;
                }
                else
                {
                    CleanupTwain();
                }
            }
            base.OnFormClosing(e);
        }
        ScanPack originalPack = null;
        ScanPack scanPack = new ScanPack();
        ScanFile _currentFile = null;
        ScanFile _currentSheet = null;
        ScanFile _sheet = null;
        TreeNode currentFileNode = null;
        private void AddNewScanPage(ScanFile scanFile, SeparateType type)
        {
            
            switch (type)
            {
                case SeparateType.None:
                    {
                        
                    }
                    break;
                case SeparateType.Persheet:
                    {
                        string filename ="File_"+ (scanPack.ScanFiles.Count + 1);
                        scanFile.FileName = filename;
                        scanFile.FileId = DateTime.Now.Ticks.ToString();
                        scanPack.ScanFiles.Add(scanFile);
                        AddFile(scanFile);
                    }
                    break;
                case SeparateType.BlankPage:
                    {
                        if (!scanFile.IsBlankSheet)
                        {
                            if (_currentFile == null)
                            {
                                _currentFile = new ScanFile();
                                _currentFile.Pages = new List<ScanPage>();
                                _currentFile.FileName = "File_" + (scanPack.ScanFiles.Count + 1);
                                _currentFile.Pages.AddRange(scanFile.Pages);
                                _currentFile.FileId = DateTime.Now.Ticks.ToString();
                                currentFileNode = AddFile(_currentFile);
                                scanPack.ScanFiles.Add(_currentFile);
                            }
                            else
                            {
                                AddPage(scanFile.Pages, currentFileNode);
                                scanPack.ScanFiles.Last().Pages.AddRange(scanFile.Pages);
                            }
                        }
                        else
                        {
                            _currentFile = null;
                        }
                        
                    }
                    break;
            }
        }

        private TreeNode AddFile(ScanFile file)
        {
            TreeNode node = new TreeNode();
            node.Tag = file;
            node.Text = file.FileName;
            foreach(ScanPage page in file.Pages)
            {
                TreeNode pageNode = new TreeNode();
                pageNode.Tag = page;
                pageNode.Text = "Page_" + page.PageIndex+"("+ page.StdDevVal + ")";
                if (page.IsBlank)
                {
                    pageNode.BackColor = Color.Red;
                }
                node.Nodes.Add(pageNode);
            }
            this.BeginInvoke(new Action(() =>
            {
                treeView1.Nodes[0].Nodes.Add(node);
                treeView1.ExpandAll();
            }));
            return node;
        }
        private void AddPage(List<ScanPage> pages, TreeNode currentFileNode)
        {
            this.BeginInvoke(new Action(() =>
            {
                foreach (ScanPage page in pages)
                {
                    TreeNode pageNode = new TreeNode();
                    pageNode.Tag = page;
                    pageNode.Text = "Page_" + page.PageIndex + "(" + page.StdDevVal + ")";
                    if (page.IsBlank)
                    {
                        pageNode.BackColor = Color.Red;
                    }
                    currentFileNode.Nodes.Add(pageNode);
                }
            }));
        }
        private void SetupTwain()
        {
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
            _twain = new TwainSession(appId);
            _twain.StateChanged += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("State changed to " + _twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            };
            _twain.TransferError += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
            };
            _twain.DataTransferred += (s, e) =>
            {
                
                PlatformInfo.Current.Log.Info("hinh gui xong");
                PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

                // example on getting ext image info
                var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
                foreach (var it in infos)
                {
                    var values = it.ReadValues();
                    PlatformInfo.Current.Log.Info(string.Format("{0} = {1}", it.InfoID, values.FirstOrDefault()));
                    break;
                }

                // handle image data
                Image img = null;
                if (e.NativeData != IntPtr.Zero)
                {
                    var stream = e.GetNativeImageStream();
                    if (stream != null)
                    {
                        img = Image.FromStream(stream);
                    }
                }
                else if (!string.IsNullOrEmpty(e.FileDataPath))
                {
                    img = new Bitmap(e.FileDataPath);
                }
                if (img != null)
                {
                    //img = ImageUtils.AddOuterWhiteBorder((Bitmap)img, 5);
                    if (originalPack == null)
                    {
                        originalPack = new ScanPack();
                    }
                    if (_sheet == null)
                    {
                        _sheet = new ScanFile();
                        _sheet.Pages = new List<ScanPage>();
                    }
                    ScanPage srcPage = new ScanPage();
                    srcPage.PageIndex = _sheet.Pages.Count + 1;
                    srcPage.PageImage = img;
                    srcPage.ImageId = DateTime.Now.Ticks.ToString();
                    double stdDevVal;
                    bool isBlank = IsBitmapBlank(new Bitmap(img), double.Parse(txtBlankValue.Text), out stdDevVal);
                    srcPage.StdDevVal = stdDevVal;
                    srcPage.IsBlank = isBlank;
                    _sheet.Pages.Add(srcPage);
                    _sheet.FileName = "Sheet_" + (originalPack.ScanFiles.Count + 1);
                    _sheet.FileId = DateTime.Now.Ticks.ToString();
                    if (_sheet.Pages.Count >= 2)
                    {
                        originalPack.ScanFiles.Add(_sheet);
                        //this.BeginInvoke(new Action(() =>
                        //{
                        SeparateType sepType = SeparateType.Persheet;
                        if (rdSheet.Checked)
                        {
                            sepType = SeparateType.Persheet;

                        }
                        else if (rdBlankSheet.Checked)
                        {
                            sepType = SeparateType.BlankPage;
                        }
                        AddNewScanPage(_sheet, sepType);
                        _sheet = null;
                        //}));
                    }

                }
            };
            _twain.SourceDisabled += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
                this.BeginInvoke(new Action(() =>
                {
                    btnStopScan.Enabled = false;
                    btnStartCapture.Enabled = true;
                    panelOptions.Enabled = true;
                    LoadSourceCaps();
                }));
            };
            _twain.TransferReady += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
                e.CancelAll = _stopScan;
                //SaveMockData();
            };

            // either set sync context and don't worry about threads during events,
            // or don't and use control.invoke during the events yourself
            PlatformInfo.Current.Log.Info("Setup thread = " + Thread.CurrentThread.ManagedThreadId);
            _twain.SynchronizationContext = SynchronizationContext.Current;
            if (_twain.State < 3)
            {
                // use this for internal msg loop
                _twain.Open();
                // use this to hook into current app loop
                //_twain.Open(new WindowsFormsMessageLoopHook(this.Handle));
            }
        }
        private void SaveMockData()
        {
            string json = JsonConvert.SerializeObject(originalPack);
            System.IO.File.WriteAllText("mockdata.json", json);     
        }
        private void CleanupTwain()
        {
            if (_twain.State == 4)
            {
                _twain.CurrentSource.Close();
            }
            if (_twain.State == 3)
            {
                _twain.Close();
            }

            if (_twain.State > 2)
            {
                // normal close down didn't work, do hard kill
                _twain.ForceStepDown(2);
            }
        }

        #endregion

        #region toolbar

        private void btnSources_DropDownOpening(object sender, EventArgs e)
        {
            if (btnSources.DropDownItems.Count == 2)
            {
                ReloadSourceList();
            }
        }

        private void reloadSourcesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadSourceList();
        }

        private void ReloadSourceList()
        {
            if (_twain.State >= 3)
            {
                while (btnSources.DropDownItems.IndexOf(sepSourceList) > 0)
                {
                    var first = btnSources.DropDownItems[0];
                    first.Click -= SourceMenuItem_Click;
                    btnSources.DropDownItems.Remove(first);
                }
                foreach (var src in _twain)
                {
                    var srcBtn = new ToolStripMenuItem(src.Name);
                    srcBtn.Tag = src;
                    srcBtn.Click += SourceMenuItem_Click;
                    srcBtn.Checked = _twain.CurrentSource != null && _twain.CurrentSource.Name == src.Name;
                    btnSources.DropDownItems.Insert(0, srcBtn);
                }
            }
        }

        void SourceMenuItem_Click(object sender, EventArgs e)
        {
            // do nothing if source is enabled
            if (_twain.State > 4) { return; }

            if (_twain.State == 4) { _twain.CurrentSource.Close(); }

            foreach (var btn in btnSources.DropDownItems)
            {
                var srcBtn = btn as ToolStripMenuItem;
                if (srcBtn != null) { srcBtn.Checked = false; }
            }

            var curBtn = (sender as ToolStripMenuItem);
            var src = curBtn.Tag as DataSource;
            if (src.Open() == ReturnCode.Success)
            {
                curBtn.Checked = true;
                btnStartCapture.Enabled = true;
                LoadSourceCaps();
            }
        }

        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            if (_twain.State == 4)
            {
                //_twain.CurrentSource.CapXferCount.Set(4);

                _stopScan = false;

                if (_twain.CurrentSource.Capabilities.CapUIControllable.IsSupported)//.SupportedCaps.Contains(CapabilityId.CapUIControllable))
                {
                    // hide scanner ui if possible
                    if (_twain.CurrentSource.Enable(SourceEnableMode.NoUI, true, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        panelOptions.Enabled = false;
                    }
                }
                else
                {
                    if (_twain.CurrentSource.Enable(SourceEnableMode.ShowUI, true, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        panelOptions.Enabled = false;
                    }
                }
            }
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            _stopScan = true;
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            //var img = null;// pictureBox1.Image;

            //if (img != null)
            //{
            //    switch (img.PixelFormat)
            //    {
            //        case PixelFormat.Format1bppIndexed:
            //            saveFileDialog1.Filter = "tiff files|*.tif";
            //            break;
            //        default:
            //            saveFileDialog1.Filter = "png files|*.png";
            //            break;
            //    }

            //    if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        if (saveFileDialog1.FileName.EndsWith(".tif", StringComparison.OrdinalIgnoreCase))
            //        {
            //            EncoderParameters tiffParam = new EncoderParameters(1);

            //            tiffParam.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionCCITT4);

            //            pictureBox1.Image.Save(saveFileDialog1.FileName, _tiffCodecInfo, tiffParam);
            //        }
            //        else
            //        {
            //            pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
            //        }
            //    }
            //}
        }

        #endregion

        #region cap control


        private void LoadSourceCaps()
        {
            var src = _twain.CurrentSource;
            _loadingCaps = true;

            //var test = src.SupportedCaps;

            if (groupDepth.Enabled = src.Capabilities.ICapPixelType.IsSupported)
            {
                LoadDepth(src.Capabilities.ICapPixelType);
            }
            if (groupDPI.Enabled = src.Capabilities.ICapXResolution.IsSupported && src.Capabilities.ICapYResolution.IsSupported)
            {
                LoadDPI(src.Capabilities.ICapXResolution);
            }
            // TODO: find out if this is how duplex works or also needs the other option
            if (groupDuplex.Enabled = src.Capabilities.CapDuplexEnabled.IsSupported)
            {
                LoadDuplex(src.Capabilities.CapDuplexEnabled);
            }
            if (groupSize.Enabled = src.Capabilities.ICapSupportedSizes.IsSupported)
            {
                LoadPaperSize(src.Capabilities.ICapSupportedSizes);
            }
            btnAllSettings.Enabled = src.Capabilities.CapEnableDSUIOnly.IsSupported;
            _loadingCaps = false;
        }

        private void LoadPaperSize(ICapWrapper<SupportedSize> cap)
        {
            var list = cap.GetValues().ToList();
            comboSize.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboSize.SelectedItem = cur;
            }
            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
            {
                groupSize.Text = labelTest;
            }
        }


        private void LoadDuplex(ICapWrapper<BoolType> cap)
        {
            ckDuplex.Checked = cap.GetCurrent() == BoolType.True;
        }


        private void LoadDPI(ICapWrapper<TWFix32> cap)
        {
            // only allow dpi of certain values for those source that lists everything
            var list = cap.GetValues().Where(dpi => (dpi % 50) == 0).ToList();
            comboDPI.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboDPI.SelectedItem = cur;
            }
        }

        private void LoadDepth(ICapWrapper<PixelType> cap)
        {
            var list = cap.GetValues().ToList();
            comboDepth.DataSource = list;
            var cur = cap.GetCurrent();
            if (list.Contains(cur))
            {
                comboDepth.SelectedItem = cur;
            }
            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
            {
                groupDepth.Text = labelTest;
            }
        }

        private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (SupportedSize)comboSize.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapSupportedSizes.SetValue(sel);
            }
        }

        private void comboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (PixelType)comboDepth.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapPixelType.SetValue(sel);
            }
        }

        private void comboDPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (TWFix32)comboDPI.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapXResolution.SetValue(sel);
                _twain.CurrentSource.Capabilities.ICapYResolution.SetValue(sel);
            }
        }

        private void ckDuplex_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                _twain.CurrentSource.Capabilities.CapDuplexEnabled.SetValue(ckDuplex.Checked ? BoolType.True : BoolType.False);
            }
        }

        private void btnAllSettings_Click(object sender, EventArgs e)
        {
            _twain.CurrentSource.Enable(SourceEnableMode.ShowUIOnly, true, this.Handle);
        }

        #endregion

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
        private List<ScanPage> GetAllPagesOfFile(string fileId)
        {
            return scanPack.ScanFiles.FirstOrDefault(f => f.FileId == fileId)?.Pages;
        }
        private Image GetPageImage(string ImageId)
        {
            return scanPack.ScanFiles.SelectMany(f => f.Pages).FirstOrDefault(p => p.ImageId == ImageId)?.PageImage;
        }
        PictureBox previewPic = null;
        TreeNode lastselectedNode = null;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (lastselectedNode != null)
            {
                lastselectedNode.BackColor = Color.Empty;
                lastselectedNode.ForeColor = Color.Black;
            }
            TreeNode selected = treeView1.SelectedNode;
            selected.BackColor = SystemColors.Highlight;
            selected.ForeColor = Color.White;
            lastselectedNode = selected;
            if (selected.Tag!=null && selected.Tag is ScanFile file)
            {
                TreeNodeCollection pages = selected.Nodes;
                flowLayoutPanel1.Controls.Clear();
                
                foreach (TreeNode page in pages)
                {
                    PictureBox pictureBox2 = new PictureBox();
                    pictureBox2.Size = new System.Drawing.Size(300, 300);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.BorderStyle = BorderStyle.FixedSingle;
                    
                    if (page.Tag is ScanPage scanPage)
                    {
                        pictureBox2.Image = GetPageImage(scanPage.ImageId);
                    }
                    flowLayoutPanel1.Controls.Add(pictureBox2);
                }
            }
            else  {
                flowLayoutPanel1.Controls.Clear();
                PictureBox pictureBox2 = new PictureBox();
               
                pictureBox2.Size = new System.Drawing.Size(flowLayoutPanel1.Size.Width - 20, flowLayoutPanel1.Size.Height - 50);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                //pictureBox2.BorderStyle = BorderStyle.FixedSingle;
                pictureBox2.MouseDown += PreviewPic_MouseDown;
                pictureBox2.MouseMove += PreviewPic_MouseMove;
                pictureBox2.MouseUp += PreviewPic_MouseUp;
                pictureBox2.Paint += PreviewPic_Paint;
                if (selected.Tag is ScanPage page)
                    pictureBox2.Image = GetPageImage(page.ImageId);
                previewPic = pictureBox2;
                flowLayoutPanel1.Controls.Add(previewPic);
            }
        }

        private void PreviewPic_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing && (startPoint != System.Drawing.Point.Empty && currentPoint != System.Drawing.Point.Empty))
            {
                
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen linePen = new Pen(Color.Red, 1))
                {
                    g.DrawLine(linePen, startPoint, currentPoint);
                }
            }else if(isComparing && (currentPoint != System.Drawing.Point.Empty))
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                using (Pen linePen = new Pen(Color.Green, 1))
                {
                    int width = ((PictureBox)sender).Width;
                    g.DrawLine(linePen, new System.Drawing.Point(0, currentPoint.Y), new System.Drawing.Point(width, currentPoint.Y));
                }
            }
            
        }

        private void PreviewPic_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                isDrawing = false;
                currentPoint = e.Location;
                PictureBox picBox = (PictureBox)sender;
                //picBox.SizeMode = PictureBoxSizeMode.Zoom;
                double angle = ImageUtils.GetAngleDegrees(startPoint.X, startPoint.Y, currentPoint.X, currentPoint.Y);
                Image img = picBox.Image;
                if (lastselectedNode.Tag!=null && lastselectedNode.Tag is ScanPage page)
                {
                    img = GetPageImage(page.ImageId);
                }
               
                Bitmap res = ImageUtils.RotateImage((Bitmap)img, -(float)angle);
              
                startPoint = System.Drawing.Point.Empty;
                currentPoint = System.Drawing.Point.Empty;
                previewPic.Image = res;

                //picBox.Invalidate(); // Final paint update
                //picBox.Refresh();
                
            }else if(isComparing && e.Button == MouseButtons.Right)
            {
                isComparing = false;
                startPoint = System.Drawing.Point.Empty;
                currentPoint = System.Drawing.Point.Empty;
            }
        }

        private void PreviewPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                currentPoint = e.Location;
                ((PictureBox)sender).Invalidate(); // Forces the PictureBox to repaint immediately
            }else if (isComparing)
            {
                currentPoint = e.Location;
                ((PictureBox)sender).Invalidate();
            }
        }
        System.Drawing.Point startPoint = new System.Drawing.Point();
        System.Drawing.Point currentPoint = new System.Drawing.Point();
        bool isDrawing = false;
        bool isComparing = false;
        private void PreviewPic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                startPoint = e.Location;
                currentPoint = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                isComparing = true;
                currentPoint = e.Location;
                ((PictureBox)sender).Invalidate();
            }
        }

        public bool IsBitmapBlank(Bitmap bitmap, double threshold , out double stdDevVal)
        {
            // Lock the bitmap's bits to access raw memory quickly
            using (Mat img = bitmap.ToMat())
            {
                if (img.Empty()) throw new Exception("Image cannot be loaded.");

                // Calculate the standard deviation and mean
                Cv2.MeanStdDev(img, out Scalar mean, out Scalar stdDev);

                // A standard deviation below the threshold indicates high uniformity (blank)
                stdDevVal = stdDev.Val0;
                return stdDev.Val0 < threshold;
            }
               

        }

        private void btnResetScan_Click(object sender, EventArgs e)
        {
         
            //listImage = null;
            scanPack = new ScanPack();
            _currentFile = null;
            _currentSheet = null;
            currentFileNode = null;
            treeView1.Nodes[0].Nodes.Clear();
            flowLayoutPanel1.Controls.Clear();
            _scanPackBackup = new ScanPack();
            originalPack = null;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        ScanPack _scanPackBackup = new ScanPack();
        private void chkBlankPage_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }
        private void RefreshTreeView()
        {
            SeparateType sepType = SeparateType.Persheet;
            if (rdSheet.Checked)
            {
                sepType = SeparateType.Persheet;

            }
            else if (rdTotalPages.Checked)
            {
                sepType = SeparateType.NumOfPage;
            }
            else if (rdBlankSheet.Checked)
            {
                sepType = SeparateType.BlankPage;
            }

            if (!chkBlankPage.Checked && !chkBlankSheet.Checked)
            {
                if (originalPack != null)
                {
                    _scanPackBackup = originalPack.Clone();
                    ReloadTreeView(_scanPackBackup, sepType);
                }
            }
            else { 
                _scanPackBackup = originalPack.Clone();

                for (int j = 0; j < _scanPackBackup.ScanFiles.Count; j++)
                {
                    ScanFile file = _scanPackBackup.ScanFiles[j];
                    
                    if (file.IsBlankSheet && chkBlankSheet.Checked)
                    {
                        _scanPackBackup.ScanFiles.RemoveAt(j);
                        j--; // Adjust the index after removal
                    }
                    else
                    {
                        if (!(!chkBlankSheet.Checked && file.IsBlankSheet))
                        {
                            for (int i = 0; i < file.Pages.Count; i++)
                            {
                                file.Pages[i].PageIndex = i + 1;
                                if (file.Pages[i].IsBlank && chkBlankPage.Checked)
                                {
                                    file.Pages.RemoveAt(i);
                                    i--; // Adjust the index after removal
                                }
                            }
                        }
                    }
                    
                }
                ReloadTreeView(_scanPackBackup, sepType);
            }
            
        }
        private void ReloadTreeView(ScanPack pack, SeparateType type)
        {

            treeView1.Nodes[0].Nodes.Clear();
            switch (type)
            {
                case SeparateType.None:
                    {

                    }
                    break;
                case SeparateType.Persheet:
                    {
                        scanPack = new ScanPack();
                        foreach (ScanFile file in pack.ScanFiles)
                        {
                            scanPack.ScanFiles.Add(file);
                            AddFile(file);
                        }
                    }
                    break;
                case SeparateType.NumOfPage:
                    {
                        scanPack = new ScanPack();
                        int fileIndex = 1;
                        int pageIndex = 1;
                        int totalPage = int.Parse(txtPages.Text);
                        ScanFile tmpFile = null;
                        foreach(ScanFile file in pack.ScanFiles)
                        {
                            if (pageIndex == 1)
                            {
                                if (tmpFile != null)
                                {
                                    AddFile(tmpFile);
                                } 
                                tmpFile = new ScanFile();
                                tmpFile.FileName = "File_" + fileIndex;
                                
                                fileIndex++;
                            }
                            for (int i = 0; i< file.Pages.Count;i++)
                            {
                                ScanPage page = file.Pages[i];
                                if (pageIndex<= totalPage)
                                {
                                    tmpFile.Pages.Add(page);
                                    if (pageIndex >= totalPage)
                                    {
                                        scanPack.ScanFiles.Add(tmpFile);
                                        pageIndex = 1;
                                        if (i + 1 < file.Pages.Count)
                                        {
                                            AddFile(tmpFile);
                                            tmpFile = new ScanFile();
                                            tmpFile.FileName = "File_" + fileIndex;
                                            fileIndex++;
                                           
                                        }
                                        
                                    }
                                    else
                                    {
                                        pageIndex++;
                                    }
                                }
                                
                            }
                        }
                        if (tmpFile != null)
                        {
                            AddFile(tmpFile);
                        }
                    }
                    break;
                case SeparateType.BlankPage:
                    {
                        scanPack = new ScanPack();
                        int fileIndex = 1;
                       
                        ScanFile tmpFile = null;
                        foreach (ScanFile file in pack.ScanFiles)
                        {
                            if (file.IsBlankSheet || tmpFile==null)
                            {
                                if (tmpFile != null)
                                {
                                    AddFile(tmpFile);
                                    scanPack.ScanFiles.Add(tmpFile);
                                }
                                tmpFile = new ScanFile();
                                tmpFile.FileName = "File_" + fileIndex;
                                fileIndex++;
                            }
                            if (!file.IsBlankSheet)
                            {
                                tmpFile.Pages.AddRange(file.Pages);
                            }
                        }
                        if (tmpFile != null)
                        {
                            AddFile(tmpFile);
                            scanPack.ScanFiles.Add(tmpFile);
                        }
                    }
                    break;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void txtBlankValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdBlankSheet_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkBlankSheet.CheckedChanged -= chkBlankSheet_CheckedChanged;
                chkBlankSheet.Checked = false;
                chkBlankSheet.Enabled = false;
                RefreshTreeView();
                chkBlankSheet.CheckedChanged += chkBlankSheet_CheckedChanged;
            }
        }

        private void btnMockData_Click(object sender, EventArgs e)
        {
            string datafile = "mockdata.json";
            if (File.Exists(datafile))
            {
                string content = File.ReadAllText(datafile);
                ScanPack data = JsonConvert.DeserializeObject<ScanPack>(content);
                if (data != null)
                {
                    originalPack = data;
                }
                RefreshTreeView();
            }
        }

        private void btnSaveMock_Click(object sender, EventArgs e)
        {
            SaveMockData();
        }

        private void rdSheet_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                RefreshTreeView();
                chkBlankSheet.Enabled = true;
            }
        }

        private void chkBlankSheet_CheckedChanged(object sender, EventArgs e)
        {
            
                RefreshTreeView();
                chkBlankSheet.Enabled = true;
            
        }

        private void rdTotalPages_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                RefreshTreeView();
                chkBlankSheet.Enabled = true;
            }
        }

        private void btnStartLoadFile_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("D:\\ScanTestImage");
            if (files.Length > 0)
            {
                this.BeginInvoke(new Action(() =>
                {
                foreach (string file in files)
                {
                    Image img = Image.FromFile(file);
                    //copy from hear
                    if (img != null)
                    {
                        if (originalPack == null)
                        {
                            originalPack = new ScanPack();
                        }
                        if (_sheet == null)
                        {
                            _sheet = new ScanFile();
                            _sheet.Pages = new List<ScanPage>();
                        }
                        ScanPage srcPage = new ScanPage();
                        srcPage.PageIndex = _sheet.Pages.Count + 1;
                        srcPage.PageImage = img;
                        double stdDevVal;
                        bool isBlank = IsBitmapBlank(new Bitmap(img), double.Parse(txtBlankValue.Text), out stdDevVal);
                        srcPage.StdDevVal = stdDevVal;
                        srcPage.IsBlank = isBlank;
                        _sheet.Pages.Add(srcPage);
                        _sheet.FileName = "Sheet_" + (originalPack.ScanFiles.Count + 1);
                        if (_sheet.Pages.Count >= 2)
                        {
                            originalPack.ScanFiles.Add(_sheet);
                            //this.BeginInvoke(new Action(() =>
                            //{
                                SeparateType sepType = SeparateType.Persheet;
                                if (rdSheet.Checked)
                                {
                                    sepType = SeparateType.Persheet;

                                }
                                else if (rdBlankSheet.Checked)
                                {
                                    sepType = SeparateType.BlankPage;
                                }
                                AddNewScanPage(_sheet, sepType);
                                _sheet = null;
                            //}));
                        }
                        
                    }
                        //copy to hear
                        //Thread.Sleep(500);
                    }
                }));
            }
        }

        private void btnPreviewBorder_Click(object sender, EventArgs e)
        {
            int top = txtTopBorder.Text == "" ? 0 : int.Parse(txtTopBorder.Text);
            int bottom = txtBottomBorder.Text == "" ? 0 : int.Parse(txtBottomBorder.Text);
            int right = txtRightBorder.Text == "" ? 0 : int.Parse(txtRightBorder.Text);
            int left = txtLeftBorder.Text == "" ? 0 : int.Parse(txtLeftBorder.Text);
            previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, top, right, bottom, left);
        }
        PDFUtils pdfUtils = new PDFUtils();
        private void btnExport_Click(object sender, EventArgs e)
        {
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                List<Image> images = new List<Image>();
                foreach (ScanPage page in file.Pages)
                {
                    images.Add(page.PageImage);
                }
                // Create PDF with the images
                
                pdfUtils.CreatePDF(file.FileName+".pdf", @"D:\ExportScanTool\", images);
            }
        }

        private void UpdatePageImage(string id, Image img)
        {
            //foreach(ScanFile file in originalPack.ScanFiles)
            //{
            //    foreach(ScanPage page in file.Pages)
            //    {
            //        if(page.ImageId == id)
            //        {
            //            page.PageImage = img;
            //        }
            //    }
            //}
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanPage page in file.Pages)
                {
                    if (page.ImageId == id)
                    {
                        page.PageImage = img;
                        return;
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(lastselectedNode != null)
            {
                ScanPage selectedPage = lastselectedNode.Tag as ScanPage;
                if(selectedPage != null)
                {
                    UpdatePageImage(selectedPage.ImageId, previewPic.Image);
                }
            }
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            int top = txtTopBorder.Text == "" ? 0 : int.Parse(txtTopBorder.Text);
            int bottom = txtBottomBorder.Text == "" ? 0 : int.Parse(txtBottomBorder.Text);
            int right = txtRightBorder.Text == "" ? 0 : int.Parse(txtRightBorder.Text);
            int left = txtLeftBorder.Text == "" ? 0 : int.Parse(txtLeftBorder.Text);
            previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, top, right, bottom, left);
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanPage page in file.Pages)
                {
                    page.PageImage = ImageUtils.AddOuterWhiteBorder((Bitmap)page.PageImage, top, right, bottom, left);
                }
            }
        }
    }
}
