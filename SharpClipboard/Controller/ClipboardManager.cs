using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS.Interface;

namespace WK.Libraries.SharpClipboardNS.Controller
{
    class ClipboardManager : NativeWindow, IClipboardManager
    {

        public event EventHandler ChangedClipboard;

        IntPtr _clipboardViewerNext = IntPtr.Zero;

        Control TargetControl;
        
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case Native.WM_DESTROY:
                    Dispose();
                    break;

                case Native.WM_DRAWCLIPBOARD:
                    if (ChangedClipboard != null)
                    {
                        ChangedClipboard(this, null);
                    }
                    Native.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;

                case Native.WM_CHANGECBCHAIN:

                    if (m.WParam == _clipboardViewerNext)
                        _clipboardViewerNext = m.LParam;
                    else
                        Native.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;
            }
        }

        public ClipboardManager(Control _targetControl)
        {
            this.TargetControl = _targetControl;

            _clipboardViewerNext = Native.SetClipboardViewer(TargetControl.Handle);
            AssignHandle(TargetControl.Handle);
        }

        void UnregisterClipboardViewer()
        {
            try
            {
                if (TargetControl != null) Native.ChangeClipboardChain(TargetControl.Handle, _clipboardViewerNext);
            }
            catch (ObjectDisposedException) { }

            _clipboardViewerNext = IntPtr.Zero;
            ReleaseHandle();
        }

        #region Dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ChangedClipboard = null;
                    UnregisterClipboardViewer();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
