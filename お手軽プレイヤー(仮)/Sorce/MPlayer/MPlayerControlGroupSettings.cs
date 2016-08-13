namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーコントロール（1つ分）の保存用情報
    /// </summary>
    public class MPlayerControlGroupSettings
    {
        /// <summary>
        /// XML読み書き用のデフォルトコンストラクタ
        /// </summary>
        public MPlayerControlGroupSettings()
        {
        }

        /// <summary>
        /// 保存情報生成用の引数有りコンストラクタ
        /// </summary>
        public MPlayerControlGroupSettings( int index, string filePath, bool loopMode, int volume )
        {
            Index = index;
            FilePath = filePath;
            LoopMode = loopMode;
            Volume = volume;
        }

        [System.Xml.Serialization.XmlElement( "index" )]
        public int Index { get; set; }

        [System.Xml.Serialization.XmlElement( "filePath" )]
        public string FilePath { get; set; }

        [System.Xml.Serialization.XmlElement( "loopMode" )]
        public bool LoopMode { get; set; }

        [System.Xml.Serialization.XmlElement( "volume" )]
        public int Volume { get; set; }
    }
}