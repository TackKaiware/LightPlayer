using System.Collections.Generic;
using System.IO;
using WMPLib;

namespace LightPlayer
{
    /// <summary>
    /// WindowsMediaPlayerのラッパークラス
    /// </summary>
    public class WmpWrapper
    {
        #region 定数
        public const int INIT_VOLUME = 80;
        static readonly List<string> AVAILABLE_FILE_TYPES = new List<string>{ ".wav", ".mp3", ".mid" };
        static readonly List<char>INVALID_PATH_CHARS = new List<char>( Path.GetInvalidPathChars() );
        #endregion 定数

        readonly WindowsMediaPlayer _mediaPlayer = new WindowsMediaPlayer();

        public WmpWrapper()
        {
            _mediaPlayer.settings.autoStart = false;
            Clear();
        }

        public WmpWrapper( string filePath, bool loopMode, int volume )
        {
            FilePath = filePath;
            LoopMode = loopMode;
            Volume = volume;

            _mediaPlayer.settings.autoStart = false;
        }

        public string FilePath
        {
            get { return _mediaPlayer.URL; }
            set { _mediaPlayer.URL = value; }
        }

        public string FileName
            => !_mediaPlayer.URL.Equals( string.Empty )
                    ? new FileInfo( _mediaPlayer.URL ).Name
                    : string.Empty;

        public bool LoopMode
        {
            get { return _mediaPlayer.settings.getMode( "loop" ); }
            set { _mediaPlayer.settings.setMode( "loop", value ); }
        }

        public bool IsPlaying
            => _mediaPlayer.playState.Equals( WMPPlayState.wmppsPlaying );

        public int Volume
        {
            get { return _mediaPlayer.settings.volume; }
            set { _mediaPlayer.settings.volume = value; }
        }

        public void Play()
        {
            // URLが有効な場合のみ再生する
            if ( !string.IsNullOrWhiteSpace( _mediaPlayer.URL ) )
                _mediaPlayer.controls.play();
        }

        public void Stop()
        {
            _mediaPlayer.controls.stop();
        }

        public void Clear()
        {
            _mediaPlayer.URL = string.Empty;
            Volume = INIT_VOLUME;
        }
    }
}