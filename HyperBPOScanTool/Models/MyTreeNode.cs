using System;
using System.Collections.Generic;
using System.Text;

namespace HyperBPOScanTool.Models
{
    public enum NodeType
    {
        Root = 0,
        File = 1,
        Sheet = 2,
        Page = 3
    }
    public class MyTreeNode:TreeNode
    {
        public MyTreeNode()
        {
            
        }
        public MyTreeNode(string text, string id, string parentId)
        {
            this.Text = text;
            this.Id = id;
            this.ParentId = parentId;
        }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public object ScanObject { get; set; }
        public NodeType NodeType { get; set; }
        public void SwapNode()
        {
            if(NodeType == NodeType.Sheet) {
                ScanSheet sheet = ScanObject as ScanSheet;
                if(sheet != null && sheet.Top != null && sheet.Bottom != null && Nodes.Count==2) {
                    ScanPage top = sheet.Top;
                    sheet.Top = sheet.Bottom;
                    sheet.Bottom = top;
                    (Nodes[0] as MyTreeNode).ScanObject = sheet.Top;
                    (Nodes[1] as MyTreeNode).ScanObject = sheet.Bottom;
                    ScanObject = sheet;
                }

            }
        }
    }
}
