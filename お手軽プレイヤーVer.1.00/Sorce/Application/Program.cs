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

            // Viewの生成
            var view = new View();
            var provider = view.Provide();

            // Modelの生成
            var playerModel = new MediaPlayerModel( provider );
            var configModel = new ConfigurationModel( provider );

            // Controllerの生成
            var playerContorller = new MediaPlayerController( provider, playerModel );
            var configController = new ConfigurationController( provider, configModel );

            // Viewのコントロールにイベントハンドラを設定
            view.SetEventHandler( playerContorller, configController );

            // アプリケーション開始
            Application.Run( view );
        }
    }
}