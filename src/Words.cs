using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;

namespace TypeLighter
{
    /// <summary>ワード一つ分の記録すべき必要十分な情報を持つ</summary>
    [Serializable] public class Word {
        [XmlAttribute("word" )] public String word      = ""; // 例："あいうえお"、英語の場合は""
        [XmlAttribute("roman")] public String roman     = ""; // 例："aiueo"
        [XmlAttribute("best1")] public int    bestKpm_1 =  0; // このワードの最速記録(基礎モード)
        [XmlAttribute("best2")] public int    bestKpm_2 =  0; // このワードの最速記録(初速モード)
        [XmlAttribute("best3")] public int    bestKpm_3 =  0; // このワードの最速記録(長文モード)
        [XmlAttribute("star" )] public int    star      =  0; // 星の数(0,1,2,3)
        [XmlAttribute("try"  )] public int    Try       =  0; // 打ち切り回数
        [XmlIgnoreAttribute   ] public int    width     =  0; // ワードの横幅（長文モード）
        /// <summary>コンストラクタ（シリアライズ用）</summary>
        public Word() { }
        /// <summary>コンストラクタ</summary>
        public Word(String word_, String roman_) { word = word_; roman = roman_; check(); }
        /// <summary>パラメータのチェック（コンストラクタ時とシリアライズ後に呼ぶ）</summary>
        public void check() {
            roman = String.Join("", roman.Trim().Where(c => validChars.Contains(c)));
            if (!roman.Any()) { roman = "*** empty ***"; word = "! " + word; }
            WordUtils.normalize(ref bestKpm_1, 0, 9999);
            WordUtils.normalize(ref bestKpm_2, 0, 9999);
            WordUtils.normalize(ref bestKpm_3, 0, 9999);
            WordUtils.normalize(ref star     , 0, 3);
            WordUtils.normalize(ref Try      , 0, 9999);
        }
        /// <summary>ローマ字として有効な文字</summary>
        public const String validChars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
    }


    /// <summary>tplファイル毎に保存すべき統計情報をもつ</summary>
    [Serializable] public class Statistics {
        [XmlAttribute("time"    )] public DateTime time          = new DateTime(1,1,1,0,0,0);
        [XmlAttribute("key1"    )] public int      key_1         = 0;
        [XmlAttribute("key2"    )] public int      key_2         = 0;
        [XmlAttribute("key3"    )] public int      key_3         = 0;
        [XmlAttribute("bestSec2")] public double   mode2_BestSec = 9.99;
        [XmlAttribute("bestKpm2")] public int      mode2_BestKpm = 0;
        [XmlAttribute("bestKpm3")] public int      mode3_BestKpm = 0;
        [XmlAttribute("try2"    )] public int      mode2_Try     = 0;
        [XmlAttribute("try3"    )] public int      mode3_Try     = 0;
        /// <summary>パラメータのチェック（コンストラクタ時とシリアライズ後に呼ぶ）</summary>
        public void check() {
            WordUtils.normalize(ref key_1         , 0, 99999999);
            WordUtils.normalize(ref key_2         , 0, 99999999);
            WordUtils.normalize(ref key_3         , 0, 99999999);
            WordUtils.normalize(ref time, new DateTime(1,1,1,0,0,0), new DateTime(2,2,21,15,59,59)); // 9999:59:59
            WordUtils.normalize(ref mode2_BestKpm, 0,   9999);
            WordUtils.normalize(ref mode2_BestSec, 0,   9.99);
            WordUtils.normalize(ref mode2_Try    , 0, 999999);
            WordUtils.normalize(ref mode3_BestKpm, 0,   9999);
            WordUtils.normalize(ref mode3_Try    , 0, 999999);
        } 
    }     
    

    /// <summary>長文モードのランキング</summary>
    [Serializable] public class Ranking2 {
        [XmlElement("item2")] public List<Item2> items = new List<Item2>();
        /// <summary>パラメータのチェック（シリアライズ後に呼ぶ）</summary>
        public void check() {
            items = items.Take(99).ToList();
            foreach (Item2 item in items) {
                WordUtils.normalize(ref item.kpm , 0,   9999);
                WordUtils.normalize(ref item.word, 0,   9999);
                WordUtils.normalize(ref item.key , 0, 999999);
                WordUtils.normalize(ref item.miss, 0,   9999);
            }
        }
        /// <summary>初速モードのランキングの項目</summary>
        [Serializable] public class Item2 {
            [XmlAttribute("kpm" )] public int      kpm   = 0;
            [XmlAttribute("word")] public int      word  = 0;
            [XmlAttribute("key" )] public int      key   = 0;
            [XmlAttribute("miss")] public double   miss  = 0;
            [XmlAttribute("time")] public DateTime time  = new DateTime(1, 1, 1, 0, 0, 0);
        }
    }

    /// <summary>長文モードのランキング</summary>
    [Serializable] public class Ranking3 {
        [XmlElement ("item3")] public List<Item3> items = new List<Item3>();
        /// <summary>パラメータのチェック（シリアライズ後に呼ぶ）</summary>
        public void check() {
            items = items.Take(99).ToList();
            foreach (Item3 item in items) {
                WordUtils.normalize(ref item.kpm , 0,   9999);
                WordUtils.normalize(ref item.word, 0,   9999);
                WordUtils.normalize(ref item.key , 0, 999999);
                WordUtils.normalize(ref item.miss, 0,   9999);
            }
        }
        /// <summary>長文モードのランキングの項目</summary>
        [Serializable] public class Item3 {
            [XmlAttribute("kpm"  )] public int    kpm    = 0;
            [XmlAttribute("word" )] public int    word   = 0;
            [XmlAttribute("key"  )] public int    key    = 0;
            [XmlAttribute("miss" )] public double miss   = 0;
            [XmlAttribute("space")] public bool   space  = false;
            [XmlAttribute("time" )] public DateTime time = new DateTime(1, 1, 1, 0, 0, 0);
        }     
    }

    /// <summary>tplファイルに保存すべきの必要十分な情報を持つ</summary>
    [Serializable] public class Words {
        [XmlElement ("Statistics")] public Statistics  statistics = new Statistics(); // 統計情報
        [XmlElement ("Ranking2"  )] public Ranking2    ranking2   = new Ranking2  (); // 初速モードのランキング
        [XmlElement ("Ranking3"  )] public Ranking3    ranking3   = new Ranking3  (); // 長文モードのランキング
        [XmlElement ("Word"      )] public List<Word>  wordList   = new List<Word>(); // 各ワードの内容
        /// <summary>ワードの内容を保存する（引数がnullの場合は何もしない）</summary>
        public void save(String fileName) {
            if (fileName == null) { return; }
            try { using (FileStream fs = new FileStream(fileName, FileMode.Create)) { new XmlSerializer(typeof(Words)).Serialize(fs, this); } }
            catch (Exception e) { MessageBox.Show(e.Message, "書き込み失敗"); }
        }
        /// <summary>.tplファイルを読み込んでWordsオブジェクトを返す(失敗時はデフォルト値を返す)</summary>
        static public Words getWordsFromFile(ref String fileName) {
            if (fileName == null) { return new Words(); }
            try {
                Words words;
                using (FileStream fs = new FileStream(fileName, FileMode.Open)) { words = (Words)new XmlSerializer(typeof(Words)).Deserialize(fs); }
                if (9999 < words.wordList.Count) { throw new Exception("ワードの数が10000以上のファイルは開けません。"); }
                words.wordList.ForEach(word => word.check());
                words.ranking2.check();
                words.ranking3.check();
                words.statistics.check();
                return words;
            }
            catch (Exception e) { MessageBox.Show(e.Message, "読み込みに失敗しました"); fileName = null; return new Words(); }
        }
    }


    /// <summary>設定情報を持つ</summary>
    [Serializable] public class Settings {
        public String lastOpenedFile    =   "";
        public bool   showsLowerPanel   = true;
        public bool   showsToolTip      = true;
        public int  mode1_TargetKpmType =    0;
        public int  mode1_TargetKpm     =   90;
        public bool mode1_Star0         = true;
        public bool mode1_Star1         = true;
        public bool mode1_Star2         = true;
        public bool mode1_Star3         = true;
        public bool mode1_ShowsRoman    = true;
        public bool mode1_Tolerant      = true;
        public bool mode1_Overwrite     = true;
        public int  mode1_NorNN         =    0;
        public int  mode1_Font          =    0;
        public bool mode1_HidesMiss     = true;
        public int  mode2_Misses      = 9999;
        public int  mode2_Problems    =   20;
        public int  mode2_Wait        =    2;
        public bool mode2_Star0       = true;
        public bool mode2_Star1       = true;
        public bool mode2_Star2       = true;
        public bool mode2_Star3       = true;
        public bool mode2_ShowsRoman  = true;
        public bool mode2_Tolerant    = true;
        public int  mode2_NorNN       =    0;
        public int  mode2_Font        =    0;
        public bool mode2_HidesMiss   = true;
        public int  mode3_Misses     = 9999;
        public int  mode3_Problems   =   20;
        public int  mode3_Wait       =    2;
        public int  mode3_Order      =    0;
        public int  mode3_Delimiter  =    0;
        public bool mode3_Star0      = true;
        public bool mode3_Star1      = true;
        public bool mode3_Star2      = true;
        public bool mode3_Star3      = true;
        public bool mode3_Tolerant   = true;
        public int  mode3_NorNN      =    0;
        public int  mode3_Font       =    0;
        public bool mode3_HidesMiss  = true;
        /// <summary>選択可能かどうかを返す</summary>
        public bool isValid(Word word, int mode) {
            if (mode == 1) { return word.star == 0 && mode1_Star0 || word.star == 1 && mode1_Star1 || word.star == 2 && mode1_Star2 || word.star == 3 && mode1_Star3; }
            if (mode == 2) { return word.star == 0 && mode2_Star0 || word.star == 1 && mode2_Star1 || word.star == 2 && mode2_Star2 || word.star == 3 && mode2_Star3; }
            if (mode == 3) { return word.star == 0 && mode3_Star0 || word.star == 1 && mode3_Star1 || word.star == 2 && mode3_Star2 || word.star == 3 && mode3_Star3; }
            return false;
        }
        /// <summary>設定を保存する</summary>
        public void save() {
            String fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "settings.xml");
            try { using (FileStream fs = new FileStream(fileName, FileMode.Create)) { new XmlSerializer(typeof(Settings)).Serialize(fs, this); } }
            catch (Exception e) { MessageBox.Show(e.Message, "設定ファイルの書き込みに失敗しました：\r\n" + fileName); }
        }
        /// <summary>設定を読み込む</summary>
        static public Settings load() {
            Settings settings = new Settings();
            String fileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "settings.xml");
            try { using (FileStream fs = new FileStream(fileName, FileMode.Open)) { settings = (Settings)new XmlSerializer(typeof(Settings)).Deserialize(fs); } } catch (Exception) {}
            WordUtils.normalize(ref settings.mode1_TargetKpmType , 0,    1);
            WordUtils.normalize(ref settings.mode1_TargetKpm     , 0, settings.mode1_TargetKpmType==0 ? 100 : 9999);
            WordUtils.normalize(ref settings.mode2_Wait          , 0,    4);
            WordUtils.normalize(ref settings.mode3_Wait          , 0,    4);
            WordUtils.normalize(ref settings.mode2_Misses        , 0, 9999);
            WordUtils.normalize(ref settings.mode3_Misses        , 0, 9999);
            WordUtils.normalize(ref settings.mode2_Problems      , 0, 9999);
            WordUtils.normalize(ref settings.mode3_Problems      , 0, 9999);
            WordUtils.normalize(ref settings.mode1_NorNN         , 0,    1);
            WordUtils.normalize(ref settings.mode2_NorNN         , 0,    1);
            WordUtils.normalize(ref settings.mode3_NorNN         , 0,    1);
            WordUtils.normalize(ref settings.mode3_Order         , 0,    1);
            WordUtils.normalize(ref settings.mode3_Delimiter     , 0,    1);
            WordUtils.normalize(ref settings.mode1_Font          , 0,    2);
            WordUtils.normalize(ref settings.mode2_Font          , 0,    2);
            WordUtils.normalize(ref settings.mode3_Font          , 0,    3);
            return settings;
        }
    }
}
