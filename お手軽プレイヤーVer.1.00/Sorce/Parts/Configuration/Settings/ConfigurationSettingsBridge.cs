namespace LightPlayer
{
    /// <summary>
    /// ConfigrationModelの情報をクラスに橋渡しするクラス
    /// </summary>
    // #よくない設計_無駄にクラスを増やしている
    public class ConfigurationSettingsBridge
    {
        #region フィールド

        /// <summary>
        /// コンフィグレーションにより変更可能なコントロール郡への参照
        /// </summary>
        private ConfigurationControls _controls;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationSettingsBridge( ConfigurationControls controls )
        {
            _controls = controls;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// 同時再生するか否か
        /// </summary>
        public bool IsParallelPlayBack => _controls.ParalledPlayBackCheckBox.Checked;

        #endregion プロパティ
    }
}