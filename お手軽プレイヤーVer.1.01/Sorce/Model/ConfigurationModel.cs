using System;

using static LightPlayer.View;

using SettingsType = LightPlayer.ConfigurationSettings;

namespace LightPlayer
{
    /// <summary>
    /// コンフィグレーションモデルクラス
    /// </summary>
    public class ConfigurationModel : ReadWriteModel
    {
        #region フィールド

        /// <summary>
        /// 処理に必要なビューの情報
        /// </summary>
        private ViewProvider _provider;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// コンフィグレーション設定保存用XMLファイルのフルパス
        /// </summary>
        private static readonly string SETTINGS_FILE_NAME = "config.xml";

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationModel( ViewProvider provider )
            : base( SETTINGS_FILE_NAME, new SettingsType(), typeof( SettingsType ) )
        {
            _provider = provider;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// 常に手前に表示するか否を設定する
        /// </summary>
        public void SetTopMost( bool check ) => _provider.TopMost = check;

        /// <summary>
        /// 不透明度を設定する
        /// </summary>
        public void SetOpacity( bool check ) => _provider.Opacity = ( check ? OPACITY_TRANSLUCENT : OPACITY_FULL );

        /// <summary>
        /// 同時再生するか否かを設定する
        /// </summary>
        public void SetParallel( bool check ) => _provider.ParallelCheckBox.Checked = check;

        #region ReadWriteModelの抽象メソッドの実装

        /// <summary>
        /// 開始処理にてfinally節で実行する処理
        /// </summary>
        protected override void FinallyAtStartProcess() { /* 何もしない */ }

        /// <summary>
        /// 終了処理にてfinally節で実行する処理
        /// </summary>

        protected override void FinallyAtEndProcess() { /* 何もしない */ }

        /// <summary>
        /// R/W用オブジェクトから設定を反映する
        /// </summary>
        protected override void LoadSettings()
        {
            //- 読み込んだ設定情報をビューに反映する
            var settings = ( SettingsType )Convert.ChangeType( _settings, typeof( SettingsType ) );
            _provider.TopMostCheckBox.Checked = settings.TopMost;
            _provider.OpacityCheckBox.Checked = settings.Opacity;
            _provider.ParallelCheckBox.Checked = settings.Parallel;
            _provider.Location = settings.Location;
        }

        /// <summary>
        /// 現在の設定をR/W用オブジェクトに格納する
        /// </summary>
        protected override void StoreSettings()
        {
            //- コンフィグレーション設定情報を生成する
            _settings = new SettingsType(
                _provider.TopMost,
                _provider.Opacity == OPACITY_FULL ? false : true,
                _provider.ParallelCheckBox.Checked,
                _provider.Location );
        }

        #endregion ReadWriteModelの抽象メソッドの実装

        #endregion 公開メソッド
    }
}