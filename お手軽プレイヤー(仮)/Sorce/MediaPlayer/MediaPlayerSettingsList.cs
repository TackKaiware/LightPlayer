using System;
using System.Collections;
using System.Collections.Generic;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーコントロール（全部）の保存用情報
    /// </summary>

    [System.Xml.Serialization.XmlRoot( "settingsList" )]
    public class MediaPlayerSettingsList : IEnumerable<MediaPlayerSettings>
    {
        public MediaPlayerSettingsList()
        {
            SettingsList = new List<MediaPlayerSettings>();
        }

        [System.Xml.Serialization.XmlElement( "settings" )]
        public List<MediaPlayerSettings> SettingsList { get; set; }

        public void Add( MediaPlayerSettings settings )
        {
            SettingsList.Add( settings );
        }

        public void Remove( MediaPlayerSettings setings )
        {
            SettingsList.Remove( setings );
        }

        public MediaPlayerSettings Find( Predicate<MediaPlayerSettings> match )
            => SettingsList.Find( match );

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

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}