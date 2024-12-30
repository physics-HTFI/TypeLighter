using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TypeLighter
{
    public class WordUtils
    {
        /// <summary>valをmin以上max以下にする</summary>
        static public T    normalize<T>(    T val, T min, T max) where T: IComparable { return val.CompareTo(min) < 0 ? min : max.CompareTo(val) < 0 ? max : val; }
        static public void normalize<T>(ref T val, T min, T max) where T: IComparable { val = normalize(val, min, max); }

        /// <summary>"aiueo" => "あいうえお"</summary>
        static public String romanToHiragana(String roman) { String hiragana; fullParseRoman(roman, out hiragana); return hiragana; }

        /// <summary>kpmを返す（0-9999）</summary>
        static public int getKpm(int charNum, long msec) { return WordUtils.normalize((int)(60000 * charNum / Math.Max(msec, 1)), 0, 9999); }

        /// <summary>0～num-1の数字からproblems個だけ取ってきたものを返す（num≠0）。（初速モード、長文モードの問題作成時にのみ呼ばれる）</summary>
        static public IEnumerable<int> getProblems(int num, int problems) { return shuffle(shuffle(Enumerable.Range(0, num)).Take(problems % num).Concat(Enumerable.Range(0, num * (problems / num)).Select(i => i % num))); }

        /// <summary>リストをシャッフルして返す</summary>
        static public IEnumerable<T> shuffle<T>(IEnumerable<T> list) { return list.Select(v => new KeyValuePair<String, T>(Path.GetRandomFileName(), v)).OrderBy(pair => pair.Key).Select(pair => pair.Value); }


        /// <summary>各ワードのデータ</summary>
        public class WordResult {
            public Word                  word;
            public List<HashSet<String>> parsed;
            public List<String>          misses      = new List<String> { "" }; // 各文字のミス数
            public List<long>            times       = new List<long>();    // 開始してから各文字が押されるまでの時間[ms]
            public long                  startTime   = -1; // -1は未スタートを意味する
            public int                   startChars  = 0;
            public int                   romanLength = 0;
            public int                   missNum     = 0;
            public bool                  isTolerant;
            public WordResult(Word word_, bool isTolerant_, bool isNN, bool needsParse = true, bool useDelim = false) {
                word = word_;
                String delimiter = useDelim ? " " : "";
                romanLength = word.roman.Length + delimiter.Length;
                isTolerant = isTolerant_ && word.word != "";
                if (needsParse) {
                    WordUtils.parseRoman(word.roman + delimiter, isTolerant, out parsed, out hiragana);
                    if (isTolerant) { parsed.Last().Remove(isNN ? "n" : "nn"); }
                }
            }
            public String Word { get { return word.word == "" ? word.roman : word.word; } }
            public String Roman { get { return parsed == null ? word.roman : String.Join("", Enumerable.Range(0, parsed.Count).Select(i => i != 0 && _doubles.Contains(parsed[i - 1].First()) ? "" : parsed[i].First())); } }
            public String hiragana;
            public bool isFinished { get { return parsed!=null ? pos1 == parsed.Count : false; } }
            /// <summary>タイプされたことを知らせる</summary>
            public bool typed(char key, long time) {
                if (exam(key)) { misses.Add(""); times.Add(time); romanLength = Roman.Length; return true; }
                else { misses[misses.Count - 1] += key=='\b' ? " " : key.ToString(); missNum++; System.Media.SystemSounds.Beep.Play(); return false; }
            }

            int pos1 = 0, pos2 = 0;
            private bool exam(char c) {
                if (!TypeLighter.Word.validChars.Contains(c)) { return false; }
                IEnumerable<String> removed = parsed[pos1].Where(s => s.Length!=pos2 && s[pos2] == c);
                if (!removed.Any()) {
                    if (isTolerant && parsed[pos1].Contains("n") && pos2 == 1 && !WordUtils.charNN.Contains(c) && pos1 + 1 != parsed.Count && parsed[pos1 + 1].Any(s => s[0] == c)) {
                        parsed[pos1].Remove("nn");
                        ++pos1;
                        pos2 = 0;
                        return exam(c);
                    }
                    else { return false; }
                }
                parsed[pos1] = new HashSet<String>(removed);
                ++pos2;
                if (parsed[pos1].Count == 1 && pos2 == parsed[pos1].First().Length) {
                    if (isTolerant) {
                        if (hiragana[pos1] == 'っ' && parsed[pos1].First().Length == 1) { parsed[pos1 + 1].RemoveWhere(s => s[0] != c); }
                        pos1 += WordUtils._doubles.Contains(parsed[pos1].First()) ? 2 : 1;
                    }
                    else { pos1++; }
                    pos2 = 0;
                }
                return true;
            }
            
            /// <summary>ローマ字を上書きする。（基礎モードのクリア時にのみ呼ばれる）</summary>
            public void overWrite() { word.roman = Roman; }
        }






///// static private //////////////////////////////////////////

        /// <summary>これらの文字の前に「ん」がある場合にはnnかxnと打たなければならない</summary>
        static private HashSet<char> charNN = new HashSet<char> { 'a', 'i', 'u', 'e', 'o', 'n', 'y' };
        /// <summary>これらの文字の前に「っ」がある場合にはxtuかltuと打たなければならない</summary>
        static private HashSet<char> charXTU = new HashSet<char> { 'a', 'i', 'u', 'e', 'o', 'n' };


        /// <summary>可能な全てのパターンを返す("caki" => {{ca, ka}, {ki}})</summary>
        static private void parseRoman(String roman, bool isTolerant, out List<HashSet<String>> parsed, out String hiragana) {
            if (isTolerant) { parsed = fullParseRoman(roman, out hiragana); }
            else { parsed = new HashSet<String>[1] { new HashSet<String>(new string[1] { roman }) }.ToList(); hiragana = romanToHiragana(roman); }
        }

        /// <summary>単純な置き換え</summary>
        static private void subparseRoman(String roman, out List<String> hiragana, out List<HashSet<String>> parsed) {
            hiragana = new List<string>();
            parsed = new List<HashSet<string>>();
            bool flagNN = false;
            for (int i = 0; i < roman.Length; ) {
                String str;
                bool dummy = _R2Hmap.ContainsKey(str = roman.Substring(i, 1)) || (i + 1 < roman.Length && _R2Hmap.ContainsKey(str = roman.Substring(i, 2))) || (i + 2 < roman.Length && _R2Hmap.ContainsKey(str = roman.Substring(i, 3))) || (str = "") == "";
                if (str != "") {
                    if (flagNN) {
                        if (hiragana.Last() == "n" && !charNN.Contains(str[0])) { hiragana[hiragana.Count - 1] = "ん"; }
                        if (hiragana.Last()[0] == str[0] && 'a' <= str[0] && str[0] <= 'z' && !charXTU.Contains(str[0])) { hiragana[hiragana.Count - 1] = "っ"; }
                    }
                    String hira = _R2Hmap[str];
                    hiragana.Add(hira[0].ToString());
                    parsed.Add(new HashSet<string>() { str });
                    if (hira.Length == 2) {
                        hiragana.Add(hira[1].ToString());
                        parsed.Add(new HashSet<string>());
                    }
                    flagNN = false;
                }
                else {
                    str = roman[i].ToString();
                    if (flagNN && hiragana.Last() == "n" && !charNN.Contains(str[0])) { hiragana[hiragana.Count - 1] = "ん"; }
                    flagNN = true;
                    hiragana.Add(str);
                    parsed.Add(new HashSet<string>() { str });
                }
                i += str.Length;
            }
            if (hiragana.Last() == "n") { hiragana[hiragana.Count - 1] = "ん"; }
        }


        /// <summary>完全にパースする</summary>
        static private List<HashSet<String>> fullParseRoman(String roman, out String hiragana) {
            List<String> hiraganas;
            List<HashSet<String>> parsed;
            subparseRoman(roman, out hiraganas, out parsed);
            for (int i = 0; i < hiraganas.Count; i++) {
                if (i != hiraganas.Count - 1 && _H2Rmap.ContainsKey("" + hiraganas[i] + hiraganas[i + 1])) { foreach (String s in _H2Rmap[hiraganas[i] + hiraganas[i + 1]]) { parsed[i].Add(s); } }
                if (_H2Rmap.ContainsKey(hiraganas[i])) { foreach (String s in _H2Rmap[hiraganas[i]]) { parsed[i].Add(s); } }
            }
            for (int i = 0; i < hiraganas.Count - 1; i++) {
                if (hiraganas[i] == "っ") {
                    foreach (char c in parsed[i + 1].Select(s => s[0]).Where(c => 'a' <= c && c <= 'z' && !charXTU.Contains(c))) { parsed[i].Add(c.ToString()); }
                }
            }
            hiragana = String.Join("", hiraganas).Replace(",", "、").Replace(".", "。").Replace("-", "ー");
            return parsed;
        }



        static private Dictionary<String, String>          _R2Hmap ; // ローマ字を平仮名に変換するための対応表
        static private Dictionary<String, HashSet<String>> _H2Rmap ; // 平仮名をローマ字に変換するための対応表
        static private HashSet<String>                     _doubles; // ひらがな2文字になるもの(e.g. kya, ye, ...）


        /// <summary>「ローマ字」-「ひらがな」対応表</summary>
        /// <returns>例：getMap()["kya"] == "きゃ"</returns>
        static WordUtils() {
            String keyStr = "", valStr = "";
            keyStr += "xa xi xu xe xo xya xyi xyu xye xyo xtu xwa xka xke xn ";
            valStr += "ぁ ぃ ぅ ぇ ぉ  ゃ  ぃ  ゅ  ぇ  ょ  っ  ゎ  ヵ  ヶ ん ";
            keyStr += "la li lu le lo lya lyi lyu lye lyo ltu lwa lka lke ";
            valStr += "ぁ ぃ ぅ ぇ ぉ  ゃ  ぃ  ゅ  ぇ  ょ  っ  ゎ  ヵ  ヶ ";
            keyStr += "ba bi bu be bo  bya  byi  byu  bye  byo ";
            valStr += "ば び ぶ べ ぼ びゃ びぃ びゅ びぇ びょ ";
            keyStr += "ca ci cu ce co  cya  cyi  cyu  cye  cyo  cha chi  chu  che  cho ";
            valStr += "か し く せ こ ちゃ ちぃ ちゅ ちぇ ちょ ちゃ  ち ちゅ ちぇ ちょ ";
            keyStr += "da di du de do  dya  dyi  dyu  dye  dyo  dha  dhi  dhu  dhe  dho  dwa  dwi  dwu  dwe  dwo ";
            valStr += "だ ぢ づ で ど ぢゃ ぢぃ ぢゅ ぢぇ ぢょ でゃ でぃ でゅ でぇ でょ どぁ どぃ どぅ どぇ どぉ ";
            keyStr += "  fa   fi fu   fe   fo  fya  fyi  fyu  fye  fyo ";
            valStr += "ふぁ ふぃ ふ ふぇ ふぉ ふゃ ふぃ ふゅ ふぇ ふょ ";
            keyStr += "ga gi gu ge go  gya  gyi  gyu  gye  gyo  gwa  gwi  gwu  gwe  gwo ";
            valStr += "が ぎ ぐ げ ご ぎゃ ぎぃ ぎゅ ぎぇ ぎょ ぐぁ ぐぃ ぐぅ ぐぇ ぐぉ ";
            keyStr += "  ja ji   ju   je   jo  jya  jyi  jyu  jye  jyo ";
            valStr += "じゃ じ じゅ じぇ じょ じゃ じぃ じゅ じぇ じょ ";
            keyStr += "ka ki ku ke ko  kya  kyi  kyu  kye  kyo ";
            valStr += "か き く け こ きゃ きぃ きゅ きぇ きょ ";
            keyStr += "ma mi mu me mo  mya  myi  myu  mye  myo ";
            valStr += "ま み む め も みゃ みぃ みゅ みぇ みょ ";
            keyStr += "na ni nu ne no  nya  nyi  nyu  nye  nyo nn ";
            valStr += "な に ぬ ね の にゃ にぃ にゅ にぇ にょ ん ";
            keyStr += "pa pi pu pe po  pya  pyi  pyu  pye  pyo ";
            valStr += "ぱ ぴ ぷ ぺ ぽ ぴゃ ぴぃ ぴゅ ぴぇ ぴょ ";
            keyStr += "  qa   qi qu   qe   qo  qya  qyi  qyu  qye  qyo  qwa  qwi  qwu  qwe  qwo ";
            valStr += "くぁ くぃ く くぇ くぉ くゃ くぃ くゅ くぇ くょ くぁ くぃ くぅ くぇ くぉ ";
            keyStr += "ra ri ru re ro  rya  ryi  ryu  rye  ryo ";
            valStr += "ら り る れ ろ りゃ りぃ りゅ りぇ りょ ";
            keyStr += "ta ti tu te to  tya  tyi  tyu  tye  tyo  tha  thi  thu  the  tho  tsa  tsi tsu  tse  tso  twa  twi  twu  twe  two ";
            valStr += "た ち つ て と ちゃ ちぃ ちゅ ちぇ ちょ てゃ てぃ てゅ てぇ てょ つぁ つぃ  つ つぇ つぉ とぁ とぃ とぅ とぇ とぉ ";
            keyStr += "sa si su se so  sya  syi  syu  sye  syo  sha shi  shu  she  sho ";
            valStr += "さ し す せ そ しゃ しぃ しゅ しぇ しょ しゃ  し しゅ しぇ しょ ";
            keyStr += "  va  vi  vu   ve   vo  vya  vyi  vyu  vye  vyo ";
            valStr += "ヴぁ ヴぃ ヴ ヴぇ ヴぉ ヴゃ ヴぃ ヴゅ ヴぇ ヴょ ";
            keyStr += "wa   wi wu   we wo  wha  whi whu  whe  who ";
            valStr += "わ うぃ う うぇ を うぁ うぃ  う うぇ うぉ ";
            keyStr += "ha hi hu he ho  hya  hyi  hyu  hye  hyo ";
            valStr += "は ひ ふ へ ほ ひゃ ひぃ ひゅ ひぇ ひょ ";
            keyStr += "za zi zu ze zo  zya  zyi  zyu  zye  zyo ";
            valStr += "ざ じ ず ぜ ぞ じゃ じぃ じゅ じぇ じょ ";
            keyStr += "ya yi yu   ye yo ";
            valStr += "や い ゆ いぇ よ ";
            keyStr += " a  i  u  e  o ";
            valStr += "あ い う え お ";
            String[] keys = keyStr.Split().Where(k => k.Any()).ToArray();
            String[] vals = valStr.Split().Where(v => v.Any()).ToArray();
            _R2Hmap = Enumerable.Range(0, keys.Count()).ToDictionary(i => keys[i], i => vals[i]);
            _H2Rmap = new Dictionary<string, HashSet<string>>();
            _R2Hmap.ToList().ForEach(p => { if (!_H2Rmap.ContainsKey(p.Value)) { _H2Rmap[p.Value] = new HashSet<string>(); } _H2Rmap[p.Value].Add(p.Key); });
            _H2Rmap["ん"].Add("n");
            _doubles = new HashSet<string>(_R2Hmap.Where(p => p.Value.Length == 2).Select(p => p.Key));
        }


    }
}
