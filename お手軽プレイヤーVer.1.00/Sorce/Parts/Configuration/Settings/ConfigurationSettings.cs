﻿using System.Drawing;

namespace LightPlayer
{
    /// <summary>
    /// プレイヤー設定の保存情報
    /// </summary>
    [System.Xml.Serialization.XmlRoot( "config" )]
    public class ConfigurationSettings
    {
        #region コンストラクタ

        /// <summary>
        /// XML読み書き用のデフォルトコンストラクタ
        /// </summary>
        public ConfigurationSettings()
        {
        }

        /// <summary>
        /// 保存情報生成用の引数有りコンストラクタ
        /// </summary>
        public ConfigurationSettings( bool isTopMost, bool isOpacity, bool isParallelPlayBack, Point location )
        {
            IsTopMost = isTopMost;
            IsOpacity = isOpacity;
            IsParallelPlayBack = isParallelPlayBack;
            Location = location;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// 常に手前に表示するか
        /// </summary>
        [System.Xml.Serialization.XmlElement( "isTopMost" )]
        public bool IsTopMost { get; set; }

        /// <summary>
        /// 不透明度
        /// </summary>
        [System.Xml.Serialization.XmlElement( "isOpacity" )]
        public bool IsOpacity { get; set; }

        /// <summary>
        /// 同時再生するか
        /// </summary>
        [System.Xml.Serialization.XmlElement( "isParallelPlayBack" )]
        public bool IsParallelPlayBack { get; set; }

        /// <summary>
        /// ウィンドウの位置
        /// </summary>
        [System.Xml.Serialization.XmlElement( "location" )]
        public Point Location { get; set; }

        #endregion プロパティ
    }
}