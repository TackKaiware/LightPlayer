using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using static LightPlayer.MediaPlayerStateEnum;

namespace LightPlayer
{
    /// <summary>
    /// モデルクラス
    /// </summary>
    public class Model
    {
        #region フィールド

        /// <summary>
        /// メディアプレイヤー（全部）への参照
        /// </summary>
        private List<MediaPlayer> _mediaPlayers;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\settings.xml";

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediaPlayers"></param>
        public Model( List<MediaPlayer> mediaPlayers )
        {
            _mediaPlayers = mediaPlayers;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// senderから取得したIDのプレイヤーにファイルパスを設定する
        /// </summary>
        public void SetFilePath( object sender, string filePath )
        {
            var id = sender.GetId();
            _mediaPlayers[id].SetFilePath( filePath );
        }

        /// <summary>
        /// senderから取得したIDの再生する
        /// </summary>
        public void Play( object sender )
        {
            // 再生中のメディアプレイヤーは停止させる
            var playingId = _mediaPlayers.GetPlayingId();
            _mediaPlayers[playingId].Player.Stop();

            // 指定したメディアプレイヤーを再生する
            var id = sender.GetId();
            _mediaPlayers[id].Player.Play();

            // メディアプレイヤーの外観を更新
            foreach ( var mp in _mediaPlayers )
            {
                if ( mp.Id == id )
                    mp.SetSate( Playing );
                else
                    mp.SetSate( LockedByOtherPlaying );
            }
        }

        /// <summary>
        /// senderから取得したIDのプレイヤーを停止する
        /// </summary>
        public void Stop( object sender )
        {
            var id = sender.GetId();
            _mediaPlayers[id].Player.Stop();

            // メディアプレイヤーの外観を更新
            foreach ( var mp in _mediaPlayers )
                mp.SetSate( Stopped );
        }

        /// <summary>
        /// senderから取得したIDのプレイヤーのループモードを設定する
        /// </summary>
        public void SwitchLoopMode( object sender )
        {
            var id = sender.GetId();
            _mediaPlayers[id].SwitchLoopMode();
        }

        /// <summary>
        /// senderから取得したIDのプレイヤーの設定を初期化する
        /// </summary>
        public void ClearSettings( object sender )
        {
            var id = sender.GetId();
            _mediaPlayers[id].ClearSettings();
        }

        /// <summary>
        /// senderから取得したIDのプレイヤー音量を設定する
        /// </summary>
        public void SetVolume( object sender )
        {
            var id = sender.GetId();
            var volume = ( ( TrackBar )sender ).Value;
            _mediaPlayers[id].SetVolume( volume );
        }

        /// <summary>
        /// 全てのメディアプレイヤーを初期化する
        /// </summary>
        public void ClearAllSettings()
        {
            foreach ( var mp in _mediaPlayers )
                mp.ClearSettings();
        }

        /// <summary>
        /// メディアプレイヤーが再生可能か？
        /// </summary>
        public bool IsPlayable( object sender )
        {
            var id = sender.GetId();

            // ファイルパスに音声ファイルが設定されいれば再生可能
            return !string.IsNullOrWhiteSpace( _mediaPlayers[id].Player.FilePath );
        }

        /// <summary>
        /// 指定さてたファイルが既に設定済みか
        /// </summary>
        public bool Exists(string filePath)
            => _mediaPlayers.Exists( x => x.Player.FilePath.Equals( filePath ) );

        /// <summary>
        /// メディアプレイヤー（全部）の設定を読み込む
        /// </summary>
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
            _mediaPlayers.ForEach( mp =>
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

        /// <summary>
        /// メディアプレイヤー（全部）の設定を保存する
        /// </summary>
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
            _mediaPlayers.ForEach( mp =>
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

        #endregion 公開メソッド
    }
}