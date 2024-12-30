using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TypeLighter.UserControls
{
    /// <summary>3つのモードの共通部分を抜き出したもの（abstractにすべきだがそうするとデザイナが使えなくなる）</summary>
    class UserControl_Base : UserControl
    {
        static public Settings    settings;
        static public Words       words    = new Words();
        static public Action<int> delegate_ExpandWidth; // FormMainWindowインスタンスの幅を指定した値にする

        public DataGridView dataGridView;

        public List<WordUtils.WordResult> result    = new List<WordUtils.WordResult>();
        public int                        position  = 0;
        public Stopwatch                  stopWatch = new Stopwatch();


        /// <summary>データを全てリセットする。（FormMainWindowのopen時にのみ呼ばれる）</summary>
        public void reset() { result.Clear(); Pos = position = 0; dataGridView.Rows.Clear(); }

        /// <summary>dataGridViewの選択変更を反映する。（dataGridViewの項目選択、ランダム時にのみ呼ばれる）</summary>
        public void move(int rowIndex) { Stop(); Pos = position = rowIndex; Focus(); updateAll(); }

        /// <summary>ワードを一つずつ巡回する（ProcessCmdKey、基礎モードの通過時にのみ呼ばれる）</summary>
        public void ProcessArrowKey(Keys keyData, UserControl_1 userControl_1) {
            if (result.Any() && keysMove.Contains(keyData & Keys.KeyCode)) {
                Stop();
                int dir = (keysMove.Take(2).Contains(keyData & Keys.KeyCode)) ? 1 : -1;
                if ((keyData & Keys.Modifiers) != Keys.Shift) { Pos = position = (position + dir + result.Count) % result.Count; }
                else {
                    result[position].word.star += dir;
                    result[position].word.check();
                    updateRow(-1); // 初速、長文モードでは全て書き直す必要がある
                    userControl_1.updateWords();
                }
                dataGridView.CurrentCell = dataGridView[0, Math.Min(position, dataGridView.RowCount-1)];
                updateAll();
            }
        }

        public    virtual void Stop(bool clear = false) { }
        protected virtual void updateRow(int position) { }
        public    virtual void updateAll() { }
        protected virtual int  Pos { set { position = value; } } // 基礎モードでprePositionを変更するのに使う
        public    virtual bool keyPress(char key) { return false;}
        static public Keys[] keysMove = { Keys.Down, Keys.Right, Keys.Up, Keys.Left };
    }
}
