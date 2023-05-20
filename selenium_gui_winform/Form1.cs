using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Text;
using selenium_gui_winform.Properties;

namespace selenium_gui_winform;

public partial class Form1 : Form {
    private static ProcessStartInfo? Psi;
    private static Process? Proc;
    private readonly Dictionary<string, string> cookie = new();
    private string _browser = @"Edge";

    public Form1() {
        InitializeComponent();
    }

    /// <summary>
    ///     Execute python file update method
    /// </summary>
    /// <param name="mode">0: Download, 1: Check</param>
    /// <returns></returns>
    private string UpdateExecute(int mode) {
        if (mode == 0) { // 0: Download latest file
            textBox1.AppendText(res.notFoundExecute);

            try {
                var path = Path.Combine(Application.StartupPath, @"execute");
                if (Directory.Exists(path)) Directory.Delete(path, true);
                Directory.CreateDirectory(path);

                var wc = new WebClient();
                wc.DownloadFile(
                    "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/main.py",
                    path + @"\main.py");
                wc.DownloadFile(
                    "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/final_a_href_crawl.py",
                    path + @"\final_a_href_crawl.py");
                wc.DownloadFile(
                    "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/strctured_data_save.py",
                    path + @"\strctured_data_save.py");
                wc.DownloadFile(
                    "https://github.com/ksj-10th-a09/selenium_crawl_p1/releases/latest/download/sqli.py",
                    path + @"\sqli.py");
                wc.DownloadFile(@"https://raw.githubusercontent.com/ksj-10th-a09/selenium_crawl_p1/main/version.txt",
                    path + @"\version.txt");
            }
            catch (IOException) {
                textBox1.AppendText(@"ERROR: Old file delete or download failed.");
                return "-1";
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString(), res.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }

            return "";
        }

        if (mode == 1) { // 1: Check Update
            // Parsing version text
            var    version = 0;
            string versionStr;
            try {
                var wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                versionStr =
                    wc.DownloadString(
                        @"https://raw.githubusercontent.com/ksj-10th-a09/selenium_crawl_p1/main/version.txt");
                int.TryParse(versionStr.Replace(".", ""), out version);
            }
            catch (Exception ex) {
                MessageBox.Show(ex + "\r\n" + @"Please contact to support team.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return "-1";
            }

            if (version == 0) {
                MessageBox.Show(res.updateFail, res.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }

            if (version > int.Parse(Application.ProductVersion.Replace(".", ""))) {
                if (MessageBox.Show(res.updateNew, res.information, MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK) UpdateExecute(0);
            }
            else {
                MessageBox.Show(res.updateNoNeed, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return version.ToString();
        }

        return "-1";
    }

    private void LangInit() {
        // Apply Localization
        Text                = res.appName;
        labelTarget.Text    = res.labelTarget + @": ";
        cbSave.Text         = res.cbSave;
        stripFile.Text      = res.stripFile;
        stripLang.Text      = res.stripLang;
        stripHelp.Text      = res.stripHelp;
        itemAbout.Text      = res.itemAbout;
        itemExit.Text       = res.itemExit;
        itemVersion.Text    = res.itemVersion;
        itemVersionGUI.Text = res.itemVersionGUI;
        groupBrowser.Text   = res.groupBrowser;
        btnStart.Text       = res.btnStart;
    }

    /// <summary>
    ///     Check exist what main execute files
    /// </summary>
    /// <returns>bool Exist or not</returns>
    private static bool CheckExecuteExist() {
        var path = Path.Combine(Application.StartupPath, @"execute");

        return File.Exists(path + @"\main.py") && File.Exists(path + @"\get_html.py") &&
               File.Exists(path + @"\crawler.py");
    }

    /// <summary>
    ///     Return exist each web driver
    /// </summary>
    /// <returns>bool Exist or not</returns>
    private bool CheckWebDriverExist() {
        return _browser switch {
            "Edge"    => File.Exists(Application.StartupPath + @"msedgedriver.exe"),
            "Chrome"  => File.Exists(Application.StartupPath + @"chromedriver.exe"),
            "Firefox" => File.Exists(Application.StartupPath + @"geckodriver.exe"),
            _         => false
        };
    }

    private void ChangeLang(string lang) {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
        Settings.Default.lastLang             = lang;
        Settings.Default.Save();
        MessageBox.Show(res.changeLang, res.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        LangInit();
    }

    /// <summary>
    ///     Async method for start crawling via python
    /// </summary>
    private async void StartCall() {
        if (Proc == null || Psi == null) return;
        Proc.StartInfo = Psi;
        Proc.Start();

        Proc.BeginOutputReadLine();
        Proc.BeginErrorReadLine();

        Proc.OutputDataReceived += (sender, e) => textBox1.AppendText(e.Data + "\r\n");
        Proc.ErrorDataReceived += (sender, e) => {
            if (e.Data == @"ModuleNotFoundError: No module named 'selenium'") {
                if (MessageBox.Show(res.seleniumNotFound, res.information, MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                    Process.Start(@"python -m pip install selenium");
                else
                    return;
            }

            textBox1.AppendText(e.Data + "\r\n");
        };

        await Proc.WaitForExitAsync();

        Proc.CancelOutputRead();
        Proc.CancelErrorRead();
        Proc.Close();

        textBox1.AppendText(@"[INFO] Finished." + "\r\n");
        btnStart.Text = res.btnStart;
    }

    private void itemEng_Click(object sender, EventArgs e) {
        if (btnStart.Text == res.btnStop) {
            MessageBox.Show(res.chgLangWhile, res.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

        if (Settings.Default.saveSettings) {
            Settings.Default.lastLang = @"en";
            Settings.Default.Save();
        }

        itemEng.Checked = true;
        itemKor.Checked = false;
        ChangeLang("en");
    }

    private void itemKor_Click(object sender, EventArgs e) {
        if (btnStart.Text == res.btnStop) {
            MessageBox.Show(res.chgLangWhile, res.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return;
        }

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
    ///     BtnStart function for each case
    /// </summary>
    private void btnStart_Click(object sender, EventArgs e) {
        if (cbSave.Checked) {
            Settings.Default.lastURL = tbURL.Text;
            Settings.Default.Save();
        }

        // Check main.py exist
        if (!CheckExecuteExist())
            try {
                var verFile = Path.Combine(Application.StartupPath, @"\execute\version.txt");
                var version = File.ReadAllLines(verFile);

                if (version.Length > 0 && version[0] != UpdateExecute(1)) UpdateExecute(0);
            }

            catch (IOException) {
                textBox1.AppendText(@"ERROR: Can't read version information.");
                UpdateExecute(0);
            }

        if (!CheckWebDriverExist()) {
            if (MessageBox.Show(_browser + res.webNo + "\n\n" + res.webPath + Application.StartupPath + "\n\n" +
                                res.webOpen, res.information, MessageBoxButtons.YesNo, MessageBoxIcon.Question
                ) != DialogResult.Yes) return;
            switch (_browser) {
                case "Edge":
                    Process.Start(
                        @"https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/");
                    break;
                case "Chrome":
                    Process.Start(@"https://chromedriver.chromium.org/downloads");
                    break;
                case "Firefox":
                    Process.Start(@"https://github.com/mozilla/geckodriver/releases");
                    break;
            }
        }

        if (btnStart.Text == res.btnStart) {
            textBox1.AppendText("\r\n" + @"[INFO] Start detection" + "\r\n");
            textBox1.AppendText(@"Browser: " + _browser + "\r\n");
            textBox1.AppendText(@"URL: " + tbURL.Text + "\r\n");
            textBox1.AppendText(@"------------------------------" + "\r\n");
            textBox1.AppendText(res.waitForEnd + "\r\n");
            btnStart.Text = res.btnStop;

            var cookie_str = "";
            if (cookie.Count != 0)
                foreach (var item in cookie)
                    if (cookie_str == "") cookie_str =  $"{item.Key}.{item.Value}";
                    else cookie_str                  += $",{item.Key}.{item.Value}";

            try {
                Psi = new ProcessStartInfo {
                    FileName = @"python",
                    Arguments = ".\\execute\\main.py --browser=\"" + _browser + "\" --url=\"" + tbURL.Text +
                                "\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    CreateNoWindow         = true
                };

                Proc                     = new Process();
                Proc.EnableRaisingEvents = true;

                StartCall();
            }
            catch (Exception ex) {
                textBox1.AppendText(ex.Message + "\r\n");
                btnStart.Text = res.btnStart;
            }
            
            //TODO: Receive result
            bool result = false;

            Uri        domain = new Uri(tbURL.Text);
            reportForm rf     = new reportForm(tbURL.Text, domain.Host, result);
            rf.ShowDialog();
        }
        else {
            textBox1.AppendText("\r\n" + @"[INFO] Stop detection");
            btnStart.Text = res.btnStart;
        }
    }

    /// <summary>
    ///     Call restore settings when I checked at saveSettings and apply language settings
    /// </summary>
    private void Form1_Load(object sender, EventArgs e) {
        if (Settings.Default.saveSettings) {
            if (Settings.Default.saveSettings) cbSave.Checked = true;
            tbURL.Text = Settings.Default.lastURL;
            switch (Settings.Default.lastBrowser) {
                case 0:
                    radioEdge.Checked = true;
                    _browser          = radioEdge.Text;
                    break;

                case 1:
                    radioChrome.Checked = true;
                    _browser            = radioChrome.Text;
                    break;

                case 2:
                    radioFirefox.Checked = true;
                    _browser             = radioFirefox.Text;
                    break;

                case 3:
                    radioOpera.Checked = true;
                    _browser           = radioOpera.Text;
                    break;
            }
        }

        // Check Last Settings
        if (Settings.Default.lastLang == null) {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Settings.Default.lastLang             = "en";
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
    ///     Call MessageBox for About
    /// </summary>
    private void itemAbout_Click(object sender, EventArgs e) {
        MessageBox.Show(res.aboutMessage, res.itemAbout, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    ///     Check update
    /// </summary>
    private void itemVersion_Click(object sender, EventArgs e) {
        // Parsing version text
        var versionStr = "";
        var version    = 0;
        try {
            var wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            versionStr = wc.DownloadString(
                @"https://raw.githubusercontent.com/ksj-10th-a09/sqli-detection-gui/main/version.txt");
            int.TryParse(versionStr.Replace(".", ""), out version);
        }
        catch (Exception ex) {
            MessageBox.Show(ex + "\r\n" + @"Please contact to support team.", @"Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally {
            if (version == 0) MessageBox.Show(res.updateFail, res.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (version > int.Parse(Application.ProductVersion.Replace(".", ""))) {
                if (MessageBox.Show(res.updateNew + "\n\n" + res.currentVersion + Application.ProductVersion + "\n" +
                                    res.newVersion + versionStr, res.information, MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
                    UpdateExecute(0);
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