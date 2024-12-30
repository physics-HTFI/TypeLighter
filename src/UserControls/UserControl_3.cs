using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TypeLighter.UserControls
{
    partial class UserControl_3 : UserControl_Base
    {
        /// <summary>コンストラクタ</summary>
        public UserControl_3() { InitializeComponent(); updateAll(); }

        private int          rank = -1;
        public  DataGridView dataGridView_Rank;

       
        private List<List<int>> rows      = new List<List<int>>(); // i行j列にあるワードはresult[rows[i][j]]
        private List<int>       wordLefts = new List<int>();       // i番目のワードの左端のx座標

///// Util //////////////////////////////////////

        /// <summary>result, rows, wordLefts を構築する。（試験開始時のみ呼ばれる）</summary>
        private void prepareWords() {
            rows.Clear();
            wordLefts.Clear();
            result.Clear();
            List<Word> wordsValid = words.wordList.Where(w => settings.isValid(w, 3)).ToList();
            if (wordsValid.Any()) {
                List<int> indices = (settings.mode3_Order == 0 ? WordUtils.getProblems(wordsValid.Count, settings.mode3_Problems) : Enumerable.Range(0, settings.mode3_Problems).Select(i => i % wordsValid.Count)).ToList();
                result = indices.Take(indices.Count - 1).Select(i => new WordUtils.WordResult(wordsValid[i], settings.mode3_Tolerant, settings.mode3_NorNN == 1, false, settings.mode3_Delimiter == 1)).ToList();
                result.Add(new WordUtils.WordResult(wordsValid[indices.Last()], settings.mode3_Tolerant, settings.mode3_NorNN == 1, false, false));

                List<int> widths = result.Select(w => w.word.width != 0 ? w.word.width : getWidth(w.word)).ToList();
                int romanWidthMax = result.Select(w => DrawUtil.getRomanWidth(w.Roman)).Max();
                delegate_ExpandWidth(Math.Max(widths.Max(), romanWidthMax + 60) + 10);

                rows.Add(new List<int>());
                for (int i = 0; i < widths.Count; i++ ) {
                    int interval = (int)(result[i].word.word.Any() ? fontsJapanese[settings.mode3_Font].Size : fontsEnglish[settings.mode3_Font].Size * 0.7);
                    if (rows.Last().Any() && Width < wordLefts.Last() + widths[i - 1] + interval + widths[i] + 5) { rows.Add(new List<int>()); }
                    wordLefts.Add(rows.Last().Any() ? wordLefts.Last() + widths[i - 1] + interval : 0);
                    rows.Last().Add(i);
                }
            }
        }

        /// <summary>ランキングを構成</summary>
        public void makeRankingGrid() {
            dataGridView_Rank.Rows.Clear();
            dataGridView_Rank.RowCount = words.ranking3.items.Count;
            for (int i = 0; i < words.ranking3.items.Count; i++) {
                dataGridView_Rank[0, i].Value = i+1;
                dataGridView_Rank[1, i].Value = words.ranking3.items[i].kpm;
                dataGridView_Rank[2, i].Value = 100.0 * words.ranking3.items[i].key / (words.ranking3.items[i].key + words.ranking3.items[i].miss);
                dataGridView_Rank[3, i].Value = words.ranking3.items[i].word;
                dataGridView_Rank[4, i].Value = words.ranking3.items[i].key;
                dataGridView_Rank[5, i].Value = words.ranking3.items[i].miss;
                dataGridView_Rank[6, i].Value = words.ranking3.items[i].space ? "○" : "-";
                dataGridView_Rank[7, i].Value = words.ranking3.items[i].time.ToString();
            }
            dataGridView.AutoResizeColumn(4);
        }

        /// <summary>ランキングの全部／選択部分を削除する</summary>
        public void clearRanking(bool isAll) {
            if (isAll) { words.ranking3.items.Clear(); }
            else { dataGridView_Rank.SelectedRows.Cast<DataGridViewRow>().Select(r => words.ranking3.items[(int)r.Cells[0].Value - 1]).Reverse().ToList().ForEach(w => words.ranking3.items.Remove(w)); }
            makeRankingGrid();
        }

        /// <summary>フォントの大きさに応じてワードパネルの高さを変える</summary>
        public void updatePanelHeight() { tableLayoutPanel.RowStyles[0].Height = panelHeights[settings.mode3_Font]; words.wordList.ForEach(w => w.width = getWidth(w)); }

///// 処理フロー //////////////////////////////////////

        /// <summary>試験を中止する。（dataGridView選択、タブ移動、試験終了、Enter/矢印押下時にのみ呼ばれる）</summary>
        public override void Stop(bool clear = false) {
            if (clear) { result.Clear(); dataGridView.Rows.Clear(); stopWatch.Reset(); }
            stopWatch.Stop();
            if (timer_Running.Enabled) {
                int itemNum = Math.Min(result.Count, (position / 100 + 1) * 100);
                for (int i = position; i < itemNum; i++) { updateRow(i); }
                dataGridView.CurrentCell = dataGridView[0, position];
                if (result.All(w => w.isFinished)) {
                    words.statistics.mode3_Try++;
                    int kpm = WordUtils.getKpm(result.Sum(w => w.times.Count), result.Last().times.Last());
                    words.statistics.mode3_BestKpm = Math.Max(words.statistics.mode3_BestKpm, kpm);

                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    Ranking3.Item3 item = new Ranking3.Item3() { kpm = kpm, word = result.Count, key = result.Sum(w => w.times.Count()), miss = result.Sum(w => w.missNum), space = settings.mode3_Delimiter == 1, time = date };
                    words.ranking3.items.Add(item);
                    words.ranking3.items = words.ranking3.items.OrderBy(i => -i.kpm).Take(99).ToList();
                    rank = words.ranking3.items.FindIndex(i => i == item);
                    if (rank != -1) {
                        makeRankingGrid();
                        dataGridView_Rank.CurrentCell = dataGridView_Rank[0, rank];
                    }
                }
            }
            timer_Running.Enabled = timer_Waiting.Enabled = false;
            updateAll();
        }

        /// <summary>開始待ちタイマーによる更新</summary>
        private void timer_Waiting_Tick(object sender = null, EventArgs e = null) {
            timer_Running.Start();
            timer_Waiting.Stop();
            stopWatch.Restart();
            updateAll();
        }

        /// <summary>文字キーが押されたときの処理。（formMainWindowからKeyPress時にのみ呼ばれる）</summary>
        public override bool keyPress(char key) {
            if (!timer_Running.Enabled && !timer_Waiting.Enabled) {
                if (key == '\r') { if (!timer_Waiting.Enabled) { Stop(true); } }
                if (!timer_Waiting.Enabled && key == ' ') {
                    prepareWords();
                    if (result.Any()) {
                        dataGridView_Rank.CurrentCell = null;
                        rank = -1;
                        Focus();
                        position = 0;
                        kpmMax = kpmMin = 0;
                        result[position] = new WordUtils.WordResult(result[position].word, settings.mode3_Tolerant, settings.mode3_NorNN == 1, true, position != result.Count - 1 && settings.mode3_Delimiter == 1);
                        dataGridView.Rows.Clear();
                        dataGridView.RowCount = result.Count;
                        if (settings.mode3_Wait == 0) { timer_Waiting_Tick(); }
                        else {
                            timer_Waiting.Interval = 250 * settings.mode3_Wait;
                            timer_Waiting.Start();
                            updateAll();
                        }
                    }
                    else { label_Info.Visible = true; label_Info.Text = "ワードがありません"; }
                }
                return false;
            }
            else {
                bool isCorrect = false;
                if (key == '\r') { Stop(); }
                else {
                    isCorrect = result[position].typed(key, stopWatch.ElapsedMilliseconds);
                    if (result[position].isFinished) {
                        int kpm = WordUtils.getKpm(result[position].times.Count, result[position].times.Last() - result[position].startTime);
                        result[position].word.bestKpm_3 = Math.Max(kpm, result[position].word.bestKpm_3);
                        kpmMax = Math.Max(kpm, kpmMax);
                        kpmMin = position == 0 ? 0 : position == 1 ? kpm : Math.Min(kpm, kpmMin);
                        updateRow(position);
                        dataGridView.CurrentCell = dataGridView[0, position];
                        if (position == result.Count - 1) { Stop(); System.Media.SystemSounds.Asterisk.Play(); }
                        else {
                            ++position;
                            result[position] = new WordUtils.WordResult(result[position].word, settings.mode3_Tolerant, settings.mode3_NorNN == 1, true, position != result.Count - 1 && settings.mode3_Delimiter == 1);
                            result[position].startChars = result[position - 1].startChars + result[position - 1].times.Count;
                            result[position].startTime = stopWatch.ElapsedMilliseconds;
                        }
                        updateWithWord();
                    }
                    if (settings.mode3_Misses != 9999 && settings.mode3_Misses < result.Sum(w => w.missNum)) { stopWatch.Stop(); MessageBox.Show("ミスが" + settings.mode3_Misses + "回を越えました", "終了"); Stop(); }
                    updateWithKey();
                }
                return isCorrect;
            }
        }

///// 表示の更新 //////////////////////////////

        /// <summary>dataGridViewの列に値を設定する。（星数変更、開始待ちタイマー、1ワード終了、中止時のみ呼ばれる）</summary>
        protected override void updateRow(int pos) {
            if (pos == -1) { Enumerable.Range(0, result.Count).ToList().ForEach(i => updateRow(i)); return; }
            dataGridView[0, pos].Value = pos + 1;
            dataGridView[1, pos].Value = new String('★', result[pos].word.star);
            dataGridView[2, pos].Value = result[pos].word.bestKpm_3;
            dataGridView[3, pos].Value = !result[pos].times.Any() ? 0 : WordUtils.getKpm(result[pos].times.Count, result[pos].times.Last() - result[pos].startTime);
            dataGridView[4, pos].Value = !result[pos].times.Any() ? 0 : (result[pos].times.Last() - result[pos].startTime) / 1000.0;
            dataGridView[5, pos].Value = result[pos].missNum;
            dataGridView[6, pos].Value = result[pos].romanLength;
            dataGridView[7, pos].Value = result[pos].Word;
            dataGridView[3, pos].Style.ForeColor = dataGridView[4, pos].Style.ForeColor = dataGridView[5, pos].Style.ForeColor = result[pos].isFinished ? SystemColors.ControlText : Color.LightGray;
            if (dataGridView[3, pos].Style.ForeColor == SystemColors.ControlText && (int)dataGridView[3, pos].Value != 0 && (int)dataGridView[2, pos].Value == (int)dataGridView[3, pos].Value) { dataGridView[3, pos].Style.ForeColor = Color.Red; }
        }


        /// <summary>ラベルやグラフを全て更新する。（dataGridView選択、矢印押下、コンストラクタ、ラベルクリック、試験終了、待ちタイマー切れ、キー押下、開始時にのみ呼ばれる）</summary>
        public override void updateAll() {
            updateWithWord();
            updateWithKey();
            updateWithTick();
        }

        /// <summary>ワード切り替え時の更新</summary>
        private void updateWithWord() {
            label_Word      .Text = !result        .Any() ?    "0000" : position + 1 + "";
            label_WordTotal .Text = !words.wordList.Any() ?   "/0000" : "/" + settings.mode3_Problems;
            label_BestKpm   .Text = !words.wordList.Any() ?    "0000" : words.statistics.mode3_BestKpm + "";
            label_Try.Text = !words.wordList.Any() ? "000000" : Math.Min(words.statistics.mode3_Try, 999999) + "";
            label_Max       .Text = !result        .Any() ?    "0000" : kpmMax+"";
            label_Min       .Text = !result        .Any() ?    "0000" : kpmMin+"";
            bool isStopped = !timer_Running.Enabled && !timer_Waiting.Enabled;
            label_Info.Visible = !result.Any();
            label_Info.Text = "Space(開始)、Enter(中止)、矢印(結果閲覧)";
            label_Left.Text = !timer_Running.Enabled ? "" : result.Count - position + "";
            pictureBox_Word.Refresh();
        }


        /// <summary>キー入力時の更新</summary>
        private void updateWithKey() {
            label_Miss      .Text = !result        .Any() ?    "0000" : Math.Min(result.Sum(w => w.missNum), 9999) + "";
            label_Key       .Text = !result        .Any() ?  "000000" : Math.Min(result.Sum(w => w.times.Count), 999999) + "";
            int romanLength = result.Sum(w => w.romanLength);
            label_KeyTotal  .Text = !result        .Any() ? "/000000" : "/" + romanLength;
            label_KeyAverage.Text = !result.Any() ? "000.0" : (romanLength / (double)result.Count).ToString("F1");
        }
        private int kpmMax, kpmMin;

        /// <summary>タイマーによる更新</summary>
        private void timer_Running_Tick(object sender, EventArgs e) { updateWithTick(); }


        /// <summary>タイマーイベント時のラベル・グラフ更新。（timer_Runningのティック、updateAll時にのみ呼ばれる）</summary>
        private void updateWithTick() {
            label_Kpm.Text = stopWatch.ElapsedMilliseconds == 0 ? "0000" : WordUtils.getKpm(result.Sum(w => w.times.Count()) + (stopWatch.IsRunning ? 1 : 0), stopWatch.ElapsedMilliseconds) + "";
            label_Sec.Text = stopWatch.ElapsedMilliseconds == 0 ? "00.00" : (stopWatch.ElapsedMilliseconds/1000.0).ToString("F2");
            pictureBox_Graph.Refresh();
            pictureBox_Roman.Refresh();
        }

        /// <summary>グラフ描画イベント</summary>
        private void pictureBox_Paint(object sender, PaintEventArgs e) {
            if (sender == pictureBox_Word) {
                if (!result.Any() || timer_Waiting.Enabled) { return; }
                int currentRow = rows.FindIndex(list => list.Contains(position));
                for (int i = currentRow; i < Math.Min(currentRow + 2, rows.Count); i++) {
                    for (int j = 0; j < rows[i].Count; j++) {
                        Color color = rows[i][j] < position && timer_Running.Enabled ? Color.FromArgb(192, 192, 192) : rows[i][j] == position ? Color.Crimson : SystemColors.ControlText;
                        TextRenderer.DrawText(e.Graphics, result[rows[i][j]].Word.Replace("&", "&&"), result[rows[i][j]].word.word != "" ? fontsJapanese[settings.mode3_Font] : fontsEnglish[settings.mode3_Font], new Point(wordLefts[rows[i][j]], 8 + (i % 2 == 1 ? fontHeights[settings.mode3_Font] : 0)), color);
                    }
                }
            }
            if (sender == pictureBox_Roman) {
                if (result.Any() && !timer_Waiting.Enabled) { DrawUtil.DrawRoman(result[position].Roman, result[position].misses, e, true, true, settings.mode3_HidesMiss); }
            }
            if (sender == pictureBox_Graph) {
                if (!result.Any() || timer_Waiting.Enabled) { DrawUtil.DrawGrid(500, e); }
                else {
                    List<int> kpms = Enumerable.Range(0, result[position].times.Count).Select(i => WordUtils.getKpm(i + 1, result[position].times[i] - result[position].startTime)).ToList();
                    List<int> kpmsInst = Enumerable.Range(0, result[position].times.Count).Select(i => WordUtils.getKpm(1, i == 0 ? result[position].times[0] - result[position].startTime : result[position].times[i] - result[position].times[i - 1])).ToList();
                    if (stopWatch.IsRunning) {
                        kpms    .Add(WordUtils.getKpm(result[position].times.Count + 1, stopWatch.ElapsedMilliseconds - result[position].startTime));
                        kpmsInst.Add(WordUtils.getKpm(1, stopWatch.ElapsedMilliseconds - (result[position].times.Any() ? result[position].times.Last() : result[position].startTime)));
                    }
                    int maxKpm = Math.Max(500, (timer_Running.Enabled && position == 0) ? words.statistics.mode3_BestKpm * 3 / 2 : kpmMax * 3 / 2);
                    DrawUtil.DrawTarget(0, result[position].word.bestKpm_3, maxKpm, e);
                    DrawUtil.DrawGraph(result[position].Roman, kpms, kpmsInst, result[position].misses, maxKpm, true, e, settings.mode3_HidesMiss);
                    if (!timer_Running.Enabled && !timer_Waiting.Enabled) { DrawUtil.DrawRank(rank, e); }
                }
            }
        }

        /// <summary>ラベルクリックによる記録のリセット</summary>
        private void label_Click(object sender, EventArgs e) {
            if (sender == label_BestKpm) { words.statistics.mode3_BestKpm = 0; }
            if (sender == label_Try    ) { words.statistics.mode3_Try = 0; }
            updateAll();
        }

        static private Font[] fontsJapanese = new Font[] { new Font("MS UI Gothic", 10), new Font("MS UI Gothic", 13), new Font("MS UI Gothic", 16), new Font("MS UI Gothic", 19) }; // 日本語ワードのフォント
        static private Font[] fontsEnglish  = new Font[] { new Font("MS Gothic"   , 10), new Font("MS Gothic"   , 13), new Font("MS Gothic"   , 16), new Font("MS Gothic"   , 19) }; // 英語ワードのフォント
        static private int[] fontHeights  = new int[] { 22, 25, 28, 31 };
        static private int[] panelHeights = new int[] { 45, 50, 58, 64 };
        /// <summary>テキストの幅を取得（長文モード用）</summary>
        static public int getWidth(Word word) { return TextRenderer.MeasureText(g_, (word.word.Any() ? word.word : word.roman).Replace("&", "&&"), word.word != "" ? fontsJapanese[settings.mode3_Font] : fontsEnglish[settings.mode3_Font], size_, TextFormatFlags.NoPadding).Width; }
        static private readonly Bitmap   bitmap_ = new Bitmap(1, 1);
        static private readonly Graphics g_      = Graphics.FromImage(bitmap_);
        static private readonly Size     size_   = new Size(1000, 20);
    }
}
