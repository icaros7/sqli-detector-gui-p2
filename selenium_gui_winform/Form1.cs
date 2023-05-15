using System.ComponentModel;
using selenium_gui_winform.Properties;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace selenium_gui_winform {
    public partial class Form1 : Form {
        private string _browser = @"Edge";

        public Form1() {
            InitializeComponent();
        }

        private void LangInit() {
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
            LangInit();
        }

        private void itemEng_Click(object sender, EventArgs e) {
            Settings.Default.lastLang = @"en";
            Settings.Default.Save();
            itemEng.Checked = true;
            itemKor.Checked = false;
            ChangeLang("en");
        }

        private void itemKor_Click(object sender, EventArgs e) {
            if (Settings.Default.saveSettings) {
                Settings.Default.lastLang = @"ko";
                Settings.Default.Save();
            }
            itemKor.Checked = true;
            itemEng.Checked = false;
            ChangeLang("ko");
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.saveSettings = cbSave.Checked;
            Settings.Default.Save();
        }

        // Group for radio button for select browser
        private void radioEdge_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 0;
                Settings.Default.Save();
            }
            _browser = radioEdge.Text;
        }

        private void radioChrome_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 1;
                Settings.Default.Save();
            }
            _browser = radioChrome.Text;
        }

        private void radioFirefox_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 2;
                Settings.Default.Save();
            }
            _browser = radioFirefox.Text;
        }

        private void radioOpera_CheckedChanged(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastBrowser = 3;
                Settings.Default.Save();
            }
            _browser = radioOpera.Text;
        }
        // End of group

        /// <summary>
        /// BtnStart function for each case
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e) {
            if (cbSave.Checked) {
                Settings.Default.lastURL = tbURL.Text;
                Settings.Default.Save();
            }

            string path = Application.StartupPath;

            if ((!File.Exists(path + @"main.py")) || (!File.Exists(path + @"get_html.py")) || (!File.Exists(path + @"crawler.py"))) {
                // TODO: Downlaod least selenium project
                return;
            }

            // TODO: Check webdriver exist

            if (btnStart.Text == res.btnStart) {
                textBox1.AppendText(@"[INFO] Start detection" + "\r\n");
                textBox1.AppendText(@"Browser: " + _browser + "\r\n");
                textBox1.AppendText(@"URL: " + tbURL.Text + "\r\n");
                textBox1.AppendText(@"------------------------------" + "\r\n");
                textBox1.AppendText(res.waitForEnd + "\r\n");
                btnStart.Text = res.btnStop;

                // TODO: Async programming
                try {
                    ProcessStartInfo proc = new ProcessStartInfo {

                        FileName = @"python",
                        Arguments = ".\\execute\\main.py --browser=\"" + _browser + "\" --url=\"" + tbURL.Text + "\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    var result = string.Empty;
                    var error = string.Empty;

                    using (Process procc = Process.Start(proc)) {
                        using (StreamReader reader = procc.StandardOutput) {
                            while (!procc.HasExited) { textBox1.AppendText("\r\n" + reader.ReadLine()); }

                            error = procc.StandardError.ReadToEnd();
                            result = procc.StandardOutput.ReadToEnd();
                        }
                    }

                    textBox1.AppendText(error + "\r\n");
                    textBox1.AppendText(result + "\r\n");
                }
                catch (Exception ex) {
                    textBox1.AppendText(ex.Message + "\r\n");
                    btnStart.Text = res.btnStart;
                }
                finally {
                    textBox1.AppendText(@"[INFO] Finished.");
                    btnStart.Text = res.btnStart;
                }

                // TODO: Open Directory
            }
            else {
                textBox1.AppendText("\r\n" + @"[INFO] Stop dectection");
                btnStart.Text = res.btnStart;
            }
        }

        /// <summary>
        /// Call restore settings when I checked at saveSettings and apply language settings
        /// </summary>
        private void Form1_Load(object sender, EventArgs e) {
            if (Settings.Default.saveSettings) {
                if (Settings.Default.saveSettings) { cbSave.Checked = true; }
                tbURL.Text = Settings.Default.lastURL;
                switch (Settings.Default.lastBrowser) {
                    case 0:
                        radioEdge.Checked = true;
                        _browser = radioEdge.Text;
                        break;

                    case 1:
                        radioChrome.Checked = true;
                        _browser = radioChrome.Text;
                        break;

                    case 2:
                        radioFirefox.Checked = true;
                        _browser = radioFirefox.Text;
                        break;

                    case 3:
                        radioOpera.Checked = true;
                        _browser = radioOpera.Text;
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
            LangInit();

            // Append Text
            textBox1.AppendText(@"Selenium SQLi Dector" + "\r\n");
            textBox1.AppendText(@"Version: " + Application.ProductVersion + "\r\n");
            textBox1.AppendText(@"K-Shield Jr.: A-09" + "\r\n");
            textBox1.AppendText(@"------------------------------" + "\r\n");
        }

        /// <summary>
        /// Call MessageBox for About
        /// </summary>
        private void itemAbout_Click(object sender, EventArgs e) {
            MessageBox.Show(res.aboutMessage, res.aboutTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Check update
        /// </summary>
        private void itemVersion_Click(object sender, EventArgs e) {
            // Parsing version text
            int version = 0;
            try {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                int.TryParse(wc.DownloadString(@"https://raw.githubusercontent.com/ksj-10th-a09/selenium_crawl_p1/main/version.txt").Replace(".", ""), out version);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString() + "\r\n" + @"Please contact to support team.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            finally {
                if (version == 0) { MessageBox.Show(res.updateFail, res.error, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                if (version > int.Parse(Application.ProductVersion.Replace(".", ""))) {
                    if (MessageBox.Show(res.updateNew, res.information, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                        // TODO: Update download func
                    }
                }
                else {
                    MessageBox.Show(res.updateNoNeed, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void itemExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}