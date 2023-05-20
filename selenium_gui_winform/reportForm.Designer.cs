namespace selenium_gui_winform {
    partial class reportForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            lbl_Target = new Label();
            lbl_Link = new LinkLabel();
            lbl_CountOfScan = new Label();
            lbl_Count = new Label();
            lbl_Status = new Label();
            lbl_Result = new Label();
            lbl_CountOfDetect = new Label();
            lbl_CounfOfFind = new Label();
            label2 = new Label();
            gbDetailed = new GroupBox();
            list_Detail = new ListBox();
            btn_Close = new Button();
            btn_Save = new Button();
            lbl_ReadMore = new LinkLabel();
            gbDetailed.SuspendLayout();
            SuspendLayout();
            // 
            // lbl_Target
            // 
            lbl_Target.AutoSize = true;
            lbl_Target.Location = new Point(12, 9);
            lbl_Target.Name = "lbl_Target";
            lbl_Target.Size = new Size(42, 15);
            lbl_Target.TabIndex = 0;
            lbl_Target.Text = "Target:";
            // 
            // lbl_Link
            // 
            lbl_Link.AutoSize = true;
            lbl_Link.Location = new Point(115, 9);
            lbl_Link.Name = "lbl_Link";
            lbl_Link.Size = new Size(385, 15);
            lbl_Link.TabIndex = 1;
            lbl_Link.TabStop = true;
            lbl_Link.Text = "https://328b-211-196-74-152.ngrok-free.app/DVWA/vulnerabilities/sqli/";
            // 
            // lbl_CountOfScan
            // 
            lbl_CountOfScan.AutoSize = true;
            lbl_CountOfScan.Location = new Point(12, 35);
            lbl_CountOfScan.Name = "lbl_CountOfScan";
            lbl_CountOfScan.Size = new Size(86, 15);
            lbl_CountOfScan.TabIndex = 2;
            lbl_CountOfScan.Text = "Count of Page:";
            // 
            // lbl_Count
            // 
            lbl_Count.AutoSize = true;
            lbl_Count.Location = new Point(115, 35);
            lbl_Count.Name = "lbl_Count";
            lbl_Count.Size = new Size(13, 15);
            lbl_Count.TabIndex = 3;
            lbl_Count.Text = "2";
            // 
            // lbl_Status
            // 
            lbl_Status.AutoSize = true;
            lbl_Status.Location = new Point(12, 62);
            lbl_Status.Name = "lbl_Status";
            lbl_Status.Size = new Size(80, 15);
            lbl_Status.TabIndex = 4;
            lbl_Status.Text = "Report Status:";
            // 
            // lbl_Result
            // 
            lbl_Result.AutoSize = true;
            lbl_Result.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Result.ForeColor = Color.Red;
            lbl_Result.Location = new Point(115, 62);
            lbl_Result.Name = "lbl_Result";
            lbl_Result.Size = new Size(162, 17);
            lbl_Result.TabIndex = 5;
            lbl_Result.Text = "FOUND Vulnerable Point";
            // 
            // lbl_CountOfDetect
            // 
            lbl_CountOfDetect.AutoSize = true;
            lbl_CountOfDetect.Location = new Point(12, 91);
            lbl_CountOfDetect.Name = "lbl_CountOfDetect";
            lbl_CountOfDetect.Size = new Size(83, 15);
            lbl_CountOfDetect.TabIndex = 6;
            lbl_CountOfDetect.Text = "Count of Find:";
            // 
            // lbl_CounfOfFind
            // 
            lbl_CounfOfFind.AutoSize = true;
            lbl_CounfOfFind.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_CounfOfFind.Location = new Point(115, 91);
            lbl_CounfOfFind.Name = "lbl_CounfOfFind";
            lbl_CounfOfFind.Size = new Size(15, 17);
            lbl_CounfOfFind.TabIndex = 7;
            lbl_CounfOfFind.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 193);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 8;
            // 
            // gbDetailed
            // 
            gbDetailed.Controls.Add(list_Detail);
            gbDetailed.Location = new Point(12, 126);
            gbDetailed.Name = "gbDetailed";
            gbDetailed.Size = new Size(370, 270);
            gbDetailed.TabIndex = 9;
            gbDetailed.TabStop = false;
            gbDetailed.Text = "Detail information";
            // 
            // list_Detail
            // 
            list_Detail.ItemHeight = 15;
            list_Detail.Items.AddRange(new object[] { "FOUND SQL Injection vulnerable point at https://328b-211-196-74-152.ngrok-free.app/DVWA/vulnerabilities/sqli/", "", "sqli.html -> <input> <- ` and 1=1 --", "", "----------------------------------------", "You know what? TIPs", "", "SQLI is not difficult tech-skill for even you.", "SQLI is just tiresome & tedious works.", "But can cause CRITICAL consequences." });
            list_Detail.Location = new Point(6, 22);
            list_Detail.Name = "list_Detail";
            list_Detail.ScrollAlwaysVisible = true;
            list_Detail.Size = new Size(358, 229);
            list_Detail.TabIndex = 0;
            // 
            // btn_Close
            // 
            btn_Close.Location = new Point(308, 402);
            btn_Close.Name = "btn_Close";
            btn_Close.Size = new Size(74, 36);
            btn_Close.TabIndex = 10;
            btn_Close.Text = "Close";
            btn_Close.UseVisualStyleBackColor = true;
            btn_Close.Click += btn_Close_Click;
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(191, 402);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(111, 36);
            btn_Save.TabIndex = 11;
            btn_Save.Text = "Save As .txt";
            btn_Save.UseVisualStyleBackColor = true;
            // 
            // lbl_ReadMore
            // 
            lbl_ReadMore.AutoSize = true;
            lbl_ReadMore.Location = new Point(12, 413);
            lbl_ReadMore.Name = "lbl_ReadMore";
            lbl_ReadMore.Size = new Size(125, 15);
            lbl_ReadMore.TabIndex = 12;
            lbl_ReadMore.TabStop = true;
            lbl_ReadMore.Text = "Read More about SQLi";
            // 
            // reportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 450);
            Controls.Add(lbl_ReadMore);
            Controls.Add(btn_Save);
            Controls.Add(btn_Close);
            Controls.Add(gbDetailed);
            Controls.Add(label2);
            Controls.Add(lbl_CounfOfFind);
            Controls.Add(lbl_CountOfDetect);
            Controls.Add(lbl_Result);
            Controls.Add(lbl_Status);
            Controls.Add(lbl_Count);
            Controls.Add(lbl_CountOfScan);
            Controls.Add(lbl_Link);
            Controls.Add(lbl_Target);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "reportForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "End Scan Report - https://328b-211-196-74-152.ngrok-free.app/DVWA/vulnerabilities/sqli";
            Load += reportForm_Load;
            Closing += reportForm_Closing;
            gbDetailed.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_Target;
        private LinkLabel lbl_Link;
        private Label lbl_CountOfScan;
        private Label lbl_Count;
        private Label lbl_Status;
        private Label lbl_Result;
        private Label lbl_CountOfDetect;
        private Label lbl_CounfOfFind;
        private Label label2;
        private GroupBox gbDetailed;
        private ListBox list_Detail;
        private Button btn_Close;
        private Button btn_Save;
        private LinkLabel lbl_ReadMore;
    }
}