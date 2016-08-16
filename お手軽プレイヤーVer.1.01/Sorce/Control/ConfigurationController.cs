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
        private ViewProvider _provider;

        /// <summary>
        /// コンフィグレーションモデルへの実体
        /// </summary>
        private ConfigurationModel _model;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ConfigurationController( ViewProvider provider, ConfigurationModel model )
        {
            _provider = provider;
            _model = model;
        }

        #endregion コンストラクタ

        #region イベントハンドラ

        #region IControllerの実装

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e ) => _model.StartProcess();

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e ) => _model.EndProcess();

        #endregion IControllerの実装

        /// <summary>
        /// 常に手前に表示のチェックを変更した時の処理
        /// </summary>
        public void TopMostCheckBox_CheckedChanged( object sender, EventArgs e ) => _model.SetTopMost( ( ( CheckBox )sender ).Checked );

        /// <summary>
        /// 半透明にするのチェックを変更した時の処理
        /// </summary>
        public void TranslucentCheckBox_CheckedChanged( object sender, EventArgs e ) => _model.SetOpacity( ( ( CheckBox )sender ).Checked );

        /// <summary>
        /// 同時再生するのチェックを変更した時の処理
        /// </summary>
        public void ParallelCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            var checkBox = ( CheckBox )sender;

            //- SetParallelPlayBack()の中でこのチェックボックスのチェック状態を書き換えている。
            //- 無限呼び出しを回避するため、一時的にイベントハンドラを解除し、
            //- SetParallelPlayBack()完了後、再び割り当てている。
            checkBox.CheckedChanged -= ParallelCheckBox_CheckedChanged;
            _model.SetParallel( ( ( CheckBox )sender ).Checked );
            checkBox.CheckedChanged += ParallelCheckBox_CheckedChanged;
        }

        #endregion イベントハンドラ
    }
}