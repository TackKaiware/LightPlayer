using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// モデルクラス
    /// </summary>
    public class Model
    {
        #region 定数

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\settings.xml";

        #endregion 定数

        public Model( List<MediaPlayer> mediaPlayers )
        {
            MediaPlayers = mediaPlayers;
        }

        private List<MediaPlayer> MediaPlayers { get; }

        public void SetFilePath( object sender, string filePath )
        {
            var id = sender.GetId();
            MediaPlayers[id].SetFilePath( filePath );
        }

        public void Play( object sender )
        {
            // 再生中のメディアプレイヤーは停止させる
            var playingId = MediaPlayers.GetPlayingId();

            // #State
            //_state.SetState( id, MediaPlayerStateEnum.StopFromPlaying );
            MediaPlayers[playingId].Player.Stop();

            // 指定したメディアプレイヤーを再生する
            var id = sender.GetId();
            MediaPlayers[id].Player.Play();

            // #State
            //_state.SetState( mediaPlayer.Id, MediaPlayerStateEnum.Playing );
        }

        public void Stop( object sender )
        {
            var id = sender.GetId();

            // #State
            //_state.SetState( index, MediaPlayerStateEnum.Stop );
            MediaPlayers[id].Player.Stop();
        }

        public void SwitchLoopMode( object sender )
        {
            var id = sender.GetId();
            MediaPlayers[id].SwitchLoopMode();
        }

        public void ClearSettings( object sender )
        {
            var id = sender.GetId();
            MediaPlayers[id].ClearSettings();
        }

        public void SetVolume( object sender )
        {
            var id = sender.GetId();
            var volume = ( ( TrackBar )sender ).Value;
            MediaPlayers[id].SetVolume( volume );
        }

        public void LoadSettings()
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
            MediaPlayers.ForEach( mp =>
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

        public void SaveSettings()
        {
            // 書き込み先のXMLファイルの存在をチェックする
            // 存在しない場合は新規作成する
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                using ( var stream = File.Create( SETTINGS_XML_PATH ) )
                    if ( stream != null )
                        stream.Close();

            // メディアプレイヤーの設定情報を生成・リストに追加する
            var settingsList = new MediaPlayerSettingsList();
            MediaPlayers.ForEach( mp =>
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
    }
}