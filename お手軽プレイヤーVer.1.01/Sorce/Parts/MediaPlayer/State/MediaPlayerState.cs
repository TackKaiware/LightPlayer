namespace LightPlayer
{
    using static MediaControl;
    using static MediaPlayerStateEnum;

    /// <summary>
    /// メディアプレイヤーの状態を表現するクラス
    /// </summary>
    public class MediaPlayerState
    {
        #region フィールド

        /// <summary>
        /// 排他制御のためのロックオブジェクト
        /// </summary>
        private static object _lockObj = new object();

        /// <summary>
        /// メディアプレイヤーへの参照
        /// </summary>
        private MediaControl _mediaControl;

        /// <summary>
        /// 現在の状態
        /// </summary>
        private MediaPlayerStateEnum _currentState;

        private InvolkeWorker _invokeWorker;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayerState( MediaControl mediaPlayer, InvolkeWorker invoke )
        {
            _invokeWorker = invoke;
            _mediaControl = mediaPlayer;
            _currentState = Stopped;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの状態を設定する
        /// </summary>
        public void SetState( MediaPlayerStateEnum state )
        {
            var lockObject = new object();
            lock ( lockObject )
            {
                // 現在の状態を更新
                _currentState = state;

                // 他スレッドから呼び出しもこれで安心
                _invokeWorker( SetState );
            }
        }

        /// <summary>
        /// メディアプレイヤーの状態を取得する
        /// </summary>
        public MediaPlayerStateEnum GetState() => _currentState;

        #endregion 公開メソッド

        private void SetState()
        {
            switch ( _currentState )
            {
                // 再生中
                case Playing:
                    _mediaControl.LoopCheckBox.Enabled = false;
                    _mediaControl.ClearButton.Enabled = false;
                    _mediaControl.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    _mediaControl.Player.PlayBack();
                    break;

                // 停止中
                case Stopped:
                    _mediaControl.LoopCheckBox.Enabled = true;
                    _mediaControl.ClearButton.Enabled = true;
                    _mediaControl.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    _mediaControl.Player.Stop();
                    break;

                // 他のメディアプレイヤーの再生により停止中
                case StoppedByOtherPlayBack:
                    _mediaControl.LoopCheckBox.Enabled = false;
                    _mediaControl.ClearButton.Enabled = false;
                    _mediaControl.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    _mediaControl.Player.Stop();
                    break;
            }
        }
    }
}