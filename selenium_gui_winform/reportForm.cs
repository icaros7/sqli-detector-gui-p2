using System.ComponentModel;

namespace selenium_gui_winform; 

public partial class reportForm : Form {
    private string _domain = "";
    private int _page;
    private readonly string _path = "";

    private ScanPoint[] _pointArr;
    private readonly bool _saved = false;
    private int _scan;
    private readonly string _url = "";

    public reportForm(string url, string path, string domain) {
        InitializeComponent();
        _url    = url;
        _path   = path;
        _domain = domain;
    }

    private void reportForm_Load(object sender, EventArgs e) {
        LangInit();
        SearchDetect();

        list_Detail.Items.Add(res.text_0 + _url);
        list_Detail.Items.Add("");
        list_Detail.Items.Add(res.text_1);

        foreach (var cur in _pointArr) list_Detail.Items.Add($"{cur.File} -> <{cur.Tag}> <- {cur.Payload}");
        list_Detail.Items.Add(@"");
        list_Detail.Items.Add(res.text_2);
        list_Detail.Items.Add(@"");
        list_Detail.Items.Add(res.text_3);
        list_Detail.Items.Add(res.text_4);
    }

    private void reportForm_Closing(object sender, CancelEventArgs e) {
        if (!_saved &&
            MessageBox.Show(res.string_NotSave, res.error, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) ==
            DialogResult.No) e.Cancel = true;
    }

    /// <summary>
    ///     Apply localization
    /// </summary>
    private void LangInit() {
        Text                   = res.string_EndScanReport + @" - " + _url;
        lbl_Link.Text          = _url;
        lbl_Count.Text         = _scan.ToString();
        lbl_CounfOfFind.Text   = _page.ToString();
        lbl_CounfOfFind.Text   = res.lbl_CountOfScan + @":";
        lbl_CountOfDetect.Text = res.lbl_CountOfDetect + @":";
        lbl_Target.Text        = res.lbl_Target + @":";
        lbl_Status.Text        = res.lbl_Status + @":";
        gbDetailed.Text        = res.gb_Deatil;
        lbl_Link.Text          = res.link_ReadMore;
        btn_Save.Text          = res.btn_SaveAs;
        btn_Close.Text         = res.btn_Exit;
    }

    /// <summary>
    ///     Get Count of find page & detect page
    /// </summary>
    private void SearchDetect() {
        _page = Directory.GetFiles(_path, @"root__*.txt", SearchOption.AllDirectories).Length;
        _scan = Directory.GetFiles(_path, @"_found_*.txt", SearchOption.AllDirectories).Length;

        _pointArr = new ScanPoint[_scan];
        for (var i = 0; i < _scan; i++)
            //TODO: Read Format of _found_*.txt
            _pointArr[i] = new ScanPoint("File", "Tag", "Payload");
    }

    private void btn_Close_Click(object sender, EventArgs e) {
        // Call Closing EventHandler
        Close();
    }

    private struct ScanPoint {
        public readonly string File;
        public readonly string Tag;
        public readonly string Payload;

        public ScanPoint(string file, string tag, string payload) {
            File    = file;
            Tag     = tag;
            Payload = payload;
        }
    }
}