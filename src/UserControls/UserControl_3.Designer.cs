namespace TypeLighter.UserControls
{
    partial class UserControl_3
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label_Info = new System.Windows.Forms.Label();
            this.label_Left = new System.Windows.Forms.Label();
            this.pictureBox_Graph = new System.Windows.Forms.PictureBox();
            this.label_WordTotal = new System.Windows.Forms.Label();
            this.label_Word = new System.Windows.Forms.Label();
            this.label_Sec = new System.Windows.Forms.Label();
            this.label_Try = new System.Windows.Forms.Label();
            this.label_KeyTotal = new System.Windows.Forms.Label();
            this.label_Kpm = new System.Windows.Forms.Label();
            this.label_Miss = new System.Windows.Forms.Label();
            this.label_Key = new System.Windows.Forms.Label();
            this.label_BestKpm = new System.Windows.Forms.Label();
            this.label_KeyAverage = new System.Windows.Forms.Label();
            this.label_Min = new System.Windows.Forms.Label();
            this.label_Max = new System.Windows.Forms.Label();
            this.pictureBox_Word = new System.Windows.Forms.PictureBox();
            this.timer_Waiting = new System.Windows.Forms.Timer(this.components);
            this.timer_Running = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel = new System.Windows.Forms.Panel();
            this.labelSec = new System.Windows.Forms.Label();
            this.labelTry = new System.Windows.Forms.Label();
            this.labelKpm = new System.Windows.Forms.Label();
            this.labelMiss = new System.Windows.Forms.Label();
            this.labelBestKpm = new System.Windows.Forms.Label();
            this.labelKeyAverage = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox_Roman = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Graph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Word)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Roman)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // label_Info
            // 
            this.label_Info.Dock = System.Windows.Forms.DockStyle.Top;
            this.label_Info.Font = new System.Drawing.Font("ＭＳ ゴシック", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Info.Location = new System.Drawing.Point(0, 0);
            this.label_Info.Margin = new System.Windows.Forms.Padding(0);
            this.label_Info.Name = "label_Info";
            this.label_Info.Padding = new System.Windows.Forms.Padding(5, 0, 0, 5);
            this.label_Info.Size = new System.Drawing.Size(410, 45);
            this.label_Info.TabIndex = 17;
            this.label_Info.Text = "Space(開始)、Enter(中止)、矢印(結果閲覧)";
            this.label_Info.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip.SetToolTip(this.label_Info, "Space：開始\r\nEnter：中止、中止後であればクリア\r\n↑↓←→：移動\r\nShift+↑↓←→：星の数を増減");
            // 
            // label_Left
            // 
            this.label_Left.AutoSize = true;
            this.label_Left.Location = new System.Drawing.Point(-2, 6);
            this.label_Left.Name = "label_Left";
            this.label_Left.Size = new System.Drawing.Size(29, 12);
            this.label_Left.TabIndex = 16;
            this.label_Left.Text = "9999";
            this.label_Left.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.label_Left, "残りの行数");
            // 
            // pictureBox_Graph
            // 
            this.pictureBox_Graph.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox_Graph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Graph.Location = new System.Drawing.Point(0, 80);
            this.pictureBox_Graph.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Graph.Name = "pictureBox_Graph";
            this.pictureBox_Graph.Size = new System.Drawing.Size(410, 145);
            this.pictureBox_Graph.TabIndex = 13;
            this.pictureBox_Graph.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_Graph, "折れ線グラフ：kpmの推移\r\n棒グラフ：瞬間kpm\r\n赤線：最高kpm\r\n");
            this.pictureBox_Graph.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // label_WordTotal
            // 
            this.label_WordTotal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_WordTotal.AutoSize = true;
            this.label_WordTotal.Location = new System.Drawing.Point(41, 9);
            this.label_WordTotal.Margin = new System.Windows.Forms.Padding(0);
            this.label_WordTotal.Name = "label_WordTotal";
            this.label_WordTotal.Size = new System.Drawing.Size(35, 12);
            this.label_WordTotal.TabIndex = 76;
            this.label_WordTotal.Text = "/0000";
            this.toolTip.SetToolTip(this.label_WordTotal, "問題数");
            // 
            // label_Word
            // 
            this.label_Word.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Word.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Word.Location = new System.Drawing.Point(7, 5);
            this.label_Word.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Word.Name = "label_Word";
            this.label_Word.Size = new System.Drawing.Size(40, 16);
            this.label_Word.TabIndex = 75;
            this.label_Word.Text = "0000";
            this.label_Word.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Word, "現在の問題番号");
            // 
            // label_Sec
            // 
            this.label_Sec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Sec.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Sec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Sec.Location = new System.Drawing.Point(247, 5);
            this.label_Sec.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Sec.Name = "label_Sec";
            this.label_Sec.Size = new System.Drawing.Size(43, 16);
            this.label_Sec.TabIndex = 71;
            this.label_Sec.Text = "00.00";
            this.label_Sec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Sec, "経過時間");
            // 
            // label_Try
            // 
            this.label_Try.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Try.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Try.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Try.ForeColor = System.Drawing.Color.Red;
            this.label_Try.Location = new System.Drawing.Point(234, 30);
            this.label_Try.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Try.Name = "label_Try";
            this.label_Try.Size = new System.Drawing.Size(56, 16);
            this.label_Try.TabIndex = 65;
            this.label_Try.Text = "000000";
            this.label_Try.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Try, "最後まで打ち切った回数（クリックでリセット）");
            this.label_Try.Click += new System.EventHandler(this.label_Click);
            // 
            // label_KeyTotal
            // 
            this.label_KeyTotal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_KeyTotal.AutoSize = true;
            this.label_KeyTotal.Location = new System.Drawing.Point(362, 9);
            this.label_KeyTotal.Margin = new System.Windows.Forms.Padding(0);
            this.label_KeyTotal.Name = "label_KeyTotal";
            this.label_KeyTotal.Size = new System.Drawing.Size(47, 12);
            this.label_KeyTotal.TabIndex = 56;
            this.label_KeyTotal.Text = "/000000";
            this.toolTip.SetToolTip(this.label_KeyTotal, "総キー数");
            // 
            // label_Kpm
            // 
            this.label_Kpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Kpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Kpm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Kpm.Location = new System.Drawing.Point(88, 5);
            this.label_Kpm.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Kpm.Name = "label_Kpm";
            this.label_Kpm.Size = new System.Drawing.Size(40, 16);
            this.label_Kpm.TabIndex = 58;
            this.label_Kpm.Text = "0000";
            this.label_Kpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Kpm, "ここまでの打鍵速度");
            // 
            // label_Miss
            // 
            this.label_Miss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Miss.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Miss.Location = new System.Drawing.Point(7, 30);
            this.label_Miss.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Miss.Name = "label_Miss";
            this.label_Miss.Size = new System.Drawing.Size(40, 16);
            this.label_Miss.TabIndex = 57;
            this.label_Miss.Text = "0000";
            this.label_Miss.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Miss, "ここまでのミス数");
            // 
            // label_Key
            // 
            this.label_Key.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Key.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Key.Location = new System.Drawing.Point(312, 5);
            this.label_Key.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Key.Name = "label_Key";
            this.label_Key.Size = new System.Drawing.Size(56, 16);
            this.label_Key.TabIndex = 60;
            this.label_Key.Text = "000000";
            this.label_Key.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Key, "ここまでのキー数（ミスを除く）");
            // 
            // label_BestKpm
            // 
            this.label_BestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_BestKpm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_BestKpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BestKpm.ForeColor = System.Drawing.Color.Red;
            this.label_BestKpm.Location = new System.Drawing.Point(88, 30);
            this.label_BestKpm.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_BestKpm.Name = "label_BestKpm";
            this.label_BestKpm.Size = new System.Drawing.Size(40, 16);
            this.label_BestKpm.TabIndex = 62;
            this.label_BestKpm.Text = "0000";
            this.label_BestKpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_BestKpm, "打鍵速度の最高記録（クリックでリセット）");
            this.label_BestKpm.Click += new System.EventHandler(this.label_Click);
            // 
            // label_KeyAverage
            // 
            this.label_KeyAverage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_KeyAverage.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_KeyAverage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_KeyAverage.Location = new System.Drawing.Point(325, 30);
            this.label_KeyAverage.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_KeyAverage.Name = "label_KeyAverage";
            this.label_KeyAverage.Size = new System.Drawing.Size(43, 16);
            this.label_KeyAverage.TabIndex = 73;
            this.label_KeyAverage.Text = "000.0";
            this.label_KeyAverage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_KeyAverage, "1ワードあたりの平均キー数");
            // 
            // label_Min
            // 
            this.label_Min.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Min.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Min.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Min.Location = new System.Drawing.Point(168, 30);
            this.label_Min.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Min.Name = "label_Min";
            this.label_Min.Size = new System.Drawing.Size(40, 16);
            this.label_Min.TabIndex = 69;
            this.label_Min.Text = "0000";
            this.label_Min.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Min, "最も遅かったワードのkpm（最初の1ワードを除く）");
            // 
            // label_Max
            // 
            this.label_Max.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Max.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Max.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Max.Location = new System.Drawing.Point(168, 5);
            this.label_Max.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Max.Name = "label_Max";
            this.label_Max.Size = new System.Drawing.Size(40, 16);
            this.label_Max.TabIndex = 67;
            this.label_Max.Text = "0000";
            this.label_Max.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Max, "最も速かったワードのkpm");
            // 
            // pictureBox_Word
            // 
            this.pictureBox_Word.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Word.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Word.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Word.Name = "pictureBox_Word";
            this.pictureBox_Word.Size = new System.Drawing.Size(410, 50);
            this.pictureBox_Word.TabIndex = 17;
            this.pictureBox_Word.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_Word, "Space：開始\r\nEnter：中止、中止後であればクリア\r\n↑↓←→：移動\r\nShift+↑↓←→：星の数を増減");
            this.pictureBox_Word.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // timer_Waiting
            // 
            this.timer_Waiting.Interval = 1000;
            this.timer_Waiting.Tick += new System.EventHandler(this.timer_Waiting_Tick);
            // 
            // timer_Running
            // 
            this.timer_Running.Interval = 40;
            this.timer_Running.Tick += new System.EventHandler(this.timer_Running_Tick);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.pictureBox_Graph, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.pictureBox_Word, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(410, 275);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.label_WordTotal);
            this.panel.Controls.Add(this.label_Word);
            this.panel.Controls.Add(this.labelSec);
            this.panel.Controls.Add(this.label_Sec);
            this.panel.Controls.Add(this.labelTry);
            this.panel.Controls.Add(this.label_Try);
            this.panel.Controls.Add(this.labelKpm);
            this.panel.Controls.Add(this.labelMiss);
            this.panel.Controls.Add(this.label_KeyTotal);
            this.panel.Controls.Add(this.labelBestKpm);
            this.panel.Controls.Add(this.label_Kpm);
            this.panel.Controls.Add(this.label_Miss);
            this.panel.Controls.Add(this.label_Key);
            this.panel.Controls.Add(this.label_BestKpm);
            this.panel.Controls.Add(this.labelKeyAverage);
            this.panel.Controls.Add(this.label_KeyAverage);
            this.panel.Controls.Add(this.labelMin);
            this.panel.Controls.Add(this.label_Min);
            this.panel.Controls.Add(this.labelMax);
            this.panel.Controls.Add(this.label_Max);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 225);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(410, 50);
            this.panel.TabIndex = 14;
            // 
            // labelSec
            // 
            this.labelSec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSec.AutoSize = true;
            this.labelSec.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSec.Location = new System.Drawing.Point(285, 9);
            this.labelSec.Margin = new System.Windows.Forms.Padding(0);
            this.labelSec.Name = "labelSec";
            this.labelSec.Size = new System.Drawing.Size(23, 12);
            this.labelSec.TabIndex = 72;
            this.labelSec.Text = "sec";
            // 
            // labelTry
            // 
            this.labelTry.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTry.AutoSize = true;
            this.labelTry.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTry.Location = new System.Drawing.Point(285, 34);
            this.labelTry.Margin = new System.Windows.Forms.Padding(0);
            this.labelTry.Name = "labelTry";
            this.labelTry.Size = new System.Drawing.Size(19, 12);
            this.labelTry.TabIndex = 66;
            this.labelTry.Text = "try";
            // 
            // labelKpm
            // 
            this.labelKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelKpm.AutoSize = true;
            this.labelKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelKpm.Location = new System.Drawing.Point(124, 9);
            this.labelKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelKpm.Name = "labelKpm";
            this.labelKpm.Size = new System.Drawing.Size(26, 12);
            this.labelKpm.TabIndex = 59;
            this.labelKpm.Text = "kpm";
            // 
            // labelMiss
            // 
            this.labelMiss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMiss.AutoSize = true;
            this.labelMiss.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMiss.Location = new System.Drawing.Point(41, 34);
            this.labelMiss.Margin = new System.Windows.Forms.Padding(0);
            this.labelMiss.Name = "labelMiss";
            this.labelMiss.Size = new System.Drawing.Size(29, 12);
            this.labelMiss.TabIndex = 61;
            this.labelMiss.Text = "miss";
            // 
            // labelBestKpm
            // 
            this.labelBestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBestKpm.AutoSize = true;
            this.labelBestKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBestKpm.Location = new System.Drawing.Point(124, 34);
            this.labelBestKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelBestKpm.Name = "labelBestKpm";
            this.labelBestKpm.Size = new System.Drawing.Size(26, 12);
            this.labelBestKpm.TabIndex = 63;
            this.labelBestKpm.Text = "kpm";
            // 
            // labelKeyAverage
            // 
            this.labelKeyAverage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelKeyAverage.AutoSize = true;
            this.labelKeyAverage.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelKeyAverage.Location = new System.Drawing.Point(363, 34);
            this.labelKeyAverage.Margin = new System.Windows.Forms.Padding(0);
            this.labelKeyAverage.Name = "labelKeyAverage";
            this.labelKeyAverage.Size = new System.Drawing.Size(23, 12);
            this.labelKeyAverage.TabIndex = 74;
            this.labelKeyAverage.Text = "key";
            // 
            // labelMin
            // 
            this.labelMin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMin.AutoSize = true;
            this.labelMin.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMin.Location = new System.Drawing.Point(203, 34);
            this.labelMin.Margin = new System.Windows.Forms.Padding(0);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(23, 12);
            this.labelMin.TabIndex = 70;
            this.labelMin.Text = "min";
            // 
            // labelMax
            // 
            this.labelMax.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMax.AutoSize = true;
            this.labelMax.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMax.Location = new System.Drawing.Point(203, 9);
            this.labelMax.Margin = new System.Windows.Forms.Padding(0);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(26, 12);
            this.labelMax.TabIndex = 68;
            this.labelMax.Text = "max";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_Left);
            this.panel1.Controls.Add(this.pictureBox_Roman);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 24);
            this.panel1.TabIndex = 18;
            // 
            // pictureBox_Roman
            // 
            this.pictureBox_Roman.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Roman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Roman.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Roman.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Roman.Name = "pictureBox_Roman";
            this.pictureBox_Roman.Size = new System.Drawing.Size(404, 24);
            this.pictureBox_Roman.TabIndex = 16;
            this.pictureBox_Roman.TabStop = false;
            this.pictureBox_Roman.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // UserControl_3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Info);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UserControl_3";
            this.Size = new System.Drawing.Size(410, 275);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Graph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Word)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Roman)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label_Kpm;
        private System.Windows.Forms.Label labelKpm;
        private System.Windows.Forms.Label labelMiss;
        private System.Windows.Forms.Label label_Miss;
        private System.Windows.Forms.Label label_Key;
        private System.Windows.Forms.Label label_KeyTotal;
        private System.Windows.Forms.Timer timer_Waiting;
        private System.Windows.Forms.Label label_Left;
        private System.Windows.Forms.PictureBox pictureBox_Roman;
        private System.Windows.Forms.Label labelBestKpm;
        private System.Windows.Forms.Label label_BestKpm;
        private System.Windows.Forms.PictureBox pictureBox_Word;
        private System.Windows.Forms.Label label_Info;
        protected System.Windows.Forms.Timer timer_Running;
        private System.Windows.Forms.Label labelTry;
        private System.Windows.Forms.Label label_Try;
        private System.Windows.Forms.Label label_WordTotal;
        private System.Windows.Forms.Label label_Word;
        private System.Windows.Forms.Label labelKeyAverage;
        private System.Windows.Forms.Label label_KeyAverage;
        private System.Windows.Forms.Label labelSec;
        private System.Windows.Forms.Label label_Sec;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label label_Min;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Label label_Max;
        private System.Windows.Forms.PictureBox pictureBox_Graph;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel1;
    }
}
