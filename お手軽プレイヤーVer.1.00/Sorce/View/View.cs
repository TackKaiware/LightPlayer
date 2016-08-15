using System.Collections.Generic;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// ビュークラス
    /// </summary>
    public partial class View : Form
    {
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

        #region プロパティ

        /// <summary>
        /// メディアプレイヤーのリスト
        /// </summary>
        public List<MediaPlayer> MediaPlayers { get; }

        /// <summary>
        /// コンフィグレーションにより変更可能なコントロール群
        /// </summary>
        public ConfigurationControls ConfigControls { get; }

        #endregion プロパティ

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public View()
        {
            InitializeComponent();

            // メディアプレイヤーのリストを生成
            MediaPlayers = new List<MediaPlayer>();
            for ( var id = 0; id < tableLayoutPanel1.RowCount; id++ )

                // #よくない設計_Viewを参照している
                MediaPlayers.Add( new MediaPlayer( id, this ) );

            // 画面に追加
            tableLayoutPanel1.Controls.AddRange( MediaPlayers.ToArray() );

            // コンフィグレーションにより変更可能なコントロール群を設定
            ConfigControls = new ConfigurationControls(
                this,
                checkBox_TopMost,
                checkBox_Opacity,
                checkBox_ParallelPlayBack );
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの各コントールにイベントを割り当てる
        /// </summary>
        /// <param name="controller"></param>
        public void SetEventHandler( Controller controller )
        {
            // フォームの設定
            Load += controller.View_Load;
            FormClosing += controller.View_FormClosing;

            // フォーム上のコントローラーの設定
            checkBox_TopMost.CheckedChanged += controller.TopMostCheckBox_CheckedChanged;
            checkBox_Opacity.CheckedChanged += controller.TranslucentCheckBox_CheckedChanged;
            checkBox_ParallelPlayBack.CheckedChanged += controller.ParallelPlayBackCheckBox_CheckedChanged;
            button_ClearAll.Click += controller.ClearAllButton_Click;

            // メディアプレイヤーの設定
            foreach ( var mp in MediaPlayers )
            {
                mp.SetEventHandler(
                    controller.FileNameTextBox_DragDrop,
                    controller.FileNameTextBox_DragEnter,
                    controller.PlayButton_Click,
                    controller.StopButton_Click,
                    controller.LoopCheckBox_CheckedChanged,
                    controller.ClearButton_Click,
                    controller.VolumeBar_Scroll );
            }
        }

        #endregion 公開メソッド
    }
}