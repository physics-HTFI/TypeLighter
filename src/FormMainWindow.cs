using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TypeLighter.UserControls;
using System.Runtime.InteropServices;

namespace TypeLighter
{
    public partial class FormMainWindow : Form
    {
        private Words    _words    = new Words();
        private String   _fileName = null;
        private int      _keysThis_1 = 0, _keysThis_2 = 0, _keysThis_3 = 0;
        private DateTime _timeThis = new DateTime(1,1,1,0,0,0);

        /// <summary>コンストラクタ</summary>
        public FormMainWindow(String fileName = null) {
            InitializeComponent();
            // ユーザコントロール
            UserControl_Base.settings = _settings = Settings.load();
            userControl_1.dataGridView = dataGridView_1;
            userControl_2.dataGridView = dataGridView_2;
            userControl_3.dataGridView = dataGridView_3;
            userControl_2.dataGridView_Rank = dataGridView_2_Rank;
            userControl_3.dataGridView_Rank = dataGridView_3_Rank;
            UserControl_Base.delegate_ExpandWidth = w => { Width += -tabControl_Main.SelectedTab.Width + Math.Max(tabControl_Main.SelectedTab.Width, w); };
            // フォーム
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            MinimumSize = Size = Size - new Size(tabPage0.Width, tabPage0.Height + panel.Height) + new Size(410, 475); 
            _minHeight = Height;
            saveFileDialog.InitialDirectory =　Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ワード");
            imageList.Images.Add(Properties.Resources.folder);
            imageList.Images.Add(Icon.ExtractAssociatedIcon(Application.ExecutablePath));
            updateTreeView();
            setParametersToGUI();
            isInit = false;
            open(fileName);
        }

///// ファイル ///////////////////////

        /// <summary>fileNameを開く（nullであれば初期状態に戻る）。（起動、破棄、開くボタンクリック時のみ呼ばれる）</summary>
        private void open(String fileName) {
            if (fileName != null) { save(); _settings.lastOpenedFile = fileName; }
            _fileName = fileName;
            _words = UserControl_Base.words = Words.getWordsFromFile(ref _fileName);
            Text = "TypeLighter: " + (_fileName == null ? "ファイルなし" : Path.GetFileName(_fileName));
            userControl_1.reset(); userControl_2.reset(); userControl_3.reset();
            updateFileTab();
            if (_fileName != null) {
                tabControl_Main.SelectedIndex = 1;
                _words.wordList.ForEach(w => w.width = UserControl_3.getWidth(w));
            }
            userControl_1.updateWords();
        }

        /// <summary>Settings.xmlと_fileNameに保存する。（open、終了、保存ボタン時にのみ呼ばれる）</summary>
        private void save() { _settings.save(); _words.save(_fileName); }

        /// <summary>終了時にファイル･設定を保存</summary>
        private void FormMainWindow_FormClosing(object sender, FormClosingEventArgs e) { save(); }

///// クリックイベント //////////////////

        /// <summary>ボタンクリック</summary>
        private void button_Click(object sender, EventArgs e) {
            if (sender == button_Save) {
                if (_fileName == null) { if (saveFileDialog.ShowDialog() == DialogResult.OK) { _fileName = saveFileDialog.FileName; save(); open(_fileName); } }
                else { save(); MessageBox.Show("保存しました"); }
            }
            if (sender == button_Last) { if(_settings.lastOpenedFile != "") {open(_settings.lastOpenedFile);} }
            if (sender == button_Discard) { open(null); }
            if (sender == button_Exit   ) { Close(); }
            if (sender == button_Manage) {
                UserControl_Base uc = tabControl_Main.SelectedTab.Controls[0] as UserControl_Base;
                if (uc != null) { uc.Stop(); }
                using (FormWordManager view = new FormWordManager(_words.wordList, _settings.showsToolTip, uc!=null && uc.result.Any() ? uc.result[uc.position].word : null)) {
                    view.ShowDialog();
                    if (view._isOK) { _words.wordList = view._words; userControl_1.updateWords(); }
                }
            }
            updateFileTab();
        }

        /// <summary>ラベルをクリックしたときに値をリセットする</summary>
        private void label_Click(object sender, EventArgs e) {
            if (sender == label_TimeThis) { _timeThis              = new DateTime(1,1,1,0,0,0); }
            if (sender == label_TimeFile) { _words.statistics.time = new DateTime(1,1,1,0,0,0); }
            if (sender == label_KeysThis_1) { _keysThis_1             = 0; }
            if (sender == label_KeysThis_2) { _keysThis_2             = 0; }
            if (sender == label_KeysThis_3) { _keysThis_3             = 0; }
            if (sender == label_KeysFile_1) { _words.statistics.key_1 = 0; }
            if (sender == label_KeysFile_2) { _words.statistics.key_2 = 0; }
            if (sender == label_KeysFile_3) { _words.statistics.key_3 = 0; }
            updateFileTab();
        }


///// タブ ////////////////////////////////

        /// <summary>タブが変更されたときの処理</summary>
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e) {
            getSettings();
            userControl_1.Stop(); userControl_2.Stop(); userControl_3.Stop();
            if (tabControl_Main.SelectedIndex == 0) { tabControl_0.BringToFront(); updateFileTab(); }
            if (tabControl_Main.SelectedIndex == 1) { tabControl_1.BringToFront(); }
            if (tabControl_Main.SelectedIndex == 2) { tabControl_2.BringToFront(); userControl_2.makeRankingGrid(); }
            if (tabControl_Main.SelectedIndex == 3) { tabControl_3.BringToFront(); userControl_3.makeRankingGrid(); userControl_3.updatePanelHeight(); }
        }

        /// <summary>ファイルタブを書き直す。（ボタン/ラベルクリック、ファイルタブ表示、open時のみ呼ばれる）</summary>
        private void updateFileTab() {
            updateTime();
            label_KeysThis_1.Text = _keysThis_1 + ""; label_KeysFile_1.Text = _words.statistics.key_1 + "";
            label_KeysThis_2.Text = _keysThis_2 + ""; label_KeysFile_2.Text = _words.statistics.key_2 + "";
            label_KeysThis_3.Text = _keysThis_3 + ""; label_KeysFile_3.Text = _words.statistics.key_3 + "";
            label_Star0.Text = _words.wordList.Count(w => w.star == 0) + "";
            label_Star1.Text = _words.wordList.Count(w => w.star == 1) + "";
            label_Star2.Text = _words.wordList.Count(w => w.star == 2) + "";
            label_Star3.Text = _words.wordList.Count(w => w.star == 3) + "";
            label_Words.Text = _words.wordList.Count + "";
            // グラフ
            if (_words.wordList.Any()) { chart.ChartAreas[0].AxisY.Maximum = chart.ChartAreas[0].AxisY.MajorGrid.Interval = double.NaN; }
            else { chart.ChartAreas[0].AxisY.Maximum = 10; chart.ChartAreas[0].AxisY.MajorGrid.Interval = 2; }
            int maxKpm = !_words.wordList.Any() ? 0 : _words.wordList.Max(w => Math.Max(Math.Max(w.bestKpm_1, w.bestKpm_2), w.bestKpm_3));
            for (int mode = 0; mode < 3; mode++) {
                int[] bin = new int[maxKpm < 500 ? 5 : maxKpm < 1000 ? 10 : maxKpm < 1500 ? 15 : 20];
                _words.wordList.ForEach(w => ++bin[Math.Min(bin.Count() - 1, (mode == 0 ? w.bestKpm_2 : mode == 1 ? w.bestKpm_3 : w.bestKpm_1) / 100)]);
                chart.Series[mode].Points.Clear();
                for (int i = 0; i < bin.Count(); i++) {
                    chart.Series[mode].Points.AddXY(i * 100, bin[i]);
                    chart.Series[mode].Points.AddXY((i + 1) * 100, bin[i]);
                }
            }
        }

        /// <summary>時間ラベルを更新する。（タイマーイベント、updateFileTab時のみ呼ばれる）</summary>
        private void updateTime() {
            label_TimeThis.Text = ((int)(_timeThis              - new DateTime(1, 1, 1, 0, 0, 0)).TotalHours) + _timeThis             .ToString(":mm:ss");
            label_TimeFile.Text = ((int)(_words.statistics.time - new DateTime(1, 1, 1, 0, 0, 0)).TotalHours) + _words.statistics.time.ToString(":mm:ss");
        }


///// キー ///////////////////////

        /// <summary>特殊キーが押されたときの処理</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            if (ActiveControl.GetType() == typeof(NumericUpDown)) {
                if (keyData == Keys.Enter) { tabControl_Main.Focus(); return true; }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            else if ((keyData & Keys.Modifiers) == Keys.Alt) {
                if (tabControl_Main.SelectedIndex == 1 && (keyData & Keys.KeyCode) == Keys.Enter) { userControl_1.shortCut("random"); }
                TabControl tabControl = new TabControl[]{ tabControl_0, tabControl_1, tabControl_2, tabControl_3 }[tabControl_Main.SelectedIndex];
                if ((keyData & Keys.KeyCode) == Keys.Left ) { tabControl.SelectedIndex = Math.Max(tabControl.SelectedIndex - 1, 0); }
                if ((keyData & Keys.KeyCode) == Keys.Right) { tabControl.SelectedIndex = Math.Min(tabControl.SelectedIndex + 1, tabControl.TabPages.Count); }
            }
            else if ((keyData & Keys.Modifiers) == Keys.Control) {
                if ((keyData & Keys.KeyCode) == Keys.Enter) { button_Click(button_Manage, null); }
                if ((keyData & Keys.KeyCode) == Keys.Left) { tabControl_Main.SelectedIndex = Math.Max(tabControl_Main.SelectedIndex - 1, 0); }
                if ((keyData & Keys.KeyCode) == Keys.Right) { tabControl_Main.SelectedIndex = Math.Min(tabControl_Main.SelectedIndex + 1, 3); }
            }
            else if (tabControl_Main.SelectedIndex == 1 && (keyData & Keys.Modifiers) == Keys.Shift && (keyData & Keys.KeyCode) == Keys.Enter) { userControl_1.shortCut("pin"); }
            else if (keyData == Keys.Escape) {
                if (tabControl_Main.SelectedIndex == 1 && userControl_1.result.Any() && userControl_1.result[userControl_1.prePosition].startTime != -1) { userControl_1.Stop(!userControl_1.stopWatch.IsRunning); }
                else if (tabControl_Main.SelectedIndex == 2 && userControl_2.result.Any()) { userControl_2.Stop(!userControl_2.stopWatch.IsRunning); }
                else if (tabControl_Main.SelectedIndex == 3 && userControl_3.result.Any()) { userControl_3.Stop(!userControl_3.stopWatch.IsRunning); }
                else { Close(); }
            }
            else if (keyData == Keys.Tab) { checkBox_ShowsLowerPanel.Checked ^= true; }
            else if (tabControl_Main.SelectedIndex != 0 && UserControl_Base.keysMove.Contains(keyData & Keys.KeyCode)) {
                ((UserControl_Base)tabControl_Main.SelectedTab.Controls[0]).ProcessArrowKey(keyData, userControl_1);
            }
            else {
                if (keyData == Keys.Space) { tabControl_Main.SelectedTab.Controls[0].Focus(); }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }

        /// <summary>通常キーが押されたときの処理</summary>
        private void FormMainWindow_KeyPress(object sender, KeyPressEventArgs e) {
            if (tabControl_Main.SelectedIndex == 0 || ActiveControl.GetType() == typeof(NumericUpDown)) { return; }
            bool isCorrectKeyPressed = ((UserControl_Base)tabControl_Main.SelectedTab.Controls[0]).keyPress(e.KeyChar);
            if (isCorrectKeyPressed) {
                if (tabControl_Main.SelectedIndex == 1) { _keysThis_1++; _words.statistics.key_1++; }
                if (tabControl_Main.SelectedIndex == 2) { _keysThis_2++; _words.statistics.key_2++; }
                if (tabControl_Main.SelectedIndex == 3) { _keysThis_3++; _words.statistics.key_3++; }
            }
        }


///// その他 ///////////////////////

        /// <summary>1秒ごとに経過時間を増やす</summary>
        private void timer_Tick(object sender, EventArgs e) {
            _timeThis = _timeThis.AddSeconds(1);
            _words.statistics.time = _words.statistics.time.AddSeconds(1);
            _words.statistics.check();
            if (tabControl_Main.SelectedIndex == 0) { updateTime(); }
        }

        /// <summary>データグリッドを選択したときにそれをUserControlに反映させる</summary>
        private void dataGridView_SelectionChanged(object sender, EventArgs e) {
            if (sender == dataGridView_1 && dataGridView_1.Focused) { userControl_1.move(dataGridView_1.SelectedRows[0].Index); }
            if (sender == dataGridView_2 && dataGridView_2.Focused) { userControl_2.move(dataGridView_2.SelectedRows[0].Index); }
            if (sender == dataGridView_3 && dataGridView_3.Focused) { userControl_3.move(dataGridView_3.SelectedRows[0].Index); }
        }


        private readonly int _minHeight;
        private          int _lowerPanelHeight;
        /// <summary>下側パネルの表示/非表示</summary>
        private void checkBox_ShowPanel_CheckedChanged(object sender, EventArgs e) {
            settings_ValueChanged(null, null);
            if(tableLayoutPanel.RowCount == 2) {
                _lowerPanelHeight = panel.Height;
                MinimumSize = new Size(MinimumSize.Width, _minHeight - 200);
                ClientSize = new Size(ClientSize.Width, (int)tableLayoutPanel.RowStyles[0].Height);
                tableLayoutPanel.RowStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel.Controls.RemoveAt(1);
                tableLayoutPanel.RowCount = 1;
            }
            else {
                tableLayoutPanel.RowStyles[0].Height = ClientSize.Height;
                tableLayoutPanel.RowStyles[0].SizeType = SizeType.Absolute;
                Height += _lowerPanelHeight;
                MinimumSize = new Size(MinimumSize.Width, Height - _lowerPanelHeight + 200);
                tableLayoutPanel.RowCount = 2;
                tableLayoutPanel.Controls.Add(panel);
                tableLayoutPanel.RowStyles[1].Height = _lowerPanelHeight;
            }
        }

    
///// ツリービュー ///////////////////////////////

        /// <summary>ツリービューを更新する。（コンストラクタ、更新ボタンクリック時にのみ呼ばれる）</summary>
        public void updateTreeView() {
            treeView.Nodes.Clear();
            try {
                List<String> entries = Directory.GetFileSystemEntries(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "ワード"), "*", SearchOption.AllDirectories).Where(f => Directory.Exists(f) || Path.GetExtension(f) == ".tpl").ToList();
                foreach (String entry in entries) {
                    TreeNode node = new TreeNode() { Text = Path.GetFileName(entry), Name = entry, ImageIndex = File.Exists(entry) ? 1 : 0, SelectedImageIndex = File.Exists(entry) ? 1 : 0 };
                    TreeNode[] nodes = treeView.Nodes.Find(Path.GetDirectoryName(entry), true);
                    (nodes.Any() ? nodes[0].Nodes : treeView.Nodes).Add(node);
                }
            }
            catch (Exception) { }
        }

        /// <summary>Enterでツリー更新</summary>
        private void treeView_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { updateTreeView(); } }
        /// <summary>ノードダブルクリックでファイルを開く</summary>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (treeView.SelectedNode != null && treeView.SelectedNode.Name.EndsWith(".tpl")) { open(treeView.SelectedNode.Name); }
        }

///// 設定パネル ///////////////////////////////

        private Settings _settings = new Settings();

        /// <summary>基礎モードの通過基準を変えたときに単位を変える</summary>
        private void comboBox_1_Kpm_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox_1_Kpm.SelectedIndex == 0) { numericUpDown_1_Kpm.Minimum = 0; numericUpDown_1_Kpm.Maximum =  100; numericUpDown_1_Kpm.Value =  90; label_1_Kpm.Text = "%"  ; }
            if (comboBox_1_Kpm.SelectedIndex == 1) { numericUpDown_1_Kpm.Minimum = 0; numericUpDown_1_Kpm.Maximum = 9999; numericUpDown_1_Kpm.Value = 500; label_1_Kpm.Text = "kpm"; }
            settings_ValueChanged(null, null);
        }

        /// <summary>checkBox_Tolerantを変更したときの処理</summary>
        private void checkBox_Tolerant_CheckedChanged(object sender, EventArgs e) {
            checkBox_1_OverwriteRoman.Enabled = checkBox_1_Tolerant.Checked; checkBox_1_OverwriteRoman.Visible = comboBox_1_N.Visible = checkBox_1_Tolerant.Checked;
            comboBox_2_N.Visible = checkBox_2_Tolerant.Checked;
            comboBox_3_N.Visible = checkBox_3_Tolerant.Checked;
            settings_ValueChanged(null, null);
        }

        /// <summary>「ツールチップを表示する」を変更</summary>
        private void checkBox_ToolTip_CheckedChanged(object sender, EventArgs e) {
            toolTip.Active = userControl_1.toolTip.Active = userControl_2.toolTip.Active = userControl_3.toolTip.Active = checkBox_ShowsToolTip.Checked;
            settings_ValueChanged(null, null);
        }


        /// <summary>設定が変更されたときに試験停止と設定を行う</summary>
        private void settings_ValueChanged(object sender, EventArgs e) {
            if (isInit) { return; }
            userControl_1.Stop(); userControl_2.Stop(); userControl_3.Stop();
            getSettings();
            userControl_3.updatePanelHeight();
            if (tabControl_Main.SelectedIndex == 1) { userControl_1.updateWords(); }
            userControl_1.updateAll(); userControl_2.updateAll(); userControl_3.updateAll(); 
        }
        private bool isInit = true;

        /// <summary>GUIに設定されているパラメータを取得（タブ切り替え、保存時のみ呼ばれる）</summary>
        private void getSettings() {
            _settings.showsToolTip    = checkBox_ShowsToolTip   .Checked;
            _settings.showsLowerPanel = checkBox_ShowsLowerPanel.Checked;

            _settings.mode1_TargetKpmType = comboBox_1_Kpm               .SelectedIndex;
            _settings.mode1_TargetKpm     = int.Parse(numericUpDown_1_Kpm.Text);
            _settings.mode1_Star0         = checkBox_1_Star0             .Checked;
            _settings.mode1_Star1         = checkBox_1_Star1             .Checked;
            _settings.mode1_Star2         = checkBox_1_Star2             .Checked;
            _settings.mode1_Star3         = checkBox_1_Star3             .Checked;
            _settings.mode1_ShowsRoman    = checkBox_1_ShowsRoman        .Checked;
            _settings.mode1_Tolerant      = checkBox_1_Tolerant          .Checked;
            _settings.mode1_Overwrite     = checkBox_1_OverwriteRoman    .Checked;
            _settings.mode1_NorNN         = comboBox_1_N                 .SelectedIndex;
            _settings.mode1_Font          = comboBox_1_Font              .SelectedIndex;
            _settings.mode1_HidesMiss      = checkBox_1_HideMiss          .Checked;
            
            _settings.mode2_Misses        = int.Parse(numericUpDown_2_Misses  .Text);
            _settings.mode2_Problems      = int.Parse(numericUpDown_2_Problems.Text);
            _settings.mode2_Wait          = comboBox_2_Wait              .SelectedIndex;
            _settings.mode2_Star0         = checkBox_2_Star0             .Checked;
            _settings.mode2_Star1         = checkBox_2_Star1             .Checked;
            _settings.mode2_Star2         = checkBox_2_Star2             .Checked;
            _settings.mode2_Star3         = checkBox_2_Star3             .Checked;
            _settings.mode2_ShowsRoman    = checkBox_2_ShowsRoman        .Checked;
            _settings.mode2_Tolerant      = checkBox_2_Tolerant          .Checked;
            _settings.mode2_NorNN         = comboBox_2_N                 .SelectedIndex;
            _settings.mode2_Font          = comboBox_2_Font              .SelectedIndex;
            _settings.mode2_HidesMiss      = checkBox_2_HideMiss          .Checked;
                                      
            _settings.mode3_Misses        = int.Parse(numericUpDown_3_Misses  .Text);
            _settings.mode3_Problems      = int.Parse(numericUpDown_3_Problems.Text);
            _settings.mode3_Wait          = comboBox_3_Wait              .SelectedIndex;
            _settings.mode3_Order         = comboBox_3_Order             .SelectedIndex;
            _settings.mode3_Delimiter     = comboBox_3_Delimiter         .SelectedIndex;
            _settings.mode3_Star0         = checkBox_3_Star0             .Checked;
            _settings.mode3_Star1         = checkBox_3_Star1             .Checked;
            _settings.mode3_Star2         = checkBox_3_Star2             .Checked;
            _settings.mode3_Star3         = checkBox_3_Star3             .Checked;
            _settings.mode3_Tolerant      = checkBox_3_Tolerant          .Checked;
            _settings.mode3_NorNN         = comboBox_3_N                 .SelectedIndex;
            _settings.mode3_Font          = comboBox_3_Font              .SelectedIndex;
            _settings.mode3_HidesMiss      = checkBox_3_HideMiss          .Checked;
        }

        /// <summary>GUIにパラメータを設定する。（起動時のみ呼ばれる）</summary>
        private void setParametersToGUI()
        {
            checkBox_ShowsToolTip   .Checked = _settings.showsToolTip;
            checkBox_ShowsLowerPanel.Checked = _settings.showsLowerPanel;

            comboBox_1_Kpm.SelectedIndex       = _settings.mode1_TargetKpmType;
            numericUpDown_1_Kpm.Value          = _settings.mode1_TargetKpm    ;
            checkBox_1_Star0.Checked           = _settings.mode1_Star0        ;
            checkBox_1_Star1.Checked           = _settings.mode1_Star1        ;
            checkBox_1_Star2.Checked           = _settings.mode1_Star2        ;
            checkBox_1_Star3.Checked           = _settings.mode1_Star3        ;
            checkBox_1_ShowsRoman.Checked      = _settings.mode1_ShowsRoman   ;
            checkBox_1_Tolerant.Checked        = _settings.mode1_Tolerant     ;
            checkBox_1_OverwriteRoman.Checked  = _settings.mode1_Overwrite    ;
            comboBox_1_N.SelectedIndex         = _settings.mode1_NorNN        ;
            comboBox_1_Font.SelectedIndex      = _settings.mode1_Font         ;
            checkBox_1_HideMiss.Checked        = _settings.mode1_HidesMiss     ;
                                                 
            numericUpDown_2_Misses.Value       = _settings.mode2_Misses       ;
            numericUpDown_2_Problems.Value     = _settings.mode2_Problems     ;
            comboBox_2_Wait.SelectedIndex      = _settings.mode2_Wait         ;
            checkBox_2_Star0.Checked           = _settings.mode2_Star0        ;
            checkBox_2_Star1.Checked           = _settings.mode2_Star1        ;
            checkBox_2_Star2.Checked           = _settings.mode2_Star2        ;
            checkBox_2_Star3.Checked           = _settings.mode2_Star3        ;
            checkBox_2_ShowsRoman.Checked      = _settings.mode2_ShowsRoman   ;
            checkBox_2_Tolerant.Checked        = _settings.mode2_Tolerant     ;
            comboBox_2_N.SelectedIndex         = _settings.mode2_NorNN        ;
            comboBox_2_Font.SelectedIndex      = _settings.mode2_Font         ;
            checkBox_2_HideMiss.Checked        = _settings.mode2_HidesMiss     ;
                                                               
            numericUpDown_3_Misses.Value       = _settings.mode3_Misses       ;
            numericUpDown_3_Problems.Value     = _settings.mode3_Problems     ;
            comboBox_3_Wait.SelectedIndex      = _settings.mode3_Wait         ;
            comboBox_3_Order.SelectedIndex     = _settings.mode3_Order        ;
            comboBox_3_Delimiter.SelectedIndex = _settings.mode3_Delimiter    ;
            checkBox_3_Star0.Checked           = _settings.mode3_Star0        ;
            checkBox_3_Star1.Checked           = _settings.mode3_Star1        ;
            checkBox_3_Star2.Checked           = _settings.mode3_Star2        ;
            checkBox_3_Star3.Checked           = _settings.mode3_Star3        ;
            checkBox_3_Tolerant.Checked        = _settings.mode3_Tolerant     ;
            comboBox_3_N.SelectedIndex         = _settings.mode3_NorNN        ;
            comboBox_3_Font.SelectedIndex      = _settings.mode3_Font         ;
            checkBox_3_HideMiss.Checked        = _settings.mode3_HidesMiss     ;
        }

        /// <summary>スクロール時、表示時に列幅を調整する</summary>
        private void dataGridView_AutoResizeColumns(object sender, EventArgs e) {
            DataGridView dgv = sender as DataGridView ?? ((TabControl)sender).SelectedTab.Controls[0] as DataGridView;
            if (dgv == null) { return; }
            (dgv).AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
            (dgv).AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
        }

        /// <summary>ランキングの消去</summary>
        private void button_Ranking_Click(object sender, EventArgs e) {
            if (sender == button_2_RemoveAll     ) { userControl_2.clearRanking(true); }
            if (sender == button_2_RemoveSelected) { userControl_2.clearRanking(false);}
            if (sender == button_3_RemoveAll     ) { userControl_3.clearRanking(true); }
            if (sender == button_3_RemoveSelected) { userControl_3.clearRanking(false);}
        }

    }
}
