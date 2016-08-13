using System.Collections.Generic;

namespace LightPlayer
{
    /// <summary>
    /// モデルクラス
    /// </summary>
    public class Model
    {
        public void SaveSettings( List<MPlayerControlGroup> controlGroupList )
        {
            MPlayerControlGroupSettingsManager.Save( controlGroupList );
        }

        public void LoadSettings( List<MPlayerControlGroup> controlGroupList )
        {
            MPlayerControlGroupSettingsManager.Load( controlGroupList );
        }
    }
}