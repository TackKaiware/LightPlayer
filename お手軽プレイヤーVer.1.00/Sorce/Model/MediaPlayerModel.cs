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
    public class MediaPlayerModel : IDisposable
    {
        #region フィールド

        /// <summary>
        /// 排他制御のためのロックオブジェクト
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// このクラスで扱うビューの情報
        /// </summary>
        private ViewProvider _provider;

        /// <summary>
        /// プレイヤーが停止したか監視するメソッドのタイマー
        /// </summary>
        private System.Timers.Timer _timer;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        private static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\mplayer.xml";

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediaPlayers"></param>
        public MediaPlayerModel( ViewProvider viewProvider )
        {
            _provider = viewProvider;

            _timer = new System.Timers.Timer();
            _timer.Elapsed += ObservePlayers;
            _timer.Interval = 100; // ms;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// IDで指定したプレイヤーにファイルパスを設定する
        /// </summary>
        public void SetFilePath( int id, string filePath ) => _provider.MediaPlayers[id].SetFilePath( filePath );

        /// <summary>
        /// 指定したIDプレイヤーを再生する
        /// </summary>
        public void PlayBack( int id, bool isParallelPlayBack = false )
        {
            // 同時再生しないときは再生中のメディアプレイヤーは停止させる
            if ( !isParallelPlayBack )
            {
                var playingId = _provider.MediaPlayers.GetPlayingId();
                _provider.MediaPlayers[playingId].Player.Stop();
            }

            // 指定したメディアプレイヤーを再生し、
            // メディアプレイヤーの外観を更新する
            _provider.MediaPlayers[id].Player.PlayBack();
            _provider.MediaPlayers[id].SetSate( Playing );

            // メディアプレイヤーの外観を更新
            foreach ( var mp in _provider.MediaPlayers )

                // 同時再生しないときは他のメディアプレイヤーをロック
                if ( !isParallelPlayBack && !mp.Equals( _provider.MediaPlayers[id] ) )
                    mp.SetSate( LockedByOtherPlaying );
        }

        /// <summary>
        /// 指定したIDのプレイヤーを停止する
        /// </summary>
        public void Stop( int id, bool isParallelPlayBack = false ) => PrivateStop( id, isParallelPlayBack );

        /// <summary>
        /// 指定したIDのプレイヤーのループモードを設定する
        /// </summary>
        public void SwitchLoopMode( int id ) => _provider.MediaPlayers[id].SwitchLoopMode();

        /// <summary>
        /// 指定したIDのプレイヤーの設定を初期化する
        /// </summary>
        public void ClearSettings( int id ) => _provider.MediaPlayers[id].ClearSettings();

        /// <summary>
        /// 指定したIDのプレイヤー音量を設定する
        /// </summary>
        public void SetVolume( int id, int volume ) => _provider.MediaPlayers[id].SetVolume( volume );

        /// <summary>
        /// 全てのメディアプレイヤーを初期化する
        /// </summary>
        public void ClearAllSettings() => _provider.MediaPlayers.ForEach( mp => mp.ClearSettings() );

        /// <summary>
        /// 指定したIDのメディアプレイヤーが再生可能か？
        /// </summary>
        public bool IsPlayable( int id ) => !string.IsNullOrWhiteSpace( _provider.MediaPlayers[id].Player.FilePath );


        /// <summary>
        /// 指定さてたファイルが既に設定済みか
        /// </summary>
        public bool Exists( string filePath ) => _provider.MediaPlayers.Exists( mp => mp.Player.FilePath.Equals( filePath ) );


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
            Parallel.ForEach( _provider.MediaPlayers, ( mp ) =>
            {
                if ( mp.GetState().Equals( Playing ) && mp.Player.IsStopped )
                    played.Add( mp );
            } );

            // 停止状態にする
            played.ForEach( p => PrivateStop( p, _provider.ParallelPlayBackCheckBox.Checked ) );
        }

        /// <summary>
        /// プレイヤーの停止
        /// 公開メソッドStop()からのコール用
        /// </summary>
        private void PrivateStop( int id, bool isParallelPlayBack = false ) => PrivateStop( _provider.MediaPlayers[id], isParallelPlayBack );


        /// <summary>
        /// プレイヤーの停止
        /// 非公開メソッドObservePlayers()からのコール用
        /// </summary>
        private void PrivateStop( MediaPlayer player, bool isParallelPlayBack = false )
        {
            // 同時再生しない場合
            if ( !isParallelPlayBack )
                _provider.MediaPlayers.ForEach( mp => mp.SetSate( Stopped ) );

            // 同時再生する場合
            else
                player.SetSate( Stopped );

            player.Player.Stop();
        }

        /// <summary>
        /// メディアプレイヤー（全部）の設定を読み込む
        /// </summary>
        private void LoadSettings()
        {
            //- 読み込むのXMLファイルの存在をチェックする
            //- 存在しない場合は例外をスローする
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                throw new FileNotFoundException();

            //- XMLファイルから設定情報を読み込む
            //- XmlAccesser.Read()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            var settingsList = ( MediaPlayerSettingsList )XmlAccesser.Read(
                SETTINGS_XML_PATH, typeof( MediaPlayerSettingsList ) );

            //- 読み込んだ設定情報をメディアプレイヤーに反映する
            _provider.MediaPlayers.ForEach( mp =>
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
            //- 書き込み先のXMLファイルの存在をチェックする
            //- 存在しない場合は新規作成する
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                using ( var stream = File.Create( SETTINGS_XML_PATH ) )
                {
                }

            //- メディアプレイヤーの設定情報を生成・リストに追加する
            var settingsList = new MediaPlayerSettingsList();
            _provider.MediaPlayers.ForEach( mp =>
            {
                settingsList.Add( new MediaPlayerSettings(
                    mp.Id,
                    mp.Player.FilePath,
                    mp.Player.LoopMode,
                    mp.Player.Volume ) );
            } );

            //- 設定情報をXMLファイルに書き込む
            //- XmlAccesser.Write()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            XmlAccesser.Write( SETTINGS_XML_PATH,
                typeof( MediaPlayerSettingsList ), settingsList );
        }

        #endregion 非公開メソッド

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose( true );

            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( !disposedValue )
            {
                if ( disposing )
                {
                    // TODO: マネージ状態を破棄します (マネージ オブジェクト)。
                    ( ( IDisposable )_timer ).Dispose();
                }

                // TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~MediaPlayerModel() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }
        #endregion IDisposable Support
    }
}