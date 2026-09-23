using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HyperBPOScanTool
{
    public static class UserControlEventBus
    {
        public static event Action<object> OnUserControlClicked;

        public static void PublishClick(object sender)
        {
            OnUserControlClicked?.Invoke(sender);
        }
    }
}
