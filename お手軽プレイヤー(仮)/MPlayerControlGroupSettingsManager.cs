using System;
using System.IO;

#region typedef

using ControlGroupList = System.Collections.Generic.List<LightPlayer.MPlayerControlGroup>;
using Settings = LightPlayer.MPlayerControlGroupSettings;
using SettingsList = LightPlayer.MPlayerControlGroupSettingsList;

#endregion typedef

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーコントロール設定の保存・読み込みを管理する
    /// </summary>
    public static class MPlayerControlGroupSettingsManager
    {
        #region 定数

        /// <summary>
        /// メディアプレイヤーコントロール設定保存用XMLファイルのフルパス
        /// </summary>
        static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\settings.xml";

        #endregion 定数

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーコントロール設定を書き込む
        /// </summary>
        public static void Save( ControlGroupList controlGroupList )
        {
            // 書き込み先のXMLファイルの存在をチェックする
            // 存在しない場合は新規作成する
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                using ( var stream = File.Create( SETTINGS_XML_PATH ) )
                    if ( stream != null )
                        stream.Close();

            // メディアプレイヤーコントロールの設定情報を生成・リストに追加する
            var settingsList = new SettingsList();
            controlGroupList.ForEach( controlGroup =>
            {
                var player = controlGroup.Player;

                settingsList.Add( new Settings(
                    controlGroup.Index,
                    player.FilePath,
                    player.LoopMode,
                    player.Volume ) );
            } );

            // 設定情報をXMLファイルに書き込む
            // XmlAccesser.Write()から例外がスローされた場合は
            // ここでcatchせずにそのままコルー元に伝播させる
            XmlAccesser.Write( SETTINGS_XML_PATH, typeof( SettingsList ), settingsList );
        }

        /// <summary>
        /// メディアプレイヤーコントロール設定を読み込む
        /// </summary>
        /// <param name="mediaCtrls"></param>
        public static void Load( ControlGroupList controlGroupList )
        {
            // 読み込むのXMLファイルの存在をチェックする
            // 存在しない場合は例外をスローする
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                throw new FileNotFoundException();

            // XMLファイルから設定情報を読み込む
            // XmlAccesser.Read()から例外がスローされた場合は
            // ここでcatchせずにそのままコルー元に伝播させる
            var settingsList = ( SettingsList )XmlAccesser.Read( SETTINGS_XML_PATH, typeof( SettingsList ) );

            // 読み込んだ設定情報をメディアプレイヤーコントロールに反映する
            controlGroupList.ForEach( controlGroup =>
            {
                var settings = settingsList.Find( x => x.Index == controlGroup.Index );
                var player = controlGroup.Player;

                player.FilePath = settings.FilePath;
                player.LoopMode = settings.LoopMode;
                player.Volume = settings.Volume;

                controlGroup.FileNameTextBox.Text = player.FileName;
                controlGroup.LoopCheckBox.Checked = player.LoopMode;
                controlGroup.VolumeBar.Value = player.Volume;
            } );
        }

        #endregion 公開メソッド
    }
}