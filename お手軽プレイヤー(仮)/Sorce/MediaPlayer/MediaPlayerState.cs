using System.Collections.Generic;

using static LightPlayer.MediaPlayer;

namespace LightPlayer
{
    public class MediaPlayerState
    {
        readonly List<MediaPlayer> _mediaPlayers;

        public MediaPlayerState( List<MediaPlayer> mediaPlayers )
        {
            _mediaPlayers = mediaPlayers;
        }

        public void SetState( int id, MediaPlayerStateEnum state )
        {
            switch ( state )
            {
                case MediaPlayerStateEnum.Stop:

                    _mediaPlayers.ForEach( mp =>
                    {
                        mp.LoopCheckBox.Enabled = true;
                        mp.ClearButton.Enabled = true;
                    } );
                    _mediaPlayers[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MediaPlayerStateEnum.StopFromPlaying:
                    _mediaPlayers.ForEach( mp =>
                    {
                        mp.LoopCheckBox.Enabled = false;
                        mp.ClearButton.Enabled = false;
                    } );

                    _mediaPlayers[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MediaPlayerStateEnum.Playing:

                    _mediaPlayers.ForEach( x =>
                    {
                        x.LoopCheckBox.Enabled = false;
                        x.ClearButton.Enabled = false;
                    } );
                    _mediaPlayers[id].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;
            }
        }
    }
}