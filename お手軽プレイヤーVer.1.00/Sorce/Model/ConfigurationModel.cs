using System;
using System.IO;
using System.Windows.Forms;

using static LightPlayer.View;

namespace LightPlayer
{
    /// <summary>
    /// コンフィグレーションモデルクラス
    /// </summary>
    public class ConfigurationModel
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
        private static readonly string SETTINGS_XML_PATH = Environment.CurrentDirectory + @"\config.xml";

        #endregion 定数


        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationModel( ViewProvider provider )
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
        public void SetParallelPlayBack( bool check ) => _provider.ParallelPlayBackCheckBox.Checked = check;

        /// <summary>
        /// 開始処理（ビューのフォームロード時に呼ぶこと）
        /// </summary>
        public void StartProcess()
        {
            try
            {
                // 設定情報をメディアプレイヤーに読み込む
                LoadSettings();
            }
            catch ( FileNotFoundException )
            {
                // 初回起動時はXMLファイルが存在しないため新規作成する
                SaveSettings();
            }
            catch ( InvalidOperationException )
            {
                // 読み込み失敗時の処理
                SaveSettings();
            }
        }

        /// <summary>
        /// 終了処理（ビューのフォームクロージング時に呼ぶこと）
        /// </summary>
        public void EndProcces()
        {
            try
            {
                // 設定情報を書き込む
                SaveSettings();
            }
            catch ( InvalidOperationException )
            {
                // 書き込み失敗時
                MessageBox.Show( "設定の保存に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        #endregion 公開メソッド

        #region 非公開メソッド

        /// <summary>
        /// コンフィグレーション設定を読み込む
        /// </summary>
        private void LoadSettings()
        {
            //- 読み込むのXMLファイルの存在をチェックする
            //- 存在しない場合は例外をスローする
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                throw new FileNotFoundException();

            //- XMLファイルから設定情報を読み込む
            //- XmlAccesser.Read()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            var settings = ( ConfigurationSettings )XmlAccesser.Read(
                SETTINGS_XML_PATH, typeof( ConfigurationSettings ) );

            //- 読み込んだ設定情報をビューに反映する
            _provider.TopMostCheckBox.Checked = settings.TopMost;
            _provider.OpacityCheckBox.Checked = settings.Opacity;
            _provider.ParallelPlayBackCheckBox.Checked = settings.ParallelPlayBack;
            _provider.Location = settings.Location;
        }

        /// <summary>
        /// コンフィグレーション設定を保存する
        /// </summary>
        private void SaveSettings()
        {
            //- 書き込み先のXMLファイルの存在をチェックする
            //- 存在しない場合は新規作成する
            if ( !File.Exists( SETTINGS_XML_PATH ) )
                using ( var stream = File.Create( SETTINGS_XML_PATH ) )
                {
                }

            //- コンフィグレーション設定情報を生成する
            var settings = new ConfigurationSettings(
                _provider.TopMost,
                _provider.Opacity == OPACITY_FULL ? false : true,
                _provider.ParallelPlayBackCheckBox.Checked,
                _provider.Location );

            //- 設定情報をXMLファイルに書き込む
            //- XmlAccesser.Write()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            XmlAccesser.Write( SETTINGS_XML_PATH, typeof( ConfigurationSettings ), settings );
        }

        #endregion 非公開メソッド
    }
}