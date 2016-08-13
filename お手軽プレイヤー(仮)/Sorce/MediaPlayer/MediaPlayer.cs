using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightPlayer
{
    public partial class MediaPlayer : UserControl
    {
        public static readonly Color COLOR_FILENAME_TEXTBOX_PLAYING = Color.GreenYellow;
        public static readonly Color COLOR_FILENAME_TEXTBOX_STOP = Color.Silver;

        public MediaPlayer( int id ) : this()
        {
            Id = id;
            FileNameTextBox.Name += $"_{ id.ToString() }";
            PlayButton.Name += $"_{ id.ToString() }";
            StopButton.Name += $"_{ id.ToString() }";
            LoopCheckBox.Name += $"_{ id.ToString() }";
            ClearButton.Name += $"_{ id.ToString() }";
            VolumeBar.Name += $"_{ id.ToString() }";
            Player = new WmpWrapper();
        }

        private MediaPlayer()
        {
            InitializeComponent();
        }

        public int Id { get; }

        public TextBox FileNameTextBox => textBox_FileName;

        public Button PlayButton => button_Play;

        public Button StopButton => button_Stop;

        public CheckBox LoopCheckBox => checkBox_Loop;

        public Button ClearButton => button_Clear;

        public TrackBar VolumeBar => trackBar_VolumeBar;

        public WmpWrapper Player { get; }

        public void SetEventHandler(
            DragEventHandler fileNameTextBox_DragEnter,
            DragEventHandler fileNameTextBox_DragDrop,
            EventHandler playButton_Click,
            EventHandler stopPlayButton_Click,
            EventHandler loopCheckBox_CheckedChanged,
            EventHandler clearButton_Click,
            EventHandler volumeBar_Scroll )
        {
            FileNameTextBox.DragEnter += fileNameTextBox_DragEnter;
            FileNameTextBox.DragDrop += fileNameTextBox_DragDrop;
            PlayButton.Click += playButton_Click;
            StopButton.Click += stopPlayButton_Click;
            LoopCheckBox.CheckedChanged += loopCheckBox_CheckedChanged;
            ClearButton.Click += clearButton_Click;
            VolumeBar.Scroll += volumeBar_Scroll;
        }
    }
}