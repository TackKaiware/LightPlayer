using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーに関連するメソッド群
    /// </summary>
    public static class MediaPlayerLibrary
    {
        /// <summary>
        /// コントロールの名前の末尾からIDを取得する
        /// </summary>
        public static int GetId( this object sender )
        {
            var id = default( int );
            try
            {
                id = Convert.ToInt32( ( ( Control )sender ).Name.Split( '_' ).Last() );
            }
            catch ( InvalidCastException )
            {
                // メッセージを出力して強制終了
                Console.WriteLine( "プログラムエラー：senderがControlクラスでない" );
                Environment.Exit( Environment.ExitCode );
            }
            catch ( FormatException )
            {
                // メッセージを出力して強制終了
                Console.WriteLine( "プログラムエラー：コントロールのNameの末尾が\"_2桁の数字\"でない" );
                Environment.Exit( Environment.ExitCode );
            }

            return id;
        }

        /// <summary>
        /// 再生中のメディアプレイヤーのIDを取得する
        /// 再生中のメディアプレイヤーが無い場合は、0を返す
        /// </summary>
        public static int GetPlayingId( this List<MediaPlayer> mediaPlayers )
        {
            var id = mediaPlayers.IndexOf( mediaPlayers.Find( mp => mp.Player.IsPlaying ) );
            return id > 0 ? id : 0;
        }

        /// <summary>
        /// 指定されたパスに存在するファイルが
        /// 対応可能な音声ファイルか？
        /// </summary>
        public static bool IsSoundFile( this string path )
        {
            var ext = new FileInfo( path ).Extension;
            return MediaPlayer.AVAILABLE_FILE_TYPES.Exists( x => ext.Equals( x ) );
        }
    }
}