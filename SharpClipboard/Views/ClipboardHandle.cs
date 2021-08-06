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
using WK.Libraries.SharpClipboardNS.Interface;
using WK.Libraries.SharpClipboardNS.Controller;

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

            // Vista Version Check
            if (Environment.OSVersion.Version.Major >= 6)
                clipManager = new ClipboardManager_Vista(this);
            else
                clipManager = new ClipboardManager(this);

            clipManager.ChangedClipboard += ClipManager_ChangedClipboard;

            Ready = true;
        }

        #endregion

        #region Fields

        IClipboardManager clipManager;

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
            get {

                if (SharpClipboardInstance.ObserveLastEntry)
                    _ready = true;

                return _ready;

            }
            set { _ready = value; }
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

        #region Clipboard Monitor

        /// <summary>
        /// Modifications in this overriden method have
        /// been added to disable viewing of the handle-
        /// window in the Task Manager.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get {


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
        /// 
        private void ClipManager_ChangedClipboard(object sender, EventArgs e)
        {
            // If clipboard-monitoring is enabled, proceed to listening.
            if (Ready && SharpClipboardInstance.MonitorClipboard)
            {
                IDataObject dataObj;
                int retryCount = 0;

                while (true)
                {
                    try
                    {
                        dataObj = Clipboard.GetDataObject();
                        break;
                    }
                    catch (ExternalException)
                    {
                        // Crashes when large data is copied from clipboard
                        // without retries. We'll therefore need to do a 5-step
                        // retry count to cut some slack for the operation to
                        // fully complete and ensure that the data is captured;
                        // if all else fails, then throw an exception.
                        // You may extend the retries if need be.
                        if (++retryCount > 5)
                            throw;

                        System.Threading.Thread.Sleep(100);
                    }
                }

                try
                {
                    // Determines whether a file/files have been cut/copied.
                    if ((SharpClipboardInstance.ObservableFormats.Files == true) &&
                        (dataObj.GetDataPresent(DataFormats.FileDrop)))
                    {
                        string[] capturedFiles = (string[])dataObj.GetData(DataFormats.FileDrop);

                        // If the 'capturedFiles' string array persists as null, then this means
                        // that the copied content is of a complex object type since the file-drop
                        // format is able to capture more-than-just-file content in the clipboard.
                        // Therefore assign the content its rightful type.
                        if (capturedFiles == null)
                        {
                            SharpClipboardInstance.ClipboardObject = dataObj;
                            SharpClipboardInstance.ClipboardText = dataObj.GetData(DataFormats.UnicodeText).ToString();

                            SharpClipboardInstance.Invoke(dataObj, SharpClipboard.ContentTypes.Other,
                                new SourceApplication(GetForegroundWindow(), SharpClipboardInstance.ForegroundWindowHandle(),
                                GetApplicationName(), GetActiveWindowTitle(), GetApplicationPath()));
                        }
                        else
                        {
                            // Clear all existing files before update.
                            SharpClipboardInstance.ClipboardFiles.Clear();

                            SharpClipboardInstance.ClipboardFiles.AddRange(capturedFiles);
                            SharpClipboardInstance.ClipboardFile = capturedFiles[0];

                            SharpClipboardInstance.Invoke(capturedFiles, SharpClipboard.ContentTypes.Files,
                                new SourceApplication(GetForegroundWindow(), SharpClipboardInstance.ForegroundWindowHandle(),
                                GetApplicationName(), GetActiveWindowTitle(), GetApplicationPath()));
                        }
                    }

                    // Determines whether text has been cut/copied.
                    else if ((SharpClipboardInstance.ObservableFormats.Texts == true) &&
                             (dataObj.GetDataPresent(DataFormats.Text) || dataObj.GetDataPresent(DataFormats.UnicodeText)))
                    {
                        string capturedText = dataObj.GetData(DataFormats.UnicodeText).ToString();
                        SharpClipboardInstance.ClipboardText = capturedText;

                        SharpClipboardInstance.Invoke(capturedText, SharpClipboard.ContentTypes.Text,
                            new SourceApplication(GetForegroundWindow(), SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(), GetActiveWindowTitle(), GetApplicationPath()));
                    }

                    // Determines whether an image has been cut/copied.
                    else if ((SharpClipboardInstance.ObservableFormats.Images == true) &&
                             (dataObj.GetDataPresent(DataFormats.Bitmap)))
                    {
                        Image capturedImage = dataObj.GetData(DataFormats.Bitmap) as Image;
                        SharpClipboardInstance.ClipboardImage = capturedImage;

                        SharpClipboardInstance.Invoke(capturedImage, SharpClipboard.ContentTypes.Image,
                            new SourceApplication(GetForegroundWindow(), SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(), GetActiveWindowTitle(), GetApplicationPath()));
                    }

                    // Determines whether a complex object has been cut/copied.
                    else if ((SharpClipboardInstance.ObservableFormats.Others == true) &&
                            !(dataObj.GetDataPresent(DataFormats.FileDrop)))
                    {
                        SharpClipboardInstance.Invoke(dataObj, SharpClipboard.ContentTypes.Other,
                            new SourceApplication(GetForegroundWindow(), SharpClipboardInstance.ForegroundWindowHandle(),
                            GetApplicationName(), GetActiveWindowTitle(), GetApplicationPath()));
                    }
                }
                catch (AccessViolationException)
                {
                    // Use-cases such as Remote Desktop usage might throw this exception. 
                    // Applications with Administrative privileges can however override  
                    // this exception when run in a production environment.
                }
                catch (NullReferenceException) { }
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

        private Int32 GetProcessID(Int32 hwnd)
        {
            Int32 processID = 1;
            GetWindowThreadProcessId(hwnd, out processID);

            return processID;
        }

        private string GetApplicationName()
        {
            try
            {
                Int32 hwnd = 0;
                hwnd = GetForegroundWindow();

                _processName = Process.GetProcessById(GetProcessID(hwnd)).ProcessName;
                _executablePath = Process.GetProcessById(GetProcessID(hwnd)).MainModule.FileName;
                _executableName = _executablePath.Substring(_executablePath.LastIndexOf(@"\") + 1);
            }
            catch (Exception) { }

            return _executableName;
        }

        private string GetApplicationPath()
        {
            return _executablePath;
        }

        private string GetActiveWindowTitle()
        {
            const int capacity = 256;
            StringBuilder content = null;
            IntPtr handle = IntPtr.Zero;

            try
            {
                content = new StringBuilder(capacity);
                handle = SharpClipboardInstance.ForegroundWindowHandle();
            }
            catch (Exception) { }

            if (GetWindowText(handle, content, capacity) > 0)
                return content.ToString();

            return null;
        }

        #endregion

        #endregion

        #endregion
        
    }
}