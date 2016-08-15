using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LightPlayer
{
    public partial class MediaPlayer : UserControl
    {
        #region フィールド

        /// <summary>
        /// メディアプレイヤーの状態
        /// </summary>
        private MediaPlayerState _state;

        #endregion フィールド

        #region 定数

        /// <summary>
        /// ファイル名テキストボックスの文字色（再生時）
        /// </summary>
        public static readonly Color COLOR_FILENAME_TEXTBOX_PLAYING = Color.Yellow;

        /// <summary>
        /// ファイル名テキストボックスの文字色（停止時）
        /// </summary>
        public static readonly Color COLOR_FILENAME_TEXTBOX_STOP = Color.YellowGreen;

        /// <summary>
        /// 対応ファイルの拡張子リスト
        /// </summary>
        public static readonly List<string> AVAILABLE_FILE_TYPES = new List<string>{ ".wav", ".mp3", ".mid" };

        /// <summary>
        /// ファイルパスとして不正な文字のリスト
        /// </summary>
        private static readonly List<char>INVALID_PATH_CHARS = new List<char>( Path.GetInvalidPathChars() );

        #endregion 定数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayer( int id, InvolkeWorker invokeWorker )
        {
            InitializeComponent();

            Id = id;
            FileNameTextBox.Name += $"_{ id.ToString() }";
            FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
            PlayButton.Name += $"_{ id.ToString() }";
            StopButton.Name += $"_{ id.ToString() }";
            LoopCheckBox.Name += $"_{ id.ToString() }";
            ClearButton.Name += $"_{ id.ToString() }";
            VolumeBar.Name += $"_{ id.ToString() }";
            Player = new WmpWrapper();

            _state = new MediaPlayerState( this, invokeWorker );
        }

        #endregion コンストラクタ

        #region プロパティ

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// ファイル名テキストボックス
        /// </summary>
        public TextBox FileNameTextBox => textBox_FileName;

        /// <summary>
        /// 再生ボタン
        /// </summary>
        public Button PlayButton => button_Play;

        /// <summary>
        /// 停止ボタン
        /// </summary>
        public Button StopButton => button_Stop;

        /// <summary>
        /// ループモードチェックボックス
        /// </summary>
        public CheckBox LoopCheckBox => checkBox_Loop;

        /// <summary>
        /// クリアボタン
        /// </summary>
        public Button ClearButton => button_Clear;

        /// <summary>
        /// 音量バー
        /// </summary>
        public TrackBar VolumeBar => trackBar_VolumeBar;

        /// <summary>
        /// Windowsメディアプレイヤーのラッパークラスオブジェクト
        /// </summary>
        public WmpWrapper Player { get; }

        #endregion プロパティ

        #region 公開メソッド

        /// <summary>
        /// メディアプレイヤーの各コントロールにイベントハンドラーを設定する
        /// </summary>
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

        /// <summary>
        /// 音楽ファイルのパスを設定する
        /// </summary>
        public void SetFilePath( string filePath )
        {
            // 対応可能なファイルタイプの場合のみセットする
            if ( AVAILABLE_FILE_TYPES.Contains( new FileInfo( filePath ).Extension.ToLower() ) )
            {
                Player.FilePath = filePath;
                FileNameTextBox.Text = Player.FileName;
            }
        }

        /// <summary>
        /// ループモードを切り替える
        /// </summary>
        public void SwitchLoopMode() => Player.LoopMode = !Player.LoopMode;

        /// <summary>
        /// 設定を初期化数する
        /// </summary>
        public void ClearSettings()
        {
            VolumeBar.Value = WmpWrapper.INIT_VOLUME;
            LoopCheckBox.Checked = false;
            Player.Clear();
            FileNameTextBox.Text = Player.FileName;
        }

        /// <summary>
        /// 音量を設定する
        /// </summary>
        public void SetVolume( int volume ) => Player.Volume = VolumeBar.Value = volume;

        /// <summary>
        /// 状態を設定する
        /// </summary>
        public void SetSate( MediaPlayerStateEnum state ) => _state.SetState( state );

        /// <summary>
        /// 状態を取得する
        /// </summary>
        public MediaPlayerStateEnum GetState() => _state.GetState();

        #region Objectクラスのオーバーライド

        /// <summary>
        /// 2つのオブジェクトが等価か判定する
        /// </summary>
        public override bool Equals( object obj )
        {
            if ( obj != null )
            {
                var other = obj as MediaPlayer;
                if ( GetHashCode() == other.GetHashCode() )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ハッシュコード値を取得する
        /// </summary>
        /// <returns></returns>
        // #手抜き版_おいおいやる
        public override int GetHashCode() => Id;    // IDは重複しないからたぶん大丈夫

        #endregion Objectクラスのオーバーライド

        #endregion 公開メソッド
    }
}