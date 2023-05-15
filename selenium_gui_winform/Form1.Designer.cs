namespace selenium_gui_winform {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            labelTarget = new Label();
            tbURL = new TextBox();
            cbSave = new CheckBox();
            menuStrip = new MenuStrip();
            stripFile = new ToolStripMenuItem();
            itemExit = new ToolStripMenuItem();
            stripLang = new ToolStripMenuItem();
            itemKor = new ToolStripMenuItem();
            itemEng = new ToolStripMenuItem();
            stripHelp = new ToolStripMenuItem();
            itemVersion = new ToolStripMenuItem();
            itemBar = new ToolStripSeparator();
            itemAbout = new ToolStripMenuItem();
            groupBrowser = new GroupBox();
            radioOpera = new RadioButton();
            radioFirefox = new RadioButton();
            radioChrome = new RadioButton();
            radioEdge = new RadioButton();
            btnStart = new Button();
            textBox1 = new TextBox();
            menuStrip.SuspendLayout();
            groupBrowser.SuspendLayout();
            SuspendLayout();
            // 
            // labelTarget
            // 
            resources.ApplyResources(labelTarget, "labelTarget");
            labelTarget.Name = "labelTarget";
            // 
            // tbURL
            // 
            resources.ApplyResources(tbURL, "tbURL");
            tbURL.Name = "tbURL";
            // 
            // cbSave
            // 
            resources.ApplyResources(cbSave, "cbSave");
            cbSave.Name = "cbSave";
            cbSave.UseVisualStyleBackColor = true;
            cbSave.CheckedChanged += cbSave_CheckedChanged;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { stripFile, stripLang, stripHelp });
            resources.ApplyResources(menuStrip, "menuStrip");
            menuStrip.Name = "menuStrip";
            // 
            // stripFile
            // 
            stripFile.DropDownItems.AddRange(new ToolStripItem[] { itemExit });
            stripFile.Name = "stripFile";
            resources.ApplyResources(stripFile, "stripFile");
            // 
            // itemExit
            // 
            itemExit.Name = "itemExit";
            resources.ApplyResources(itemExit, "itemExit");
            itemExit.Click += itemExit_Click;
            // 
            // stripLang
            // 
            stripLang.DropDownItems.AddRange(new ToolStripItem[] { itemKor, itemEng });
            stripLang.Name = "stripLang";
            resources.ApplyResources(stripLang, "stripLang");
            // 
            // itemKor
            // 
            itemKor.CheckOnClick = true;
            itemKor.Name = "itemKor";
            resources.ApplyResources(itemKor, "itemKor");
            itemKor.Click += itemKor_Click;
            // 
            // itemEng
            // 
            itemEng.Checked = true;
            itemEng.CheckOnClick = true;
            itemEng.CheckState = CheckState.Checked;
            itemEng.Name = "itemEng";
            resources.ApplyResources(itemEng, "itemEng");
            itemEng.Click += itemEng_Click;
            // 
            // stripHelp
            // 
            stripHelp.DropDownItems.AddRange(new ToolStripItem[] { itemVersion, itemBar, itemAbout });
            stripHelp.Name = "stripHelp";
            resources.ApplyResources(stripHelp, "stripHelp");
            // 
            // itemVersion
            // 
            itemVersion.Name = "itemVersion";
            resources.ApplyResources(itemVersion, "itemVersion");
            itemVersion.Click += itemVersion_Click;
            // 
            // itemBar
            // 
            itemBar.Name = "itemBar";
            resources.ApplyResources(itemBar, "itemBar");
            // 
            // itemAbout
            // 
            itemAbout.Name = "itemAbout";
            resources.ApplyResources(itemAbout, "itemAbout");
            itemAbout.Click += itemAbout_Click;
            // 
            // groupBrowser
            // 
            groupBrowser.Controls.Add(radioOpera);
            groupBrowser.Controls.Add(radioFirefox);
            groupBrowser.Controls.Add(radioChrome);
            groupBrowser.Controls.Add(radioEdge);
            resources.ApplyResources(groupBrowser, "groupBrowser");
            groupBrowser.Name = "groupBrowser";
            groupBrowser.TabStop = false;
            // 
            // radioOpera
            // 
            resources.ApplyResources(radioOpera, "radioOpera");
            radioOpera.Name = "radioOpera";
            radioOpera.UseVisualStyleBackColor = true;
            radioOpera.CheckedChanged += radioOpera_CheckedChanged;
            // 
            // radioFirefox
            // 
            resources.ApplyResources(radioFirefox, "radioFirefox");
            radioFirefox.Name = "radioFirefox";
            radioFirefox.UseVisualStyleBackColor = true;
            radioFirefox.CheckedChanged += radioFirefox_CheckedChanged;
            // 
            // radioChrome
            // 
            resources.ApplyResources(radioChrome, "radioChrome");
            radioChrome.Name = "radioChrome";
            radioChrome.UseVisualStyleBackColor = true;
            radioChrome.CheckedChanged += radioChrome_CheckedChanged;
            // 
            // radioEdge
            // 
            resources.ApplyResources(radioEdge, "radioEdge");
            radioEdge.Checked = true;
            radioEdge.Name = "radioEdge";
            radioEdge.TabStop = true;
            radioEdge.UseVisualStyleBackColor = true;
            radioEdge.CheckedChanged += radioEdge_CheckedChanged;
            // 
            // btnStart
            // 
            resources.ApplyResources(btnStart, "btnStart");
            btnStart.Name = "btnStart";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox1);
            Controls.Add(btnStart);
            Controls.Add(groupBrowser);
            Controls.Add(cbSave);
            Controls.Add(tbURL);
            Controls.Add(labelTarget);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "Form1";
            Load += Form1_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            groupBrowser.ResumeLayout(false);
            groupBrowser.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTarget;
        private TextBox tbURL;
        private CheckBox cbSave;
        private MenuStrip menuStrip;
        private ToolStripMenuItem stripFile;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem stripLang;
        private ToolStripMenuItem itemKor;
        private ToolStripMenuItem itemEng;
        private ToolStripMenuItem stripHelp;
        private ToolStripMenuItem itemVersion;
        private ToolStripSeparator itemBar;
        private ToolStripMenuItem itemAbout;
        private GroupBox groupBrowser;
        private RadioButton radioOpera;
        private RadioButton radioFirefox;
        private RadioButton radioChrome;
        private RadioButton radioEdge;
        private Button btnStart;
        private TextBox textBox1;
    }
}