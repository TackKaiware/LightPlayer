using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LightPlayer
{
    public static class MediaPlayerUtility
    {
        /// <summary>
        /// コントロールの名前の末尾からIDを取得する
        /// </summary>
        public static int GetId( object sender )
        {
            var id = default( int );
            try
            {
                id = Convert.ToInt32( ( ( Control )sender ).Name.Split( '_' ).Last() );
            }
            catch ( InvalidCastException )
            {
                Console.WriteLine( "プログラムエラー：senderがControlクラスでない" );

                // 強制終了
                Environment.Exit( Environment.ExitCode );
            }
            catch ( FormatException )
            {
                Console.WriteLine( "プログラムエラー：コントロールのNameの末尾が\"_2桁の数字\"でない" );

                // 強制終了
                Environment.Exit( Environment.ExitCode );
            }

            return id;
        }

        /// <summary>
        /// 再生中のメディアプレイヤーのIDを取得する
        /// 再生中のメディアプレイヤーが無い場合は、0を返す
        /// </summary>
        /// <param name="mediaPlayerList"></param>
        /// <returns></returns>
        public static int GetPlayingId( this List<MediaPlayer> mediaPlayerList )
        {
            var id = mediaPlayerList.IndexOf( mediaPlayerList.Find( mp => mp.Player.IsPlaying ) );
            return id > 0 ? id : 0;
        }
    }
}