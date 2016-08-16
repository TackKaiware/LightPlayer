using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using static LightPlayer.MediaPlayerStateEnum;
using ItemType = LightPlayer.MediaPlayerSettingsItem;
using SettingsType = LightPlayer.MediaPlayerSettings;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーモデルクラス
    /// </summary>
    public class MediaPlayerModel : ReadWriteModel, IDisposable
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
        /// 監視タイマー
        /// </summary>
        private System.Timers.Timer _observationTimer;

        /// <summary>
        /// 同時再生するか否かの前回値
        /// </summary>
        private bool _isParallelPrev;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// 監視タイマーの動作周期(ms)
        /// </summary>
        private const int OBSERVATION_TIMER_ELAPSED = 100;

        /// <summary>
        /// メディアプレイヤー設定保存用XMLファイルのフルパス
        /// </summary>
        private static readonly string SETTINGS_FILE_PATH = "mplayer.xml";

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mediaPlayers"></param>
        public MediaPlayerModel( ViewProvider viewProvider )
            : base( SETTINGS_FILE_PATH, new SettingsType(), typeof( SettingsType ) )

        {
            _provider = viewProvider;
            _isParallelPrev = viewProvider.ParallelCheckBox.Checked;

            _observationTimer = new System.Timers.Timer();
            _observationTimer.Elapsed += ObservePlayers;
            _observationTimer.Elapsed += ObserveParalell;
            _observationTimer.Interval = OBSERVATION_TIMER_ELAPSED;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// IDで指定したプレイヤーにファイルパスを設定する
        /// </summary>
        public void SetFilePath( int id, string filePath ) => _provider.MediaControls[id].SetFilePath( filePath );

        /// <summary>
        /// 指定したIDプレイヤーを再生する
        /// </summary>
        public void PlayBack( int id, bool parallel )
        {
            // 指定されたメディアプレイヤー
            var selected = _provider.MediaControls[id];

            if ( !parallel )
            {
                // 同時再生しないときは他のメディアプレイヤーを停止する
                _provider.MediaControls.ForEach( mc =>
                {
                    if ( mc.Id != selected.Id )
                        mc.SetSate( StoppedByOtherPlayBack );
                } );
            }

            // 指定されたメディアプレイヤーを再生する
            selected.Player.PlayBack();
        }

        /// <summary>
        /// 指定したIDのプレイヤーを停止する
        /// </summary>
        public void Stop( int id, bool parallel )
        {
            // 同時再生する？
            if ( parallel )
                _provider.MediaControls[id].SetSate( Stopped );
            else
                _provider.MediaControls.ForEach( mc => mc.SetSate( Stopped ) );
        }

        /// <summary>
        /// 指定したIDのプレイヤーのループモードを設定する
        /// </summary>
        public void SwitchLoopMode( int id ) => _provider.MediaControls[id].SwitchLoopMode();

        /// <summary>
        /// 指定したIDのプレイヤーの設定を初期化する
        /// </summary>
        public void ClearSettings( int id ) => _provider.MediaControls[id].ClearSettings();

        /// <summary>
        /// 指定したIDのプレイヤー音量を設定する
        /// </summary>
        public void SetVolume( int id, int volume ) => _provider.MediaControls[id].SetVolume( volume );

        /// <summary>
        /// 全てのメディアプレイヤーを初期化する
        /// </summary>
        public void ClearAllSettings() => _provider.MediaControls.ForEach( mc => mc.ClearSettings() );

        /// <summary>
        /// 指定したIDのメディアプレイヤーが再生可能か？
        /// </summary>
        public bool IsPlayable( int id ) => !string.IsNullOrWhiteSpace( _provider.MediaControls[id].Player.FilePath );

        #endregion 公開メソッド

        #region 非公開メソッド

        /// <summary>
        /// 再生中のプレイヤーを監視し、再生が完了したら停止状態にする
        /// </summary>
        private void ObservePlayers( object sender, ElapsedEventArgs e )
        {
            var played = new List<MediaControl>();
            Parallel.ForEach( _provider.MediaControls, ( mc ) =>
            {
                if ( mc.GetState().Equals( Playing ) && mc.Player.IsStopped )
                    played.Add( mc );
            } );

            // 停止状態にする
            played.ForEach( p => Stop( p.Id, _provider.ParallelCheckBox.Checked ) );
        }

        /// <summary>
        /// 同時再生するかのチェックボックスを監視し、
        /// チェック状態が切り替わったら全プレイヤーを停止させる
        /// </summary>
        private void ObserveParalell( object sender, ElapsedEventArgs e )
        {
            var isParallalCurrent = _provider.ParallelCheckBox.Checked;

            if ( _isParallelPrev != isParallalCurrent )
                _provider.MediaControls.ForEach( mc => mc.Player.Stop() );

            // 前回値更新
            _isParallelPrev = isParallalCurrent;
        }

        #endregion 非公開メソッド

        #region ReadWriteModelの抽象メソッドの実装

        /// <summary>
        /// 開始処理にてfinally節で実行する処理
        /// </summary>
        protected override void FinallyAtStartProcess() => _observationTimer.Start(); // タイマーを開始

        /// <summary>
        /// 終了処理にてfinally節で実行する処理
        /// </summary>
        protected override void FinallyAtEndProcess() => _observationTimer.Stop();// タイマーを停止

        /// <summary>
        /// R/W用オブジェクトから設定を反映する
        /// </summary>
        protected override void LoadSettings()
        {
            //- 読み込んだ設定情報をメディアプレイヤーに反映する
            var settings = ( SettingsType )Convert.ChangeType( _settings, typeof( SettingsType ) );
            _provider.MediaControls.ForEach( mc =>
            {
                var item = settings.Find( s => s.Id == mc.Id );

                mc.Player.FilePath = item.FilePath;
                mc.Player.LoopMode = item.LoopMode;
                mc.Player.Volume = item.Volume;

                mc.FileNameTextBox.Text = mc.Player.FileName;
                mc.LoopCheckBox.Checked = mc.Player.LoopMode;
                mc.VolumeBar.Value = mc.Player.Volume;
            } );
        }

        /// <summary>
        /// 現在の設定をR/W用オブジェクトに格納する
        /// </summary>

        protected override void StoreSettings()
        {
            //- メディアプレイヤーの設定情報を生成・リストに追加する
            _settings = new SettingsType();
            _provider.MediaControls.ForEach( mc =>
            {
                ( ( SettingsType )_settings ).Add( new ItemType(
                    mc.Id,
                    mc.Player.FilePath,
                    mc.Player.LoopMode,
                    mc.Player.Volume ) );
            } );
        }

        #endregion ReadWriteModelの抽象メソッドの実装

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
                    ( ( IDisposable )_observationTimer ).Dispose();
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