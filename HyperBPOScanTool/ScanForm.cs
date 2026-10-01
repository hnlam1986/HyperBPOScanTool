using HyperBPOScanTool.Models;
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
        MyTreeNode currentFileNode = null;
        private void AddNewScanImage(ScanSheet sheet, SeparateType type, bool isRescan = false)
        {
            if (isRescan) return;
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
                                ScanFile selFile = scanPack.ScanFiles.Where(i => i.FileId == _currentFile.FileId).FirstOrDefault();
                                selFile.Sheets.Add(sheet);
                            }
                        }
                        else
                        {
                            _currentFile = null;
                        }

                    }
                    break;
            }
            DisplayBatchInfo();
        }

        private void DisplayBatchInfo()
        {
            this.BeginInvoke(new Action(() =>
            {
                tslblTotalFile.Text = "Total File: " + scanPack.GetTotalFileCount() + " files";
                tslblTotalPages.Text = "Total Sheet: " + scanPack.GetTotalPageCount() + " pages";
                if (lastselectedNode.NodeType == NodeType.File)
                {
                    ScanFile file = lastselectedNode.ScanObject as ScanFile;
                    tslblSelectedTotalPage.Text = scanPack.GetTotalPageCountByFile(file.FileId) + " pages";
                }
                else
                {
                    tslblSelectedTotalPage.Text = "";
                }

            }));
        }

        private MyTreeNode AddFile(ScanFile file)
        {
            MyTreeNode node = new MyTreeNode();
            node.ScanObject = file;
            node.NodeType = NodeType.File;
            node.Text = file.FileName;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            node.Expand();
            foreach (ScanSheet sheet in file.Sheets)
            {
                MyTreeNode sheetNode = new MyTreeNode();
                sheetNode.ScanObject = sheet;
                sheetNode.NodeType = NodeType.Sheet;
                sheetNode.Text = "Page_" + (sheet.SheetIndex + 1);
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


                MyTreeNode imageNodeTop = new MyTreeNode();
                imageNodeTop.ScanObject = sheet.Top;
                imageNodeTop.NodeType = NodeType.Page;
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


                MyTreeNode imageNodeBottom = new MyTreeNode();
                imageNodeBottom.ScanObject = sheet.Bottom;
                imageNodeBottom.NodeType = NodeType.Page;
                imageNodeBottom.Text = "Image_2";
                imageNodeBottom.Expand();
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
        private void AddSheet(ScanSheet sheet, MyTreeNode currentFileNode)
        {
            MyTreeNode sheetNode = new MyTreeNode();
            sheetNode.ScanObject = sheet;
            sheetNode.NodeType = NodeType.Sheet;
            sheetNode.Text = "Page_" + (currentFileNode.Nodes.Count + 1);
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
                MyTreeNode imageNodeTop = new MyTreeNode();
                imageNodeTop.ScanObject = sheet.Top;
                imageNodeTop.NodeType = NodeType.Page;
                imageNodeTop.Text = "Image_1";
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


                MyTreeNode imageNodeBottom = new MyTreeNode();
                imageNodeBottom.ScanObject = sheet.Bottom;
                imageNodeBottom.NodeType = NodeType.Page;
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
            //PlatformInfo.Current.Log.Info("Start: " + DateTime.Now.ToLongTimeString());
            //PlatformInfo.Current.Log.Info("hinh gui xong");
            //PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

            // example on getting ext image info
            var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
            string camInfoString = (infos.FirstOrDefault()).ReadValues().FirstOrDefault().ToString();
            CamMode camInfo = CamMode.Top;
            if (camInfoString == "/Camera_Color_Top")
            {
                camInfo = CamMode.Top;
            }
            else if (camInfoString == "/Camera_Color_Bottom")
            {
                camInfo = CamMode.Bottom;
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
                scanImage.CamMode = camInfo;

                _queue.Enqueue(scanImage);
            }

            //PlatformInfo.Current.Log.Info("End: " + DateTime.Now.ToLongTimeString());
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
                int pos = 0;
                int selectedIndex = -1;
                tsSelectScanner.SelectedChanged -= SourceMenuItem_Click;
                foreach (var src in _twain)
                {
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
                toolStrip1.Enabled = true;
                toolStrip2.Enabled = true;
            }
        }
        private void DoScan()
        {
            PlatformInfo.Current.Log.Info("Start scan button: " + DateTime.Now.ToLongTimeString());
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
        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            DoScan();
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

                    if (_sheet == null)
                    {
                        _sheet = new ScanSheet();
                        _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                        _sheet.SheetId = DateTime.Now.Ticks.ToString();
                        if (documentNumber + 1 < scanImage.DocIndex && sepType == SeparateType.BlankSheet)
                        {
                            //add blank sheet
                            AddNewScanImage(_sheet, sepType, IsRescanMode);
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
                            AddNewScanImage(_sheet, sepType, IsRescanMode);
                            //create blank sheet
                            _sheet = new ScanSheet();
                            _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                            _sheet.SheetId = DateTime.Now.Ticks.ToString();
                            //add blank sheet
                            AddNewScanImage(_sheet, sepType, IsRescanMode);
                            //create next new sheet
                            _sheet = new ScanSheet();
                            _sheet.SheetName = "Sheet_" + (scanPack.ScanFiles.Count + 1);
                            _sheet.SheetId = DateTime.Now.Ticks.ToString();
                        }
                        documentNumber = scanImage.DocIndex;
                    }

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
                        AddNewScanImage(_sheet, sepType, IsRescanMode);
                        if (!IsRescanMode) _sheet = null;
                    }
                    progress.Report(_queue.Count);
                }
                if (_queue.Count == 0)
                {
                    if (_sheet != null)
                    {
                        if (IsRescanMode)
                        {
                            UpdateRescanImage(lastselectedNode.ScanObject as ScanSheet, _sheet);
                            IsRescanMode = false;
                            _sheet = null;
                        }
                        else
                        {

                            AddNewScanImage(_sheet, sepType, IsRescanMode);
                            _sheet = null;
                        }
                    }
                    Thread.Sleep(200);
                }

            }
        }
        private MyTreeNode ShowIconTreeNode(MyTreeNode node)
        {
            if (node.NodeType == NodeType.Root)
            {

            }
            else if (node.NodeType == NodeType.File)
            {

            }
            else if (node.NodeType == NodeType.Sheet)
            {

            }
            else if (node.NodeType == NodeType.Page)
            {
                if (node.ScanObject != null)
                {
                    ScanPage page = node.ScanObject as ScanPage;
                    if (page.IsBlank)
                    {
                        node.ImageIndex = 5;
                        node.SelectedImageIndex = 5;
                    }
                    else
                    {
                        node.ImageIndex = 4;
                        node.SelectedImageIndex = 4;
                    }
                }
            }
            return node;
        }
        private void UpdateRescanImage(ScanSheet selectedSheet, ScanSheet newSheet)
        {
            if (selectedSheet != null)
            {
                selectedSheet.Top = null;
                selectedSheet.Bottom = null;
                MyTreeNode top = null;
                MyTreeNode bottom = null;
                if (_sheet.Top != null)
                {
                    selectedSheet.Top = _sheet.Top;
                    top = new MyTreeNode();
                    top.Text = "Image_1";
                    top.ScanObject = selectedSheet.Top;
                    top.NodeType = NodeType.Page;
                    top = ShowIconTreeNode(top);

                }
                if (_sheet.Bottom != null)
                {
                    selectedSheet.Bottom = _sheet.Bottom;
                    bottom = new MyTreeNode();
                    bottom.Text = "Image_2";
                    bottom.ScanObject = selectedSheet.Bottom;
                    bottom.NodeType = NodeType.Page;
                    bottom = ShowIconTreeNode(bottom);
                }
                lastselectedNode.ScanObject = selectedSheet;
                ScanSheet tmp = scanPack.ScanFiles.SelectMany(f => f.Sheets).Where(s => s.SheetId == selectedSheet.SheetId).FirstOrDefault();
                if (tmp != null)
                {
                    tmp.Top = selectedSheet.Top;
                    tmp.Bottom = selectedSheet.Bottom;
                    this.BeginInvoke(new Action(() =>
                    {
                        lastselectedNode.Nodes.Clear();
                        if (top != null)
                        {
                            lastselectedNode.Nodes.Add(top);
                        }
                        if (bottom != null)
                        {
                            lastselectedNode.Nodes.Add(bottom);
                        }
                        DisplayNodeDetail();
                    }));

                }
            }
        }
        private void btnStopScan_Click(object sender, EventArgs e)
        {
            _stopScan = true;
        }



        #endregion

        #region cap control


        private void LoadSourceCaps()
        {
            var src = _twain.CurrentSource;
            _loadingCaps = true;


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
            toolStrip1.Enabled = false;
            toolStrip2.Enabled = false;
            ImageList listImage = new ImageList();
            listImage.Images.Add(Properties.Resources.batch); //0
            listImage.Images.Add(Properties.Resources.document);  //1
            listImage.Images.Add(Properties.Resources.ok_page);  //2
            listImage.Images.Add(Properties.Resources.blank_page);  //3
            listImage.Images.Add(Properties.Resources.image);  //4
            listImage.Images.Add(Properties.Resources.blank_img);  //5
            treeView1.ImageList = listImage;
            MyTreeNode root = new MyTreeNode();
            root.Text = "Batch";
            root.Tag = "root";
            root.ImageIndex = 0;
            root.SelectedImageIndex = 0;
            treeView1.Nodes.Add(root);
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
        MyTreeNode lastselectedNode = null;
        private void DisplayNodeDetail()
        {
            if (lastselectedNode != null)
            {
                lastselectedNode.BackColor = Color.Empty;
                lastselectedNode.ForeColor = Color.Black;
            }
            MyTreeNode selected = (MyTreeNode)treeView1.SelectedNode;
            selected.BackColor = SystemColors.Highlight;
            selected.ForeColor = Color.White;
            lastselectedNode = selected;
            tsClearArea.Enabled = false;
            tsStraigth.Enabled = false;
            tsRescan.Enabled = false;
            if (selected.Tag == "root")
            {
                previewPic = null;
                MyTreeNode root = treeView1.Nodes[0] as MyTreeNode;
                if (root.Nodes.Count > 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    foreach (MyTreeNode fileNode in root.Nodes)
                    {
                        foreach (MyTreeNode page in fileNode.Nodes)
                        {
                            foreach (MyTreeNode imgNode in page.Nodes)
                            {
                                ucThumbnail pbThumbnail = new ucThumbnail();
                                pbThumbnail.Size = new System.Drawing.Size(300, 300);
                                //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                                //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;

                                pbThumbnail.Tag = imgNode;
                                pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                                if (imgNode.NodeType == NodeType.Page)
                                {
                                    pbThumbnail.ScanPage = (ScanPage)imgNode.ScanObject;
                                    if (pbThumbnail.ScanPage.IsBlank)
                                    {
                                        //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                                    }
                                    pbThumbnail.Image = GetPageImage(pbThumbnail.ScanPage.ImageId);
                                }
                                flowLayoutPanel1.Controls.Add(pbThumbnail);
                            }

                        }
                    }
                    // Handle root node selection
                }
            }
            else if (selected.ScanObject != null && selected.NodeType == NodeType.File)
            {
                previewPic = null;
                TreeNodeCollection pages = selected.Nodes;
                flowLayoutPanel1.Controls.Clear();

                foreach (MyTreeNode page in pages)
                {
                    foreach (MyTreeNode img in page.Nodes)
                    {
                        ucThumbnail pbThumbnail = new ucThumbnail();
                        pbThumbnail.Size = new System.Drawing.Size(300, 300);
                        //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                        //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;
                        pbThumbnail.Tag = img;
                        pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                        if (img.NodeType == NodeType.Page)
                        {
                            pbThumbnail.ScanPage = (ScanPage)img.ScanObject;
                            if (pbThumbnail.ScanPage.IsBlank)
                            {
                                //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                            }
                            pbThumbnail.Image = GetPageImage(pbThumbnail.ScanPage.ImageId);
                        }
                        flowLayoutPanel1.Controls.Add(pbThumbnail);
                    }
                }
            }
            else if (selected.ScanObject != null && selected.NodeType == NodeType.Sheet)
            {
                tsRescan.Enabled = true;
                previewPic = null;
                TreeNodeCollection imgNode = selected.Nodes;
                flowLayoutPanel1.Controls.Clear();
                foreach (MyTreeNode img in imgNode)
                {
                    ucThumbnail pbThumbnail = new ucThumbnail();
                    pbThumbnail.Size = new System.Drawing.Size(300, 300);
                    //pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
                    //pbThumbnail.BorderStyle = BorderStyle.FixedSingle;
                    pbThumbnail.Tag = img;
                    pbThumbnail.PictureBoxDoubleClicked += pbThumbnail_DoubleClick;
                    if (img.NodeType == NodeType.Page)
                    {
                        pbThumbnail.ScanPage = (ScanPage)img.ScanObject;
                        if (pbThumbnail.ScanPage.IsBlank)
                        {
                            //pbThumbnail.BackColor = Color.FromArgb(255, 240, 240);
                        }
                        pbThumbnail.Image = GetPageImage(pbThumbnail.ScanPage.ImageId);
                    }
                    flowLayoutPanel1.Controls.Add(pbThumbnail);
                }
            }
            else
            {
                tsClearArea.Enabled = true;
                tsStraigth.Enabled = true;
                tsRescan.Enabled = false;
                flowLayoutPanel1.Controls.Clear();
                PictureBox pbPreview = new PictureBox();

                pbPreview.Size = new System.Drawing.Size(flowLayoutPanel1.Size.Width, flowLayoutPanel1.Size.Height - 10);
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
                //pbPreview.BorderStyle = BorderStyle.FixedSingle;
                pbPreview.MouseDown += PreviewPic_MouseDown;
                pbPreview.MouseMove += PreviewPic_MouseMove;
                pbPreview.MouseUp += PreviewPic_MouseUp;
                pbPreview.Paint += PreviewPic_Paint;
                if (selected.NodeType == NodeType.Page)
                {
                    ScanPage page = new ScanPage();
                    page = (ScanPage)selected.ScanObject;
                    pbPreview.Image = GetPageImage(page.ImageId);
                    previewPic = pbPreview;
                    flowLayoutPanel1.Controls.Add(pbPreview);
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            DisplayNodeDetail();
            DisplayBatchInfo();
        }

        private void pbThumbnail_DoubleClick(object? sender, EventArgs e)
        {
            ucThumbnail pb = (ucThumbnail)sender;
            MyTreeNode node = (MyTreeNode)pb.Tag;
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
            if (((PictureBox)sender).Image != null && isStraightenMode)
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
                if (source != null)
                {
                    source.Close();
                    source.Open();
                    SetDiscardBlankPage(_separateObject);
                }
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
                            if (file.Sheets.Count == 1 && file.Sheets[0].IsBlankSheet && _separateObject.HideBlankSheet)
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
                                        if (i + 1 < file.Sheets.Count)
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

                pdfUtils.CreatePDF(file.FileName + ".pdf", @"D:\ExportScanTool\", images);
            }
        }

        private Image UpdatePageImage(string id, Image img)
        {
            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanSheet sheet in file.Sheets)
                {
                    if (sheet.Top != null && sheet.Top.ImageId == id)
                    {
                        sheet.Top.PageImage = img;
                        return img;
                    }
                    if (sheet.Bottom != null && sheet.Bottom.ImageId == id)
                    {
                        sheet.Bottom.PageImage = img;
                        return img;
                    }
                }
            }
            return null;
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
                ScanPage selectedPage = lastselectedNode.ScanObject as ScanPage;
                UpdatePageImage(selectedPage.ImageId, previewPic.Image);
            }
            else //kiểm tra nếu có thumbnail dc chọn
            {
                foreach (object child in flowLayoutPanel1.Controls)
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
            if (scanPack.BatchName == null || scanPack.ExportPath == null)
            {
                MessageBox.Show("Export name or export path is empty!");
            }
            else if (scanPack.ScanFiles == null || scanPack.ScanFiles.Count == 0)
            {
                MessageBox.Show("No data to export!", "Export scan batch", MessageBoxButtons.OK);
            }
            else if (MessageBox.Show("Do you want to export this scan batch?", "Export scan batch", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
                MyTreeNode selected = (MyTreeNode)e.Node;
                if (selected.Tag == "root")
                {
                    DisplayRootNodeMenu();

                }
                else if (selected.ScanObject != null && selected.NodeType == NodeType.File)
                {
                    DisplayFileNodeMenu();
                }
                else if (selected.ScanObject != null && selected.NodeType == NodeType.Sheet)
                {
                    DisplaySheetNodeMenu();
                }
                else if (selected.ScanObject != null && selected.NodeType == NodeType.Page)
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
            miSwapPage.Enabled = false;
        }
        private void DisplayFileNodeMenu()
        {
            miIsblank.Enabled = false;
            miAddNew.Enabled = true;
            miInsertScan.Enabled = true;
            miRescan.Enabled = false;
            miDelete.Enabled = true;
            miRename.Enabled = false;
            miSwapPage.Enabled = false;
        }
        private void DisplaySheetNodeMenu()
        {
            miIsblank.Enabled = true;
            miAddNew.Enabled = false;
            miInsertScan.Enabled = false;
            miRescan.Enabled = true;
            miDelete.Enabled = true;
            miRename.Enabled = false;
            miSwapPage.Enabled = true;
        }
        private void DisplayPageNodeMenu()
        {
            miIsblank.Enabled = true;
            miAddNew.Enabled = false;
            miInsertScan.Enabled = false;
            miRescan.Enabled = false;
            miDelete.Enabled = true;
            miRename.Enabled = false;
            miSwapPage.Enabled = false;
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

        BatchSettingObject _batchSetting = null;
        private void tsNewBatch_Click(object sender, EventArgs e)
        {
            BatchSetting batchSettingDialog = new BatchSetting();
            batchSettingDialog.CurrentBatchSetting = _batchSetting;

            if (batchSettingDialog.ShowDialog() == DialogResult.OK)
            {
                _batchSetting = batchSettingDialog.CurrentBatchSetting;
                // Update the batch name
                UpdateBatchName(_batchSetting.BatchName);
                // Update the export path
                if (scanPack == null)
                {
                    scanPack = new ScanBatch();
                }
                scanPack.ExportPath = _batchSetting.ExportFolder;
                scanPack.BatchName = _batchSetting.BatchName;
                scanPack.IndexFormat = _batchSetting.IndexFormat;
                scanPack.SeparateChar = _batchSetting.SeparateChar;
                scanPack.CreateSubFolder = _batchSetting.HasCreateSubFolder;
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
        bool isStraightenMode = false;
        private void tsStraigth_Click(object sender, EventArgs e)
        {
            tsOK.Visible = true;
            tsCancel.Visible = true;
            tsStraigth.BackColor = Color.DarkGray;
            tsStraigth.Enabled = false;
            isStraightenMode = true;
        }

        private void tsCancel_Click(object sender, EventArgs e)
        {
            tsOK.Visible = false;
            tsCancel.Visible = false;
            tsStraigth.BackColor = Color.Transparent;
            tsStraigth.Enabled = true;
            isStraightenMode = false;
            previewPic.Image = (lastselectedNode.ScanObject as ScanPage).PageImage;
        }

        private void tsOK_Click(object sender, EventArgs e)
        {
            Image res = UpdatePageImage((lastselectedNode.ScanObject as ScanPage).ImageId, previewPic.Image);
            previewPic.Image = res;
            tsOK.Visible = false;
            tsCancel.Visible = false;
            tsStraigth.BackColor = Color.Transparent;
            tsStraigth.Enabled = true;
            isStraightenMode = false;
        }
        bool IsRescanMode = false;
        private void DoRescan()
        {
            IsRescanMode = true;
            DoScan();
        }

        private void tsRescan_Click(object sender, EventArgs e)
        {
            DoRescan();
        }

        private void miRescan_Click(object sender, EventArgs e)
        {
            DoRescan();
        }

        private void miInsertScan_Click(object sender, EventArgs e)
        {
            if (_separateObject.SeparateMode == SeparateType.Persheet)
            {
                DoScan();
            }
            else if (_separateObject.SeparateMode == SeparateType.BlankSheet)
            {
                if (lastselectedNode.NodeType == NodeType.File)
                {
                    _currentFile = lastselectedNode.ScanObject as ScanFile;
                    currentFileNode = lastselectedNode;
                    DoScan();
                }

            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (lastselectedNode.NodeType == NodeType.File)
            {
                ScanFile file = lastselectedNode.ScanObject as ScanFile;
                scanPack.ScanFiles.Remove(file);
                treeView1.SelectedNode.Remove();
            }
            else if (lastselectedNode.NodeType == NodeType.Sheet)
            {
                ScanSheet sheet = lastselectedNode.ScanObject as ScanSheet;
                scanPack.ScanFiles.Where(f => f.Sheets.Contains(sheet)).ToList().ForEach(f => f.Sheets.Remove(sheet));
                treeView1.SelectedNode.Remove();
            }
            else if (lastselectedNode.NodeType == NodeType.Page)
            {
                CallDeleteImage();
            }
        }

        private void tsSwapPage_Click(object sender, EventArgs e)
        {
            if (lastselectedNode != null && lastselectedNode.NodeType == NodeType.Sheet)
            {
                lastselectedNode.SwapNode();
                DisplayNodeDetail();
            }
        }
        private void DeleteSelectedImage(string ImageId)
        {
            string fileId = "";
            string sheetId = "";
            bool isFound = false;

            foreach (ScanFile file in scanPack.ScanFiles)
            {
                foreach (ScanSheet sheet in file.Sheets)
                {
                    if (sheet.Top != null && sheet.Top.ImageId == ImageId)
                    {
                        sheet.Top = null;
                        fileId = file.FileId;
                        sheetId = sheet.SheetId;
                        isFound = true;
                        break;
                    }
                    if (sheet.Bottom != null && sheet.Bottom.ImageId == ImageId)
                    {
                        sheet.Bottom = null;
                 
                        fileId = file.FileId;
                        sheetId = sheet.SheetId;
                        isFound = true;
                        break;
                    }
                }
                if (isFound) break;
            }
            if (isFound)
            {
                foreach (MyTreeNode file in treeView1.Nodes[0].Nodes)
                {
                    if ((file.ScanObject as ScanFile).FileId == fileId)
                    {
                        foreach (MyTreeNode sheet in file.Nodes)
                        {
                            if ((sheet.ScanObject as ScanSheet).SheetId == sheetId)
                            {
                                for (int i = 0; i < sheet.Nodes.Count; i++)
                                {
                                    MyTreeNode page = (MyTreeNode)sheet.Nodes[i];
                                    ScanPage scanPage = page.ScanObject as ScanPage;
                                    if (scanPage != null && scanPage.ImageId == ImageId)
                                    {
                                        sheet.Nodes.RemoveAt(i);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CallDeleteImage()
        {
            if (MessageBox.Show("Do you want to delete the selected images?", "Confirm delete images", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (previewPic != null && previewPic.Image != null)
                {

                    DeleteSelectedImage((lastselectedNode.ScanObject as ScanPage).ImageId);
                }
                else
                {
                    for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                    {
                        object child = flowLayoutPanel1.Controls[i];
                        ucThumbnail tmp = (ucThumbnail)child;
                        if (tmp.IsSelected)
                        {

                            ScanPage selectedPage = tmp.ScanPage;
                            DeleteSelectedImage(selectedPage.ImageId);
                            flowLayoutPanel1.Controls.RemoveAt(i);
                            i--;
                        }
                    }

                }
            }
        }
        private void tsDeleteImage_Click(object sender, EventArgs e)
        {
            CallDeleteImage();
        }
    }
}
