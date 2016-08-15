using System.Collections.Generic;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// ビュークラス
    /// </summary>
    public partial class View : Form
    {
        #region フィールド

        /// <summary>
        /// メディアプレイヤーのリスト
        /// </summary>
        private List<MediaPlayer> _mediaPlayers;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// 不透明度（100%）
        /// </summary>
        public const double OPACITY_FULL = 1.0;

        /// <summary>
        /// 不透明度(半透明)
        /// </summary>
        public const double OPACITY_TRANSLUCENT = 0.7;

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public View()
        {
            InitializeComponent();

            // メディアプレイヤーのリストを生成
            _mediaPlayers = new List<MediaPlayer>();
            for ( var id = 0; id < tableLayoutPanel1.RowCount; id++ )
                _mediaPlayers.Add( new MediaPlayer( id, Invoke ) );

            // 画面に追加
            tableLayoutPanel1.Controls.AddRange( _mediaPlayers.ToArray() );
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの各コントールにイベントを割り当てる
        /// </summary>
        /// <param name="controller"></param>
        public void SetEventHandler( MediaPlayerController playerCont, ConfigurationController congifCont )
        {
            // コンフィグレーションコントローラのイベントハンドラを設定
            Load += congifCont.View_Load;
            FormClosing += congifCont.View_FormClosing;
            checkBox_TopMost.CheckedChanged += congifCont.TopMostCheckBox_CheckedChanged;
            checkBox_Opacity.CheckedChanged += congifCont.TranslucentCheckBox_CheckedChanged;
            checkBox_ParallelPlayBack.CheckedChanged += congifCont.ParallelPlayBackCheckBox_CheckedChanged;

            // メディアプレイヤーコントローラのイベントハンドラを設定
            Load += playerCont.View_Load;
            FormClosing += playerCont.View_FormClosing;
            button_ClearAll.Click += playerCont.ClearAllButton_Click;
            foreach ( var mp in _mediaPlayers )
            {
                mp.SetEventHandler(
                    playerCont.FileNameTextBox_DragDrop,
                    playerCont.FileNameTextBox_DragEnter,
                    playerCont.PlayButton_Click,
                    playerCont.StopButton_Click,
                    playerCont.LoopCheckBox_CheckedChanged,
                    playerCont.ClearButton_Click,
                    playerCont.VolumeBar_Scroll );
            }
        }

        /// <summary>
        /// 他のモデルクラスが操作する情報を提供する
        /// </summary>
        /// <returns></returns>
        public ViewProvider Provide()
            => new ViewProvider( this, checkBox_TopMost, checkBox_Opacity, checkBox_ParallelPlayBack, _mediaPlayers );

        #endregion 公開メソッド
    }
}