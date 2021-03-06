﻿namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤー（1つ分）の保存情報
    /// </summary>
    public class MediaPlayerSettingsItem
    {
        #region コンストラクタ

        /// <summary>
        /// XML読み書き用のデフォルトコンストラクタ
        /// </summary>
        public MediaPlayerSettingsItem()
        {
        }

        /// <summary>
        /// 保存情報生成用の引数有りコンストラクタ-
        /// </summary>
        public MediaPlayerSettingsItem( int id, string filePath, bool loopMode, int volume )
        {
            Id = id;
            FilePath = filePath;
            LoopMode = loopMode;
            Volume = volume;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// ID
        /// </summary>
        [System.Xml.Serialization.XmlElement( "id" )]
        public int Id { get; set; }

        /// <summary>
        /// ファイルパス
        /// </summary>
        [System.Xml.Serialization.XmlElement( "filePath" )]
        public string FilePath { get; set; }

        /// <summary>
        /// ループモード
        /// </summary>
        [System.Xml.Serialization.XmlElement( "loopMode" )]
        public bool LoopMode { get; set; }

        /// <summary>
        /// 音量
        /// </summary>
        [System.Xml.Serialization.XmlElement( "volume" )]
        public int Volume { get; set; }

        #endregion プロパティ
    }
}