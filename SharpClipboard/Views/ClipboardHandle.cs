#region Copyright

/*
 * The SharpClipboard Handle.
 * ---------------------------------------------+
 * Please preserve this window.
 * It acts as the core message-processing handle
 * for receiving broadcasted clipboard messages.
 *
 * The window however will not be visible to
 * end users both via the Taskbar and the Task-
 * Manager so no need to panic. At the very least
 * you may change the window's title using the
 * static property 'SharpClipboard.HandleCaption'.
 * ---------------------------------------------+
 *
 */

#endregion


using System;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WK.Libraries.SharpClipboardNS.Views
{
    /// <summary>
    /// This window acts as a handle to the clipboard-monitoring process and
    /// thus will be launched in the background once the component has started
    /// the monitoring service. However, it won't be visible to anyone even via
    /// the Task Manager.
    /// </summary>
    public partial class ClipboardHandle : Form
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of <see cref="ClipboardHandle"/>.
        /// </summary>
        public ClipboardHandle()
        {
            InitializeComponent();

            // [optional] Applies the default window title.
            // This may only be necessary for forensic purposes.
            Text = SharpClipboard.HandleCaption;
        }

        #endregion

        #region Fields

        const int WM_CLIPBOARDUPDATE = 0x031D;

        private bool _ready;

        private string _processName = string.Empty;
        private string _executableName = string.Empty;
        private string _executablePath = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Checks if the handle is ready to monitor the system clipboard.
        /// It is used to provide a final value for use whenever the property
        /// 'ObserveLastEntry' is enabled.
        /// </summary>
        [Browsable(false)]
        internal bool Ready
        {
            get
            {
                if (SharpClipboardInstance.ObserveLastEntry)
                    _ready = true;
                return _ready;
            }
            set => _ready = value;
        }

        /// <summary>
        /// Gets or sets an active <see cref="SharpClipboard"/> instance
        /// for use when managing the current clipboard handle.
        /// </summary>
        [Browsable(false)]
        internal SharpClipboard SharpClipboardInstance { get; set; }

        #endregion

        #region Methods

        #region Clipboard Management

        #region Win32 Integration

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion

        #region Clipboard Monitor

        /// <summary>
        /// Modifications in this overriden method have
        /// been added to disable viewing of the handle-
        /// window in the Task Manager.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                // Turn on WS_EX_TOOLWINDOW.
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        /// <summary>
        /// This is the main clipboard detection method.
        /// Algorithmic customizations are most welcome.
        /// </summary>
        /// <param name="m">The processed window-reference message.</param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_CLIPBOARDUPDATE:
                    OnDrawClipboardChanged();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnDrawClipboardChanged()
        {
            // If clipboard-monitoring is enabled, proceed to listening.
            if (!Ready || !SharpClipboardInstance.MonitorClipboard) return;
            var dataObj = Retry.Do(() => { return Clipboard.GetDataObject(); }, 100, 5);
            if (dataObj is null)
                return;
            try
            {
                // Determines whether a file/files have been cut/copied.
                if (
                    SharpClipboardInstance.ObservableFormats.Files
                    && dataObj.GetDataPresent(DataFormats.FileDrop)
                )
                {
                    // If the 'capturedFiles' string array persists as null, then this means
                    // that the copied content is of a complex object type since the file-drop
                    // format is able to capture more-than-just-file content in the clipboard.
                    // Therefore assign the content its rightful type.
                    if (!(dataObj.GetData(DataFormats.FileDrop) is string[] capturedFiles))
                    {
                        SharpClipboardInstance.ClipboardObject = dataObj;
                        SharpClipboardInstance.ClipboardText = dataObj
                            .GetData(DataFormats.UnicodeText)
                            .ToString();

                        SharpClipboardInstance.Invoke(
                            dataObj,
                            SharpClipboard.ContentTypes.Other,
                            new SourceApplication(
                                GetForegroundWindow(),
                                SharpClipboardInstance.ForegroundWindowHandle(),
                                GetApplicationName(),
                                GetActiveWindowTitle(),
                                GetApplicationPath()
                            )
                        );
                    }
                    else
                    {
                        // Clear all existing files before update.
                        SharpClipboardInstance.ClipboardFiles.Clear();
                        SharpClipboardInstance.ClipboardFiles.AddRange(capturedFiles);
                        SharpClipboardInstance.ClipboardFile = capturedFiles[0];

                        SharpClipboardInstance.Invoke(
                            capturedFiles,
                            SharpClipboard.ContentTypes.Files,
                            new SourceApplication(
                                GetForegroundWindow(),
                                SharpClipboardInstance.ForegroundWindowHandle(),
                                GetApplicationName(),
                                GetActiveWindowTitle(),
                                GetApplicationPath()
                            )
                        );
                    }
                }
                // Determines whether text has been cut/copied.
                else if (
                    SharpClipboardInstance.ObservableFormats.Texts
                    && (
                        dataObj.GetDataPresent(DataFormats.Text)
                        || dataObj.GetDataPresent(DataFormats.UnicodeText)
                    )
                )
                {
                    string capturedText = dataObj
                        .GetData(DataFormats.UnicodeText)
                        .ToString();
                    SharpClipboardInstance.ClipboardText = capturedText;

                    SharpClipboardInstance.Invoke(
                        capturedText,
                        SharpClipboard.ContentTypes.Text,
                        new SourceApplication(
                            GetForegroundWindow(),
                            SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(),
                            GetActiveWindowTitle(),
                            GetApplicationPath()
                        )
                    );
                }
                // Determines whether an image has been cut/copied.
                else if (
                    SharpClipboardInstance.ObservableFormats.Images
                    && dataObj.GetDataPresent(DataFormats.Bitmap)
                )
                {
                    Image capturedImage = dataObj.GetData(DataFormats.Bitmap) as Image;
                    SharpClipboardInstance.ClipboardImage = capturedImage;

                    SharpClipboardInstance.Invoke(
                        capturedImage,
                        SharpClipboard.ContentTypes.Image,
                        new SourceApplication(
                            GetForegroundWindow(),
                            SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(),
                            GetActiveWindowTitle(),
                            GetApplicationPath()
                        )
                    );
                }
                // Determines whether a complex object has been cut/copied.
                else if (
                    SharpClipboardInstance.ObservableFormats.Others
                    && !dataObj.GetDataPresent(DataFormats.FileDrop)
                )
                {
                    SharpClipboardInstance.Invoke(
                        dataObj,
                        SharpClipboard.ContentTypes.Other,
                        new SourceApplication(
                            GetForegroundWindow(),
                            SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(),
                            GetActiveWindowTitle(),
                            GetApplicationPath()
                        )
                    );
                }
            }
            catch (AccessViolationException)
            {
                // Use-cases such as Remote Desktop usage might throw this exception.
                // Applications with Administrative privileges can however override
                // this exception when run in a production environment.
            }
            catch (NullReferenceException)
            {
            }
        }

        #endregion

        #region Helper Methods

        public void StartMonitoring()
        {
            this.Show();
        }

        public void StopMonitoring()
        {
            this.Close();
        }

        #endregion

        #endregion

        #region Source App Management

        #region Win32 Externals

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindowPtr();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        #endregion

        #region Helper Methods

        private Int32 GetProcessId(Int32 hwnd)
        {
            GetWindowThreadProcessId(hwnd, out var processId);
            return processId;
        }

        private string GetApplicationName()
        {
            try
            {
                var hwnd = GetForegroundWindow();
                _processName = Process.GetProcessById(GetProcessId(hwnd)).ProcessName;
                var processModule = Process.GetProcessById(GetProcessId(hwnd)).MainModule;
                if (processModule != null)
                    _executablePath = processModule.FileName;
                _executableName =
                    _executablePath.Substring(_executablePath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
            }
            catch (Exception)
            {
                // ignored
            }

            return _executableName;
        }

        private string GetApplicationPath()
        {
            return _executablePath;
        }

        private string GetActiveWindowTitle()
        {
            const int capacity = 256;
            StringBuilder content = new StringBuilder(capacity);;
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = SharpClipboardInstance.ForegroundWindowHandle();
            }
            catch (Exception)
            {
                // ignored
            }

            return GetWindowText(handle, content, capacity) > 0 ? content.ToString() : null;
        }

        #endregion

        #endregion

        #endregion

        #region Events

        private void OnLoad(object sender, EventArgs e)
        {
            // Start listening for clipboard changes.
            Retry.Do(() => AddClipboardFormatListener(this.Handle), 100, 5);
            Ready = true;
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            // Stop listening to clipboard changes.
            Retry.Do(() => RemoveClipboardFormatListener(this.Handle), 100, 5);
        }

        #endregion
    }
}