using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static LightPlayer.MediaPlayerStateEnum;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーモデルクラス
    /// </summary>
    public class MediaPlayerModel
    {
        #region フィールド

        /// <summary>
        /// 排他制御のためのロックオブジェクト
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// メディアプレイヤー（全部）への参照
        /// </summary>
        private List<MediaPlayer> _mediaPlayers;

        // #よくない設計_無駄にクラスを増やしている
        /// <summary>
        /// コンフィグレーションモデルの情報を受け取るオブジェクト
        /// </summary>
        private ConfigurationSettingsBridge _configSettingBridge;

        /// <summary>
        /// プレイヤーが停止したか監視するメソッドのタイマー
        /// </summary>
        private System.Timers.Timer _timer;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        private static readonly string SETTINGS_XML_PATH =
                                       Environment.CurrentDirectory + @"\mplayer.xml";

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediaPlayers"></param>
        // #よくない設計_無駄にクラスを増やしている
        public MediaPlayerModel( List<MediaPlayer> mediaPlayers, ConfigurationSettingsBridge configBridge )
        {
            _mediaPlayers = mediaPlayers;
            _configSettingBridge = configBridge;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += ObservePlayers;
            _timer.Interval = 100; // ms;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// メディアプレイヤーの数
        /// </summary>
        public int PlayersCount => _mediaPlayers.Count;

        #endregion プロパティ

        #region 公開メソッド

        /// <summary>
        /// IDで指定したプレイヤーにファイルパスを設定する
        /// </summary>
        public void SetFilePath( int id, string filePath )
        {
            _mediaPlayers[id].SetFilePath( filePath );
        }

        /// <summary>
        /// senderから取得したIDの再生する
        /// </summary>
        public void SetFilePath( object sender, string filePath )
        {
            var id = sender.GetId();
            _mediaPlayers[id].SetFilePath( filePath );
        }

        /// <summary>
        /// senderから取得したIDの再生する
        /// </summary>
        public void PlayBack( object sender, bool isParallelPlayBack = false )
        {
            // 同時再生しないときは再生中のメディアプレイヤーは停止させる
            if ( !isParallelPlayBack )
            {
                var playingId = _mediaPlayers.GetPlayingId();
                _mediaPlayers[playingId].Player.Stop();
            }

            // 指定したメディアプレイヤーを再生し、
            // メディアプレイヤーの外観を更新する
            var id = sender.GetId();
            _mediaPlayers[id].Player.PlayBack();
            _mediaPlayers[id].SetSate( Playing );

            // メディアプレイヤーの外観を更新
            foreach ( var mp in _mediaPlayers )
            {
                // 同時再生しないときは他のメディアプレイヤーをロック
                if ( !isParallelPlayBack && !mp.Equals( _mediaPlayers[id] ) )
                {
                    mp.SetSate( LockedByOtherPlaying );
                }
            }
        }

        /// <summary>
        /// senderから取得したIDのプレイヤーを停止する
        /// </summary>
        public void Stop( object sender, bool isParallelPlayBack = false )
        {
            Stop_Private( sender.GetId(), isParallelPlayBack );
        }

        /// <summary>
        /// 全てのプレイヤーを停止する
        /// </summary>
        public void StopAll()
        {
            foreach ( var player in _mediaPlayers )
            {
                Stop_Private( player );
            }
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
        public bool Exists( string filePath )
            => _mediaPlayers.Exists( x => x.Player.FilePath.Equals( filePath ) );

        /// <summary>
        /// 開始処理（ビューのフォームロード時に呼ぶこと）
        /// </summary>
        public void StartProcess()
        {
            try
            {
                // 設定情報をメディアプレイヤーに読み込む
                LoadSettings();
            }
            catch ( FileNotFoundException )
            {
                // 初回起動時はXMLファイルが存在しないため新規作成する
                SaveSettings();
            }
            catch ( InvalidOperationException )
            {
                // 読み込み失敗時の処理
                SaveSettings();
            }
            finally
            {
                // タイマーを開始
                _timer.Start();
            }
        }

        /// <summary>
        /// 終了処理（ビューのフォームクロージング時に呼ぶこと）
        /// </summary>
        public void EndProcces()
        {
            try
            {
                // 設定情報を書き込む
                SaveSettings();
            }
            catch ( InvalidOperationException )
            {
                // 書き込み失敗時
                MessageBox.Show( "設定の保存に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            finally
            {
                // タイマーを停止
                _timer.Stop();
            }
        }

        #endregion 公開メソッド

        #region 非公開メソッド

        /// <summary>
        /// 再生中のプレイヤーを監視し、再生が完了したら停止状態にする
        /// </summary>
        private void ObservePlayers( object sender, ElapsedEventArgs e )
        {
            var played = new List<MediaPlayer>();
            Parallel.ForEach( _mediaPlayers, ( mp ) =>
            {
                //lock ( _lockObj )
                //{
                // 再生が完了したプレイヤーを保存
                if ( mp.GetState().Equals( Playing ) && mp.Player.IsStopped )
                {
                    played.Add( mp );
                }

                //}
            } );

            // #よくない設計_無駄にクラスを増やしている
            var isParalledPlatBack = _configSettingBridge.IsParallelPlayBack;

            // 停止状態にする
            foreach ( var p in played )
            {
                Stop_Private( p, isParalledPlatBack );
            }
        }

        /// <summary>
        /// プレイヤーの停止
        /// 公開メソッドStop()からのコール用
        /// </summary>
        private void Stop_Private( int id, bool isParallelPlayBack = false )
        {
            Stop_Private( _mediaPlayers[id], isParallelPlayBack );
        }

        /// <summary>
        /// プレイヤーの停止
        /// 非公開メソッドObservePlayers()からのコール用
        /// </summary>
        private void Stop_Private( MediaPlayer player, bool isParallelPlayBack = false )
        {
            player.Player.Stop();

            if ( !isParallelPlayBack )
            {
                // 同時再生しない場合
                foreach ( var mp in _mediaPlayers )
                    mp.SetSate( Stopped );
            }
            else
            {
                // 同時再生する場合
                player.SetSate( Stopped );
            }
        }

        /// <summary>
        /// メディアプレイヤー（全部）の設定を読み込む
        /// </summary>
        private void LoadSettings()
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
        private void SaveSettings()
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

        #endregion 非公開メソッド
    }
}