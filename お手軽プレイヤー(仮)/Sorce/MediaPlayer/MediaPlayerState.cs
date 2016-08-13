using System.Collections.Generic;

using static LightPlayer.MediaPlayer;

namespace LightPlayer
{
    public class MediaPlayerState
    {
        readonly List<MediaPlayer> _mediaPlayerList;

        public MediaPlayerState( List<MediaPlayer> mediaPlayerList )
        {
            _mediaPlayerList = mediaPlayerList;
        }

        public void SetState( int id, MediaPlayerStateEnum state )
        {
            switch ( state )
            {
                case MediaPlayerStateEnum.Stop:

                    _mediaPlayerList.ForEach( mp =>
                    {
                        mp.LoopCheckBox.Enabled = true;
                        mp.ClearButton.Enabled = true;
                    } );
                    _mediaPlayerList[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MediaPlayerStateEnum.StopFromPlaying:
                    _mediaPlayerList.ForEach( mp =>
                    {
                        mp.LoopCheckBox.Enabled = false;
                        mp.ClearButton.Enabled = false;
                    } );

                    _mediaPlayerList[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MediaPlayerStateEnum.Playing:

                    _mediaPlayerList.ForEach( x =>
                    {
                        x.LoopCheckBox.Enabled = false;
                        x.ClearButton.Enabled = false;
                    } );
                    _mediaPlayerList[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;
            }
        }
    }
}