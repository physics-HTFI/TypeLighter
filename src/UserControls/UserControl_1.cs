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
    partial class UserControl_1 : UserControl_Base
    {
        /// <summary>コンストラクタ</summary>
        public UserControl_1() { InitializeComponent(); }

        static private Font fontDefault = new Font("MS Gothic", 13); // メッセージのフォント
        static private Font[] fontsJapanese = new Font[] { new Font("MS UI Gothic", 13), new Font("MS UI Gothic", 16), new Font("MS UI Gothic", 19) }; // 日本語ワードのフォント
        static private Font[] fontsEnglish  = new Font[] { new Font("MS Gothic"   , 13), new Font("MS Gothic"   , 16), new Font("MS Gothic"   , 19) }; // 英語ワードのフォント

        public int prePosition = 0;
        protected override int Pos { set { prePosition = value; } }

///// util ///////////////////////////////////

        /// <summary>wordがクリア状態であればtrueを返す。（残り数表示、通過したかの判定、dataGridViewの色、○×、全てクリアしたかどうかの判定にのみ呼ばれる）</summary>
        static public bool isPassed(WordUtils.WordResult word) {
            if (!word.isFinished || word.missNum != 0) { return false; }
            return (settings.mode1_TargetKpmType == 1 ? settings.mode1_TargetKpm : word.word.bestKpm_1 * settings.mode1_TargetKpm / 100) <= WordUtils.getKpm(word.times.Count - 1, word.times.Last());
        }

        /// <summary>ピン止め／ランダムの変更を</summary>
        public void shortCut(String type) {
            if (type == "pin") { checkBox_Pinning.Checked ^= true; }
            if (type == "random") { button_Random_Click(null, null); }
        }

///// 処理フロー //////////////////////////////////////
  
         /// <summary>試験を中止する。（dataGridView選択、タブ移動、試験終了、Enter/矢印押下時にのみ呼ばれる）</summary>
         public override void Stop(bool clear = false) {
             if (clear) { result[position] = new WordUtils.WordResult(result[position].word, settings.mode1_Tolerant, settings.mode1_NorNN == 1); }
             if (!result.Any()) { return; }
             stopWatch.Stop();
             int pre = prePosition = position;
             if (timer_Running.Enabled) {
                 timer_Running.Enabled = false;
                 if (result[position].isFinished) {
                     ++result[position].word.Try;
                     result[position].word.check();
                     if (settings.mode1_Overwrite) { result[position].overWrite(); }
                     if (result[position].missNum == 0) {
                         result[position].word.bestKpm_1 = Math.Max(result[position].word.bestKpm_1, WordUtils.getKpm(result[position].times.Count - 1, result[position].times.Last()));
                         if (!checkBox_Pinning.Checked && isPassed(result[position])) {
                             ProcessArrowKey(Keys.Down, null);
                             if (result.All(w => isPassed(w))) { System.Media.SystemSounds.Asterisk.Play(); }
                         }
                     }
                 }
             }
             updateRow(pre);
             prePosition = pre;
             updateAll();
        }


         /// <summary>文字キーが押されたときの処理。（formMainWindowからKeyPress時にのみ呼ばれる）</summary>
         public override bool keyPress(char key) {
             if (!result.Any()) { label_Word.Text = "ワードがありません"; return false; }
             if (key == '\r') { Stop(!timer_Running.Enabled); return false; }
             if (!timer_Running.Enabled) {
                 result[position] = new WordUtils.WordResult(result[position].word, settings.mode1_Tolerant, settings.mode1_NorNN == 1);
                 result[position].startTime = 0;
                 stopWatch.Restart();
                 prePosition = position;
                 timer_Running.Start();
             }
             bool isCorrect = result[position].typed(key, stopWatch.ElapsedMilliseconds);
             if (result[position].isFinished) { Stop(); } else { updateWithKey(); }
             return isCorrect;
         }



///// 表示の更新 //////////////////////////////

        /// <summary>pos番目のdataGridViewを更新する。（星の数変更、updateWords、ラベルクリック、試験中止時にのみ呼ばれる）</summary>
        protected override void updateRow(int pos) {
            if (pos == -1) { pos = position; }
            dataGridView[0, pos].Value = pos + 1;
            dataGridView[1, pos].Value = new String('★', result[pos].word.star);
            dataGridView[2, pos].Value = result[pos].word.bestKpm_1;
            dataGridView[3, pos].Value = !result[pos].times.Any() ? 0 : WordUtils.getKpm(result[pos].times.Count - 1, result[pos].times.Last());
            dataGridView[4, pos].Value = !result[pos].times.Any() ? 0 : result[pos].times.Last() / 1000.0;
            dataGridView[5, pos].Value = result[pos].word.Try;
            dataGridView[6, pos].Value = result[pos].Roman.Length;
            dataGridView[7, pos].Value = result[pos].Word;
            dataGridView[3, pos].Style.ForeColor = !isPassed(result[pos]) ? Color.LightGray : (result[pos].word.bestKpm_1 == 0 || (int)(dataGridView[3, pos].Value) < result[pos].word.bestKpm_1) ? SystemColors.ControlText : Color.Red;
            dataGridView[4, pos].Style.ForeColor = !isPassed(result[pos]) ? Color.LightGray : SystemColors.ControlText;
        }

        /// <summary>resultとdataGridView構築（タブ表示、設定変更時にのみ呼ばれる）</summary>
        public void updateWords() {
            if (dataGridView == null) { return; }
            List<Word> valids = words.wordList.Where(w => settings.isValid(w, 1)).ToList();
            List<WordUtils.WordResult> backup = result;
            result = new List<WordUtils.WordResult>();
            valids.ForEach(w => result.Add(backup.Any(b => b.word == w) ? backup.Find(b => b.word == w) : new WordUtils.WordResult(w, settings.mode1_Tolerant, settings.mode1_NorNN == 1, false)));
            dataGridView.RowCount = result.Count;
            foreach (int i in Enumerable.Range(0, result.Count)) { updateRow(i); }
            if (dataGridView.RowCount != 0) {
                if (dataGridView.RowCount - 1 < position) { position = prePosition = 0; }
                dataGridView.CurrentCell = dataGridView[0, position];
            }
            updateAll();
        }

        /// <summary>ラベルやグラフを全て更新する。（dataGridView選択、updateWords、矢印押下、ラベルクリック、試験終了、文字キー押下時にのみ呼ばれる）</summary>
        public override void updateAll() {
            updateWithKey();
            updateWithTick();
        }

        /// <summary>キー入力時の更新</summary>
        private void updateWithKey()　{
            label_Miss.Text = !result.Any() ? "000" : Math.Min(result[prePosition].missNum, 999) + "";
            label_Char.Text = !result.Any() ? "000" : Math.Min(result[prePosition].Roman.Length, 999) + "";
            if (!stopWatch.IsRunning)　{
                label_Current.Text = !result.Any() ? "0000" : prePosition + 1 + "";
                label_NumWords.Text = !result.Any() ? "/0000" : "/" + result.Count;
                label_Left.Text = !result.Any() ? "0000" : result.Count(w => !isPassed(w)) + "";
                label_BestKpm.Text = !result.Any() ? "0000" : result[prePosition].word.bestKpm_1 + "";
                label_Try.Text = !result.Any() ? "0000" : result[prePosition].word.Try + "";
                label_Word.Font = !result.Any() ? fontDefault : result[position].word.word != "" ? fontsJapanese[settings.mode1_Font] : fontsEnglish[settings.mode1_Font];
                label_Word.Text = !result.Any() ? "Enter(中止)、矢印(移動)" : result[position].Word.Replace("&", "&&");
            }
            if (result.Any() && !result[position].times.Any()) { delegate_ExpandWidth(Math.Max(label_Word.PreferredWidth, DrawUtil.getRomanWidth(result[position].Roman + 10))); }
        }

        /// <summary>タイマーイベント時のラベル・グラフ更新。（timer_Runningのティック、updateAll時にのみ呼ばれる）</summary>
        private void updateWithTick() {
            if (!result.Any() || !result[prePosition].times.Any()) {
                label_Sec.Text = "0.00";
                label_Kpm.Text = !result.Any() ? "0000" : "0";
            }
            else {
                long time = timer_Running.Enabled ? stopWatch.ElapsedMilliseconds : result[prePosition].times.Last();
                label_Sec.Text = (time / 1000.0).ToString("F2");
                label_Kpm.Text = WordUtils.getKpm(result[prePosition].times.Count - 1, time) + "";
            }
            pictureBox_Roman.Refresh();
            pictureBox_Graph.Refresh();
        }


///// イベント //////////////

        /// <summary>timer_Runningタイマーによる更新</summary>
        private void timer_Running_Tick(object sender, EventArgs e) { updateWithTick(); }


        /// <summary>各パネルのグラフ描画</summary>
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender == pictureBox_Roman && result.Any()) { DrawUtil.DrawRoman(result[position].Roman, result[position].misses, e, settings.mode1_ShowsRoman, true, settings.mode1_HidesMiss); }
            if (sender == pictureBox_Graph) {
                if (!result.Any()) { DrawUtil.DrawGrid(500, e); }
                else {
                    int targetKpm = settings.mode1_TargetKpmType == 1 ? settings.mode1_TargetKpm : result[prePosition].word.bestKpm_1 * settings.mode1_TargetKpm / 100;
                    int maxKpm = Math.Max(500, targetKpm * 3 / 2);
                    List<int> kpms = Enumerable.Range(0, result[prePosition].times.Count).Select(i => WordUtils.getKpm(i, result[prePosition].times[i])).ToList();
                    List<int> kpmsInst = Enumerable.Range(0, result[prePosition].times.Count).Select(i => i == 0 ? 0 : WordUtils.getKpm(1, result[prePosition].times[i] - result[prePosition].times[i - 1])).ToList();
                    if (stopWatch.IsRunning) {
                        kpms.Add(WordUtils.getKpm(result[prePosition].times.Count, stopWatch.ElapsedMilliseconds));
                        kpmsInst.Add(WordUtils.getKpm(1, stopWatch.ElapsedMilliseconds - (result[prePosition].times.Any() ? result[prePosition].times.Last() : 0)));
                    }
                    if (kpms.Any()) { kpms[0] = kpms.Count == 1 ? 0 : kpms[1]; }
                    DrawUtil.DrawTarget(targetKpm, result[prePosition].word.bestKpm_1, maxKpm, e);
                    DrawUtil.DrawGraph(result[prePosition].Roman, kpms, kpmsInst, result[prePosition].misses, maxKpm, true, e, settings.mode1_HidesMiss);
                    if (!stopWatch.IsRunning && result[prePosition].startTime != -1) {
                        DrawUtil.DrawResult(!isPassed(result[prePosition]) ? 0 : label_BestKpm.Text == label_Kpm.Text ? 2 : 1, e);
                    }
                }
            }
        }

        /// <summary>ラベルクリック時の記録消去</summary>
        private void label_Click(object sender, EventArgs e) {
            if (!result.Any()) { return; }
            if (sender == label_BestKpm || sender == label_Try) { result[prePosition] = new WordUtils.WordResult(result[prePosition].word, settings.mode1_Tolerant, settings.mode1_NorNN == 1); }
            if (sender == label_BestKpm) { result[prePosition].word.bestKpm_1 = 0; }
            if (sender == label_Try    ) { result[prePosition].word.Try     = 0; }
            updateRow(prePosition);
            position = prePosition;
            dataGridView.CurrentCell = dataGridView.Rows[position].Cells[0];
            updateAll();
        }

        /// <summary>チェックボックス選択時に、フォーカスをはずす</summary>
        private void checkBox_Pinning_CheckedChanged(object sender, EventArgs e) { pictureBox_Graph.Focus(); }

        /// <summary>ランダムボタンクリック</summary>
        private void button_Random_Click(object sender, EventArgs e) {
            if (result.Any()) {
                int i = new Random().Next(dataGridView.RowCount);
                dataGridView.CurrentCell = dataGridView[0, i];
                move(i);
                pictureBox_Graph.Focus();
            }
        }

    }
}
