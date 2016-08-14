using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static LightPlayer.View;

namespace LightPlayer
{
    /// <summary>
    /// コントロールクラス
    /// </summary>
    public class Controller
    {
        #region フィールド

        /// <summary>
        /// ビューへの参照
        /// </summary>
        private View _view;

        /// <summary>
        /// モデルへの参照
        /// </summary>
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

            // 設定済みでないファイルの場合のみ設定
            if ( !_model.Exists( filePath ) )
            {
                _model.SetFilePath( sender, filePath );
            }
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
            if ( _model.IsPlayable( sender ) )
            {
                _view.SetControlsEnabled( false );
                _model.Play( sender );
            }
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e )
        {
            _view.SetControlsEnabled( true );
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
        public void VolumeBar_Scroll( object sender, EventArgs e )
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

        /// <summary>
        /// 常に手前に表示のチェックを変更した時の処理
        /// </summary>
        public void TopMostCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            // 常に手前に表示されるかを決めるboolを反転する
            _view.TopMost = !_view.TopMost;
        }

        /// <summary>
        /// 半透明にするのチェックを変更した時の処理
        /// </summary>
        public void TranslucentCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            // 不透明ならば半透明に、半透明ならば不透明にする
            _view.Opacity = _view.Opacity >= OPACITY_FULL
                                ? OPACITY_TRANSLUCENT
                                : OPACITY_FULL;
        }

        /// <summary>
        /// ALLクリアーボタン押下時の処理
        /// </summary>
        public void ClearAllButton_Click( object sender, EventArgs e )
        {
            _model.ClearAllSettings();
        }

        #endregion 公開メソッド（コントロールのイベント）
    }
}