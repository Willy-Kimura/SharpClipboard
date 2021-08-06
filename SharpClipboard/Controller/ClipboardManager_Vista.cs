using System;
using System.Windows.Forms;
using WK.Libraries.SharpClipboardNS.Interface;

namespace WK.Libraries.SharpClipboardNS.Controller
{
    class ClipboardManager_Vista : NativeWindow, IClipboardManager
    {

        public event EventHandler ChangedClipboard;

        Control TargetControl;

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Native.WM_DESTROY:
                    Dispose();
                    break;

                case Native.WM_CLIPBOARDUPDATE:
                    if (ChangedClipboard != null)
                    {
                        ChangedClipboard(this, null);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        public ClipboardManager_Vista(Control _targetControl)
        {
            TargetControl = _targetControl;

            Native.AddClipboardFormatListener(TargetControl.Handle);
            AssignHandle(TargetControl.Handle);
        }

        public void UnregisterClipboardViewer()
        {
            try
            {
                Native.RemoveClipboardFormatListener(TargetControl.Handle);
            }
            catch (ObjectDisposedException ex) { }

            ReleaseHandle();
        }

        #region DIspose

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
            // 이 코드를 변경하지 마세요. 'Dispose(bool disposing)' 메서드에 정리 코드를 입력합니다.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
