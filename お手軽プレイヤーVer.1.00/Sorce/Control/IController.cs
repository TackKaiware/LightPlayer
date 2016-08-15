using System;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// コントローラの共通インターフェース
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        void View_Load( object sender, EventArgs e );

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        void View_FormClosing( object sender, FormClosingEventArgs e );
    }
}