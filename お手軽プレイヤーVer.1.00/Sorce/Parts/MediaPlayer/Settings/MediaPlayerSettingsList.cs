using System;
using System.Collections;
using System.Collections.Generic;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤー（全部）の保存情報クラス
    /// </summary>

    [System.Xml.Serialization.XmlRoot( "mediaPlyaers" )]
    public class MediaPlayerSettingsList : IEnumerable<MediaPlayerSettings>
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayerSettingsList()
        {
            SettingsList = new List<MediaPlayerSettings>();
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// メディアプレイヤー（全部）の保存情報リスト
        /// </summary>
        [System.Xml.Serialization.XmlElement( "mediaPlyaer" )]
        public List<MediaPlayerSettings> SettingsList { get; set; }

        #endregion プロパティ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの保存設定を追加する
        /// </summary>
        public void Add( MediaPlayerSettings settings )
        {
            SettingsList.Add( settings );
        }

        /// <summary>
        /// メディアプレイヤーの保存設定を削除する
        /// </summary>
        public void Remove( MediaPlayerSettings setings )
        {
            SettingsList.Remove( setings );
        }

        /// <summary>
        /// 条件にマッチした保存設定を取得する
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public MediaPlayerSettings Find( Predicate<MediaPlayerSettings> match ) => SettingsList.Find( match );

        /// <summary>
        /// IEnumerable<T>の実装
        /// </summary>
        public IEnumerator<MediaPlayerSettings> GetEnumerator()
        {
            foreach ( var settings in SettingsList )
            {
                if ( settings != null )
                {
                    yield return settings;
                }
            }
        }

        #endregion 公開メソッド

        #region 非公開メソッド

        /// <summary>
        /// IEnumerable<T>の実装
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion 非公開メソッド
    }
}