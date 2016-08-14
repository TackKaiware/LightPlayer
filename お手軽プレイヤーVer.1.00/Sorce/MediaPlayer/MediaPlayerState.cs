namespace LightPlayer
{
    using static MediaPlayer;

    public class MediaPlayerState
    {
        private MediaPlayer _mediaPlayer;

        public MediaPlayerState( MediaPlayer mediaPlayer )
        {
            _mediaPlayer = mediaPlayer;
        }

        public void SetState( MediaPlayerStateEnum state )
        {
            switch ( state )
            {
                case MediaPlayerStateEnum.Stopped:
                    _mediaPlayer.LoopCheckBox.Enabled = true;
                    _mediaPlayer.ClearButton.Enabled = true;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MediaPlayerStateEnum.Playing:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;

                case MediaPlayerStateEnum.LockedByOtherPlaying:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

            }
        }
    }
}