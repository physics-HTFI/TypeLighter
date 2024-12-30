using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TypeLighter
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Any()) Application.Run(new FormMainWindow(args[0]));
            else Application.Run(new FormMainWindow());
        }
    }
}
