using HyperBPOScanTool.SettingForm;
using HyperBPOScanTool.UserControls;
using Newtonsoft.Json;
using NTwain;
using NTwain.Data;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.GdipExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Media3D;
using System.Xml;
using static OpenCvSharp.ML.DTrees;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;
using RadioButton = System.Windows.Forms.RadioButton;

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
            this.KeyPreview = true;
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
            _separateObject = new SeparateObject
            {
                SeparateMode = SeparateType.Persheet,
                TotalPage = 2,
                BlankValue = (decimal)0.002,
                HideBlankPage = false,
                HideBlankSheet = false
            };
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
        ScanBatch originalPack = null;
        ScanBatch scanPack = new ScanBatch();
        ScanFile _currentFile = null;
        ScanFile _currentSheet = null;
        ScanSheet _sheet = null;
        TreeNode currentFileNode = null;
        private void AddNewScanImage(ScanSheet sheet, SeparateType type)
        {

            switch (type)
            {
                case SeparateType.None:
                    {

                    }
                    break;
                case SeparateType.Persheet:
                    {
                        string filename = "File_" + (scanPack.ScanFiles.Count + 1);
                        ScanFile file = new ScanFile();
                        file.FileName = filename;
                        file.FileId = DateTime.Now.Ticks.ToString();
                        file.Sheets = new List<ScanSheet>();
                        file.Sheets.Add(sheet);
                        scanPack.ScanFiles.Add(file);
                        AddFile(file);
                    }
                    break;
                case SeparateType.BlankSheet:
                    {
                        if (!sheet.IsBlankSheet)
                        {
                            if (_currentFile == null)
                            {
                                _currentFile = new ScanFile();
                                _currentFile.Sheets = new List<ScanSheet>();
                                _currentFile.FileName = "File_" + (scanPack.ScanFiles.Count + 1);
                                _currentFile.Sheets.Add(sheet);
                                _currentFile.FileId = DateTime.Now.Ticks.ToString();
                                currentFileNode = AddFile(_currentFile);
                                scanPack.ScanFiles.Add(_currentFile);
                            }
                            else
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    AddSheet(sheet, currentFileNode);
                                }));
                                
                                scanPack.ScanFiles.Last().Sheets.Add(sheet);
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
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            node.Expand();
            foreach (ScanSheet sheet in file.Sheets)
            {
                TreeNode sheetNode = new TreeNode();
                sheetNode.Tag = sheet;
                sheetNode.Text = "Page_" + (sheet.SheetIndex+1);
                sheetNode.ImageIndex = 2;
                sheetNode.SelectedImageIndex = 2;
                sheetNode.Expand();
                if (sheet.IsBlankSheet)
                {
                    //pageNode.Text = "Page_" + page.PageIndex + "(" + page.StdDevVal + ")";
                    //pageNode.BackColor = Color.Red;
                    sheetNode.ImageIndex = 3;
                    sheetNode.SelectedImageIndex = 3;
                }
                
                
                TreeNode imageNodeTop = new TreeNode();
                imageNodeTop.Tag = sheet.Top;
                imageNodeTop.Text = "Image_1";
                imageNodeTop.Expand();
                if (sheet.Top != null && !sheet.Top.IsBlank)
                {
                    imageNodeTop.ImageIndex = 4;
                    imageNodeTop.SelectedImageIndex = 4;
                    sheetNode.Nodes.Add(imageNodeTop);
                }
                else if (sheet.Top != null)
                {
                    imageNodeTop.ImageIndex = 5;
                    imageNodeTop.SelectedImageIndex = 5;
                    sheetNode.Nodes.Add(imageNodeTop);
                }
                    

                TreeNode imageNodeBottom = new TreeNode();
                imageNodeBottom.Tag = sheet.Bottom;
                imageNodeBottom.Text = "Image_2";
                imageNodeBottom.Expand();
                if (sheet.Bottom != null && !sheet.Bottom.IsBlank)
                {
                    imageNodeBottom.ImageIndex = 4;
                    imageNodeBottom.SelectedImageIndex = 4;
                    sheetNode.Nodes.Add(imageNodeBottom);
                }
                else if(sheet.Bottom != null)
                {
                    imageNodeBottom.ImageIndex = 5;
                    imageNodeBottom.SelectedImageIndex = 5;
                    sheetNode.Nodes.Add(imageNodeBottom);
                }
                    

                    

                    
                

                node.Nodes.Add(sheetNode);
            }
            this.BeginInvoke(new Action(() =>
            {
                treeView1.Nodes[0].Nodes.Add(node);
                treeView1.ExpandAll();
            }));
            PlatformInfo.Current.Log.Info("Add file: " + DateTime.Now.ToLongTimeString());
            return node;
        }
        private void AddSheet(ScanSheet sheet, TreeNode currentFileNode)
        {
            TreeNode sheetNode = new TreeNode();
            sheetNode.Tag = sheet;
            sheetNode.Text = "Page_" + (currentFileNode.Nodes.Count+1);
            sheetNode.ImageIndex = 2;
            sheetNode.SelectedImageIndex = 2;
            sheetNode.Expand();
            if (sheet.IsBlankSheet)
            {
                //pageNode.Text = "Page_" + page.PageIndex + "(" + page.StdDevVal + ")";
                sheetNode.ImageIndex = 3;
                sheetNode.SelectedImageIndex = 3;
            }
            else
            {
                TreeNode imageNodeTop = new TreeNode();
                imageNodeTop.Tag = sheet.Top;
                imageNodeTop.Text = "Image_1";
                if (sheet.Top != null && !sheet.Top.IsBlank)
                {
                    imageNodeTop.ImageIndex = 4;
                    imageNodeTop.SelectedImageIndex = 4;
                    sheetNode.Nodes.Add(imageNodeTop);
                }
                else if(sheet.Top!=null)
                {
                    imageNodeTop.ImageIndex = 5;
                    imageNodeTop.SelectedImageIndex = 5;
                    sheetNode.Nodes.Add(imageNodeTop);
                }
                    

                TreeNode imageNodeBottom = new TreeNode();
                imageNodeBottom.Tag = sheet.Bottom;
                imageNodeBottom.Text = "Image_2";
                if (sheet.Bottom != null && !sheet.Bottom.IsBlank)
                {
                    imageNodeBottom.ImageIndex = 4;
                    imageNodeBottom.SelectedImageIndex = 4;
                    sheetNode.Nodes.Add(imageNodeBottom);
                }
                else if (sheet.Bottom != null)
                {
                    imageNodeBottom.ImageIndex = 5;
                    imageNodeBottom.SelectedImageIndex = 5;
                    sheetNode.Nodes.Add(imageNodeBottom);
                }
            }
            currentFileNode.Nodes.Add(sheetNode);
            PlatformInfo.Current.Log.Info("Add sheet: " + DateTime.Now.ToLongTimeString());
           
        }
        int documentNumber = 0;
        Queue _queue = new Queue();
        private void OnStateChanged(object s, EventArgs e)
        {
            PlatformInfo.Current.Log.Info("State changed to " + _twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            if (_twain.State == 4)
            {
                var source = _twain.CurrentSource;

                // Kiểm tra xem máy quét có hỗ trợ ICAP_AUTODISCARDBLANKPAGES không
                if (source.Capabilities.ICapAutoDiscardBlankPages.IsSupported)
                {
                    // Đặt chế độ tự động bỏ trang trắng (TWBP_AUTO = -2)
                    BlankPage autoDiscardBlankPages = BlankPage.Disable;
                    if (_separateObject.HideBlankPage)
                    {
                        autoDiscardBlankPages = BlankPage.Auto;
                    }
                    var status = source.Capabilities.ICapAutoDiscardBlankPages.SetValue(autoDiscardBlankPages);

                    if (status == ReturnCode.Success)
                    {
                        Console.WriteLine("Đã bật tính năng tự động loại bỏ trang trắng (TWBP_AUTO).");
                    }
                    else
                    {
                        Console.WriteLine("Máy quét hỗ trợ nhưng không thể đặt giá trị TWBP_AUTO.");
                    }
                }
                else
                {
                    Console.WriteLine("Máy quét không hỗ trợ ICAP_AUTODISCARDBLANKPAGES.");
                }
            }
        }
        private void OnTransferError(object s, TransferErrorEventArgs e)
        {
            PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
        }
        private void OnDataTransferred(object s, DataTransferredEventArgs e)
        {
            PlatformInfo.Current.Log.Info("Start: " +DateTime.Now.ToLongTimeString());
            PlatformInfo.Current.Log.Info("hinh gui xong");
            PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

            // example on getting ext image info
            var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
            string camInfoString = (infos.FirstOrDefault()).ReadValues().FirstOrDefault().ToString();
            int camInfo = 0;
            if (camInfoString == "/Camera_Color_Top")
            {
                camInfo = 1;
            }
            else if(camInfoString == "/Camera_Color_Bottom")
            {
                camInfo = 2;
            }
            var docNum = e.GetExtImageInfo(ExtendedImageInfo.DocumentNumber).Where(it => it.ReturnCode == ReturnCode.Success);
            int docNumValue = int.Parse((docNum.FirstOrDefault()).ReadValues().FirstOrDefault().ToString());
            

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

            if (img != null)
            {
                ScanImage scanImage = new ScanImage();
                scanImage.Image = img;
                scanImage.DocIndex = docNumValue;   
                if(camInfo == 1)
                {
                    scanImage.CamMode = CamMode.Top;
                }
                else if(camInfo == 2)
                {
                    scanImage.CamMode = CamMode.Bottom;
                }
                _queue.Enqueue(scanImage);
            }
                
                PlatformInfo.Current.Log.Info("End: " + DateTime.Now.ToLongTimeString());
        }
        private void OnSourceDisabled(object s, EventArgs e)
        {
            PlatformInfo.Current.Log.Info("OnSourceDisabled: " + DateTime.Now.ToLongTimeString());
            
            this.BeginInvoke(new Action(() =>
            {
                btnStopScan.Enabled = false;
                btnStartCapture.Enabled = true;
                treeView1.Enabled = true;
                LoadSourceCaps();
                _stopScan = true;
            }));
        }
        private void OnTransferReady(object s, TransferReadyEventArgs e)
        {
            PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
            e.CancelAll = _stopScan;
        }
        private void SetupTwain()
        {

            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
            _twain = new TwainSession(appId);
            _twain.StateChanged += OnStateChanged;
            _twain.TransferError += OnTransferError;
            _twain.DataTransferred += OnDataTransferred;
            _twain.SourceDisabled += OnSourceDisabled;
            _twain.TransferReady += OnTransferReady;

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
            if (_twain.State >= 4)
            {
                _twain.CurrentSource.Close();
            }
            if (_twain.State == 2)
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



        private void reloadSourcesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadSourceList();
        }
        List<DataSourceObject> _sourceList = new List<DataSourceObject>();
        private void ReloadSourceList()
        {
            if (_twain.State >= 3)
            {
                //while (btnSources.DropDownItems.IndexOf(sepSourceList) > 0)
                //{
                //    var first = btnSources.DropDownItems[0];
                //    first.Click -= SourceMenuItem_Click;
                //    btnSources.DropDownItems.Remove(first);
                //}
                int pos = 0;
                int selectedIndex = -1;
                tsSelectScanner.SelectedChanged -= SourceMenuItem_Click;
                foreach (var src in _twain)
                {
                    //var srcBtn = new ToolStripMenuItem(src.Name);
                    //srcBtn.Tag = src;
                    //srcBtn.Click += SourceMenuItem_Click;
                    //srcBtn.Checked = _twain.CurrentSource != null && _twain.CurrentSource.Name == src.Name;
                    //btnSources.DropDownItems.Insert(0, srcBtn);
                    DataSourceObject srcObj = new DataSourceObject();
                    srcObj.Name = src.Name;
                    srcObj.DS = src;
                    _sourceList.Add(srcObj);

                    if (_twain.CurrentSource != null && _twain.CurrentSource.Name == src.Name)
                    {
                        selectedIndex = pos;
                    }
                    tsSelectScanner.Items.Add(src.Name);
                    pos++;
                }
                tsSelectScanner.SelectedChanged += SourceMenuItem_Click;
                if (selectedIndex >= 0)
                {
                    tsSelectScanner.SelectedIndex = selectedIndex;
                }
            }
        }

        void SourceMenuItem_Click(object sender, EventArgs e)
        {
            if (tsSelectScanner.SelectedIndex >= 0)
            {
                // do nothing if source is enabled
                if (_twain.State > 4) { return; }

                if (_twain.State == 4) { _twain.CurrentSource.Close(); }

                //foreach (var btn in btnSources.DropDownItems)
                //{
                //    var srcBtn = btn as ToolStripMenuItem;
                //    if (srcBtn != null) { srcBtn.Checked = false; }
                //}

                //var curBtn = (sender as ToolStripMenuItem);
                //var src = curBtn.Tag as DataSource;
                string itemSelected = tsSelectScanner.SelectedItem.ToString();
                DataSource src = _sourceList.Where(i => i.Name == itemSelected).Select(i => i.DS).FirstOrDefault();
                if (src != null && src.Open() == ReturnCode.Success)
                {
                    btnStartCapture.Enabled = true;
                    LoadSourceCaps();
                }
                else
                {
                    MessageBox.Show("Can not open this scanner, please make sure this scan device is turnin ON");
                }
            }
        }

        private async void btnStartCapture_Click(object sender, EventArgs e)
        {
            PlatformInfo.Current.Log.Info("Start scan button: " + DateTime.Now.ToLongTimeString());
            //var source = _twain.CurrentSource;
            //source.Close();
            //source.Open();
            //documentNumber = 0;
            if (_twain.State == 4)
            {
                //_twain.CurrentSource.CapXferCount.Set(4);

                _stopScan = false;

                if (_twain.CurrentSource.Capabilities.CapUIControllable.IsSupported)//.SupportedCaps.Contains(CapabilityId.CapUIControllable))
                {
                    // hide scanner ui if possible
                    if (_twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        treeView1.Enabled = false;
                    }
                }
                else
                {
                    if (_twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        treeView1.Enabled = false;
                    }
                }

                //implement so get queue and process image in another thread
                _queue.Clear();
                
            }
        }
        private void GetQueueImage(IProgress<int> progress)
        { 

            while (true)
            {
                SeparateType sepType = _separateObject.SeparateMode;
                if (_queue.Count > 0)
                {
                    ScanImage scanImage = (ScanImage)_queue.Dequeue();
                    PlatformInfo.Current.Log.Info("Queue image: " + scanImage.DocIndex + " | " + scanImage.CamMode);
                    decimal stdDevVal;
                    bool isBlank = BlankPageDetector.IsBlankPage(new Bitmap(scanImage.Image), out stdDevVal, 0.05, 200, (double)_separateObject.BlankValue);
                   
                    if (_sheet == null )
                    {
                        _sheet = new ScanSheet();
                        _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                        _sheet.SheetId = DateTime.Now.Ticks.ToString();
                        if (documentNumber + 1 < scanImage.DocIndex && sepType == SeparateType.BlankSheet)
                        {
                            //add blank sheet
                            AddNewScanImage(_sheet, sepType);
                            documentNumber = scanImage.DocIndex;
                            //add next sheet
                            _sheet = new ScanSheet();
                            _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                            _sheet.SheetId = DateTime.Now.Ticks.ToString();
                        }
                        
                        documentNumber = scanImage.DocIndex;
                    }
                    else 
                    {
                        if (documentNumber + 1 < scanImage.DocIndex && sepType == SeparateType.BlankSheet)
                        {
                            //add not finish sheet
                            AddNewScanImage(_sheet, sepType);
                            //create blank sheet
                            _sheet = new ScanSheet();
                            _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                            _sheet.SheetId = DateTime.Now.Ticks.ToString();
                            //add blank sheet
                            AddNewScanImage(_sheet, sepType);
                            //create next new sheet
                            _sheet = new ScanSheet();
                            _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                            _sheet.SheetId = DateTime.Now.Ticks.ToString();
                        }
                        documentNumber = scanImage.DocIndex;
                    }
                    //if (_sheet == null)
                    //{
                    //    _sheet = new ScanSheet();
                    //    _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                    //    _sheet.SheetId = DateTime.Now.Ticks.ToString();
                    //}
                    ScanPage srcPage = new ScanPage();
                    srcPage.PageIndex = (int)scanImage.CamMode;
                    srcPage.PageImage = scanImage.Image;
                    srcPage.ImageId = DateTime.Now.Ticks.ToString();

                    srcPage.StdDevVal = stdDevVal;
                    srcPage.IsBlank = isBlank;
                    if (scanImage.CamMode == CamMode.Top)
                    {
                        _sheet.Top = srcPage;
                    }
                    else
                    {
                        _sheet.Bottom = srcPage;
                    }
                    if (_sheet != null && _sheet.Top != null && _sheet.Bottom != null)
                    {
                        AddNewScanImage(_sheet, sepType);
                        _sheet = null;
                    }
                    progress.Report(_queue.Count);
                }
                if (_queue.Count == 0)
                {
                    if (_sheet != null)
                    {
                        AddNewScanImage(_sheet, sepType);
                        _sheet = null;
                    }
                    Thread.Sleep(200);
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

            //if (groupDepth.Enabled = src.Capabilities.ICapPixelType.IsSupported)
            //{
            //    LoadDepth(src.Capabilities.ICapPixelType);
            //}
            //if (groupDPI.Enabled = src.Capabilities.ICapXResolution.IsSupported && src.Capabilities.ICapYResolution.IsSupported)
            //{
            //    LoadDPI(src.Capabilities.ICapXResolution);
            //}
            //// TODO: find out if this is how duplex works or also needs the other option
            //if (groupDuplex.Enabled = src.Capabilities.CapDuplexEnabled.IsSupported)
            //{
            //    LoadDuplex(src.Capabilities.CapDuplexEnabled);
            //}
            //if (groupSize.Enabled = src.Capabilities.ICapSupportedSizes.IsSupported)
            //{
            //    LoadPaperSize(src.Capabilities.ICapSupportedSizes);
            //}
            btnAllSettings.Enabled = src.Capabilities.CapEnableDSUIOnly.IsSupported;
            _loadingCaps = false;
        }

        //private void LoadPaperSize(ICapWrapper<SupportedSize> cap)
        //{
        //    var list = cap.GetValues().ToList();
        //    comboSize.DataSource = list;
        //    var cur = cap.GetCurrent();
        //    if (list.Contains(cur))
        //    {
        //        comboSize.SelectedItem = cur;
        //    }
        //    var labelTest = cap.GetLabel();
        //    if (!string.IsNullOrEmpty(labelTest))
        //    {
        //        groupSize.Text = labelTest;
        //    }
        //}


        //private void LoadDuplex(ICapWrapper<BoolType> cap)
        //{
        //    ckDuplex.Checked = cap.GetCurrent() == BoolType.True;
        //}


        //private void LoadDPI(ICapWrapper<TWFix32> cap)
        //{
        //    // only allow dpi of certain values for those source that lists everything
        //    var list = cap.GetValues().Where(dpi => (dpi % 50) == 0).ToList();
        //    comboDPI.DataSource = list;
        //    var cur = cap.GetCurrent();
        //    if (list.Contains(cur))
        //    {
        //        comboDPI.SelectedItem = cur;
        //    }
        //}

        //private void LoadDepth(ICapWrapper<PixelType> cap)
        //{
        //    var list = cap.GetValues().ToList();
        //    comboDepth.DataSource = list;
        //    var cur = cap.GetCurrent();
        //    if (list.Contains(cur))
        //    {
        //        comboDepth.SelectedItem = cur;
        //    }
        //    var labelTest = cap.GetLabel();
        //    if (!string.IsNullOrEmpty(labelTest))
        //    {
        //        groupDepth.Text = labelTest;
        //    }
        //}

        //private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!_loadingCaps && _twain.State == 4)
        //    {
        //        var sel = (SupportedSize)comboSize.SelectedItem;
        //        _twain.CurrentSource.Capabilities.ICapSupportedSizes.SetValue(sel);
        //    }
        //}

        //private void comboDepth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!_loadingCaps && _twain.State == 4)
        //    {
        //        var sel = (PixelType)comboDepth.SelectedItem;
        //        _twain.CurrentSource.Capabilities.ICapPixelType.SetValue(sel);
        //    }
        //}

        //private void comboDPI_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!_loadingCaps && _twain.State == 4)
        //    {
        //        var sel = (TWFix32)comboDPI.SelectedItem;
        //        _twain.CurrentSource.Capabilities.ICapXResolution.SetValue(sel);
        //        _twain.CurrentSource.Capabilities.ICapYResolution.SetValue(sel);
        //    }
        //}

        //private void ckDuplex_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (!_loadingCaps && _twain.State == 4)
        //    {
        //        _twain.CurrentSource.Capabilities.CapDuplexEnabled.SetValue(ckDuplex.Checked ? BoolType.True : BoolType.False);
        //    }
        //}

        private void btnAllSettings_Click(object sender, EventArgs e)
        {
            ushort kodakShortcutCapId = 0x8002;
            CapabilityId targetCap = (CapabilityId)kodakShortcutCapId;
            var obj = _twain.CurrentSource.DGCustom;
            // CapabilityId customCap = (CapabilityId)kodakShortcutCapId;
            //_twain.CurrentSource.Capabilities.GetCurrent(customCap);
            _twain.CurrentSource.Enable(SourceEnableMode.ShowUIOnly, true, this.Handle);
        }

        #endregion

        private async void TestForm_Load(object sender, EventArgs e)
        {
            ImageList listImage = new ImageList();
            listImage.Images.Add(Properties.Resources.batch); //0
            listImage.Images.Add(Properties.Resources.document);  //1
            listImage.Images.Add(Properties.Resources.ok_page);  //2
            listImage.Images.Add(Properties.Resources.blank_page);  //3
            listImage.Images.Add(Properties.Resources.image);  //4
            listImage.Images.Add(Properties.Resources.blank_img);  //5
            treeView1.ImageList = listImage;
            treeView1.Nodes[0].ImageIndex = 0;
            treeView1.Nodes[0].SelectedImageIndex = 0;
            ReloadSourceList();
            var progress = new Progress<int>(percent =>
            {

            });
            await Task.Run(() => GetQueueImage(progress));
        }
        private List<ScanPage> GetAllPagesOfFile(string fileId)
        {
            List<ScanSheet> sheets = scanPack.ScanFiles.FirstOrDefault(f => f.FileId == fileId)?.Sheets;
            List<ScanPage> pages = new List<ScanPage>();
            foreach (ScanSheet sheet in sheets)
            {
                if (sheet.Top != null)
                {
                    pages.Add(sheet.Top);
                }
                if (sheet.Bottom != null)
                {
                    pages.Add(sheet.Bottom);
                }
            }
            return pages;   
        }
        private Image GetPageImage(string ImageId)
        {
            return scanPack.ScanFiles.SelectMany(f => f.Sheets).Select(s => s.Top).Where(p => p != null).Concat(scanPack.ScanFiles.SelectMany(f => f.Sheets).Select(s => s.Bottom).Where(p => p != null)).FirstOrDefault(p => p.ImageId == ImageId)?.PageImage;                                      
        }
        PictureBox previewPic = null;
        TreeNode lastselectedNode = null;
        private void DisplayNodeDetail()
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
            tsClearArea.Enabled = false;
            tsStraigth.Enabled = false;
            tsRescan.Enabled = false;
            if (selected.Tag == "root")
            {
                previewPic = null;
                TreeNode root = treeView1.Nodes[0];
                if (root.Nodes.Count > 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    foreach (TreeNode fileNode in root.Nodes)
                    {
                        foreach (TreeNode page in fileNode.Nodes)
                        {
                            foreach (TreeNode imgNode in page.Nodes)
                            {
                                ucThumbnail pbThumbnail = new ucThumbnail();
                                pbThumbnail.Size = new System.Drawing.Size(300, 300);
                                //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                                //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;

                                pbThumbnail.Tag = imgNode;
                                pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                                if (imgNode.Tag is ScanPage scanImage)
                                {
                                    pbThumbnail.ScanPage = (ScanPage)imgNode.Tag;
                                    if (scanImage.IsBlank)
                                    {
                                        //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                                    }
                                    pbThumbnail.Image = GetPageImage(scanImage.ImageId);
                                }
                                flowLayoutPanel1.Controls.Add(pbThumbnail);
                            }

                        }
                    }
                    // Handle root node selection
                }
            }
            else if (selected.Tag != null && selected.Tag is ScanFile file)
            {
                previewPic = null;
                TreeNodeCollection pages = selected.Nodes;
                flowLayoutPanel1.Controls.Clear();

                foreach (TreeNode page in pages)
                {
                    foreach (TreeNode img in page.Nodes)
                    {
                        ucThumbnail pbThumbnail = new ucThumbnail();
                        pbThumbnail.Size = new System.Drawing.Size(300, 300);
                        //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                        //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;
                        pbThumbnail.Tag = img;
                        pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                        if (img.Tag is ScanPage scanPage)
                        {
                            pbThumbnail.ScanPage = (ScanPage)img.Tag;
                            if (scanPage.IsBlank)
                            {
                                //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                            }
                            pbThumbnail.Image = GetPageImage(scanPage.ImageId);
                        }
                        flowLayoutPanel1.Controls.Add(pbThumbnail);
                    }
                }
            }
            else if (selected.Tag != null && selected.Tag is ScanSheet sheet)
            {
                previewPic = null;
                TreeNodeCollection imgNode = selected.Nodes;
                flowLayoutPanel1.Controls.Clear();
                foreach (TreeNode img in imgNode)
                {
                    ucThumbnail pbThumbnail = new ucThumbnail();
                    pbThumbnail.Size = new System.Drawing.Size(300, 300);
                    //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                    //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;
                    pbThumbnail.Tag = img;
                    pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                    if (img.Tag is ScanPage scanPage)
                    {
                        pbThumbnail.ScanPage = (ScanPage)img.Tag;
                        if (scanPage.IsBlank)
                        {
                            //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                        }
                        pbThumbnail.Image = GetPageImage(scanPage.ImageId);
                    }
                    flowLayoutPanel1.Controls.Add(pbThumbnail);
                }
            }
            else
            {
                tsClearArea.Enabled = true;
                tsStraigth.Enabled = true;
                tsRescan.Enabled = true;
                flowLayoutPanel1.Controls.Clear();
                PictureBox pbPreview = new PictureBox();

                pbPreview.Size = new System.Drawing.Size(flowLayoutPanel1.Size.Width, flowLayoutPanel1.Size.Height - 10);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                //pbPreview.BorderStyle = BorderStyle.FixedSingle;
                pbPreview.MouseDown += PreviewPic_MouseDown;
                pbPreview.MouseMove += PreviewPic_MouseMove;
                pbPreview.MouseUp += PreviewPic_MouseUp;
                pbPreview.Paint += PreviewPic_Paint;
                if (selected.Tag is ScanPage page)
                    pbPreview.Image = GetPageImage(page.ImageId);
                previewPic = pbPreview;
                flowLayoutPanel1.Controls.Add(pbPreview);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayNodeDetail();
        }

        private void pbThumbnail_DoubleClick(object? sender, EventArgs e)
        {
            ucThumbnail pb = (ucThumbnail)sender;
            TreeNode node = (TreeNode)pb.Tag;
            treeView1.SelectedNode = node;
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
            }
            else if (isComparing && (currentPoint != System.Drawing.Point.Empty))
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
                if (lastselectedNode.Tag != null && lastselectedNode.Tag is ScanPage page)
                {
                    img = GetPageImage(page.ImageId);
                }

                Bitmap res = ImageUtils.RotateImage((Bitmap)img, -(float)angle);

                startPoint = System.Drawing.Point.Empty;
                currentPoint = System.Drawing.Point.Empty;
                picBox.Image = res;

                //picBox.Invalidate(); // Final paint update
                //picBox.Refresh();

            }
            else if (isComparing && e.Button == MouseButtons.Right)
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
            }
            else if (isComparing)
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
            if (((PictureBox)sender).Image != null)
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
        }

        public bool IsBitmapBlank(Bitmap bitmap, decimal threshold, out decimal stdDevVal)
        {
            // Lock the bitmap's bits to access raw memory quickly
            using (Mat img = bitmap.ToMat())
            {
                if (img.Empty()) throw new Exception("Image cannot be loaded.");

                // Calculate the standard deviation and mean
                Cv2.MeanStdDev(img, out Scalar mean, out Scalar stdDev);

                // A standard deviation below the threshold indicates high uniformity (blank)
                stdDevVal = (decimal)stdDev.Val0;
                return (decimal)stdDev.Val0 < threshold;
            }


        }

        private void btnResetScan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear this scan batch?", "Export scan batch", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _queue.Clear();
                _sheet = null;
                documentNumber = 0;
                //listImage = null;
                scanPack = new ScanBatch();
                _currentFile = null;
                _currentSheet = null;
                currentFileNode = null;
                treeView1.Nodes[0].Nodes.Clear();
                flowLayoutPanel1.Controls.Clear();
                _scanPackBackup = new ScanBatch();
                originalPack = null;
                var source = _twain.CurrentSource;
                source.Close();
                source.Open();
                SetDiscardBlankPage(_separateObject);
            }
        }



        ScanBatch _scanPackBackup = new ScanBatch();
        private void chkBlankPage_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void UpdateBlankValue(decimal val)
        {
            //if (originalPack == null) return;
            //for (int j = 0; j < originalPack.ScanFiles.Count; j++)
            //{
            //    ScanFile file = originalPack.ScanFiles[j];
            //    foreach (ScanPage page in file.Pages)
            //    {
            //        if (page.StdDevVal <= val)
            //        {
            //            page.IsBlank = true;
            //        }
            //        else
            //        {
            //            page.IsBlank = false;
            //        }
            //    }


            //}
        }
        private void RefreshTreeView()
        {
            if (originalPack == null) return;
            SeparateType sepType = SeparateType.Persheet;
            if (_separateObject != null && _separateObject.SeparateMode != null)
            {
                sepType = _separateObject.SeparateMode;
                if (!_separateObject.HideBlankPage && !_separateObject.HideBlankSheet)
                {
                    if (originalPack != null)
                    {
                        _scanPackBackup = originalPack.Clone();
                        ReloadTreeView(_scanPackBackup, sepType);
                    }
                }
                else
                {
                    _scanPackBackup = originalPack.Clone();

                    for (int j = 0; j < _scanPackBackup.ScanFiles.Count; j++)
                    {
                        ScanFile file = _scanPackBackup.ScanFiles[j];

                        if (_separateObject.SeparateMode != SeparateType.BlankSheet)
                        {
                            if (file.Sheets.Count==1 && file.Sheets[0].IsBlankSheet && _separateObject.HideBlankSheet)
                            {
                                _scanPackBackup.ScanFiles.RemoveAt(j);
                                j--;

                            }
                            if (_separateObject.HideBlankPage && !(file.Sheets.Count == 1 && file.Sheets[0].IsBlankSheet))
                            {
                                for (int i = 0; i < file.Sheets.Count; i++)
                                {
                                    file.Sheets[i].SheetIndex = i + 1;
                                    if (file.Sheets[i].IsBlankSheet && _separateObject.HideBlankPage)
                                    {
                                        file.Sheets.RemoveAt(i);
                                        i--; // Adjust the index after removal
                                    }
                                    //i++;
                                }
                            }

                            // Adjust the index after removal
                        }
                        else
                        {
                            //if (!(!(_separateObject.SeparateMode == SeparateType.BlankSheet) && file.IsBlankSheet))
                            if (_separateObject.SeparateMode == SeparateType.BlankSheet)
                            {
                                if (file.Sheets.Count == 1 && file.Sheets[0].IsBlankSheet) break;
                                for (int i = 0; i < file.Sheets.Count; i++)
                                {
                                    file.Sheets[i].SheetIndex = i + 1;
                                    if (file.Sheets[i].IsBlankSheet && _separateObject.HideBlankPage)
                                    {
                                        file.Sheets.RemoveAt(i);
                                        i--; // Adjust the index after removal
                                    }
                                }
                            }
                        }

                    }
                    ReloadTreeView(_scanPackBackup, sepType);
                }

            }




        }
        private void ReloadTreeView(ScanBatch pack, SeparateType type)
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
                        scanPack = new ScanBatch();
                        foreach (ScanFile file in pack.ScanFiles)
                        {
                            scanPack.ScanFiles.Add(file);
                            AddFile(file);
                        }
                    }
                    break;
                case SeparateType.NumOfPage:
                    {
                        scanPack = new ScanBatch();
                        int fileIndex = 1;
                        int pageIndex = 1;
                        int totalPage = _separateObject.TotalPage;
                        ScanFile tmpFile = null;
                        foreach (ScanFile file in pack.ScanFiles)
                        {
                            //if (pageIndex == 1)
                            //{
                            //    if (tmpFile != null)
                            //    {
                            //        AddFile(tmpFile);
                            //    }
                            //    tmpFile = new ScanFile();
                            //    tmpFile.FileName = "File_" + fileIndex;

                            //    fileIndex++;
                            //}
                            for (int i = 0; i < file.Sheets.Count; i++)
                            {
                                ScanSheet sheet = file.Sheets[i];
                                if (pageIndex <= totalPage)
                                {
                                    tmpFile.Sheets.Add(sheet);
                                    if (pageIndex >= totalPage)
                                    {
                                        scanPack.ScanFiles.Add(tmpFile);
                                        pageIndex = 1;
                                        if (i + 1 < file.Sheets .Count)
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
                case SeparateType.BlankSheet:
                    {
                        scanPack = new ScanBatch();
                        int fileIndex = 1;

                        ScanFile tmpFile = null;
                        foreach (ScanFile file in pack.ScanFiles)
                        {
                            if ((file.Sheets.Count == 1 && file.Sheets[0].IsBlankSheet) || tmpFile == null)
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
                            if (!(file.Sheets.Count == 1 && file.Sheets[0].IsBlankSheet))
                            {
                                tmpFile.Sheets.AddRange(file.Sheets);
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



        private void btnMockData_Click(object sender, EventArgs e)
        {
            string datafile = "mockdata.json";
            if (File.Exists(datafile))
            {
                string content = File.ReadAllText(datafile);
                ScanBatch data = JsonConvert.DeserializeObject<ScanBatch>(content);
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






        WhiteBorderObject _whiteBorderSetting = new WhiteBorderObject { Top = 10, Right = 10, Bottom = 10, Left = 10 };
        private void PreviewWhiteBorder(WhiteBorderObject border)
        {
            _whiteBorderSetting = border;
            previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
        }
        PDFUtils pdfUtils = new PDFUtils();
        private void btnExport_Click(object sender, EventArgs e)
        {
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                List<Image> images = new List<Image>();
                foreach (ScanSheet sheet in file.Sheets)
                {
                    
                    if (sheet.Top != null) {
                        images.Add(sheet.Top.PageImage);
                    }
                    if (sheet.Bottom != null)
                    {
                        images.Add(sheet.Bottom.PageImage);
                    }
                }
                // Create PDF with the images

                pdfUtils.CreatePDF(file.FileName + ".pdf", @"D:\ExportScanTool\", images);
            }
        }

        private void UpdatePageImage(string id, Image img)
        {
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanSheet sheet in file.Sheets)
                {
                    if (sheet.Top != null && sheet.Top.ImageId == id)
                    {
                        sheet.Top.PageImage = img;
                        return;
                    }
                    if (sheet.Bottom != null && sheet.Bottom.ImageId == id)
                    {
                        sheet.Bottom.PageImage = img;
                        return;
                    }   
                }
            }
        }



        private void ApplyChangeOneImage()
        {
            if (lastselectedNode != null)
            {
                ScanPage selectedPage = lastselectedNode.Tag as ScanPage;
                if (selectedPage != null)
                {
                    UpdatePageImage(selectedPage.ImageId, previewPic.Image);
                }
            }
        }

        private void ApplyCurrentPicture(WhiteBorderObject border)
        {
            _whiteBorderSetting = border;
            if (previewPic != null && previewPic.Image != null)
            {
                previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, _whiteBorderSetting.Top, _whiteBorderSetting.Right, _whiteBorderSetting.Bottom, _whiteBorderSetting.Left);
                //previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
                ScanPage selectedPage = lastselectedNode.Tag as ScanPage;
                UpdatePageImage(selectedPage.ImageId, previewPic.Image);
            }
            else //kiểm tra nếu có thumbnail dc chọn
            {
                foreach(object child in flowLayoutPanel1.Controls)
                {
                    ucThumbnail tmp = (ucThumbnail)child;
                    if (tmp.IsSelected)
                    {
                        tmp.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)tmp.Image, _whiteBorderSetting.Top, _whiteBorderSetting.Right, _whiteBorderSetting.Bottom, _whiteBorderSetting.Left);
                        //previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
                        ScanPage selectedPage = tmp.ScanPage;
                        UpdatePageImage(selectedPage.ImageId, tmp.Image);
                    }
                }
            }
            //DisplayNodeDetail();
        }

        private void ApplyAllBatchPicture(WhiteBorderObject border)
        {
            _whiteBorderSetting = border;

            if (previewPic != null && previewPic.Image != null)
            {
                previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, _whiteBorderSetting.Top, _whiteBorderSetting.Right, _whiteBorderSetting.Bottom, _whiteBorderSetting.Left);
            }
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanSheet sheet in file.Sheets)
                {
                    if (sheet.Top != null)
                    {
                        sheet.Top.PageImage = ImageUtils.AddOuterWhiteBorder((Bitmap)sheet.Top.PageImage, _whiteBorderSetting.Top, _whiteBorderSetting.Right, _whiteBorderSetting.Bottom, _whiteBorderSetting.Left);
                    }
                    if (sheet.Bottom != null)
                    {
                        sheet.Bottom.PageImage = ImageUtils.AddOuterWhiteBorder((Bitmap)sheet.Bottom.PageImage, _whiteBorderSetting.Top, _whiteBorderSetting.Right, _whiteBorderSetting.Bottom, _whiteBorderSetting.Left);
                    }
                }
            }
            DisplayNodeDetail();
        }

        private string GetSubfitIndexNumber(string prefitString, string separateChar, int index)
        {
            string res = "";
            string num = index.ToString();
            string indexFormat = scanPack.IndexFormat;
            prefitString = prefitString == null ? "Batch_" : prefitString;
            indexFormat = indexFormat == null ? "" : indexFormat;
            for (int i = 0; i < indexFormat.Length - num.Length; i++)
            {
                res = "0" + res;
            }
            return prefitString + separateChar + res + index;

        }

        private async void btnExportBatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to export this scan batch?", "Export scan batch", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (scanPack.BatchName == null || scanPack.ExportPath == null)
                {
                    MessageBox.Show("Export name or export path is empty!");
                }
                else
                {
                    lblStatusText.Text = "Exporting...";

                    if (scanPack.CreateSubFolder)
                    {
                        string path = System.IO.Path.Combine(scanPack.ExportPath, scanPack.BatchName);
                        if (!Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                    }
                    var progress = new Progress<int>(percent =>
                    {
                        tsProgressBar.Value = percent;
                        if (percent == 100)
                        {
                            MessageBox.Show("Export Done!");
                            tsProgressBar.Value = 0;
                            lblStatusText.Text = "Status";
                        }
                    });
                    await Task.Run(() => ExportBatch(progress));
                    //MessageBox.Show("Export Done!");
                }
            }
        }

        private void ExportBatch(IProgress<int> progress)
        {
            int index = 1;

            foreach (ScanFile file in scanPack.ScanFiles)
            {
                List<Image> images = new List<Image>();
                foreach (ScanSheet sheet in file.Sheets)
                {
                    if (sheet.Top != null)
                    {
                        images.Add(sheet.Top.PageImage);
                    }
                    if (sheet.Bottom != null)
                    {
                        images.Add(sheet.Bottom.PageImage);
                    }           
                }
                // Create PDF with the images
                string name = GetSubfitIndexNumber(scanPack.BatchName, scanPack.SeparateChar, index);
                pdfUtils.CreatePDF(name + ".pdf", scanPack.GetFullExportPath(), images);
                progress?.Report((int)((double)index / scanPack.ScanFiles.Count * 100));
                index++;
            }

        }

        private void tsSelectScanner_SelectedChanged(object sender, EventArgs e)
        {

        }
        private void SetDiscardBlankPage(SeparateObject _separateObject)
        {
            var source = _twain.CurrentSource;
            // Kiểm tra xem máy quét có hỗ trợ ICAP_AUTODISCARDBLANKPAGES không
            if (source.Capabilities.ICapAutoDiscardBlankPages.IsSupported)
            {
                // Đặt chế độ tự động bỏ trang trắng (TWBP_AUTO = -2)
                BlankPage autoDiscardBlankPages = BlankPage.Disable;
                if (_separateObject.HideBlankPage)
                {
                    autoDiscardBlankPages = BlankPage.Auto;
                }
                var status = source.Capabilities.ICapAutoDiscardBlankPages.SetValue(autoDiscardBlankPages);

                if (status == ReturnCode.Success)
                {
                    Console.WriteLine("Đã bật tính năng tự động loại bỏ trang trắng (TWBP_AUTO).");
                }
                else
                {
                    Console.WriteLine("Máy quét hỗ trợ nhưng không thể đặt giá trị TWBP_AUTO.");
                }
            }
            else
            {
                Console.WriteLine("Máy quét không hỗ trợ ICAP_AUTODISCARDBLANKPAGES.");
            }
        }
        SeparateObject _separateObject = new SeparateObject();
        private void tsSeparate_Click(object sender, EventArgs e)
        {
            SeparateDialog separateDialog = new SeparateDialog(_separateObject);
            if (scanPack != null && scanPack.ScanFiles.Count > 0) separateDialog.DisableSeparateMode = true;
            if (separateDialog.ShowDialog() == DialogResult.OK)
            {
                bool hasChangeBlankValue = false;
                if (_separateObject.BlankValue != separateDialog.SeparatedDataObject.BlankValue)
                {
                    hasChangeBlankValue = true;
                    UpdateBlankValue(separateDialog.SeparatedDataObject.BlankValue);
                }
                _separateObject = separateDialog.SeparatedDataObject;
                SetDiscardBlankPage(_separateObject);
                RefreshTreeView();
            }
        }

        private void tsWhiteBorder_Click(object sender, EventArgs e)
        {
            WhiteBorderDialog whiteBorderDialog = new WhiteBorderDialog();
            whiteBorderDialog.WhiteBorderData = _whiteBorderSetting;
            whiteBorderDialog.CallPreview = PreviewWhiteBorder;
            whiteBorderDialog.CallApply = ApplyCurrentPicture;
            whiteBorderDialog.CallApplyAll = ApplyAllBatchPicture;
            ;
            whiteBorderDialog.ShowDialog();

        }

        private void miIsblank_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode.Tag is ScanPage)
            {
                ScanPage page = (ScanPage)selectedNode.Tag;
                _separateObject.BlankValue = (decimal)page.StdDevVal;
                UpdateBlankValue(_separateObject.BlankValue);
                RefreshTreeView();
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                TreeNode selected = e.Node;
                if (selected.Tag == "root")
                {
                    DisplayRootNodeMenu();

                }
                else if (selected.Tag != null && selected.Tag is ScanFile)
                {
                    DisplayFileNodeMenu();
                }
                else if (selected.Tag != null && selected.Tag is ScanPage)
                {
                    DisplayPageNodeMenu();
                }
                menuTreview.Show(treeView1, e.X, e.Y);
            }

        }

        private void DisplayRootNodeMenu()
        {
            miIsblank.Enabled = false;
            miAddNew.Enabled = true;
            miInsertScan.Enabled = false;
            miRescan.Enabled = false;
            miDelete.Enabled = false;
            miRename.Enabled = true;
        }
        private void DisplayFileNodeMenu()
        {
            miIsblank.Enabled = false;
            miAddNew.Enabled = true;
            miInsertScan.Enabled = true;
            miRescan.Enabled = true;
            miDelete.Enabled = true;
            miRename.Enabled = false;
        }
        private void DisplayPageNodeMenu()
        {
            miIsblank.Enabled = true;
            miAddNew.Enabled = false;
            miInsertScan.Enabled = true;
            miRescan.Enabled = true;
            miDelete.Enabled = true;
            miRename.Enabled = false;
        }

        private void DisplayNoNodeMenu()
        {
            miIsblank.Enabled = false;
            miAddNew.Enabled = true;
            miInsertScan.Enabled = false;
            miRescan.Enabled = false;
            miDelete.Enabled = false;
            miRename.Enabled = false;
        }

        private void treeView1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            treeView1.SelectedNode = null;
        }
        private void UpdateBatchName(string newName)
        {
            treeView1.Nodes[0].Text = newName;
        }
        private void miRename_Click(object sender, EventArgs e)
        {
            BatchRename renameDialog = new BatchRename();
            if (renameDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the batch name
                UpdateBatchName(renameDialog.NewBatchName);
            }
        }

        private void tsNewBatch_Click(object sender, EventArgs e)
        {
            BatchSetting batchSettingDialog = new BatchSetting();
            batchSettingDialog.ExportPath = scanPack.ExportPath;
            batchSettingDialog.BatchName = scanPack.BatchName;
            batchSettingDialog.IndexFormat = scanPack.IndexFormat;
            batchSettingDialog.CreateSubFolder = scanPack.CreateSubFolder;
            batchSettingDialog.SeparateChar = scanPack.SeparateChar;

            if (batchSettingDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the batch name
                UpdateBatchName(batchSettingDialog.BatchName);
                // Update the export path
                if (scanPack == null)
                {
                    scanPack = new ScanBatch();
                }
                scanPack.ExportPath = batchSettingDialog.ExportPath;
                scanPack.BatchName = batchSettingDialog.BatchName;
                scanPack.IndexFormat = batchSettingDialog.IndexFormat;
                scanPack.SeparateChar = batchSettingDialog.SeparateChar;
                scanPack.CreateSubFolder = batchSettingDialog.CreateSubFolder;
            }
        }

        private void tsRotateRight_Click(object sender, EventArgs e)
        {
            if (previewPic != null && previewPic.Image != null)
            {
                previewPic.Image = ImageUtils.RotateDegrees((Bitmap)previewPic.Image, true);
                ApplyChangeOneImage();
            }
            else
            {
                foreach (object child in flowLayoutPanel1.Controls)
                {
                    ucThumbnail tmp = (ucThumbnail)child;
                    if (tmp.IsSelected)
                    {
                        tmp.Image = ImageUtils.RotateDegrees((Bitmap)tmp.Image, true);
                        //previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
                        ScanPage selectedPage = tmp.ScanPage;
                        UpdatePageImage(selectedPage.ImageId, tmp.Image);
                    }
                }
            }

        }

        private void tsRotateLeft_Click(object sender, EventArgs e)
        {
            if (previewPic != null && previewPic.Image != null)
            {

                previewPic.Image = ImageUtils.RotateDegrees((Bitmap)previewPic.Image, false);
                ApplyChangeOneImage();
            }
            else
            {
                foreach (object child in flowLayoutPanel1.Controls)
                {
                    ucThumbnail tmp = (ucThumbnail)child;
                    if (tmp.IsSelected)
                    {
                        tmp.Image = ImageUtils.RotateDegrees((Bitmap)tmp.Image, false);
                        //previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
                        ScanPage selectedPage = tmp.ScanPage;
                        UpdatePageImage(selectedPage.ImageId, tmp.Image);
                    }
                }
            }
        }

        private void tsUpDown_Click(object sender, EventArgs e)
        {
            if (previewPic != null && previewPic.Image != null)
            {

                previewPic.Image = ImageUtils.RotateDegrees((Bitmap)previewPic.Image, true, true);
                ApplyChangeOneImage();
            }
            else
            {
                foreach (object child in flowLayoutPanel1.Controls)
                {
                    ucThumbnail tmp = (ucThumbnail)child;
                    if (tmp.IsSelected)
                    {
                        tmp.Image = ImageUtils.RotateDegrees((Bitmap)tmp.Image, true, true);
                        //previewPic.Image = ImageUtils.AddOuterWhiteBorder((Bitmap)previewPic.Image, border.Top, border.Right, border.Bottom, border.Left);
                        ScanPage selectedPage = tmp.ScanPage;
                        UpdatePageImage(selectedPage.ImageId, tmp.Image);
                    }
                }
            }
        }

        
    }
}
