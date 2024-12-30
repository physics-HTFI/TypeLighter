namespace TypeLighter.UserControls
{
    partial class UserControl_1
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
            this.pictureBox_Graph = new System.Windows.Forms.PictureBox();
            this.button_Random = new System.Windows.Forms.Button();
            this.label_NumWords = new System.Windows.Forms.Label();
            this.label_Miss = new System.Windows.Forms.Label();
            this.label_Kpm = new System.Windows.Forms.Label();
            this.label_BestKpm = new System.Windows.Forms.Label();
            this.label_Current = new System.Windows.Forms.Label();
            this.label_Left = new System.Windows.Forms.Label();
            this.label_Sec = new System.Windows.Forms.Label();
            this.label_Try = new System.Windows.Forms.Label();
            this.checkBox_Pinning = new System.Windows.Forms.CheckBox();
            this.label_Char = new System.Windows.Forms.Label();
            this.label_Word = new System.Windows.Forms.Label();
            this.timer_Running = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox_Roman = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.labelTry = new System.Windows.Forms.Label();
            this.labelMiss = new System.Windows.Forms.Label();
            this.labelKpm = new System.Windows.Forms.Label();
            this.labelBestKpm = new System.Windows.Forms.Label();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelMsec = new System.Windows.Forms.Label();
            this.labelChar = new System.Windows.Forms.Label();
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
            // pictureBox_Graph
            // 
            this.pictureBox_Graph.BackColor = System.Drawing.Color.White;
            this.pictureBox_Graph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Graph.Location = new System.Drawing.Point(0, 70);
            this.pictureBox_Graph.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Graph.Name = "pictureBox_Graph";
            this.pictureBox_Graph.Size = new System.Drawing.Size(410, 155);
            this.pictureBox_Graph.TabIndex = 12;
            this.pictureBox_Graph.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBox_Graph, "折れ線グラフ：kpmの推移\r\n棒グラフ：瞬間kpm\r\n黒線：目標kpm\r\n赤線：最高kpm");
            this.pictureBox_Graph.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // button_Random
            // 
            this.button_Random.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_Random.Location = new System.Drawing.Point(335, 24);
            this.button_Random.Name = "button_Random";
            this.button_Random.Size = new System.Drawing.Size(64, 23);
            this.button_Random.TabIndex = 42;
            this.button_Random.Text = "ランダム";
            this.toolTip.SetToolTip(this.button_Random, "ランダムに移動します (Alt+Enter)");
            this.button_Random.UseVisualStyleBackColor = true;
            this.button_Random.Click += new System.EventHandler(this.button_Random_Click);
            // 
            // label_NumWords
            // 
            this.label_NumWords.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_NumWords.Location = new System.Drawing.Point(44, 9);
            this.label_NumWords.Margin = new System.Windows.Forms.Padding(0);
            this.label_NumWords.Name = "label_NumWords";
            this.label_NumWords.Size = new System.Drawing.Size(35, 12);
            this.label_NumWords.TabIndex = 37;
            this.label_NumWords.Text = "/0000";
            this.toolTip.SetToolTip(this.label_NumWords, "対象となる総ワード数");
            // 
            // label_Miss
            // 
            this.label_Miss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Miss.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Miss.Location = new System.Drawing.Point(265, 5);
            this.label_Miss.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_Miss.Name = "label_Miss";
            this.label_Miss.Size = new System.Drawing.Size(32, 16);
            this.label_Miss.TabIndex = 26;
            this.label_Miss.Text = "000";
            this.label_Miss.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Miss, "ミス数");
            // 
            // label_Kpm
            // 
            this.label_Kpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Kpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Kpm.Location = new System.Drawing.Point(99, 5);
            this.label_Kpm.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_Kpm.Name = "label_Kpm";
            this.label_Kpm.Size = new System.Drawing.Size(40, 16);
            this.label_Kpm.TabIndex = 29;
            this.label_Kpm.Text = "0000";
            this.label_Kpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Kpm, "平均打鍵速度\r\n＝1分あたりの打鍵数\r\n＝(キー数-1) / (合計時間[秒]) x 60\r\n");
            // 
            // label_BestKpm
            // 
            this.label_BestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_BestKpm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_BestKpm.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BestKpm.ForeColor = System.Drawing.Color.Red;
            this.label_BestKpm.Location = new System.Drawing.Point(99, 31);
            this.label_BestKpm.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_BestKpm.Name = "label_BestKpm";
            this.label_BestKpm.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_BestKpm.Size = new System.Drawing.Size(40, 16);
            this.label_BestKpm.TabIndex = 32;
            this.label_BestKpm.Text = "0000";
            this.label_BestKpm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_BestKpm, "打鍵速度の最高記録（クリックでリセット）");
            this.label_BestKpm.Click += new System.EventHandler(this.label_Click);
            // 
            // label_Current
            // 
            this.label_Current.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Current.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Current.ForeColor = System.Drawing.Color.Black;
            this.label_Current.Location = new System.Drawing.Point(9, 5);
            this.label_Current.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label_Current.Name = "label_Current";
            this.label_Current.Size = new System.Drawing.Size(40, 16);
            this.label_Current.TabIndex = 40;
            this.label_Current.Text = "0000";
            this.label_Current.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Current, "現在のワードの番号");
            // 
            // label_Left
            // 
            this.label_Left.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Left.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Left.Location = new System.Drawing.Point(9, 31);
            this.label_Left.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.label_Left.Name = "label_Left";
            this.label_Left.Size = new System.Drawing.Size(40, 16);
            this.label_Left.TabIndex = 36;
            this.label_Left.Text = "0000";
            this.label_Left.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Left, "未通過のワード数");
            // 
            // label_Sec
            // 
            this.label_Sec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Sec.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Sec.Location = new System.Drawing.Point(180, 5);
            this.label_Sec.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_Sec.Name = "label_Sec";
            this.label_Sec.Size = new System.Drawing.Size(35, 16);
            this.label_Sec.TabIndex = 33;
            this.label_Sec.Text = "0.00";
            this.label_Sec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Sec, "時間");
            // 
            // label_Try
            // 
            this.label_Try.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Try.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_Try.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Try.ForeColor = System.Drawing.Color.Red;
            this.label_Try.Location = new System.Drawing.Point(257, 31);
            this.label_Try.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_Try.Name = "label_Try";
            this.label_Try.Size = new System.Drawing.Size(40, 16);
            this.label_Try.TabIndex = 38;
            this.label_Try.Text = "0000";
            this.label_Try.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Try, "このワードを最後まで打ち切った回数（クリックでリセット）");
            this.label_Try.Click += new System.EventHandler(this.label_Click);
            // 
            // checkBox_Pinning
            // 
            this.checkBox_Pinning.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkBox_Pinning.AutoSize = true;
            this.checkBox_Pinning.Location = new System.Drawing.Point(339, 5);
            this.checkBox_Pinning.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.checkBox_Pinning.Name = "checkBox_Pinning";
            this.checkBox_Pinning.Size = new System.Drawing.Size(64, 16);
            this.checkBox_Pinning.TabIndex = 41;
            this.checkBox_Pinning.Text = "ピン止め";
            this.toolTip.SetToolTip(this.checkBox_Pinning, "クリアしても次のワードに移動しなくなります (Shift+Enter)");
            this.checkBox_Pinning.UseVisualStyleBackColor = true;
            this.checkBox_Pinning.CheckedChanged += new System.EventHandler(this.checkBox_Pinning_CheckedChanged);
            // 
            // label_Char
            // 
            this.label_Char.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label_Char.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Char.Location = new System.Drawing.Point(183, 31);
            this.label_Char.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label_Char.Name = "label_Char";
            this.label_Char.Size = new System.Drawing.Size(32, 16);
            this.label_Char.TabIndex = 34;
            this.label_Char.Text = "000";
            this.label_Char.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip.SetToolTip(this.label_Char, "ローマ字の字数");
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
            this.label_Word.Padding = new System.Windows.Forms.Padding(5, 0, 0, 5);
            this.label_Word.Size = new System.Drawing.Size(410, 40);
            this.label_Word.TabIndex = 1;
            this.label_Word.Text = "Enter(中止)、矢印(移動)";
            this.label_Word.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip.SetToolTip(this.label_Word, "Enter：中止、中止後であればクリア\r\n↑↓←→：移動\r\nShift+↑↓←→：星の数を増減");
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
            this.tableLayoutPanel.Controls.Add(this.label_Word, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.pictureBox_Roman, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.pictureBox_Graph, 0, 2);
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
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // pictureBox_Roman
            // 
            this.pictureBox_Roman.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox_Roman.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox_Roman.Location = new System.Drawing.Point(0, 40);
            this.pictureBox_Roman.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_Roman.Name = "pictureBox_Roman";
            this.pictureBox_Roman.Size = new System.Drawing.Size(410, 30);
            this.pictureBox_Roman.TabIndex = 11;
            this.pictureBox_Roman.TabStop = false;
            this.pictureBox_Roman.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.button_Random);
            this.panel.Controls.Add(this.labelTry);
            this.panel.Controls.Add(this.labelMiss);
            this.panel.Controls.Add(this.label_NumWords);
            this.panel.Controls.Add(this.labelKpm);
            this.panel.Controls.Add(this.labelBestKpm);
            this.panel.Controls.Add(this.labelLeft);
            this.panel.Controls.Add(this.labelMsec);
            this.panel.Controls.Add(this.label_Miss);
            this.panel.Controls.Add(this.label_Kpm);
            this.panel.Controls.Add(this.label_BestKpm);
            this.panel.Controls.Add(this.label_Current);
            this.panel.Controls.Add(this.label_Left);
            this.panel.Controls.Add(this.label_Sec);
            this.panel.Controls.Add(this.label_Try);
            this.panel.Controls.Add(this.checkBox_Pinning);
            this.panel.Controls.Add(this.labelChar);
            this.panel.Controls.Add(this.label_Char);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 225);
            this.panel.Margin = new System.Windows.Forms.Padding(0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(410, 50);
            this.panel.TabIndex = 13;
            // 
            // labelTry
            // 
            this.labelTry.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTry.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTry.Location = new System.Drawing.Point(292, 35);
            this.labelTry.Margin = new System.Windows.Forms.Padding(0);
            this.labelTry.Name = "labelTry";
            this.labelTry.Size = new System.Drawing.Size(21, 12);
            this.labelTry.TabIndex = 35;
            this.labelTry.Text = "try";
            // 
            // labelMiss
            // 
            this.labelMiss.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMiss.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMiss.Location = new System.Drawing.Point(292, 9);
            this.labelMiss.Margin = new System.Windows.Forms.Padding(0);
            this.labelMiss.Name = "labelMiss";
            this.labelMiss.Size = new System.Drawing.Size(29, 12);
            this.labelMiss.TabIndex = 28;
            this.labelMiss.Text = "miss";
            // 
            // labelKpm
            // 
            this.labelKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelKpm.Location = new System.Drawing.Point(134, 9);
            this.labelKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelKpm.Name = "labelKpm";
            this.labelKpm.Size = new System.Drawing.Size(26, 12);
            this.labelKpm.TabIndex = 30;
            this.labelKpm.Text = "kpm";
            // 
            // labelBestKpm
            // 
            this.labelBestKpm.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBestKpm.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBestKpm.Location = new System.Drawing.Point(134, 35);
            this.labelBestKpm.Margin = new System.Windows.Forms.Padding(0);
            this.labelBestKpm.Name = "labelBestKpm";
            this.labelBestKpm.Size = new System.Drawing.Size(26, 12);
            this.labelBestKpm.TabIndex = 31;
            this.labelBestKpm.Text = "kpm";
            // 
            // labelLeft
            // 
            this.labelLeft.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelLeft.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelLeft.Location = new System.Drawing.Point(44, 35);
            this.labelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(22, 12);
            this.labelLeft.TabIndex = 39;
            this.labelLeft.Text = "left";
            // 
            // labelMsec
            // 
            this.labelMsec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMsec.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMsec.Location = new System.Drawing.Point(210, 9);
            this.labelMsec.Margin = new System.Windows.Forms.Padding(0);
            this.labelMsec.Name = "labelMsec";
            this.labelMsec.Size = new System.Drawing.Size(23, 12);
            this.labelMsec.TabIndex = 27;
            this.labelMsec.Text = "sec";
            // 
            // labelChar
            // 
            this.labelChar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelChar.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelChar.Location = new System.Drawing.Point(210, 35);
            this.labelChar.Margin = new System.Windows.Forms.Padding(0);
            this.labelChar.Name = "labelChar";
            this.labelChar.Size = new System.Drawing.Size(27, 12);
            this.labelChar.TabIndex = 25;
            this.labelChar.Text = "key";
            // 
            // UserControl_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "UserControl_1";
            this.Size = new System.Drawing.Size(410, 275);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Graph)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Roman)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label label_Word;
        private System.Windows.Forms.PictureBox pictureBox_Roman;
        private System.Windows.Forms.PictureBox pictureBox_Graph;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label_Char;
        private System.Windows.Forms.Label labelTry;
        private System.Windows.Forms.Label label_Miss;
        private System.Windows.Forms.Label labelMiss;
        private System.Windows.Forms.Label label_NumWords;
        private System.Windows.Forms.Label labelKpm;
        private System.Windows.Forms.Label label_Kpm;
        private System.Windows.Forms.Label labelBestKpm;
        private System.Windows.Forms.Label label_BestKpm;
        private System.Windows.Forms.Label label_Current;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label label_Left;
        private System.Windows.Forms.Label label_Sec;
        private System.Windows.Forms.Label labelMsec;
        private System.Windows.Forms.Label label_Try;
        private System.Windows.Forms.Label labelChar;
        private System.Windows.Forms.CheckBox checkBox_Pinning;
        protected System.Windows.Forms.Timer timer_Running;
        private System.Windows.Forms.Button button_Random;
        public System.Windows.Forms.ToolTip toolTip;
    }
}
