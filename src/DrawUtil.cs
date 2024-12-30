using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace TypeLighter
{
    class DrawUtil
    {
        // フォント
        static private Font fontRomanBold  = new Font("MS UI Gothic", 12, FontStyle.Bold); // 未タイプのローマ字
        static private Font fontRoman      = new Font("MS UI Gothic", 12);                 // タイプ済みのローマ字
        static private Font fontRomanGraph = Control.DefaultFont;                          // グラフ中のアルファベット
        static private Font fontGraphLabel = new Font("tahoma", 6);                        // グラフ中の数字
        static private int LeftPadding = 35;
        static private Dictionary<char, int > widths     = new Dictionary<char,  int>(); // ローマ字表示文字のサイズ
        static private Dictionary<char, Size> smallChars = new Dictionary<char, Size>(); // グラフ中の文字のサイズ
        static DrawUtil() {
            using (Bitmap bitmap = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bitmap)) {
                foreach (char c in Word.validChars) {
                    widths    .Add(c, TextRenderer.MeasureText(g, c.ToString(), DrawUtil.fontRomanBold , new Size(100, 100), TextFormatFlags.NoPadding).Width);
                    smallChars.Add(c, TextRenderer.MeasureText(g, c.ToString(), DrawUtil.fontRomanGraph, new Size(100, 100), TextFormatFlags.NoPadding));
                }
                widths    ['&'] = TextRenderer.MeasureText(g, "&&", DrawUtil.fontRomanBold, new Size(100, 100), TextFormatFlags.NoPadding).Width;
                smallChars['&'] = TextRenderer.MeasureText(g, "&&", DrawUtil.fontRomanGraph, new Size(100, 100), TextFormatFlags.NoPadding);
            }
        }

        /// <summary>ローマ字表示での各文字の位置</summary>
        static private float[] getX(IEnumerable<char> chars, bool isCenter, int width) {
            float[] x = new float[chars.Count() + 1]; {
                x[0] = 0;
                for (int i = 1; i < chars.Count() + 1; i++) { x[i] = x[i - 1] + widths[chars.ElementAt(i - 1)]; }
                float offset = isCenter ? (width - x.Last()) / 2f : LeftPadding;
                for (int i = 0; i < x.Count(); i++) { x[i] += offset; }
            }
            return x;
        }


///// public /////////////////////


        /// <summary>ローマ字の幅を取得</summary>
        static public int getRomanWidth(String roman) { return roman.Select(c => widths[c]).Sum(); }



        // ローマ字を描画
        static public void DrawRoman(String roman, List<String> miss, PaintEventArgs e, bool showsRoman, bool isCenter, bool hidesMiss) {
            if (!roman.Any()) { return; }

            int missLen = hidesMiss ? 0 : miss.Sum(s => s.Length);
            char[] chars = new char[roman.Length + missLen];
            bool[] misses = new bool[roman.Length + missLen];
            for (int i = 0, c = 0; i < roman.Length; i++, c++) {
                if (missLen != 0 && i < miss.Count) { foreach (char ch in miss[i]) { chars[c] = ch; misses[c++] = true; } }
                chars[c] = roman[i];
                misses[c] = hidesMiss ? i<miss.Count && miss[i].Any() : false;
            }

            int current = miss.Count + missLen;

            float[] x = getX(chars, isCenter, e.ClipRectangle.Width);
            using (SolidBrush b = new SolidBrush(Color.White)) {
                for (int i = 0; i < chars.Length; i++) {
                    if (!showsRoman && (current - 1 < i || (current - 1 == i && (missLen == 0 && !misses[i] || missLen != 0 && !misses[i - 1])))) { return; }
                    int darkness = (current - 1 <= i ? 0 : 192);
                    b.Color = Color.FromArgb(misses[i] || darkness == 0 && !showsRoman ? 255 : darkness, darkness, darkness);
                    e.Graphics.DrawString(chars[i].ToString(), (current - 1 <= i || misses[i]) ? fontRomanBold : fontRoman, b, new Point((int)x[i] - 1, (e.ClipRectangle.Height - fontRomanBold.Height) / 2));
                }
            }
        }

        /// <summary>グリッドを描画</summary>
        static public void DrawGrid(int kpmMax, PaintEventArgs e) {
            using (Pen pen = new Pen(Color.White))
            using (SolidBrush solidBrush = new SolidBrush(Color.White)) {
                // グリッドの高さを計算
                int[] spans = new int[] { 10, 25, 50, 100, 250, 500, 1000, 2500, 5000 }; // [kpm]
                int index = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }.FirstOrDefault(i => kpmMax * (fontGraphLabel.Height + 2) / e.ClipRectangle.Height < spans[i]);
                int span = spans[index];
                int tics = new int[] { 5, 4, 2 }[index % 3];
                // グリッドを描画
                for (int i = 0; i < kpmMax / span + 1; i++) {
                    int y = e.ClipRectangle.Height * (kpmMax - i * span) / kpmMax;
                    if (i % tics == 0) {
                        solidBrush.Color = Color.FromArgb(128, 0, 0, 0);
                        e.Graphics.DrawString((i * span).ToString(), fontGraphLabel, solidBrush, 0, y + (i == 0 ? -fontGraphLabel.Height : 1));
                    }
                    pen.Color = Color.FromArgb(i % tics == 0 ? 64 : 32, 0, 0, 0);
                    e.Graphics.DrawLine(pen, 0, y, e.ClipRectangle.Width, y);
                }
            }
        }


        /// <summary>グラフを描画</summary>
        static public void DrawGraph(String roman, List<int> kpms, List<int> kpmsInst, List<String> misses, int kpmMax, bool isCenter, PaintEventArgs e, bool hidesMiss)
        {
            DrawGrid(kpmMax, e);
            if (!kpms.Any()) { return; }

            using (Pen pen = new Pen(Color.White))
            using (SolidBrush solidBrush = new SolidBrush(Color.White)) {
                float[] x = getX(roman, isCenter, e.ClipRectangle.Width);
                // wpmグラフ（瞬間）を描画
                PointF[] posKpm1 = new PointF[kpms.Count * 2 + 2];
                for (int i = 0; i < kpms.Count; i++) {
                    posKpm1[2 * i].X = x[i];
                    posKpm1[2 * i].Y = (float)e.ClipRectangle.Height * (kpmMax - kpmsInst[i]) / kpmMax;
                    posKpm1[2 * i + 1].X = x[i + 1];
                    posKpm1[2 * i + 1].Y = posKpm1[2 * i].Y;
                }
                posKpm1[kpms.Count * 2].X = posKpm1[kpms.Count * 2 - 1].X;
                posKpm1[kpms.Count * 2].Y = e.ClipRectangle.Height;
                posKpm1[kpms.Count * 2 + 1].X = posKpm1[0].X;
                posKpm1[kpms.Count * 2 + 1].Y = e.ClipRectangle.Height;
                solidBrush.Color = Color.FromArgb(80, 128, 128, 255);
                e.Graphics.FillPolygon(solidBrush, posKpm1);
                // kpmグラフを描画
                PointF[] posKpm2 = new PointF[kpms.Count];
                for (int i = 0; i < kpms.Count; i++) {
                    posKpm2[i].X = (x[i] + x[i + 1]) / 2f + 1;
                    posKpm2[i].Y = (float)e.ClipRectangle.Height * (kpmMax - kpms[i]) / kpmMax;
                }
                if (2 <= kpms.Count) { pen.Color = Color.FromArgb(96, 96, 255); e.Graphics.DrawLines(pen, posKpm2); }
                // 文字を描画
                for (int i = 0; i < kpms.Count; i++) {
                    posKpm2[i].X -= smallChars[roman[i]].Width / 2f + 2;
                    posKpm2[i].Y = Math.Min(e.ClipRectangle.Height - smallChars[roman[i]].Height, Math.Max(0, posKpm2[i].Y - smallChars[roman[i]].Height / 2f));
                    solidBrush.Color = misses[i] == "" ? Color.Blue : Color.Red;
                    e.Graphics.DrawString(roman[i].ToString(), fontRomanGraph, solidBrush, posKpm2[i]);
                }
            }
        }


        /// <summary>目標wpm・最高wpmを描画</summary>
        static public void DrawTarget(int target, int best, int max,  PaintEventArgs e) {
            using (Pen pen = new Pen(Color.White))
            using (SolidBrush solidBrush = new SolidBrush(Color.White)) {
                int yTarget = e.ClipRectangle.Height * (max - target) / max;
                int yHigh   = e.ClipRectangle.Height * (max - best  ) / max;
                int widthTarget = TextRenderer.MeasureText(target.ToString(), fontGraphLabel).Width;
                int widthHigh   = TextRenderer.MeasureText(best.ToString  (), fontGraphLabel).Width;
                int offsetTarget = (fontGraphLabel.Height < yTarget && (e.ClipRectangle.Height - fontGraphLabel.Height < yTarget || best   <  target)) ? -fontGraphLabel.Height : 1;
                int offsetHigh   = (fontGraphLabel.Height < yHigh   && (e.ClipRectangle.Height - fontGraphLabel.Height < yHigh   || target <= best  )) ? -fontGraphLabel.Height : 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                pen.Color = Color.FromArgb(128, 255, 0, 0); e.Graphics.DrawLine(pen, 0, yHigh  , e.ClipRectangle.Width, yHigh  ); solidBrush.Color = Color.Red  ; e.Graphics.DrawString(best  .ToString(), fontGraphLabel, solidBrush, e.ClipRectangle.Width - widthHigh  , yHigh   + offsetHigh  );
                if (target == 0) { return; }
                pen.Color = Color.FromArgb(128,   0, 0, 0); e.Graphics.DrawLine(pen, 0, yTarget, e.ClipRectangle.Width, yTarget); solidBrush.Color = Color.Black; e.Graphics.DrawString(target.ToString(), fontGraphLabel, solidBrush, e.ClipRectangle.Width - widthTarget, yTarget + offsetTarget);
            }
        }

        /// <summary>○×を描く（0:×、1:○、2:◎）</summary>
        static public void DrawResult(int type, PaintEventArgs e) {
            using (Pen pen = new Pen(Color.White)) {
                e.Graphics.TranslateTransform(e.ClipRectangle.Width / 2, e.ClipRectangle.Height / 2);
                int radius = (int)(Math.Min(e.ClipRectangle.Width, e.ClipRectangle.Height) * 0.42);
                pen.Width = radius / 5;
                if (type == 0) {
                    pen.Color = Color.FromArgb(32, 0, 0, 0);
                    e.Graphics.DrawLine(pen, -radius, -radius, radius, radius);
                    e.Graphics.DrawLine(pen, -radius, radius, radius, -radius);
                }
                if (type == 1 || type == 2) {
                    pen.Color = Color.FromArgb(32, 255, 0, 0);
                    e.Graphics.DrawEllipse(pen, -radius, -radius, radius * 2, radius * 2);
                    if (type == 2) { e.Graphics.DrawEllipse(pen, -radius / 2, -radius / 2, radius, radius); }
                }
            }
        }

    
        /// <summary>順位を描画</summary>
        static public void DrawRank(int rank, PaintEventArgs e) {
            using (SolidBrush brush = new SolidBrush(Color.White))
            using (Font font = new Font("MS UI Gothic", Math.Max(100, Math.Min(e.ClipRectangle.Width/2, e.ClipRectangle.Height))*2/3)) {
                e.Graphics.TranslateTransform(e.ClipRectangle.Width / 2, e.ClipRectangle.Height / 2);
                String str = rank == -1 ? "圏外" : (rank+1).ToString()+"位";
                Size size = TextRenderer.MeasureText(str, font);
                brush.Color = rank == -1 ? Color.FromArgb(40, 0, 0, 0) : rank < 10 ? Color.FromArgb(40, 255, 0, 0) : Color.FromArgb(40, 0, 0, 0);
                e.Graphics.DrawString(str, font, brush, -size.Width/2, -size.Height/2);
            }
        }
    
    }
}
