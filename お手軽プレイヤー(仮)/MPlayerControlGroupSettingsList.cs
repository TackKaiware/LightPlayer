using System;
using System.Collections;
using System.Collections.Generic;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーコントロール（全部）の保存用情報
    /// </summary>

    [System.Xml.Serialization.XmlRoot( "settingsList" )]
    public class MPlayerControlGroupSettingsList : IEnumerable<MPlayerControlGroupSettings>
    {
        public MPlayerControlGroupSettingsList()
        {
            SettingsList = new List<MPlayerControlGroupSettings>();
        }

        [System.Xml.Serialization.XmlElement( "settings" )]
        public List<MPlayerControlGroupSettings> SettingsList { get; set; }

        public void Add( MPlayerControlGroupSettings settings )
        {
            SettingsList.Add( settings );
        }

        public void Remove( MPlayerControlGroupSettings setings )
        {
            SettingsList.Remove( setings );
        }

        public MPlayerControlGroupSettings Find( Predicate<MPlayerControlGroupSettings> match )
            => SettingsList.Find( match );

        public IEnumerator<MPlayerControlGroupSettings> GetEnumerator()
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