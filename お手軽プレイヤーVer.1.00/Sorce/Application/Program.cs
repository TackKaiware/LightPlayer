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

            // Modelの生成
            var configModel = new ConfigurationModel( view.ConfigControls );

            // #よくない設計_無駄にクラスを増やしている
            var playerModel = new MediaPlayerModel( view.MediaPlayers, configModel.SettingsBridge );

            // Controllerの生成
            var contorller = new Controller( view, playerModel, configModel );

            // Viewのコントロールにイベントハンドラを設定
            view.SetEventHandler( contorller );

            // アプリケーション開始
            Application.Run( view );
        }
    }
}