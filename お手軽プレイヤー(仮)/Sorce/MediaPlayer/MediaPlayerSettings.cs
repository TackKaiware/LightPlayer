namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤー（1つ分）の保存用情報
    /// </summary>
    public class MediaPlayerSettings
    {
        /// <summary>
        /// XML読み書き用のデフォルトコンストラクタ
        /// </summary>
        public MediaPlayerSettings()
        {
        }

        /// <summary>
        /// 保存情報生成用の引数有りコンストラクタ
        /// </summary>
        public MediaPlayerSettings( int id, string filePath, bool loopMode, int volume )
        {
            Id = id;
            FilePath = filePath;
            LoopMode = loopMode;
            Volume = volume;
        }

        [System.Xml.Serialization.XmlElement( "id" )]
        public int Id { get; set; }

        [System.Xml.Serialization.XmlElement( "filePath" )]
        public string FilePath { get; set; }

        [System.Xml.Serialization.XmlElement( "loopMode" )]
        public bool LoopMode { get; set; }

        [System.Xml.Serialization.XmlElement( "volume" )]
        public int Volume { get; set; }
    }
}