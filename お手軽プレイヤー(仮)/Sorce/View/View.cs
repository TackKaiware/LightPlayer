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
        /// メディアプレイヤーコントロールのリスト
        /// </summary>
        public List<MPlayerControlGroup> MediaControls { get; }

        #endregion プロパティ

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public View()
        {
            InitializeComponent();

            // メディアプレイヤーコントロールのリストを生成
            var index = -1;
            MediaControls = new List<MPlayerControlGroup>
            {
                new MPlayerControlGroup( ++index, textBox_FileName_00, button_Play_00, button_Stop_00, checkBox_Loop_00, button_Clear_00, trackBar_VolumeBar_00, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_01, button_Play_01, button_Stop_01, checkBox_Loop_01, button_Clear_01, trackBar_VolumeBar_01, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_02, button_Play_02, button_Stop_02, checkBox_Loop_02, button_Clear_02, trackBar_VolumeBar_02, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_03, button_Play_03, button_Stop_03, checkBox_Loop_03, button_Clear_03, trackBar_VolumeBar_03, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_04, button_Play_04, button_Stop_04, checkBox_Loop_04, button_Clear_04, trackBar_VolumeBar_04, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_05, button_Play_05, button_Stop_05, checkBox_Loop_05, button_Clear_05, trackBar_VolumeBar_05, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_06, button_Play_06, button_Stop_06, checkBox_Loop_06, button_Clear_06, trackBar_VolumeBar_06, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_07, button_Play_07, button_Stop_07, checkBox_Loop_07, button_Clear_07, trackBar_VolumeBar_07, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_08, button_Play_08, button_Stop_08, checkBox_Loop_08, button_Clear_08, trackBar_VolumeBar_08, new WmpWrapper() ),
                new MPlayerControlGroup( ++index, textBox_FileName_09, button_Play_09, button_Stop_09, checkBox_Loop_09, button_Clear_09, trackBar_VolumeBar_09, new WmpWrapper() )
            };
        }

        /// <summary>
        /// メディアプレイヤコントロールーの各コントールにイベントを割り当てる
        /// </summary>
        /// <param name="controller"></param>
        public void SetEventHandler( Controller controller )
        {
            Load += controller.View_Load;
            FormClosing += controller.View_FormClosing;

            MediaControls.ForEach( x =>
            {
                x.FileNameTextBox.DragDrop += controller.FileNameTextBox_DragDrop;
                x.FileNameTextBox.DragEnter += controller.FileNameTextBox_DragEnter;
                x.PlayButton.Click += controller.PlayButton_Click;
                x.StopButton.Click += controller.StopButton_Click;
                x.LoopCheckBox.CheckedChanged += controller.LoopCheckBox_CheckedChanged;
                x.ClearButton.Click += controller.ClearButton_Click;
                x.VolumeBar.Scroll += controller.trackBar_VolumeBar_Scroll;
            } );
        }

        #endregion コンストラクタ
    }
}