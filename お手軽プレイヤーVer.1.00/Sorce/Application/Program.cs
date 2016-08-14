using System;
using System.Windows.Forms;

namespace LightPlayer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            var view = new View();
            var model = new Model( view.MediaPlayers );
            var contorller = new Controller( view, model );
            view.SetEventHandler( contorller );

            Application.Run( view );
        }
    }
}