using System;
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
        private View _view;
        private Model _model;
        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Controller( View view, Model model )
        {
            _view = view;
            _model = model;
        }

        #endregion コンストラクタ

        #region 公開メソッド（コントロールのイベント）

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドロップした時の処理
        /// </summary>
        public void FileNameTextBox_DragDrop( object sender, DragEventArgs e )
        {
            var filePath = ( ( string[] )e.Data.GetData( DataFormats.FileDrop, false ) ).First();
            _model.SetFilePath( sender, filePath );
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
            _model.Play( sender );
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e )
        {
            _model.Stop( sender );
        }

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _model.SwitchLoopMode( sender );
        }

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e )
        {
            _model.ClearSettings( sender );
        }

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void trackBar_VolumeBar_Scroll( object sender, EventArgs e )
        {
            _model.SetVolume( sender );
        }

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e )
        {
            try
            {
                // 設定情報をメディアプレイヤーに読み込む
                _model.LoadSettings();
            }
            catch ( FileNotFoundException )
            {
                // 初回起動時はXMLファイルが存在しないため新規作成する
                _model.SaveSettings();
            }
            catch ( InvalidOperationException )
            {
                // 読み込み失敗時の処理
                _model.SaveSettings();
            }
        }

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                // メディアプレイヤーの設定情報を書き込む
                _model.SaveSettings();
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