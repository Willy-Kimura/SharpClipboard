using System;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;

namespace WK.Libraries.SharpClipboard
{
    public partial class SharpClipboard : Component
    {
        #region Constructors

        public SharpClipboard()
        {
            InitializeComponent();
        }

        public SharpClipboard(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion
    }
}
