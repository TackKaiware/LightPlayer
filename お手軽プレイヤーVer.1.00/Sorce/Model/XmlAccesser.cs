using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LightPlayer
{
    /// <summary>
    /// XMLファイル読み書きクラス
    /// </summary>
    public static class XmlAccesser
    {
        /// <summary>
        /// XMLの内容をオブジェクトに復元する
        /// </summary>
        public static object Read( string xmlPath, Type type )
        {
            using ( var reader = new StreamReader( xmlPath, new UTF8Encoding( false ) ) )
            {
                var serializer = new XmlSerializer( type );
                return ( serializer.Deserialize( reader ) );
            }
        }

        /// <summary>
        /// オブジェクトの内容をXMLファイルに保存する
        /// </summary>
        public static void Write( string xmlPath, Type type, object obj )
        {
            using ( var writer = new StreamWriter( xmlPath, false, new UTF8Encoding( false ) ) )
            {
                var serializer = new XmlSerializer( type );
                serializer.Serialize( writer, obj );
            }
        }
    }
}