namespace LightPlayer
{
    /// <summary>
    /// モデルクラス
    /// </summary>
    public class Model
    {
        public Model()
        {
            SettingManager = new MediaPlayerSettingsManager();
        }

        public MediaPlayerSettingsManager SettingManager { get; }
    }
}