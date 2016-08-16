using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    ///　コンフィグレーションにより変更可能なコントロール群
    /// </summary>
    //　Viewで生成しConfigurationModelに参照を渡す
    public class ConfigurationControls
    {
        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationControls(
            View view,
            CheckBox topMostCheckBox,
            CheckBox opacityCheckBox,
            CheckBox paralledPlayBackCheckBox )
        {
            View = view;
            TopMostCheckBox = topMostCheckBox;
            OpacityCheckBox = opacityCheckBox;
            ParalledPlayBackCheckBox = paralledPlayBackCheckBox;
        }

        #endregion コンストラクタ

        /// <summary>
        /// ビューへの参照
        /// </summary>
        public View View { get; }

        /// <summary>
        /// 常に手前に表示するかチェックボックスへの表示
        /// </summary>
        public CheckBox TopMostCheckBox { get; }

        /// <summary>
        /// 不透明チェックボックスへの表示
        /// </summary>
        public CheckBox OpacityCheckBox { get; }

        /// <summary>
        /// 同時再生チェックボックスへの表示
        /// </summary>
        public CheckBox ParalledPlayBackCheckBox { get; }
    }
}