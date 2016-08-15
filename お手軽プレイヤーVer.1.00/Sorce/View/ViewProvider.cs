using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// 他のモデルクラスが処理に必要な
    /// ビューのプロパティとビュー上のコントロールを公開する
    /// </summary>
    public class ViewProvider
    {
        #region フィールド

        /// <summary>
        /// ビューへの参照
        /// </summary>
        private View _view;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewProvider(
            View view,
            CheckBox topMostCheckBox,
            CheckBox opacityCheckBox,
            CheckBox paralledPlayBackCheckBox,
            List<MediaPlayer> mediaPlayers )
        {
            _view = view;
            TopMostCheckBox = topMostCheckBox;
            OpacityCheckBox = opacityCheckBox;
            ParallelPlayBackCheckBox = paralledPlayBackCheckBox;
            MediaPlayers = mediaPlayers;
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// フォームの位置
        /// </summary>
        public Point Location
        {
            get { return _view.Location; }
            set { _view.Location = value; }
        }

        /// <summary>
        /// フォームを常に手前に表示するか
        /// </summary>
        public bool TopMost
        {
            get { return _view.TopMost; }
            set { _view.TopMost = value; }
        }

        /// <summary>
        /// フォームの透明度
        /// </summary>
        public double Opacity
        {
            get { return _view.Opacity; }
            set { _view.Opacity = value; }
        }

        /// <summary>
        /// 常に手前に表示するかチェックボックスへの参照
        /// </summary>
        public CheckBox TopMostCheckBox { get; }

        /// <summary>
        /// 不透明チェックボックスへの参照
        /// </summary>
        public CheckBox OpacityCheckBox { get; }

        /// <summary>
        /// 同時再生チェックボックスへの参照
        /// </summary>
        public CheckBox ParallelPlayBackCheckBox { get; }

        /// <summary>
        /// メディアプレイヤーへの参照
        /// </summary>
        public List<MediaPlayer> MediaPlayers { get; }

        #endregion プロパティ
    }
}