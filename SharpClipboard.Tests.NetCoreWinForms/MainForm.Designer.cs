using WK.Libraries.SharpClipboardNS;

namespace SharpClipboard.Tests.NetCoreWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chkMonitorClipboard = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblOptions = new System.Windows.Forms.Label();
            this.grpLibrarySettings = new System.Windows.Forms.GroupBox();
            this.grpObservableFormats = new System.Windows.Forms.GroupBox();
            this.lblObservableFilesDesc = new System.Windows.Forms.Label();
            this.chkObserveFiles = new System.Windows.Forms.CheckBox();
            this.lblObservableImagesDesc = new System.Windows.Forms.Label();
            this.chkObserveImages = new System.Windows.Forms.CheckBox();
            this.lblObservableTextsDesc = new System.Windows.Forms.Label();
            this.chkObserveTexts = new System.Windows.Forms.CheckBox();
            this.txtCopiedTexts = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pbCopiedImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstCopiedFiles = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sharpClipboard1 = new WK.Libraries.SharpClipboardNS.SharpClipboard(this.components);
            this.panel1.SuspendLayout();
            this.grpLibrarySettings.SuspendLayout();
            this.grpObservableFormats.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCopiedImage)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMonitorClipboard
            // 
            this.chkMonitorClipboard.AutoSize = true;
            this.chkMonitorClipboard.Checked = true;
            this.chkMonitorClipboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMonitorClipboard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.chkMonitorClipboard.ForeColor = System.Drawing.Color.Black;
            this.chkMonitorClipboard.Location = new System.Drawing.Point(17, 33);
            this.chkMonitorClipboard.Name = "chkMonitorClipboard";
            this.chkMonitorClipboard.Size = new System.Drawing.Size(138, 21);
            this.chkMonitorClipboard.TabIndex = 1;
            this.chkMonitorClipboard.Text = "Monitor Clipboard";
            this.chkMonitorClipboard.UseVisualStyleBackColor = true;
            this.chkMonitorClipboard.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.lblTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTitle.Location = new System.Drawing.Point(22, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(155, 30);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "SharpClipboard";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblOptions);
            this.panel1.Controls.Add(this.grpLibrarySettings);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 397);
            this.panel1.TabIndex = 4;
            // 
            // lblOptions
            // 
            this.lblOptions.AutoSize = true;
            this.lblOptions.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F);
            this.lblOptions.ForeColor = System.Drawing.Color.Black;
            this.lblOptions.Location = new System.Drawing.Point(171, 14);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(89, 30);
            this.lblOptions.TabIndex = 6;
            this.lblOptions.Text = "Options";
            // 
            // grpLibrarySettings
            // 
            this.grpLibrarySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLibrarySettings.Controls.Add(this.grpObservableFormats);
            this.grpLibrarySettings.Controls.Add(this.chkMonitorClipboard);
            this.grpLibrarySettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLibrarySettings.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpLibrarySettings.Location = new System.Drawing.Point(26, 57);
            this.grpLibrarySettings.Name = "grpLibrarySettings";
            this.grpLibrarySettings.Size = new System.Drawing.Size(261, 327);
            this.grpLibrarySettings.TabIndex = 5;
            this.grpLibrarySettings.TabStop = false;
            this.grpLibrarySettings.Text = "Change library settings";
            // 
            // grpObservableFormats
            // 
            this.grpObservableFormats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObservableFormats.Controls.Add(this.lblObservableFilesDesc);
            this.grpObservableFormats.Controls.Add(this.chkObserveFiles);
            this.grpObservableFormats.Controls.Add(this.lblObservableImagesDesc);
            this.grpObservableFormats.Controls.Add(this.chkObserveImages);
            this.grpObservableFormats.Controls.Add(this.lblObservableTextsDesc);
            this.grpObservableFormats.Controls.Add(this.chkObserveTexts);
            this.grpObservableFormats.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpObservableFormats.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.grpObservableFormats.Location = new System.Drawing.Point(17, 74);
            this.grpObservableFormats.Name = "grpObservableFormats";
            this.grpObservableFormats.Size = new System.Drawing.Size(229, 238);
            this.grpObservableFormats.TabIndex = 6;
            this.grpObservableFormats.TabStop = false;
            this.grpObservableFormats.Text = "Observable Formats";
            // 
            // lblObservableFilesDesc
            // 
            this.lblObservableFilesDesc.AutoSize = true;
            this.lblObservableFilesDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservableFilesDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblObservableFilesDesc.Location = new System.Drawing.Point(33, 184);
            this.lblObservableFilesDesc.Name = "lblObservableFilesDesc";
            this.lblObservableFilesDesc.Size = new System.Drawing.Size(176, 30);
            this.lblObservableFilesDesc.TabIndex = 11;
            this.lblObservableFilesDesc.Text = "Monitors any files/directories \r\nthat are copied to the clipboard.";
            // 
            // chkObserveFiles
            // 
            this.chkObserveFiles.AutoSize = true;
            this.chkObserveFiles.Checked = true;
            this.chkObserveFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObserveFiles.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.chkObserveFiles.ForeColor = System.Drawing.Color.Black;
            this.chkObserveFiles.Location = new System.Drawing.Point(17, 163);
            this.chkObserveFiles.Name = "chkObserveFiles";
            this.chkObserveFiles.Size = new System.Drawing.Size(53, 21);
            this.chkObserveFiles.TabIndex = 10;
            this.chkObserveFiles.Text = "Files";
            this.chkObserveFiles.UseVisualStyleBackColor = true;
            this.chkObserveFiles.CheckedChanged += new System.EventHandler(this.chkObserveFiles_CheckedChanged);
            // 
            // lblObservableImagesDesc
            // 
            this.lblObservableImagesDesc.AutoSize = true;
            this.lblObservableImagesDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservableImagesDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblObservableImagesDesc.Location = new System.Drawing.Point(33, 120);
            this.lblObservableImagesDesc.Name = "lblObservableImagesDesc";
            this.lblObservableImagesDesc.Size = new System.Drawing.Size(164, 30);
            this.lblObservableImagesDesc.TabIndex = 9;
            this.lblObservableImagesDesc.Text = "Monitors any images that are \r\ncopied to the clipboard.";
            // 
            // chkObserveImages
            // 
            this.chkObserveImages.AutoSize = true;
            this.chkObserveImages.Checked = true;
            this.chkObserveImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObserveImages.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.chkObserveImages.ForeColor = System.Drawing.Color.Black;
            this.chkObserveImages.Location = new System.Drawing.Point(17, 99);
            this.chkObserveImages.Name = "chkObserveImages";
            this.chkObserveImages.Size = new System.Drawing.Size(71, 21);
            this.chkObserveImages.TabIndex = 8;
            this.chkObserveImages.Text = "Images";
            this.chkObserveImages.UseVisualStyleBackColor = true;
            this.chkObserveImages.CheckedChanged += new System.EventHandler(this.chkObserveImages_CheckedChanged);
            // 
            // lblObservableTextsDesc
            // 
            this.lblObservableTextsDesc.AutoSize = true;
            this.lblObservableTextsDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblObservableTextsDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblObservableTextsDesc.Location = new System.Drawing.Point(33, 54);
            this.lblObservableTextsDesc.Name = "lblObservableTextsDesc";
            this.lblObservableTextsDesc.Size = new System.Drawing.Size(151, 30);
            this.lblObservableTextsDesc.TabIndex = 7;
            this.lblObservableTextsDesc.Text = "Monitors any texts that are \r\ncopied to the clipboard.";
            // 
            // chkObserveTexts
            // 
            this.chkObserveTexts.AutoSize = true;
            this.chkObserveTexts.Checked = true;
            this.chkObserveTexts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkObserveTexts.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.chkObserveTexts.ForeColor = System.Drawing.Color.Black;
            this.chkObserveTexts.Location = new System.Drawing.Point(17, 33);
            this.chkObserveTexts.Name = "chkObserveTexts";
            this.chkObserveTexts.Size = new System.Drawing.Size(58, 21);
            this.chkObserveTexts.TabIndex = 1;
            this.chkObserveTexts.Text = "Texts";
            this.chkObserveTexts.UseVisualStyleBackColor = true;
            this.chkObserveTexts.CheckedChanged += new System.EventHandler(this.chkObserveTexts_CheckedChanged);
            // 
            // txtCopiedTexts
            // 
            this.txtCopiedTexts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCopiedTexts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCopiedTexts.Location = new System.Drawing.Point(8, 69);
            this.txtCopiedTexts.Multiline = true;
            this.txtCopiedTexts.Name = "txtCopiedTexts";
            this.txtCopiedTexts.Size = new System.Drawing.Size(236, 314);
            this.txtCopiedTexts.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Honeydew;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCopiedTexts);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(312, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 397);
            this.panel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 30);
            this.label1.TabIndex = 7;
            this.label1.Text = "Copied Texts";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(27, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "will appear here...";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.pbCopiedImage);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(564, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 397);
            this.panel3.TabIndex = 7;
            // 
            // pbCopiedImage
            // 
            this.pbCopiedImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCopiedImage.BackColor = System.Drawing.Color.Transparent;
            this.pbCopiedImage.Image = null;
            this.pbCopiedImage.Location = new System.Drawing.Point(7, 69);
            this.pbCopiedImage.Name = "pbCopiedImage";
            this.pbCopiedImage.Size = new System.Drawing.Size(237, 313);
            this.pbCopiedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCopiedImage.TabIndex = 9;
            this.pbCopiedImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(25, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 30);
            this.label3.TabIndex = 7;
            this.label3.Text = "Copied Image(s)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(27, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "will appear here...";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.Ivory;
            this.panel4.Controls.Add(this.lstCopiedFiles);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Location = new System.Drawing.Point(816, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(252, 397);
            this.panel4.TabIndex = 8;
            // 
            // lstCopiedFiles
            // 
            this.lstCopiedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCopiedFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCopiedFiles.FormattingEnabled = true;
            this.lstCopiedFiles.HorizontalScrollbar = true;
            this.lstCopiedFiles.Location = new System.Drawing.Point(8, 69);
            this.lstCopiedFiles.Name = "lstCopiedFiles";
            this.lstCopiedFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCopiedFiles.Size = new System.Drawing.Size(236, 314);
            this.lstCopiedFiles.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(25, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 30);
            this.label5.TabIndex = 7;
            this.label5.Text = "Copied File(s)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(27, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "will appear here...";
            // 
            // sharpClipboard1
            // 
            this.sharpClipboard1.MonitorClipboard = true;
            this.sharpClipboard1.ObservableFormats.All = true;
            this.sharpClipboard1.ObservableFormats.Files = true;
            this.sharpClipboard1.ObservableFormats.Images = true;
            this.sharpClipboard1.ObservableFormats.Others = true;
            this.sharpClipboard1.ObservableFormats.Texts = true;
            this.sharpClipboard1.ObserveLastEntry = true;
            this.sharpClipboard1.Tag = null;
            this.sharpClipboard1.ClipboardChanged += new System.EventHandler<WK.Libraries.SharpClipboardNS.SharpClipboard.ClipboardChangedEventArgs>(this.sharpClipboard1_ClipboardChanged);
            this.sharpClipboard1.MonitorClipboardChanged += new System.EventHandler<System.EventArgs>(this.sharpClipboard1_MonitorClipboardChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 397);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpClipboard: Tests";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpLibrarySettings.ResumeLayout(false);
            this.grpLibrarySettings.PerformLayout();
            this.grpObservableFormats.ResumeLayout(false);
            this.grpObservableFormats.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCopiedImage)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private WK.Libraries.SharpClipboardNS.SharpClipboard sharpClipboard1;
        private System.Windows.Forms.CheckBox chkMonitorClipboard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpLibrarySettings;
        private System.Windows.Forms.GroupBox grpObservableFormats;
        private System.Windows.Forms.CheckBox chkObserveTexts;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.Label lblObservableFilesDesc;
        private System.Windows.Forms.CheckBox chkObserveFiles;
        private System.Windows.Forms.Label lblObservableImagesDesc;
        private System.Windows.Forms.CheckBox chkObserveImages;
        private System.Windows.Forms.Label lblObservableTextsDesc;
        private System.Windows.Forms.TextBox txtCopiedTexts;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pbCopiedImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstCopiedFiles;
    }
}

