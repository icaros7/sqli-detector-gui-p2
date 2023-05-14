using selenium_gui_winform.Properties;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace selenium_gui_winform {
    public partial class Form1 : Form {
        private string browser = @"Edge";

        public Form1() {
            InitializeComponent();
        }

        private void langInit() {
            // Apply Localization
            Text = res.appName;
            labelTarget.Text = res.labelTarget + @": ";
            cbSave.Text = res.cbSave;
            stripFile.Text = res.stripFile;
            stripLang.Text = res.stripLang;
            stripHelp.Text = res.stripHelp;
            itemAbout.Text = res.itemAbout;
            itemExit.Text = res.itemExit;
            itemVersion.Text = res.itemVersion;
            groupBrowser.Text = res.groupBrowser;
            btnStart.Text = res.btnStart;
        }

        private void changeLang(string lang) {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Settings.Default.lastLang = lang;
            Settings.Default.Save();
            MessageBox.Show(res.changeLang, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            langInit();
        }

        private void itemEng_Click(object sender, EventArgs e) {
            Settings.Default.lastLang = @"en";
            Settings.Default.Save();
            itemEng.Checked = true;
            itemKor.Checked = false;
            changeLang("en");
        }

        private void itemKor_Click(object sender, EventArgs e) {
            if (Settings.Default.saveSettings) {
                Settings.Default.lastLang = @"ko";
                Settings.Default.Save();
            }
            itemKor.Checked = true;
            itemEng.Checked = false;
            changeLang("ko");
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.saveSettings = cbSave.Checked;
            Settings.Default.Save();
        }

        private void radioEdge_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 0;
                Settings.Default.Save();
            }
            browser = radioEdge.Text;
        }

        private void radioChrome_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 1;
                Settings.Default.Save();
            }
            browser = radioChrome.Text;
        }

        private void radioFirefox_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 2;
                Settings.Default.Save();
            }
            browser = radioFirefox.Text;
        }

        private void radioOpera_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 3;
                Settings.Default.Save();
            }
            browser = radioOpera.Text;
        }

        private void btnStart_Click(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastURL = tbURL.Text;
                Settings.Default.Save();
            }
            if (btnStart.Text == res.btnStart) {
                textBox1.AppendText("\r\n" + @"[INFO] Start detection");
                btnStart.Text = res.btnStop;

                try {
                    ProcessStartInfo proc = new ProcessStartInfo {

                        FileName = @"python", 
                        Arguments = "main.py --browser=\"Edge\" --url=\"https://minnote.net\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    var result = string.Empty;
                    var error = string.Empty;

                    using (Process procc = Process.Start(proc)) {
                        using (StreamReader reader = procc.StandardOutput) {
                            while (!procc.HasExited) { textBox1.AppendText("\r\n" +  reader.ReadLine()); }

                            error = procc.StandardError.ReadToEnd();
                            result = procc.StandardOutput.ReadToEnd();
                        }
                    }

                    textBox1.AppendText("\r\n" + error);
                    textBox1.AppendText("\r\n" + result);
                }
                catch (Exception ex) {
                    textBox1.AppendText("\r\n" +  ex.Message);
                    btnStart.Text = res.btnStart;
                }
            }
            else {
                textBox1.AppendText("\r\n" + @"[INFO] Stop dectection");
                btnStart.Text = res.btnStart;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            if (Settings.Default.saveSettings) {
                if (Settings.Default.saveSettings) { cbSave.Checked = true; }
                tbURL.Text = Settings.Default.lastURL;
                switch (Settings.Default.lastBrowser) {
                    case 0:
                        radioEdge.Checked = true;
                        browser = radioEdge.Text;
                        break;

                    case 1:
                        radioChrome.Checked = true;
                        browser = radioChrome.Text;
                        break;

                    case 2:
                        radioFirefox.Checked = true;
                        browser = radioFirefox.Text;
                        break;

                    case 3:
                        radioOpera.Checked = true;
                        browser = radioOpera.Text;
                        break;
                }
            }

            // Check Last Settings
            if (Settings.Default.lastLang == null) {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Settings.Default.lastLang = "en";
                Settings.Default.Save();
            }
            else {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.lastLang);
            }

            // Apply Language
            langInit();
        }
    }
}