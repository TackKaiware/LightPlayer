namespace LightPlayer
{
    using System;
    using static MediaPlayer;
    using static MediaPlayerStateEnum;

    public delegate object InvolkeWorker( Action action );

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
        private MediaPlayer _mediaPlayer;

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
        public MediaPlayerState( MediaPlayer mediaPlayer, InvolkeWorker invoke )
        {
            _invokeWorker = invoke;
            _mediaPlayer = mediaPlayer;
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
                _invokeWorker( PrivateSetState );
            }
        }

        /// <summary>
        /// メディアプレイヤーの状態を取得する
        /// </summary>
        public MediaPlayerStateEnum GetState() => _currentState;

        #endregion 公開メソッド

        private void PrivateSetState()
        {
            switch ( _currentState )
            {
                // 停止中
                case Stopped:
                    _mediaPlayer.LoopCheckBox.Enabled = true;
                    _mediaPlayer.ClearButton.Enabled = true;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                // 再生中
                case Playing:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;

                // 他のメディアプレイヤーの再生により停止中にロック
                case LockedByOtherPlaying:
                    _mediaPlayer.LoopCheckBox.Enabled = false;
                    _mediaPlayer.ClearButton.Enabled = false;
                    _mediaPlayer.FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;
            }
        }
    }
}