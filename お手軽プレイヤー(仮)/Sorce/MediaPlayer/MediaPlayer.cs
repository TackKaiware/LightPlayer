using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LightPlayer
{
    public partial class MediaPlayer : UserControl
    {
        #region 定数
        public static readonly Color COLOR_FILENAME_TEXTBOX_PLAYING = Color.GreenYellow;
        public static readonly Color COLOR_FILENAME_TEXTBOX_STOP = Color.Silver;
        private static readonly List<string> AVAILABLE_FILE_TYPES = new List<string>{ ".wav", ".mp3", ".mid" };
        private static readonly List<char>INVALID_PATH_CHARS = new List<char>( Path.GetInvalidPathChars() );
        #endregion 定数

        #region コンストラクタ

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

        #endregion コンストラクタ

        #region プロパティ
        public int Id { get; }
        public TextBox FileNameTextBox => textBox_FileName;
        public Button PlayButton => button_Play;
        public Button StopButton => button_Stop;
        public CheckBox LoopCheckBox => checkBox_Loop;
        public Button ClearButton => button_Clear;
        public TrackBar VolumeBar => trackBar_VolumeBar;
        public WmpWrapper Player { get; }
        private MediaPlayerState State { get; }
        #endregion プロパティ

        #region 公開メソッド

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

        public void SetFilePath( string filePath )
        {
            // 対応可能なファイルタイプの場合のみセットする
            var fileInfo = new FileInfo( filePath );
            if ( AVAILABLE_FILE_TYPES.Contains( fileInfo.Extension.ToLower() ) )
            {
                Player.FilePath = filePath;
                FileNameTextBox.Text = Player.FileName;
            }
        }

        public void SwitchLoopMode()
            => Player.LoopMode = !Player.LoopMode;

        public void ClearSettings()
        {
            VolumeBar.Value = WmpWrapper.INIT_VOLUME;
            LoopCheckBox.Checked = false;
            Player.Clear();
            FileNameTextBox.Text = Player.FileName;
        }

        public void SetVolume( int volume )
            => Player.Volume = VolumeBar.Value = volume;

        public void SetSate( MediaPlayerStateEnum state )
            => State.SetState( Id, state );

        #endregion 公開メソッド
    }
}