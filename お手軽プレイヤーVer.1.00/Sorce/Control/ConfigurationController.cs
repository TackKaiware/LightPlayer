using System;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// コンフィグレーションコントローラクラス
    /// </summary>
    public class ConfigurationController : IController
    {
        #region フィールド

        /// <summary>
        /// このクラスで扱うビューの情報
        /// </summary>
        private ViewProvider _viewProvider;

        /// <summary>
        /// コンフィグレーションモデルへの実体
        /// </summary>
        private ConfigurationModel _model;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationController( ViewProvider viewProvider, ConfigurationModel model )
        {
            _viewProvider = viewProvider;
            _model = model;
        }

        #endregion コンストラクタ

        #region イベントハンドラ

        #region IControllerの実装

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e )
            => _model.StartProcess();

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e )
            => _model.EndProcces();

        #endregion IControllerの実装

        /// <summary>
        /// 常に手前に表示のチェックを変更した時の処理
        /// </summary>
        public void TopMostCheckBox_CheckedChanged( object sender, EventArgs e )
            => _model.SetTopMost( ( ( CheckBox )sender ).Checked );

        /// <summary>
        /// 半透明にするのチェックを変更した時の処理
        /// </summary>
        public void TranslucentCheckBox_CheckedChanged( object sender, EventArgs e )
            => _model.SetOpacity( ( ( CheckBox )sender ).Checked );

        /// <summary>
        /// 同時再生するのチェックを変更した時の処理
        /// </summary>
        public void ParallelPlayBackCheckBox_CheckedChanged( object sender, EventArgs e )
            => _model.SetParallelPlayBack( ( ( CheckBox )sender ).Checked );

        #endregion イベントハンドラ
    }
}