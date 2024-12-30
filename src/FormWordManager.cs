using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TypeLighter
{
    public partial class FormWordManager : Form
    {

        public List<Word> _words;
        public bool       _isOK = false;
        static int        _selectedNum = 1;

        /// <summary>コンストラクタ</summary>
        public FormWordManager(List<Word> words, bool showsToolTip, Word current) {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            _words = words.ToList();
            makeGrid(true);
            comboBox_1   .SelectedIndex = 0;
            comboBox_2   .SelectedIndex = 0;
            tabControl   .SelectedIndex = _selectedNum;
            toolTip.Active = showsToolTip;
            if (current != null && words.Contains(current)) { dataGridView.CurrentCell = dataGridView[0, words.IndexOf(current)]; }
        }


        /// <summary>グリッド構築</summary>
        private void makeGrid(bool isInit = false) {
            int rowNumChange = _words.Count - dataGridView.Rows.Count;
            dataGridView.RowCount = _words.Count;
            if (dataGridView.RowCount == 0) return;
            foreach (DataGridViewColumn column in dataGridView.Columns) { column.HeaderCell.SortGlyphDirection = SortOrder.None; }
            foreach (int i in Enumerable.Range(0, _words.Count)) {
                dataGridView.Rows[i].HeaderCell.Value = (i+1).ToString();
                dataGridView[0, i].Value = new String('★', _words[i].star);
                dataGridView[1, i].Value = _words[i].bestKpm_1;
                dataGridView[2, i].Value = _words[i].bestKpm_2;
                dataGridView[3, i].Value = _words[i].bestKpm_3;
                dataGridView[4, i].Value = _words[i].Try;
                dataGridView[5, i].Value = _words[i].roman.Length;
                dataGridView[6, i].Value = _words[i].word.Any() ? _words[i].word : _words[i].roman;
                dataGridView[7, i].Value = _words[i].word.Any() ? _words[i].roman : "";
            }
            dataGridView.RowHeadersWidth = dataGridView.Rows[dataGridView.RowCount - 1].HeaderCell.PreferredSize.Width - 20;
            if (isInit || rowNumChange != 0) { dataGridView.ClearSelection(); }
            if (isInit) { if (dataGridView.RowCount != 0) { dataGridView.CurrentCell = dataGridView[0, 0]; } }
            else if (rowNumChange < 0) { dataGridView.CurrentCell.Selected = true; }
            else if (0 < rowNumChange) { dataGridView.CurrentCell = dataGridView[0, dataGridView.RowCount - 1]; }
        }

        /// <summary>OKまたはキャンセルボタンクリック</summary>
        private void button_OK_Cansel_Click(object sender, EventArgs e) { _isOK = sender == button_OK; Close(); }

        /// <summary>終了時には選択されているタブを保存する</summary>
        private void FormWordManager_FormClosing(object sender, FormClosingEventArgs e) { _selectedNum = tabControl.SelectedIndex; }

        /// <summary>タブ変更時にテキストボックスをクリアしフォーカスを移す</summary>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (tabControl.SelectedIndex == 1) { textBox_1_Roman.Focus(); }
            if (tabControl.SelectedIndex == 2) { textBox_2_Word .Focus(); }
            if (tabControl.SelectedIndex == 3) { textBox_3_Word .Focus(); }
            clearAll();
        }


///// ソート ////////////////////////

        /// <summary>列ヘッダクリック字のソート処理</summary>
        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            switch (e.ColumnIndex) {
                case 0: _words = _words.OrderBy(w => w.star        ).ToList(); break;
                case 1: _words = _words.OrderBy(w => w.bestKpm_1   ).ToList(); break;
                case 2: _words = _words.OrderBy(w => w.bestKpm_2   ).ToList(); break;
                case 3: _words = _words.OrderBy(w => w.bestKpm_3   ).ToList(); break;
                case 4: _words = _words.OrderBy(w => w.Try         ).ToList(); break;
                case 5: _words = _words.OrderBy(w => w.roman.Length).ToList(); break;
                case 6: _words = _words.OrderBy(w => w.word.Any() ? WordUtils.romanToHiragana(w.roman) : w.roman).ToList(); break;
                case 7: _words = _words.OrderBy(w => w.roman       ).ToList(); break;
            }
            bool isAscending = dataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending;
            if (isAscending) { _words.Reverse(); }
            makeGrid();
            dataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = isAscending ? SortOrder.Descending : SortOrder.Ascending;
        }

        /// <summary>セルの編集</summary>
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            bool isEnglish = _words[e.RowIndex].word == "";
            String word  = isEnglish ? ""                                        : (String)dataGridView[6, e.RowIndex].Value; word  = (word  ?? "").Trim();
            String roman = isEnglish ? (String)dataGridView[6, e.RowIndex].Value : (String)dataGridView[7, e.RowIndex].Value; roman = (roman ?? "").Trim();
            if (roman.Any() && _words[e.RowIndex].roman != roman || !isEnglish && word.Any() && _words[e.RowIndex].word != word) { _words[e.RowIndex] = new Word(word, roman); }
            makeGrid();
        }


///// 編集 ////////////////////////

        /// <summary>編集ボタンクリック</summary>
        private void button_Edit_Click(object sender, EventArgs e) {
            try {
                if (sender == button_Shuffle ) { _words = WordUtils.shuffle(_words).ToList(); return; }
                List<Word> sub = dataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => _words[int.Parse((String)r.HeaderCell.Value) - 1]).Reverse().ToList();
                if (sender == button_ToTop       ) { _words = sub.Concat(_words.Where(w => !sub.Contains(w))).ToList(); return; }
                if (sender == button_AddStar     ) { sub.ForEach(w => { Word w0 = new Word(w.word, w.roman) { star = w.star+1, bestKpm_1 = w.bestKpm_1, bestKpm_2 = w.bestKpm_2, bestKpm_3 = w.bestKpm_3, Try = w.Try }; w0.check(); _words[_words.IndexOf(w)] = w0; }); return; }
                if (sender == button_SubtractStar) { sub.ForEach(w => { Word w0 = new Word(w.word, w.roman) { star = w.star-1, bestKpm_1 = w.bestKpm_1, bestKpm_2 = w.bestKpm_2, bestKpm_3 = w.bestKpm_3, Try = w.Try }; w0.check(); _words[_words.IndexOf(w)] = w0; }); return; }
                if (sender == button_Remove      ) { sub.ForEach(w => _words.Remove(w)); return; }
            } finally { makeGrid(); }
        }

///// ワードの追加 ////////////////////////

        /// <summary>テキストボックスをすべてクリア</summary>
        private void clearAll() {
            button_1_Turn.Enabled = button_1_Add.Enabled = button_2_Turn.Enabled = button_2_Add.Enabled = button_3_Add.Enabled = false;
            textBox_1_Roman.Text = textBox_1_Word.Text = textBox_2_Roman.Text = textBox_2_Word.Text = textBox_3_Word.Text = "";
            setLabel(label_1, 0); setLabel(label_2, 0); setLabel(label_3, 1);
        }

        /// <summary>追加ボタン、変換ボタン</summary>
        private void button_Add_Click(object sender, EventArgs e) {
            if (sender == button_1_Turn) { textBox_KeyDown(textBox_1_Roman, new KeyEventArgs(Keys.Enter)); }
            if (sender == button_1_Add ) { textBox_KeyDown(textBox_1_Word , new KeyEventArgs(Keys.Enter)); }
            if (sender == button_2_Turn) { textBox_KeyDown(textBox_2_Word , new KeyEventArgs(Keys.Enter)); }
            if (sender == button_2_Add ) { textBox_KeyDown(textBox_2_Roman, new KeyEventArgs(Keys.Enter)); }
            if (sender == button_3_Add ) { textBox_KeyDown(textBox_3_Word , new KeyEventArgs(Keys.Enter)); }
        }

        /// <summary>テキストボックスに文字があればボタンを有効にする</summary>
        private void textBox_TextChanged(object sender, EventArgs e) {
            button_1_Turn.Enabled = textBox_1_Roman.Text.Trim().Any();
            button_1_Add .Enabled = textBox_1_Word .Text.Trim().Any() && button_1_Turn.Enabled;
            button_2_Turn.Enabled = textBox_2_Word .Text.Trim().Any();
            button_2_Add .Enabled = textBox_2_Roman.Text.Trim().Any() && button_2_Turn.Enabled;
            button_3_Add .Enabled = textBox_3_Word .Text.Trim().Any();
        }


        /// <summary>テキストボックス中でEnter押下</summary>
        private void textBox_KeyDown(object sender, KeyEventArgs e) {
            if (_words.Count == 9999) { setLabel(label_1, 3); setLabel(label_2, 3); setLabel(label_3, 3); System.Media.SystemSounds.Beep.Play(); e.SuppressKeyPress = true; return; }
            String str = ((TextBox)sender).Text.Trim();
            if (e.KeyCode == Keys.Enter && str.Any()) {
                if(sender == textBox_1_Roman || sender == textBox_2_Word) {
                    if (sender == textBox_1_Roman) {
                        if (_words.Any(w => comboBox_1.SelectedIndex == 0 ? false : comboBox_1.SelectedIndex == 1 ? WordUtils.romanToHiragana(w.roman) == WordUtils.romanToHiragana(textBox_1_Roman.Text) : w.roman == textBox_1_Roman.Text)) { clearAll(); System.Media.SystemSounds.Beep.Play(); setLabel(label_1, 2); }
                        else {textBox_1_Word .Text = WordUtils.romanToHiragana(str); textBox_1_Word .Focus(); SendKeys.Send("{F13}"                  ); setLabel(label_1, 1);}
                    }
                    if (sender == textBox_2_Word ) {
                        if (_words.Any(w => comboBox_2.SelectedIndex == 0 ? w.word == textBox_2_Word.Text : false)) { clearAll(); System.Media.SystemSounds.Beep.Play(); setLabel(label_2, 2); }
                        else { textBox_2_Roman.Text = str; textBox_2_Roman.Focus(); SendKeys.Send("{F13}+{Esc}{F10}{Enter}"); setLabel(label_2, 1); }
                    }
                }
                else {
                    bool isValid;
                    String word, roman;
                    Label label = sender == textBox_1_Word ? label_1 : sender == textBox_2_Roman ? label_2 : label_3;
                    if (sender == textBox_1_Word) {
                        word = str; roman = textBox_1_Roman.Text.Trim();
                        if (!word.Any() || !roman.Any()) { return; }
                        textBox_1_Roman.Focus();
                        isValid = _words.All(w => comboBox_1.SelectedIndex == 0 ? w.word != word : comboBox_1.SelectedIndex == 1 ? WordUtils.romanToHiragana(w.roman) != WordUtils.romanToHiragana(roman) : w.roman != roman);
                    }
                    else if (sender == textBox_2_Roman) {
                        word = textBox_2_Word.Text.Trim(); roman = str;
                        if (!word.Any() || !roman.Any()) { return; }
                        textBox_2_Word.Focus();
                        isValid = _words.All(w => comboBox_2.SelectedIndex == 0 ? w.word != word : comboBox_2.SelectedIndex == 1 ? WordUtils.romanToHiragana(w.roman) != WordUtils.romanToHiragana(roman) : w.roman != roman);
                    }
                    else { word = ""; roman = str; isValid = _words.All(w => w.roman != roman); }
                    clearAll();
                    if (isValid) { _words.Add(new Word(word, roman)); makeGrid(); } else { System.Media.SystemSounds.Beep.Play(); setLabel(label, 2); }
                }
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>説明用ラベルの文字列を設定する</summary>
        private void setLabel(Label label, int mode) {
            String[] messages = { "", "Enterで追加します", "既に存在しています", "ワード数が上限9999に達しています" };
            messages[0] = label == label_1 ? "Enterで変換します（変換キー＝F13）" : label == label_2 ? "Enterでローマ字に変換します（F13→Shift+Esc→F10）" : "";
            label.ForeColor = 2 <= mode ? Color.Red : SystemColors.ControlText;
            label.Text = messages[mode];
        }

        /// <summary>テキストボックスを移ったときにラベルを変更する</summary>
        private void textBox_Enter(object sender, EventArgs e) {
            if (sender == textBox_1_Word  && label_1.ForeColor != Color.Red) { setLabel(label_1, 1); }
            if (sender == textBox_1_Roman && label_1.ForeColor != Color.Red) { setLabel(label_1, 0); }
            if (sender == textBox_2_Word  && label_2.ForeColor != Color.Red) { setLabel(label_2, 0); }
            if (sender == textBox_2_Roman && label_2.ForeColor != Color.Red) { setLabel(label_2, 1); }
            if (sender == textBox_3_Word  && label_3.ForeColor != Color.Red) { setLabel(label_3, 1); }
        }

        /// <summary>スクロールしたときに列幅を調節する</summary>
        private void dataGridView_Scroll(object sender, ScrollEventArgs e) { dataGridView.AutoResizeColumn(0, DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader); }


        /// <summary>重複削除／リセット</summary>
        private void comboBox_DropDownClosed(object sender, EventArgs e) {
            if (((ComboBox)sender).SelectedIndex == -1) { return; }
            if (sender == comboBox_Edit) {
                int pre = _words.Count;
                _words = _words.GroupBy(w => w.word == "" ? w.roman : comboBox_Edit.SelectedIndex == 0 ? w.word : comboBox_Edit.SelectedIndex == 1 ? WordUtils.romanToHiragana(w.roman) : w.roman).Select(w => w.First()).ToList();
                MessageBox.Show(pre - _words.Count + "個削除しました。", "基準："+(String)comboBox_Edit.SelectedItem);
                comboBox_Edit.SelectedIndex = -1;
            }
            if (sender == comboBox_Reset) {
                int pre = _words.Count;

                List<Word> sub = dataGridView.SelectedRows.Cast<DataGridViewRow>().Select(r => _words[int.Parse((String)r.HeaderCell.Value) - 1]).Reverse().ToList();
                sub.ForEach(w => {
                    Word w0 = new Word(w.word, w.roman) { star = w.star, bestKpm_1 = w.bestKpm_1, bestKpm_2 = w.bestKpm_2, bestKpm_3 = w.bestKpm_3, Try = w.Try };
                    if (comboBox_Reset.SelectedIndex == 0) { w0.bestKpm_1 = 0; }
                    if (comboBox_Reset.SelectedIndex == 1) { w0.bestKpm_2 = 0; }
                    if (comboBox_Reset.SelectedIndex == 2) { w0.bestKpm_3 = 0; }
                    if (comboBox_Reset.SelectedIndex == 3) { w0.Try       = 0; }
                    if (comboBox_Reset.SelectedIndex == 4) { w0.bestKpm_1 = w0.bestKpm_2 = w0.bestKpm_3 = w0.Try = 0; }
                    _words[_words.IndexOf(w)] = w0;
                });
                comboBox_Reset.SelectedIndex = -1;
            }
            makeGrid();
        }

        /// <summary>キーによるOK/キャンセル</summary>
        private void FormWordManager_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.Enter) { button_OK_Cansel_Click(button_OK, null); }
            if (e.KeyCode == Keys.Escape) { button_OK_Cansel_Click(button_Cansel, null); }
            if (e.Control && e.KeyCode == Keys.Right) { tabControl.SelectedIndex = Math.Min(tabControl.SelectedIndex + 1, tabControl.TabPages.Count); }
            if (e.Control && e.KeyCode == Keys.Left ) { tabControl.SelectedIndex = Math.Max(tabControl.SelectedIndex - 1, 0); }
        }
    }
}
