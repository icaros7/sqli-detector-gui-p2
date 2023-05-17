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
        public static ProcessStartInfo? Psi = null;
        public static Process? Proc = null;

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
            cbTag.Text = res.cbTag;
        }

        /// <summary>
        /// Check exist what main execute files
        /// </summary>
        /// <returns>bool Exist or not</returns>
        private static bool CheckExecuteExist() {
            string path = Application.StartupPath;

            return File.Exists(path + @"\execute\main.py") && File.Exists(path + @"\execute\get_html.py") &&
                    File.Exists(path + @"\execute\crawler.py");
        }

        /// <summary>
        /// Return exist each web driver
        /// </summary>
        /// <returns>bool Exist or not</returns>
        private bool CheckWebDriverExist() {
            return _browser switch {
                "Edge" => File.Exists(Application.StartupPath + @"msedgedriver.exe"),
                "Chrome" => File.Exists(Application.StartupPath + @"chromedriver.exe"),
                "Firefox" => File.Exists(Application.StartupPath + @"geckodriver.exe"),
                _ => false
            };
        }

        private void ChangeLang(string lang) {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            Settings.Default.lastLang = lang;
            Settings.Default.Save();
            MessageBox.Show(res.changeLang, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            LangInit();
        }

        private async void StartCall() {
            Proc.StartInfo = Psi;
            Proc.Start();
            
            Proc.BeginOutputReadLine();
            Proc.BeginErrorReadLine();

            Proc.OutputDataReceived += (sender, e) => textBox1.AppendText(e.Data + "\r\n");
            Proc.ErrorDataReceived  += (sender, e) => textBox1.AppendText(e.Data + "\r\n");
                    
            await Proc.WaitForExitAsync();
            
            Proc.CancelOutputRead();
            Proc.CancelErrorRead();
            Proc.Close();
            
            textBox1.AppendText(@"[INFO] Finished." + "\r\n");
            btnStart.Text = res.btnStart;
            
            var reg     = new Regex(@"://(?<host>([a-z\d][-a-z\d]*[a-z\d]\.)*[a-z][-a-z\d]+[a-z])");
            var workdir = Application.StartupPath + reg.Match(tbURL.Text).Result("${host}");
            
            try {
                Process.Start(workdir);
            }
            catch (Win32Exception) {
                MessageBox.Show(res.doneCrawl + "\n\n" + res.downPath + workdir, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

            // Check main.py exist
            if (!CheckExecuteExist()) {
                textBox1.AppendText(res.notFoundExecute);

                //TODO: Split download application
                try {
                    Directory.CreateDirectory(Application.StartupPath + @"\execute");
                    WebClient wc = new WebClient();
                    wc.DownloadFile(
                        "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/main.py",
                        @".\execute\main.py");
                    wc.DownloadFile(
                        "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/crawler.py",
                        @".\execute\crawler.py");
                    wc.DownloadFile(
                        "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/get_html.py",
                        @".\execute\get_html.py");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.ToString(), res.error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            if (!CheckWebDriverExist()) {
                MessageBox.Show(_browser + res.webNo + "\n\n" + res.webPath + Application.StartupPath, res.information,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnStart.Text == res.btnStart) {
                textBox1.AppendText("\r\n" + @"[INFO] Start detection" + "\r\n");
                textBox1.AppendText(@"Browser: " + _browser + "\r\n");
                textBox1.AppendText(@"URL: " + tbURL.Text + "\r\n");
                textBox1.AppendText(@"Tag: " + cbTag.Checked + "\r\n");
                textBox1.AppendText(@"------------------------------" + "\r\n");
                textBox1.AppendText(res.waitForEnd + "\r\n");
                btnStart.Text = res.btnStop;

                try {
                    Psi = new ProcessStartInfo {
                        FileName = @"python",
                        Arguments = ".\\execute\\main.py --browser=\"" + _browser + "\" --url=\"" + tbURL.Text + "\" --tag=\"" + cbTag.Checked + "\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    Proc                     = new Process();
                    Proc.EnableRaisingEvents = true;

                    // Proc.OutputDataReceived += new DataReceivedEventHandler((sender, e) => {
                    //     if (!string.IsNullOrEmpty(e.Data)) { textBox1.AppendText(e.Data + "\r\n"); }
                    // });
                    // Proc.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => {
                    //     if (!string.IsNullOrEmpty(e.Data)) { textBox1.AppendText(@"[ERROR] " + e.Data + "\r\n"); }
                    // });
            
                    StartCall();
                }
                catch (Exception ex) {
                    textBox1.AppendText(ex.Message + "\r\n");
                    btnStart.Text = res.btnStart;
                }
            }
            else {
                ;
                textBox1.AppendText("\r\n" + @"[INFO] Stop detection");
                btnStart.Text = res.btnStart;
            }
        }

        /// <summary>
        /// Call restore settings when I checked at saveSettings and apply language settings
        /// </summary>
        private void Form1_Load(object sender, EventArgs e) {
            if (Settings.Default.saveSettings) {
                if (Settings.Default.saveSettings) { cbSave.Checked = true; }
                if (Settings.Default.searchTag) { cbTag.Checked = true; }
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
            textBox1.AppendText(@"Selenium SQLi Detector" + "\r\n");
            textBox1.AppendText(@"Version: " + Application.ProductVersion + "\r\n");
            textBox1.AppendText(@"K-Shield Jr.: A-09" + "\r\n");
            textBox1.AppendText(@"------------------------------" + "\r\n");
        }

        /// <summary>
        /// Call MessageBox for About
        /// </summary>
        private void itemAbout_Click(object sender, EventArgs e) {
            MessageBox.Show(res.aboutMessage, res.itemAbout, MessageBoxButtons.OK, MessageBoxIcon.Information);
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