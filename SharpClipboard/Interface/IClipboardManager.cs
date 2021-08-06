using System;
using System.Collections.Generic;
using System.Text;

namespace WK.Libraries.SharpClipboardNS.Interface
{
    interface IClipboardManager : IDisposable
    {
        event EventHandler ChangedClipboard;
    }
}
