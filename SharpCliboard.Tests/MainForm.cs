using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

using WK.Libraries.SharpClipboardNS;

namespace SharpClipboardPreview.Tests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            chkMonitorClipboard.Checked = sharpClipboard1.MonitorClipboard;
            chkObserveTexts.Checked = sharpClipboard1.ObservableFormats.Texts;
            chkObserveFiles.Checked = sharpClipboard1.ObservableFormats.Files;
            chkObserveImages.Checked = sharpClipboard1.ObservableFormats.Images;
        }

        private void sharpClipboard1_MonitorClipboardChanged(object sender, EventArgs e)
        {
            chkMonitorClipboard.Checked = sharpClipboard1.MonitorClipboard;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            sharpClipboard1.MonitorClipboard = chkMonitorClipboard.Checked;
        }

        private void chkObserveTexts_CheckedChanged(object sender, EventArgs e)
        {
            sharpClipboard1.ObservableFormats.Texts = chkObserveTexts.Checked;
        }

        private void chkObserveImages_CheckedChanged(object sender, EventArgs e)
        {
            sharpClipboard1.ObservableFormats.Images = chkObserveImages.Checked;
        }

        private void chkObserveFiles_CheckedChanged(object sender, EventArgs e)
        {
            sharpClipboard1.ObservableFormats.Files = chkObserveFiles.Checked;
        }

        private void sharpClipboard1_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                txtCopiedTexts.Text = sharpClipboard1.ClipboardText;

                // Alternatively, you can use:
                // ---------------------------
                // txtCopiedTexts.Text = (string)e.Content;
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Image)
            {
                pbCopiedImage.Image = sharpClipboard1.ClipboardImage;

                // Alternatively, you can use:
                // ---------------------------
                // pbCopiedImage.Image = (Image)e.Content;
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Files)
            {
                List<string> files = new List<string>();

                foreach (string file in sharpClipboard1.ClipboardFiles)
                {
                    files.Add(Path.GetFileName(file));
                }

                Debug.WriteLine(sharpClipboard1.ClipboardFiles.ToArray());

                lstCopiedFiles.Items.Clear();
                lstCopiedFiles.Items.AddRange(files.ToArray());
                
                // Alternatively, you can use:
                // ---------------------------
                // lstCopiedFiles.Items.AddRange(((List<string>)e.Content).ToArray()));
            }
            else if (e.ContentType == SharpClipboard.ContentTypes.Other)
            {
                // Do something with 'e.Content' or alternatively
                // 'sharpClipboard1.ClipboardObject' property here...

                // A great example here is when a user has copied an Outlook Mail item.
                // Such an item will be of a complex object-type format which can be parsed and
                // examined by using the 'Microsoft.Office.Interop.Outlook' namespace features.
                // See here: https://stackoverflow.com/questions/25375367/how-to-copy-mailitem-in-outlook-c-sharp

                // You can however still use the 'ClipboardText' property if you
                // prefer simply displaying the copied object in text format.
                txtCopiedTexts.Text = sharpClipboard1.ClipboardText.ToString();
            }

            // Add a TextBox, uncomment the lines below and run.
            // -------------------------------------------------
            // textBox1.Text = $"Name: {e.SourceApplication.Name}, Title: {e.SourceApplication.Title}, " +
            //                 $"ID: {e.SourceApplication.ID}, Path: {e.SourceApplication.Path}";
        }
    }
}
