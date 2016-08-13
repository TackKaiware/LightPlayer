using System;
using System.Collections.Generic;
using System.IO;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤー設定の保存・読み込みを管理する
    /// </summary>
    public class MediaPlayerSettingsManager
    {
        #region 定数

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\settings.xml";

        #endregion 定数

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤー設定を書き込む
        /// </summary>
        public void Save( List<MediaPlayer> mediaPlayerList )
        {
            // 書き込み先のXMLファイルの存在をチェックする
            // 存在しない場合は新規作成する
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                using ( var stream = File.Create( SETTINGS_XML_PATH ) )
                    if ( stream != null )
                        stream.Close();

            // メディアプレイヤーの設定情報を生成・リストに追加する
            var settingsList = new MediaPlayerSettingsList();
            mediaPlayerList.ForEach( mp =>
            {
                settingsList.Add( new MediaPlayerSettings(
                    mp.Id,
                    mp.Player.FilePath,
                    mp.Player.LoopMode,
                    mp.Player.Volume ) );
            } );

            // 設定情報をXMLファイルに書き込む
            // XmlAccesser.Write()から例外がスローされた場合は
            // ここでcatchせずにそのままコルー元に伝播させる
            XmlAccesser.Write( SETTINGS_XML_PATH,
                typeof( MediaPlayerSettingsList ), settingsList );
        }

        /// <summary>
        /// メディアプレイヤー設定を読み込む
        /// </summary>
        /// <param name="mediaPlayerList"></param>
        public void Load( List<MediaPlayer> mediaPlayerList )
        {
            // 読み込むのXMLファイルの存在をチェックする
            // 存在しない場合は例外をスローする
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                throw new FileNotFoundException();

            // XMLファイルから設定情報を読み込む
            // XmlAccesser.Read()から例外がスローされた場合は
            // ここでcatchせずにそのままコルー元に伝播させる
            var settingsList = ( MediaPlayerSettingsList )XmlAccesser.Read(
                SETTINGS_XML_PATH, typeof( MediaPlayerSettingsList ) );

            // 読み込んだ設定情報をメディアプレイヤーに反映する
            mediaPlayerList.ForEach( mp =>
            {
                var settings = settingsList.Find( s => s.Id == mp.Id );

                mp.Player.FilePath = settings.FilePath;
                mp.Player.LoopMode = settings.LoopMode;
                mp.Player.Volume = settings.Volume;

                mp.FileNameTextBox.Text = mp.Player.FileName;
                mp.LoopCheckBox.Checked = mp.Player.LoopMode;
                mp.VolumeBar.Value = mp.Player.Volume;
            } );
        }

        #endregion 公開メソッド
    }
}