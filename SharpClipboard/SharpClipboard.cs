#region Copyright

/*
 * Developer    : Willy Kimura (WK).
 * Library      : SharpCliboard.
 * License      : MIT.
 * 
 * This handy library was designed to assist .NET developers
 * monitor the system cliboard in an easier and pluggable
 * fashion that before. It provides support for detecting
 * data formats including texts, images & files. To use it
 * at design-time, simply add the component in the Toolbox
 * then drag-n-drop it inside your Form to customize its
 * options and features. Improvements are always welcome.
 * 
 */

#endregion


using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;

using WK.Libraries.SharpClipboardNS.Views;

namespace WK.Libraries.SharpClipboardNS
{
    /// <summary>
    /// Assists in anonymously monitoring the system clipboard by
    /// detecting any copied/cut data and the type of data it is.
    /// </summary>
    [Designer(typeof(WKDesigner))]
    [DefaultEvent("ClipboardChanged")]
    [DefaultProperty("MonitorClipboard")]
    [Description("Assists in anonymously monitoring the system clipboard by " +
                 "detecting any copied/cut data and the type of data it is.")]
    public partial class SharpClipboard : Component
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="SharpClipboard"/>.
        /// </summary>
        public SharpClipboard()
        {
            InitializeComponent();

            SetDefaults();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SharpClipboard"/>.
        /// </summary>
        /// <param name="container">
        /// The container hosting the component.
        /// </param>
        public SharpClipboard(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            SetDefaults();
        }

        #endregion

        #region Fields

        private bool _monitorClipboard;
        private bool _observeLastEntry;

        private Timer _timer = new Timer();
        private ClipboardHandle _handle = new ClipboardHandle();
        private ObservableDataFormats _observableFormats = new ObservableDataFormats();

        #endregion

        #region Enumerations

        /// <summary>
        /// Provides a list of the supported clipboard content types.
        /// </summary>
        public enum ContentTypes
        {
            /// <summary>
            /// Represents <see cref="String"/> content.
            /// </summary>
            Text = 0,

            /// <summary>
            /// Represents <see cref="Image"/> content.
            /// </summary>
            Image = 1,

            /// <summary>
            /// Represents content as a <see cref="List{string}"/> of files.
            /// </summary>
            Files = 2,

            /// <summary>
            /// Represents any complex objects.
            /// </summary>
            Other = 3
        }

        #endregion

        #region Properties
        
        #region Browsable

        /// <summary>
        /// Gets or sets a value indicating whether the clipboard
        /// will be monitored once the application launches.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether the clipboard " +
                     "will be monitored once the application launches.")]
        public bool MonitorClipboard
        {
            get { return _monitorClipboard; }
            set {

                _monitorClipboard = value;
                MonitorClipboardChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        /// <summary>
        /// When set to true, the last cut/copied clipboard item will
        /// not be auto-picked once monitoring is enabled. However when 
        /// set to false, the last cut/copied clipboard item will be 
        /// auto-picked once monitoring is enabled.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("When set to true, the last cut/copied clipboard item will " +
                     "be auto-picked once monitoring is enabled. However when " +
                     "set to false, the last cut/copied clipboard item will not " +
                     "be auto-picked once monitoring is enabled.")]
        public bool ObserveLastEntry
        {
            get { return _observeLastEntry; }
            set {

                _observeLastEntry = value;
                ObserveLastEntryChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        /// <summary>
        /// Gets or sets the data formats that will be observed
        /// or monitored when cut/copy actions are triggered.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets the data formats that will be observed " +
                     "or monitored when cut/copy actions are triggered.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableDataFormats ObservableFormats
        {
            get { return _observableFormats; }
            set {

                _observableFormats = value;
                ObservableFormatsChanged?.Invoke(this, EventArgs.Empty);

            }
        }

        /// <summary>
        /// Gets or sets the object that contains programmer-
        /// supplied data associated with the component.
        /// </summary>
        [Bindable(true)]
        [Category("#Clipboard: Miscellaneous")]
        [TypeConverter(typeof(StringConverter))]
        [Description("Sets the object that contains programmer-" +
                     "supplied data associated with the component.")]
        public virtual object Tag { get; set; }

        #endregion

        #region Non-browsable

        /// <summary>
        /// Gets the currently cut/copied clipboard text.
        /// </summary>
        [Browsable(false)]
        public string ClipboardText { get; internal set; }

        /// <summary>
        /// Gets the currently cut/copied clipboard <see cref="object"/>.
        /// This is necessary when handling complex content copied to the clipboard.
        /// </summary>
        [Browsable(false)]
        public object ClipboardObject { get; internal set; }

        /// <summary>
        /// Gets the currently cut/copied clipboard file-path.
        /// </summary>
        [Browsable(false)]
        public string ClipboardFile { get; internal set; }

        /// <summary>
        /// Gets the currently cut/copied clipboard file-paths.
        /// </summary>
        [Browsable(false)]
        public List<string> ClipboardFiles { get; internal set; } = new List<string>();

        /// <summary>
        /// Gets the currently cut/copied clipboard image.
        /// </summary>
        [Browsable(false)]
        public Image ClipboardImage { get; internal set; }

        /// <summary>
        /// Lets you change the invisible clipboard-window-handle's title 
        /// that is designed to receive broadcasted clipboard messages. This is 
        /// however unnecessary for normal users but may be essential if you're 
        /// working under special circumstances that require supervision.
        /// The window will however remain hidden from all users.
        /// </summary>
        [Browsable(false)]
        public static string HandleCaption { get; set; } = string.Empty;

        #endregion

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Gets the current foreground window's handle.
        /// </summary>
        /// <returns></returns>
        public IntPtr ForegroundWindowHandle()
        {
            return GetForegroundWindow();
        }

        /// <summary>
        /// Starts the clipboard-monitoring process and
        /// initializes the system clipboard-access handle.
        /// </summary>
        public void StartMonitoring()
        {
            _handle.Show();
        }

        /// <summary>
        /// Ends the clipboard-monitoring process and
        /// shuts the system clipboard-access handle.
        /// </summary>
        public void StopMonitoring()
        {
            _handle.Close();
        }

        #endregion

        #region Private

        /// <summary>
        /// Apply library-default settings and launch code.
        /// </summary>
        private void SetDefaults()
        {
            _handle.SharpClipboardInstance = this;

            _timer.Enabled = true;
            _timer.Interval = 1000;
            _timer.Tick += OnLoad;
        
            MonitorClipboard = true;
            ObserveLastEntry = true;
        }

        /// <summary>
        /// Invokes the <see cref="ClipboardChanged"/> event with formal parameters.
        /// </summary>
        /// <param name="content">The current clipboard content.</param>
        /// <param name="type">The current clipboard content-type.</param>
        internal void Invoke(object content, ContentTypes type, SourceApplication source)
        {
            ClipboardChanged?.Invoke(this, new ClipboardChangedEventArgs(content, type, source));
        }

        /// <summary>
        /// Gets the foreground or currently active window handle.
        /// </summary>
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        #endregion

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// This event is triggered whenever the
        /// system clipboard has been modified.
        /// </summary>
        [Category("#Clipboard: Events")]
        [Description("This event is triggered whenever the " +
                     "system clipboard has been modified.")]
        public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged = null;

        /// <summary>
        /// Occurs whenever the clipboard-monitoring status has been changed.
        /// </summary>
        [Category("#Clipboard: Events")]
        [Description("Occurs whenever the clipboard-monitoring status has been changed.")]
        public event EventHandler<EventArgs> MonitorClipboardChanged = null;

        /// <summary>
        /// Occurs whenever the allowed observable formats have been changed.
        /// </summary>
        [Category("#Clipboard: Events")]
        [Description("Occurs whenever the allowed observable formats have been changed.")]
        public event EventHandler<EventArgs> ObservableFormatsChanged = null;

        /// <summary>
        /// Occurs whenever the 'ObserveLastEntry' property has been changed.
        /// </summary>
        [Category("#Clipboard: Events")]
        [Description("Occurs whenever the allowed observable formats have been changed.")]
        public event EventHandler<EventArgs> ObserveLastEntryChanged = null;

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="ClipboardChanged"/> event.
        /// </summary>
        public class ClipboardChangedEventArgs : EventArgs
        {
            #region Constructor

            /// <summary>
            /// Provides data for the <see cref="ClipboardChanged"/> event.
            /// </summary>
            /// <param name="content">The current clipboard content.</param>
            /// <param name="contentType">The current clipboard-content-type.</param>
            public ClipboardChangedEventArgs(object content, ContentTypes contentType, SourceApplication source)
            {
                Content = content;
                ContentType = contentType;

                _source = new SourceApplication(source.ID, source.Handle, source.Name,
                                                source.Title, source.Path);
            }

            #endregion

            #region Fields

            private SourceApplication _source;

            #endregion

            #region Properties

            /// <summary>
            /// Gets the currently copied clipboard content.
            /// </summary>
            public object Content { get; }

            /// <summary>
            /// Gets the currently copied clipboard content-type.
            /// </summary>
            public ContentTypes ContentType { get; }

            /// <summary>
            /// Gets the application from where the
            /// clipboard's content were copied.
            /// </summary>
            public SourceApplication SourceApplication
            {
                get { return _source; }

            }

            #endregion
        }

        #endregion

        #endregion

        #region Private

        /// <summary>
        /// This initiates a Timer that then begins the 
        /// clipboard-monitoring service. The Timer will 
        /// auto-shutdown once the service has started.
        /// </summary>
        private void OnLoad(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                _timer.Stop();
                _timer.Enabled = false;

                StartMonitoring();
            }
        }

        #endregion

        #endregion

        #region Smart Tags

        #region Standard: Designer

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public class WKDesigner : ComponentDesigner
        {
            private DesignerActionListCollection actionLists;

            // Use pull model to populate smart tag menu.
            public override DesignerActionListCollection ActionLists
            {
                get {
                    if (null == actionLists)
                    {
                        actionLists = new DesignerActionListCollection
                        {
                            new WKComponentActionList(this.Component)
                        };
                    }

                    return actionLists;
                }
            }
        }

        #endregion

        #region Modifiers: Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="WKComponentActionList"/> class.
        /// </summary>
        public class WKComponentActionList : DesignerActionList
        {
            private SharpClipboard WKComponent;
            private DesignerActionUIService designerActionUISvc = null;

            public WKComponentActionList(IComponent component) : base(component)
            {
                this.WKComponent = component as SharpClipboard;

                // Cache a reference to DesignerActionUIService so 
                // that the DesignerActionList can be refreshed.
                this.designerActionUISvc = GetService(typeof(DesignerActionUIService))
                                           as DesignerActionUIService;

                // Automatically display Smart Tags for quick access
                // to the most common properties needed by users.
                this.AutoShow = true;
            }

            #region Properties Manager

            internal static PropertyDescriptor GetPropertyDescriptor(IComponent component, string propertyName)
            {
                return TypeDescriptor.GetProperties(component)[propertyName];
            }

            internal static IDesignerHost GetDesignerHost(IComponent component)
            {
                return (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
            }

            internal static IComponentChangeService GetChangeService(IComponent component)
            {
                return (IComponentChangeService)component.Site.GetService(typeof(IComponentChangeService));
            }

            internal static void SetValue(IComponent component, string propertyName, object value)
            {
                PropertyDescriptor propertyDescriptor = GetPropertyDescriptor(component, propertyName);
                IComponentChangeService svc = GetChangeService(component);
                IDesignerHost host = GetDesignerHost(component);
                DesignerTransaction txn = host.CreateTransaction();

                try
                {
                    svc.OnComponentChanging(component, propertyDescriptor);
                    propertyDescriptor.SetValue(component, value);
                    svc.OnComponentChanged(component, propertyDescriptor, null, null);
                    txn.Commit();
                    txn = null;
                }

                finally
                {
                    if (txn != null)
                        txn.Cancel();
                }
            }

            #endregion

            #region Items Manager

            /// <summary>
            /// Implementation of this abstract method creates Smart Tag items,
            /// associates their targets, and collects them into a list.
            /// </summary>
            public override DesignerActionItemCollection GetSortedActionItems()
            {
                DesignerActionItemCollection items = new DesignerActionItemCollection
                {
                    // Define static section header entries.
                    new DesignerActionHeaderItem("Behaviour"),

                    // Add Designer items.
                    new DesignerActionPropertyItem("MonitorClipboard",
                                     "Monitor Clipboard", "Behaviour",
                                     GetPropertyDescriptor(this.Component, "MonitorClipboard").Description)

                };

                return items;
            }

            #region Properties

            public bool MonitorClipboard
            {
                get { return WKComponent.MonitorClipboard; }
                set { SetValue(WKComponent, "MonitorClipboard", value); }
            }

            #endregion

            #endregion
        }

        #endregion

        #endregion
    }

    #region Property Classes

    /// <summary>
    /// Provides a list of supported observable data formats
    /// that can be monitored from the system clipboard.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("Provides a list of supported observable data formats " +
                 "that can be monitored from the system clipboard.")]
    public class ObservableDataFormats
    {
        /// <summary>
        /// Creates a new <see cref="ObservableDataFormats"/> options class-instance.
        /// </summary>
        public ObservableDataFormats()
        {
            _all = true;
        }

        #region Fields

        private bool _all;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether all the
        /// supported observable formats will be monitored.
        /// </summary>
        [ParenthesizePropertyName(true)]
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether all the supported " +
                     "observable formats will be monitored.")]
        public bool All
        {
            get { return _all; }
            set {

                _all = value;

                Texts = value;
                Files = value;
                Images = value;
                Others = value;

            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether texts will be monitored.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether texts will be monitored.")]
        public bool Texts { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether files will be monitored.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether files will be monitored.")]
        public bool Files { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether images will be monitored.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether images will be monitored.")]
        public bool Images { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether other 
        /// complex object-types will be monitored.
        /// </summary>
        [Category("#Clipboard: Behaviour")]
        [Description("Sets a value indicating whether other " +
                     "complex object-types will be monitored.")]
        public bool Others { get; set; } = true;

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="string"/> containing the list of observable data
        /// formats provided and their applied statuses separated by semi-colons.
        /// </summary>
        public override string ToString()
        {
            return $"Texts: {Texts}; Images: {Images}; Files: {Files}; Others: {Others}";
        }

        #endregion
    }
    
    /// <summary>
    /// Stores details of the application from
    /// where the clipboard's content were copied.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class SourceApplication
    {
        /// <summary>
        /// Creates a new <see cref="SourceApplication"/> class-instance.
        /// </summary>
        /// <param name="id">The application's ID.</param>
        /// <param name="handle">The application's handle.</param>
        /// <param name="name">The application's name.</param>
        /// <param name="title">The application's title.</param>
        /// <param name="path">The application's path.</param>
        internal SourceApplication(int id, IntPtr handle, string name,
                                   string title, string path)
        {
            ID = id;
            Name = name;
            Path = path;
            Title = title;
            Handle = handle;
        }

        #region Properties

        /// <summary>
        /// Gets the application's process-ID.
        /// </summary>
        public int ID { get; }
        
        /// <summary>
        /// Gets the appliation's window-handle.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// Gets the application's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the application's title-text.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the application's absolute path.
        /// </summary>
        public string Path { get; }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="string"/> containing the list 
        /// of application details provided.
        /// </summary>
        public override string ToString()
        {
            return $"ID: {ID}; Handle: {Handle}, Name: {Name}; " +
                   $"Title: {Title}; Path: {Path}";
        }

        #endregion
    }

    #endregion
}