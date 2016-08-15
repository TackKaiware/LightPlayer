using System.IO;
using System.Threading.Tasks;
using WMPLib;

namespace LightPlayer
{
    /// <summary>
    /// Windowsメディアプレイヤーのラッパークラス
    /// </summary>
    public class WmpWrapper
    {
        #region 定数

        /// <summary>
        /// 音量初期値
        /// </summary>
        public const int INIT_VOLUME = 80;

        #endregion 定数

        #region フィールド

        /// <summary>
        /// 排他制御のためのロックオブジェクト
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// Windowsメディアプレイヤー
        /// </summary>
        readonly WindowsMediaPlayer _mediaPlayer = new WindowsMediaPlayer();

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WmpWrapper()
        {
            _mediaPlayer.settings.autoStart = false;
            Clear();
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// 音声ファイルのパス
        /// </summary>
        public string FilePath
        {
            get { return _mediaPlayer.URL; }
            set { _mediaPlayer.URL = value; }
        }

        /// <summary>
        /// 音声ファイル名
        /// </summary>
        public string FileName => !_mediaPlayer.URL.Equals( string.Empty ) ? new FileInfo( _mediaPlayer.URL ).Name : string.Empty;

        /// <summary>
        /// ループ再生するorしない
        /// </summary>
        public bool LoopMode
        {
            get { return _mediaPlayer.settings.getMode( "loop" ); }
            set { _mediaPlayer.settings.setMode( "loop", value ); }
        }

        /// <summary>
        /// 再生中か？
        /// </summary>
        public bool IsPlaying
            => _mediaPlayer.playState.Equals( WMPPlayState.wmppsPlaying ) ||
               _mediaPlayer.playState.Equals( WMPPlayState.wmppsTransitioning );

        /// <summary>
        /// 停止したか？
        /// </summary>
        public bool IsStopped => _mediaPlayer.playState.Equals( WMPPlayState.wmppsStopped );

        /// <summary>
        /// 音量
        /// </summary>
        public int Volume
        {
            get { return _mediaPlayer.settings.volume; }
            set { _mediaPlayer.settings.volume = value; }
        }

        #endregion プロパティ

        #region 公開メソッド

        /// <summary>
        /// 再生する
        /// </summary>
        public void PlayBack()
        {
            Task.Run( () =>
             {
                 // URLが有効な場合のみ　かつ
                 // 現在再生中でない場合再生する
                 if ( !string.IsNullOrWhiteSpace( _mediaPlayer.URL ) && !IsPlaying )
                     lock ( _lockObj )
                         _mediaPlayer.controls.play();
             } );
        }

        /// <summary>
        /// 停止する
        /// </summary>
        public void Stop()
        {
            lock ( _lockObj )
                _mediaPlayer.controls.stop();
        }

        /// <summary>
        /// 設定を初期化する
        /// </summary>
        public void Clear()
        {
            lock ( _lockObj )
                _mediaPlayer.URL = string.Empty;

            Volume = INIT_VOLUME;
        }

        /// <summary>
        /// ハッシュコード値いを取得する
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
            => FilePath.GetHashCode() ^
               FileName.GetHashCode() ^
               LoopMode.GetHashCode() ^
               IsPlaying.GetHashCode() ^
               IsStopped.GetHashCode() ^
               Volume.GetHashCode();
    
        #endregion 公開メソッド
    }
}