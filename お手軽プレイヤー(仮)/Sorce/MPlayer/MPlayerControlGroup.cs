using System.Drawing;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーを構成するコントロール群
    /// </summary>
    public class MPlayerControlGroup
    {
        public static readonly Color COLOR_FILENAME_TEXTBOX_PLAYING = Color.GreenYellow;
        public static readonly Color COLOR_FILENAME_TEXTBOX_STOP = Color.Silver;

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MPlayerControlGroup(
            int index,
            TextBox fileNameTextBox,
            Button playButton,
            Button stopButton,
            CheckBox loopCheckBox,
            Button clearButton,
            TrackBar volumeBar,
            WmpWrapper player )
        {
            Index = index;
            FileNameTextBox = fileNameTextBox;
            PlayButton = playButton;
            StopButton = stopButton;
            LoopCheckBox = loopCheckBox;
            ClearButton = clearButton;
            VolumeBar = volumeBar;
            Player = player;
        }

        #endregion コンストラクタ

        #region プロパティ
        public int Index { get; }
        public TextBox FileNameTextBox { get; }
        public Button PlayButton { get; }
        public Button StopButton { get; }
        public CheckBox LoopCheckBox { get; }
        public Button ClearButton { get; }
        public TrackBar VolumeBar { get; }
        public WmpWrapper Player { get; }

        #endregion プロパティ
    }
}