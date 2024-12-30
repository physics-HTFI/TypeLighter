namespace TypeLighter.UserControls
{
    partial class UserControl_2
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
            this.label_Left = new System.Windows.Forms.Label();
            this.label_Word = new System.Windows.Forms.Label();
            this.label_Try = new System.Windows.Forms.Label();
            this.label_KeyTotal = new System.Windows.Forms.Label();
            this.label_Key = new System.Windows.Forms.Label();
            this.label_NumWords = new System.Windows.Forms.Label();
            this.label_Current = new System.Windows.Forms.Label();
            this.label_SecInit = new System.Windows.Forms.Label();
            this.label_Kpm = new System.Windows.Forms.Label();
            this.label_Miss = new System.Windows.Forms.Label();
            this.label_KpmLatter = new System.Windows.Forms.Label();
            this.label_KeyAverage = new System.Windows.Forms.Label();
            this.label_BestKpm = new System.Windows.Forms.Label();
            this.label_BestSec = new System.Windows.Forms.Label();
            this.pictureBox_Graph = new System.Windows.Forms.PictureBox();
            this.labelTry = new System.Windows.Forms.Label();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelKpm = new System.Windows.Forms.Label();
            this.labelChar = new System.Windows.Forms.Label();
            this.labelMiss = new System.Windows.Forms.Label();
            this.labelKpmLatter = new System.Windows.Forms.Label();
            this.labelBestKpm = new System.Windows.Forms.Label();
            this.labelBestSec = new System.Windows.Forms.Label();
            this.timer_Waiting = new System.Windows.Forms.Timer(this.components);
            this.timer_Running = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_Roman = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Graph)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Roman)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 100;
            // 
            // label_Left
            // 
            this.label_Left.AutoSize = true;
            this.label_Left.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label_Left.Location = new System.Drawing.Point(1, 51);
            this.label_Left.Name = "label_Left";
            this.label_Left.Size = new System.Drawing.Size(29, 12);
            this.label_Left.TabIndex = 2;
            this.label_Left.Text = "9999";
            this.toolTip.SetToolTip(this.label_Left, "残りの問題数");
            // 
            // label_Word
            // 
            this.label_Word.AutoSize = true;
            this.label_Word.BackColor = System.Drawing.SystemColors.Control;
            this.label_Word.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Word.Font = new System.Drawing.Font("ＭＳ ゴシック", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Word.Location = new System.Drawing.Point(0, 0);
            this.label_Word.Margin = new System.Windows.Forms.Padding(0);
            this.label_Word.Name = "label_Word";
            this.label_Word.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label_Word.Size = new System.Drawing.Size(410, 40);
            this.label_Word.TabIndex = 69;
            this.label_Word.Text = "Space(開始)、Enter(中止)、矢印(結果閲覧)";
            this.label_Word.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip.SetToolTip(this.label_Word, "Space：開始\r\nEnter：中止、中止後であればクリア\r\n↑↓←→：移動\r\nShift+↑↓←→：星の数を増減");
            // 
            // label_Try
            // 
            this.label_Try.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Try.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Try.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Try.ForeColor = System.Drawing.Color.Red;
            this.label_Try.Location = new System.Drawing.Point(229, 30);
            this.label_Try.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Try.Name = "label_Try";
            this.label_Try.Size = new System.Drawing.Size(56, 16);
            this.label_Try.TabIndex = 80;
            this.label_Try.Text = "000000";
            this.label_Try.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Try, "最後まで打ち切った回数（クリックでリセット）");
            this.label_Try.Click += new System.EventHandler(this.label_Click);
            // 
            // label_KeyTotal
            // 
            this.label_KeyTotal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_KeyTotal.AutoSize = true;
            this.label_KeyTotal.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_KeyTotal.Location = new System.Drawing.Point(361, 9);
            this.label_KeyTotal.Margin = new System.Windows.Forms.Padding(0);
            this.label_KeyTotal.Name = "label_KeyTotal";
            this.label_KeyTotal.Size = new System.Drawing.Size(47, 12);
            this.label_KeyTotal.TabIndex = 78;
            this.label_KeyTotal.Text = "/000000";
            this.toolTip.SetToolTip(this.label_KeyTotal, "総キー数");
            // 
            // label_Key
            // 
            this.label_Key.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Key.AutoEllipsis = true;
            this.label_Key.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Key.Location = new System.Drawing.Point(310, 5);
            this.label_Key.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_Key.Name = "label_Key";
            this.label_Key.Size = new System.Drawing.Size(56, 16);
            this.label_Key.TabIndex = 79;
            this.label_Key.Text = "000000";
            this.label_Key.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Key, "ここまでのキー数（ミスを除く）");
            // 
            // label_NumWords
            // 
            this.label_NumWords.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NumWords.AutoSize = true;
            this.label_NumWords.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_NumWords.Location = new System.Drawing.Point(39, 9);
            this.label_NumWords.Margin = new System.Windows.Forms.Padding(0);
            this.label_NumWords.Name = "label_NumWords";
            this.label_NumWords.Size = new System.Drawing.Size(35, 12);
            this.label_NumWords.TabIndex = 63;
            this.label_NumWords.Text = "/0000";
            this.toolTip.SetToolTip(this.label_NumWords, "問題数");
            // 
            // label_Current
            // 
            this.label_Current.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Current.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Current.ForeColor = System.Drawing.Color.Black;
            this.label_Current.Location = new System.Drawing.Point(4, 5);
            this.label_Current.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label_Current.Name = "label_Current";
            this.label_Current.Size = new System.Drawing.Size(40, 16);
            this.label_Current.TabIndex = 64;
            this.label_Current.Text = "0000";
            this.label_Current.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Current, "現在の問題番号");
            // 
            // label_SecInit
            // 
            this.label_SecInit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_SecInit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_SecInit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_SecInit.Location = new System.Drawing.Point(171, 5);
            this.label_SecInit.Margin = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.label_SecInit.Name = "label_SecInit";
            this.label_SecInit.Size = new System.Drawing.Size(35, 16);
            this.label_SecInit.TabIndex = 61;
            this.label_SecInit.Text = "0.00";
            this.label_SecInit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_SecInit, "1ワードあたりの平均初速(＝1文字目を打つまでの時間)");
            // 
            // label_Kpm
            // 
            this.label_Kpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Kpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Kpm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_Kpm.Location = new System.Drawing.Point(89, 5);
            this.label_Kpm.Margin = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.label_Kpm.Name = "label_Kpm";
            this.label_Kpm.Size = new System.Drawing.Size(40, 16);
            this.label_Kpm.TabIndex = 59;
            this.label_Kpm.Text = "0000";
            this.label_Kpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Kpm, "平均打鍵速度\r\n＝1分あたりの打鍵数\r\n＝(合計キー数) / (合計時間[秒]) x 60");
            // 
            // label_Miss
            // 
            this.label_Miss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Miss.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Miss.Location = new System.Drawing.Point(4, 30);
            this.label_Miss.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label_Miss.Name = "label_Miss";
            this.label_Miss.Size = new System.Drawing.Size(40, 16);
            this.label_Miss.TabIndex = 57;
            this.label_Miss.Text = "0000";
            this.label_Miss.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Miss, "ミスの総数");
            // 
            // label_KpmLatter
            // 
            this.label_KpmLatter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_KpmLatter.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_KpmLatter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_KpmLatter.Location = new System.Drawing.Point(245, 5);
            this.label_KpmLatter.Margin = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.label_KpmLatter.Name = "label_KpmLatter";
            this.label_KpmLatter.Size = new System.Drawing.Size(40, 16);
            this.label_KpmLatter.TabIndex = 67;
            this.label_KpmLatter.Text = "0000";
            this.label_KpmLatter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_KpmLatter, "初速を除いた平均打鍵速度");
            // 
            // label_KeyAverage
            // 
            this.label_KeyAverage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_KeyAverage.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_KeyAverage.Location = new System.Drawing.Point(323, 30);
            this.label_KeyAverage.Margin = new System.Windows.Forms.Padding(23, 0, 0, 0);
            this.label_KeyAverage.Name = "label_KeyAverage";
            this.label_KeyAverage.Size = new System.Drawing.Size(43, 16);
            this.label_KeyAverage.TabIndex = 62;
            this.label_KeyAverage.Text = "000.0";
            this.label_KeyAverage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_KeyAverage, "1ワードあたりの平均キー数");
            // 
            // label_BestKpm
            // 
            this.label_BestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_BestKpm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_BestKpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BestKpm.ForeColor = System.Drawing.Color.Red;
            this.label_BestKpm.Location = new System.Drawing.Point(89, 30);
            this.label_BestKpm.Margin = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.label_BestKpm.Name = "label_BestKpm";
            this.label_BestKpm.Size = new System.Drawing.Size(40, 16);
            this.label_BestKpm.TabIndex = 68;
            this.label_BestKpm.Text = "0000";
            this.label_BestKpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_BestKpm, "平均打鍵速度の最高記録（クリックでリセット）");
            this.label_BestKpm.Click += new System.EventHandler(this.label_Click);
            // 
            // label_BestSec
            // 
            this.label_BestSec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_BestSec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_BestSec.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BestSec.ForeColor = System.Drawing.Color.Red;
            this.label_BestSec.Location = new System.Drawing.Point(171, 30);
            this.label_BestSec.Margin = new System.Windows.Forms.Padding(21, 0, 0, 0);
            this.label_BestSec.Name = "label_BestSec";
            this.label_BestSec.Size = new System.Drawing.Size(35, 16);
            this.label_BestSec.TabIndex = 70;
            this.label_BestSec.Text = "0.00";
            this.label_BestSec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_BestSec, "平均初速の最速記録（クリックでリセット）");
            this.label_BestSec.Click += new System.EventHandler(this.label_Click);
            // 
            // pictureBox_Graph
            // 
            this.pictureBox_Graph.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox_Graph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Graph.Location = new System.Drawing.Point(0, 70);
            this.pictureBox_Graph.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Graph.Name = "pictureBox_Graph";
            this.pictureBox_Graph.Size = new System.Drawing.Size(410, 155);
            this.pictureBox_Graph.TabIndex = 70;
            this.pictureBox_Graph.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_Graph, "折れ線グラフ：kpmの推移\r\n棒グラフ：瞬間kpm\r\n赤線：最高kpm\r\n");
            this.pictureBox_Graph.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // labelTry
            // 
            this.labelTry.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTry.AutoSize = true;
            this.labelTry.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTry.Location = new System.Drawing.Point(279, 34);
            this.labelTry.Margin = new System.Windows.Forms.Padding(0);
            this.labelTry.Name = "labelTry";
            this.labelTry.Size = new System.Drawing.Size(19, 12);
            this.labelTry.TabIndex = 81;
            this.labelTry.Text = "try";
            // 
            // labelStartTime
            // 
            this.labelStartTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStartTime.AutoSize = true;
            this.labelStartTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelStartTime.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelStartTime.Location = new System.Drawing.Point(201, 9);
            this.labelStartTime.Margin = new System.Windows.Forms.Padding(0);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(23, 12);
            this.labelStartTime.TabIndex = 58;
            this.labelStartTime.Text = "sec";
            // 
            // labelKpm
            // 
            this.labelKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelKpm.AutoSize = true;
            this.labelKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelKpm.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelKpm.Location = new System.Drawing.Point(124, 9);
            this.labelKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelKpm.Name = "labelKpm";
            this.labelKpm.Size = new System.Drawing.Size(26, 12);
            this.labelKpm.TabIndex = 60;
            this.labelKpm.Text = "kpm";
            // 
            // labelChar
            // 
            this.labelChar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelChar.AutoSize = true;
            this.labelChar.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelChar.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelChar.Location = new System.Drawing.Point(361, 34);
            this.labelChar.Margin = new System.Windows.Forms.Padding(0);
            this.labelChar.Name = "labelChar";
            this.labelChar.Size = new System.Drawing.Size(23, 12);
            this.labelChar.TabIndex = 56;
            this.labelChar.Text = "key";
            // 
            // labelMiss
            // 
            this.labelMiss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMiss.AutoSize = true;
            this.labelMiss.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMiss.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelMiss.Location = new System.Drawing.Point(39, 34);
            this.labelMiss.Margin = new System.Windows.Forms.Padding(0);
            this.labelMiss.Name = "labelMiss";
            this.labelMiss.Size = new System.Drawing.Size(29, 12);
            this.labelMiss.TabIndex = 65;
            this.labelMiss.Text = "miss";
            // 
            // labelKpmLatter
            // 
            this.labelKpmLatter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelKpmLatter.AutoSize = true;
            this.labelKpmLatter.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelKpmLatter.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelKpmLatter.Location = new System.Drawing.Point(280, 9);
            this.labelKpmLatter.Margin = new System.Windows.Forms.Padding(0);
            this.labelKpmLatter.Name = "labelKpmLatter";
            this.labelKpmLatter.Size = new System.Drawing.Size(28, 12);
            this.labelKpmLatter.TabIndex = 66;
            this.labelKpmLatter.Text = "kpm\'";
            // 
            // labelBestKpm
            // 
            this.labelBestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBestKpm.AutoSize = true;
            this.labelBestKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBestKpm.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelBestKpm.Location = new System.Drawing.Point(124, 34);
            this.labelBestKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelBestKpm.Name = "labelBestKpm";
            this.labelBestKpm.Size = new System.Drawing.Size(26, 12);
            this.labelBestKpm.TabIndex = 69;
            this.labelBestKpm.Text = "kpm";
            // 
            // labelBestSec
            // 
            this.labelBestSec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBestSec.AutoSize = true;
            this.labelBestSec.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBestSec.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelBestSec.Location = new System.Drawing.Point(201, 34);
            this.labelBestSec.Margin = new System.Windows.Forms.Padding(0);
            this.labelBestSec.Name = "labelBestSec";
            this.labelBestSec.Size = new System.Drawing.Size(23, 12);
            this.labelBestSec.TabIndex = 71;
            this.labelBestSec.Text = "sec";
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
            this.tableLayoutPanel.Controls.Add(this.label_Word, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.pictureBox_Roman, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(410, 275);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // pictureBox_Roman
            // 
            this.pictureBox_Roman.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Roman.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Roman.Location = new System.Drawing.Point(0, 40);
            this.pictureBox_Roman.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Roman.Name = "pictureBox_Roman";
            this.pictureBox_Roman.Size = new System.Drawing.Size(410, 30);
            this.pictureBox_Roman.TabIndex = 68;
            this.pictureBox_Roman.TabStop = false;
            this.pictureBox_Roman.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.labelBestKpm);
            this.panel.Controls.Add(this.label_KeyTotal);
            this.panel.Controls.Add(this.label_Key);
            this.panel.Controls.Add(this.label_NumWords);
            this.panel.Controls.Add(this.labelKpm);
            this.panel.Controls.Add(this.labelChar);
            this.panel.Controls.Add(this.labelMiss);
            this.panel.Controls.Add(this.labelKpmLatter);
            this.panel.Controls.Add(this.label_Current);
            this.panel.Controls.Add(this.label_Kpm);
            this.panel.Controls.Add(this.label_Miss);
            this.panel.Controls.Add(this.label_KpmLatter);
            this.panel.Controls.Add(this.label_KeyAverage);
            this.panel.Controls.Add(this.label_BestKpm);
            this.panel.Controls.Add(this.labelTry);
            this.panel.Controls.Add(this.label_Try);
            this.panel.Controls.Add(this.labelStartTime);
            this.panel.Controls.Add(this.labelBestSec);
            this.panel.Controls.Add(this.label_SecInit);
            this.panel.Controls.Add(this.label_BestSec);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 225);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(410, 50);
            this.panel.TabIndex = 71;
            // 
            // UserControl_2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Left);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UserControl_2";
            this.Size = new System.Drawing.Size(410, 275);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Graph)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Roman)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox_Roman;
        private System.Windows.Forms.Label label_Word;
        private System.Windows.Forms.PictureBox pictureBox_Graph;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label_NumWords;
        private System.Windows.Forms.Label label_Current;
        private System.Windows.Forms.Label labelChar;
        private System.Windows.Forms.Label label_KeyAverage;
        private System.Windows.Forms.Label label_SecInit;
        private System.Windows.Forms.Label label_Kpm;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Label labelKpm;
        private System.Windows.Forms.Label labelMiss;
        private System.Windows.Forms.Label label_Miss;
        private System.Windows.Forms.Label labelKpmLatter;
        private System.Windows.Forms.Label label_KpmLatter;
        private System.Windows.Forms.Label label_BestKpm;
        private System.Windows.Forms.Label labelBestKpm;
        private System.Windows.Forms.Label label_BestSec;
        private System.Windows.Forms.Label labelBestSec;
        private System.Windows.Forms.Timer timer_Waiting;
        private System.Windows.Forms.Label label_Left;
        protected System.Windows.Forms.Timer timer_Running;
        private System.Windows.Forms.Label label_KeyTotal;
        private System.Windows.Forms.Label label_Key;
        private System.Windows.Forms.Label labelTry;
        private System.Windows.Forms.Label label_Try;
        public System.Windows.Forms.ToolTip toolTip;
    }
}
