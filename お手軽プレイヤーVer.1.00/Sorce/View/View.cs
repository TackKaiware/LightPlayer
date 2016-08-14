using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// ビュークラス
    /// </summary>
    public partial class View : Form
    {
        #region プロパティ

        /// <summary>
        /// メディアプレイヤーのリスト
        /// </summary>
        public List<MediaPlayer> MediaPlayers { get; }

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
            {
                MediaPlayers.Add( new MediaPlayer( id ) );
            }

            // 画面に追加
            tableLayoutPanel1.Controls.AddRange( MediaPlayers.ToArray() );
        }

        /// <summary>
        /// メディアプレイヤーの各コントールにイベントを割り当てる
        /// </summary>
        /// <param name="controller"></param>
        public void SetEventHandler( Controller controller )
        {
            Load += controller.View_Load;
            FormClosing += controller.View_FormClosing;

            MediaPlayers.ForEach( mp => mp.SetEventHandler(
                controller.FileNameTextBox_DragDrop,
                controller.FileNameTextBox_DragEnter,
                controller.PlayButton_Click,
                controller.StopButton_Click,
                controller.LoopCheckBox_CheckedChanged,
                controller.ClearButton_Click,
                controller.trackBar_VolumeBar_Scroll ) );
        }

        #endregion コンストラクタ

        private void checkBox_TopMost_CheckedChanged( object sender, System.EventArgs e )
        {
            TopMost = !TopMost;
        }

        private void checkBox_Translucent_CheckedChanged( object sender, System.EventArgs e )
        {
            Opacity = Opacity >= 1.0 ? 0.7 : 1.0; 
        }
    }
}