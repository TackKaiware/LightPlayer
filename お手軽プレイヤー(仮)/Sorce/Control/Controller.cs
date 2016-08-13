using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// コントロールクラス
    /// </summary>
    public class Controller
    {
        #region フィールド
        List<MPlayerControlGroup> _mediaControlGroupList;
        Model _model;
        MPlayerControlGroupState _state;
        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Controller( View view, Model model )
        {
            _mediaControlGroupList = view.MediaControls;
            _model = model;
            _state = new MPlayerControlGroupState( _mediaControlGroupList );
        }

        #endregion コンストラクタ

        #region 公開メソッド（コントロールのイベント）

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドロップした時の処理
        /// </summary>
        public void FileNameTextBox_DragDrop( object sender, DragEventArgs e )
        {
            var index = ( ( TextBox )sender ).Index();
            var mediaControlGroup = _mediaControlGroupList[index];

            var filePath = ( ( string[] )e.Data.GetData( DataFormats.FileDrop, false ) ).First();
            mediaControlGroup.Player.FilePath = filePath;
            mediaControlGroup.FileNameTextBox.Text = mediaControlGroup.Player.FileName;
        }

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドラッグした時の処理
        /// </summary>
        public void FileNameTextBox_DragEnter( object sender, DragEventArgs e )
        {
            if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 再生ボタンを押下した時の処理
        /// </summary>
        public void PlayButton_Click( object sender, EventArgs e )
        {
            // 再生中のメディアプレイヤーは停止させる
            var stopIndex = _mediaControlGroupList.IndexOf( _mediaControlGroupList.Find( x => x.Player.IsPlaying ) );
            if ( stopIndex >= 0 )
            {
                _state.SetState( stopIndex, MPlayerControlGroupStateEnum.StopFromPlaying );
                _mediaControlGroupList[stopIndex].Player.Stop();
            }

            // 指定したメディアプレイヤーを再生する
            var mediaControlGroup = _mediaControlGroupList[( ( Button )sender ).Index()];
            mediaControlGroup.Player.Play();
            _state.SetState( mediaControlGroup.Index, MPlayerControlGroupStateEnum.Playing );
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e )
        {
            var index = ( ( Button )sender ).Index();

            _state.SetState( index, MPlayerControlGroupStateEnum.Stop );
            _mediaControlGroupList[index].Player.Stop();
        }

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            var loopCheckBox = ( CheckBox )sender;
            var index = loopCheckBox.Index();

            _mediaControlGroupList[index].Player.LoopMode = loopCheckBox.Checked;
        }

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e )
        {
            var mediaControlGroup = _mediaControlGroupList[( ( Button )sender ).Index()];

            mediaControlGroup.VolumeBar.Value = WmpWrapper.INIT_VOLUME;
            mediaControlGroup.LoopCheckBox.Checked = false;
            mediaControlGroup.Player.Clear();
            mediaControlGroup.FileNameTextBox.Text = mediaControlGroup.Player.FileName;
        }

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void trackBar_VolumeBar_Scroll( object sender, EventArgs e )
        {
            var volumeBar = ( TrackBar )sender;
            var mediaControlGroup = _mediaControlGroupList[volumeBar.Index()];

            mediaControlGroup.Player.Volume = volumeBar.Value;
        }

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e )
        {
            try
            {
                // 設定情報をメディアプレイヤーコントロールに読み込む
                _model.LoadSettings( _mediaControlGroupList );
            }
            catch ( FileNotFoundException )
            {
                // 初回起動時はXMLファイルが存在しないため新規作成する
                _model.SaveSettings( _mediaControlGroupList );
            }
            catch ( InvalidOperationException )
            {
                // 読み込み失敗時の処理
                _model.SaveSettings( _mediaControlGroupList );
            }
        }

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                // メディアプレイヤーコントロールの設定情報を書き込む
                _model.SaveSettings( _mediaControlGroupList );
            }
            catch ( InvalidOperationException )
            {
                // 書き込み失敗時
                MessageBox.Show( "設定上の保存に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        #endregion 公開メソッド（コントロールのイベント）
    }
}