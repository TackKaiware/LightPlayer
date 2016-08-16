namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤー状態列挙体
    /// </summary>
    public enum MediaPlayerStateEnum
    {
        /// <summary>
        /// 再生中
        /// </summary>
        Playing,

        /// <summary>
        /// 停止中
        /// </summary>
        Stopped,

        /// <summary>
        /// 他のメディアプレイヤーの再生により停止中
        /// </summary>
        StoppedByOtherPlayBack
    }
}