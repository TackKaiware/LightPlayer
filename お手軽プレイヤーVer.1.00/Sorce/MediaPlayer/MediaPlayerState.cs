namespace LightPlayer
{
    using static MediaPlayer;

    /// <summary>
    /// メディアプレイヤーの状態を表現するクラス
    /// </summary>
    public class MediaPlayerState
    {
        #region フィールド

        /// <summary>
        /// メディアプレイヤーへの参照
        /// </summary>
        private MediaPlayer _mediaPlayer;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayerState( MediaPlayer mediaPlayer )
        {
            _mediaPlayer = mediaPlayer;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの状態を設定する
        /// </summary>
        public void SetState( MediaPlayerStateEnum state )
        {
            switch ( state )
            {
                // 停止中
                case MediaPlayerStateEnum.Stopped:
                    _mediaPlayer.LoopCheckBox.Enabled = true;
                    _mediaPlayer.ClearButton.Enabled = true;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                // 再生中
                case MediaPlayerStateEnum.Playing:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;

                // 他のメディアプレイヤーの再生により停止中にロック
                case MediaPlayerStateEnum.LockedByOtherPlaying:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;
            }
        }

        #endregion 公開メソッド
    }
}