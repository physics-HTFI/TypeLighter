using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace TypeLighter.UserControls
{
    partial class UserControl_2 : UserControl_Base
    {
        static private Font fontDefault = new Font("MS Gothic", 13); // メッセージのフォント
        static private Font[] fontsJapanese = new Font[] { new Font("MS UI Gothic", 13), new Font("MS UI Gothic", 16), new Font("MS UI Gothic", 19) }; // 日本語ワードのフォント
        static private Font[] fontsEnglish  = new Font[] { new Font("MS Gothic"   , 13), new Font("MS Gothic"   , 16), new Font("MS Gothic"   , 19) }; // 英語ワードのフォント

        private int          rank = -1;
        public  DataGridView dataGridView_Rank;


        /// <summary>コンストラクタ</summary>
        public UserControl_2() { InitializeComponent(); updateAll(); }


///// Util //////////////////////////////////////

        /// <summary>ランキングを構成</summary>
        public void makeRankingGrid() {
            dataGridView_Rank.Rows.Clear();
            dataGridView_Rank.RowCount = words.ranking2.items.Count;
            for (int i = 0; i < words.ranking2.items.Count; i++) {
                dataGridView_Rank[0, i].Value = i+1;
                dataGridView_Rank[1, i].Value = words.ranking2.items[i].kpm;
                dataGridView_Rank[2, i].Value = 100.0 * words.ranking2.items[i].key / (words.ranking2.items[i].key + words.ranking2.items[i].miss);
                dataGridView_Rank[3, i].Value = words.ranking2.items[i].word;
                dataGridView_Rank[4, i].Value = (double)words.ranking2.items[i].key / words.ranking2.items[i].word;
                dataGridView_Rank[5, i].Value = words.ranking2.items[i].miss;
                dataGridView_Rank[6, i].Value = words.ranking2.items[i].time.ToString();
            }
        }

        /// <summary>ランキングの全部／選択部分を削除する</summary>
        public void clearRanking(bool isAll) {
            if (isAll) { words.ranking2.items.Clear(); }
            else { dataGridView_Rank.SelectedRows.Cast<DataGridViewRow>().Select(r => words.ranking2.items[(int)r.Cells[0].Value - 1]).Reverse().ToList().ForEach(w => words.ranking2.items.Remove(w)); }
            makeRankingGrid();
        }

///// 処理フロー //////////////////////////////////////

        /// <summary>試験を中止する。（dataGridView選択、タブ移動、試験終了、Enter/矢印押下時にのみ呼ばれる）</summary>
        public override void Stop(bool clear = false) {
            if (clear) { result.Clear(); dataGridView.Rows.Clear(); }
            if (timer_Running.Enabled || timer_Waiting.Enabled) {
                stopWatch.Stop();
                int itemNum = Math.Min(result.Count, (position/100+1)*100);
                for (int i = position; i < itemNum; i++) { updateRow(i); }
                dataGridView.CurrentCell = dataGridView[0, position];
            }
            if (result.Any() && timer_Running.Enabled && result.All(w => w.isFinished)) {
                int kpm = WordUtils.getKpm(result.Last().startChars + result.Last().times.Count, result.Last().times.Last());
                words.statistics.mode2_BestKpm = Math.Max(words.statistics.mode2_BestKpm, kpm);
                words.statistics.mode2_BestSec = Math.Min(words.statistics.mode2_BestSec, double.Parse(label_SecInit.Text));
                words.statistics.mode2_Try++;
                timer_Running.Stop();

                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Ranking2.Item2 item = new Ranking2.Item2() { kpm = kpm, word = result.Count, key = result.Sum(w => w.times.Count()), miss = result.Sum(w => w.missNum), time = date };
                words.ranking2.items.Add(item);
                words.ranking2.items = words.ranking2.items.OrderBy(i => -i.kpm).Take(99).ToList();
                rank = words.ranking2.items.FindIndex(i => i == item);
                if (rank != -1) {
                    makeRankingGrid();
                    dataGridView_Rank.CurrentCell = dataGridView_Rank[0, rank];
                }
            }
            timer_Waiting.Enabled = timer_Running.Enabled = false;
            updateAll();
        }


        /// <summary>timer_Waitingタイマーによる待ち時間が切れたときの処理</summary>
        private void timer_Waiting_Tick(object sender, EventArgs e) {
            timer_Waiting.Stop();
            if (timer_Running.Enabled) { ++position; }
            stopWatch.Start();
            result[position] = new WordUtils.WordResult(result[position].word, settings.mode2_Tolerant, settings.mode2_NorNN == 1);
            result[position].startTime = stopWatch.ElapsedMilliseconds;
            result[position].startChars = position==0 ? 0 : result[position - 1].startChars + result[position - 1].times.Count;
            timer_Running.Start();
            updateAll();
        }

        /// <summary>文字キーが押されたときの処理。（formMainWindowからKeyPress時にのみ呼ばれる）</summary>
        public override bool keyPress(char key) {
            if (!timer_Running.Enabled && !timer_Waiting.Enabled) {
                if (key == '\r') { Stop(true); }
                if (key == ' ') {
                    List<Word> wordsValid = words.wordList.FindAll(w => settings.isValid(w, 2));
                    if (!wordsValid.Any()) { label_Word.Text = "ワードがありません"; }
                    else {
                        dataGridView_Rank.CurrentCell = null;
                        rank = -1;
                        kpmMax = 0;
                        result = WordUtils.getProblems(wordsValid.Count, settings.mode2_Problems).Select(i => new WordUtils.WordResult(wordsValid[i], settings.mode2_Tolerant, settings.mode2_NorNN == 1, false)).ToList();
                        position = 0;
                        result[position] = new WordUtils.WordResult(result[position].word, settings.mode2_Tolerant, settings.mode2_NorNN == 1);
                        dataGridView.Rows.Clear();
                        dataGridView.RowCount = result.Count;
                        pictureBox_Graph.Focus();
                        timer_Waiting.Interval = Math.Max(1, settings.mode2_Wait * 250);
                        timer_Waiting.Start();
                        stopWatch.Reset();
                        updateAll();
                    }
                }
                return false;
            }
            else {
                bool isCorrect = false;
                if (key == '\r') { Stop(); }
                else if (timer_Running.Enabled && !timer_Waiting.Enabled) {
                    isCorrect = result[position].typed(key, stopWatch.ElapsedMilliseconds);
                    if (result[position].isFinished) {
                        int kpm = WordUtils.getKpm(result[position].times.Count, result[position].times.Last() - result[position].startTime);
                        result[position].word.bestKpm_2 = Math.Max(result[position].word.bestKpm_2, kpm);
                        kpmMax = Math.Max(kpmMax, kpm);
                        stopWatch.Stop();
                        updateRow(position);
                        dataGridView.CurrentCell = dataGridView[0, position];
                        if (position + 1 == result.Count) { Stop(); System.Media.SystemSounds.Asterisk.Play(); }
                        else { timer_Waiting.Start(); }
                        updateWithWord();
                    }
                    else if (settings.mode2_Misses != 9999 && settings.mode2_Misses < result.Sum(w => w.missNum)) { stopWatch.Stop(); MessageBox.Show("ミスが" + settings.mode2_Misses + "回を越えました", "終了"); Stop(); }
                    updateWithKey();
                }
                return isCorrect;
            }
        }

///// 表示の更新 //////////////////////////////

        /// <summary>pos番目のdataGridViewを更新する。（星の数変更、試験中止、1ワード終了時にのみ呼ばれる）</summary>
        protected override void updateRow(int pos) {
            if (pos == -1) { Enumerable.Range(0, result.Count).ToList().ForEach(i => updateRow(i)); return; }
            dataGridView[0, pos].Value = pos + 1;
            dataGridView[1, pos].Value = new String('★', result[pos].word.star);
            dataGridView[2, pos].Value = result[pos].word.bestKpm_2;
            dataGridView[3, pos].Value = !result[pos].times.Any() ? 0 : WordUtils.getKpm(result[pos].times.Count, result[pos].times.Last() - result[pos].startTime);
            dataGridView[4, pos].Value = result[pos].times.Count < 2 ? 0 : WordUtils.getKpm(result[pos].times.Count - 1, result[pos].times.Last() - result[pos].times.First());
            dataGridView[5, pos].Value = !result[pos].times.Any() ? 0 : WordUtils.normalize((result[pos].times[0] - result[pos].startTime) / 1000.0, 0, 9.99);
            dataGridView[6, pos].Value = result[pos].missNum;
            dataGridView[7, pos].Value = result[pos].Roman.Length;
            dataGridView[8, pos].Value = result[pos].Word;
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
            label_Current   .Text = !result        .Any() ?    "0000" : position + 1 + "";
            label_NumWords  .Text = !words.wordList.Any() ?   "/0000" : "/" + settings.mode2_Problems;
            label_BestKpm   .Text = !words.wordList.Any() ?    "0000" : words.statistics.mode2_BestKpm + "";
            label_BestSec   .Text = !words.wordList.Any() ?    "0.00" : words.statistics.mode2_BestSec.ToString("F2");
            label_Try.Text = !words.wordList.Any() ? "000000" : Math.Min(words.statistics.mode2_Try, 999999) + "";
            // ワード
            if (!result.Any() || timer_Waiting.Enabled) {
                label_Word.Font = fontDefault;
                label_Word.TextAlign = ContentAlignment.BottomCenter;
                label_Word.Padding = new Padding(5, 0, 0, 5);
                label_Word.Text = !result.Any() ? "Space(開始)、Enter(中止)、矢印(結果閲覧)" : "";
            }
            else {
                label_Word.Font = result[position].word.word != "" ? fontsJapanese[settings.mode2_Font] : fontsEnglish[settings.mode2_Font];
                label_Word.Padding = new Padding(30, 0, 0, 5);
                label_Word.TextAlign = ContentAlignment.BottomLeft;
                label_Word.Text = result[position].Word.Replace("&", "&&");
                if(!result[position].times.Any()) {delegate_ExpandWidth(Math.Max(label_Word.PreferredWidth, DrawUtil.getRomanWidth(result[position].Roman)+35));}
            }
            label_Left.Text = (!timer_Running.Enabled && !timer_Waiting.Enabled) ? "" : result.Count - position - (timer_Waiting.Enabled?1:0) + (timer_Running.Enabled?0:1) + "";
        }

        /// <summary>キー入力時の更新</summary>
        private void updateWithKey() {
            label_Miss      .Text = !result        .Any() ?    "0000" : Math.Min(result.Sum(w => w.missNum), 9999) + "";
            label_Key       .Text = !result        .Any() ?  "000000" : Math.Min(result.Sum(w => w.times.Count), 999999) + "";
            int romanLength = result.Sum(w => w.romanLength);
            label_KeyTotal  .Text = !result.Any() ? "/000000" : "/" + Math.Min(romanLength, 999999);
            label_KeyAverage.Text = !result.Any() ? "000.0" : (romanLength / (double)result.Count).ToString("F1");
        }
        private int kpmMax;


        /// <summary>タイマーによる更新</summary>
        private void timer_Running_Tick(object sender, EventArgs e) { updateWithTick(); }

        /// <summary>タイマーイベント時のラベル・グラフ更新。（タイマーによる更新、updateAll時にのみ呼ばれる）</summary>
        private void updateWithTick() {
            if (!result.Any() || result[0].startTime == -1 || (!timer_Running.Enabled && !timer_Waiting.Enabled && !result[0].times.Any())) { 
                label_SecInit    .Text =  "0.00";
                label_Kpm        .Text =  "0000";
                label_KpmLatter  .Text =  "0000";
            }
            else {
                bool ticking = timer_Running.Enabled && !timer_Waiting.Enabled;
                IEnumerable<WordUtils.WordResult> taken = result.Where(w => w.times.Any() || (w.startTime != -1 && ticking));
                label_SecInit  .Text = (taken.Sum(w => w.times.Any() ? (w.times[0] - w.startTime) : stopWatch.ElapsedMilliseconds - w.startTime) / 1000.0 / taken.Count()).ToString("F2");
                label_Kpm      .Text = WordUtils.getKpm(result.Sum(w => w.times.Count) + (ticking ? 1 : 0), ticking ? stopWatch.ElapsedMilliseconds : taken.Last().times.Last()) + "";
                taken = result.Where(w => 2 <= w.times.Count || (w.times.Any() && ticking));
                bool ticking2 = ticking && result[position].times.Any();
                label_KpmLatter.Text = !taken.Any() ? "0" : WordUtils.getKpm(taken.Sum(w => w.times.Count - 1) + (ticking2 ? 1 : 0), (ticking2 ? stopWatch.ElapsedMilliseconds : taken.Last().times.Last()) - taken.Sum(w => w.times[0] - w.startTime)) + "";
            }
            pictureBox_Graph.Refresh();
            pictureBox_Roman.Refresh();
        }


        /// <summary>グラフ描画イベント</summary>
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender == pictureBox_Roman && result.Any()) {
                if (timer_Running.Enabled || !timer_Waiting.Enabled) { DrawUtil.DrawRoman(result[position].Roman, result[position].misses, e, settings.mode2_ShowsRoman, false, settings.mode2_HidesMiss); }
            }
            if (sender == pictureBox_Graph) {
                if (!result.Any()) { DrawUtil.DrawGrid(500, e); }
                else {
                    List<int> kpms = Enumerable.Range(0, result[position].times.Count).Select(i => WordUtils.getKpm(i + 1, result[position].times[i] - result[position].startTime)).ToList();
                    List<int> kpmsInst = Enumerable.Range(0, result[position].times.Count).Select(i => WordUtils.getKpm(1, i == 0 ? result[position].times[0] - result[position].startTime : result[position].times[i] - result[position].times[i - 1])).ToList();
                    if (stopWatch.IsRunning) {
                        kpms.Add(WordUtils.getKpm(result[position].times.Count + 1, stopWatch.ElapsedMilliseconds - result[position].startTime));
                        kpmsInst.Add(WordUtils.getKpm(1, stopWatch.ElapsedMilliseconds - (result[position].times.Any() ? result[position].times.Last() : result[position].startTime)));
                    }
                    int maxKpm = Math.Max(500, (timer_Running.Enabled && position == 0) ? words.statistics.mode2_BestKpm * 3 / 2 : kpmMax * 3 / 2);
                    DrawUtil.DrawTarget(0, result[position].word.bestKpm_2, maxKpm, e);
                    DrawUtil.DrawGraph(result[position].Roman, kpms, kpmsInst, result[position].misses, maxKpm, false, e, settings.mode2_HidesMiss);
                    if (!timer_Running.Enabled && !timer_Waiting.Enabled) { DrawUtil.DrawRank(rank, e); }
                }
            }
        }

        private void label_Click(object sender, EventArgs e) {
            if (sender == label_BestKpm) { words.statistics.mode2_BestKpm = 0; }
            if (sender == label_BestSec) { words.statistics.mode2_BestSec = 9.99; }
            if (sender == label_Try) { words.statistics.mode2_Try = 0; }
            updateAll();
        }
    }



}
