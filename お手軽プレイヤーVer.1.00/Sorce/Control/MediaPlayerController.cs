using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LightPlayer
{
    /// <summary>
    /// メディアプレイヤーコントローラクラス
    /// </summary>
    public class MediaPlayerController : IController
    {
        #region フィールド

        /// <summary>
        /// このクラスで扱うビューの情報
        /// </summary>
        private ViewProvider _viewProvider;

        /// <summary>
        /// メディアプレイヤーモデルへの参照
        /// </summary>
        private MediaPlayerModel _model;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayerController( ViewProvider viewProvider, MediaPlayerModel playerModel )
        {
            _viewProvider = viewProvider;
            _model = playerModel;
        }

        #endregion コンストラクタ

        #region イベントハンドラ

        #region IControllerの実装

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e ) => _model.StartProcess();

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e ) => _model.EndProcces();

        #endregion IControllerの実装

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドロップした時の処理
        /// </summary>
        public void FileNameTextBox_DragDrop( object sender, DragEventArgs e )
        {
            // #改善の余地あり_コードが冗長
            var paths = ( string[] )e.Data.GetData( DataFormats.FileDrop, false );

            // ファイルを1つD&Dした場合
            if ( paths.Length == 1 )
            {
                var path = paths.First();

                // ファイルの場合
                if ( File.Exists( path ) )
                {
                    // 設定済みでないファイルの場合のみ設定
                    if ( !_model.Exists( path ) )
                        _model.SetFilePath( sender, path );
                }

                // フォルダの場合
                else if ( Directory.Exists( path ) )
                {
                    // メディアプレイヤーをすべて初期化する
                    _model.ClearAllSettings();

                    // フォルダの内容から対応可能な音声ファイルのみ抽出
                    // ※再帰的にサブフォルダの内容は取得しない
                    var files = Directory.GetFiles( path )
                                         .Where( x => x.IsSoundFile() )
                                         .ToArray();

                    for ( var i = 0; i < files.Length; i++ )
                        _model.SetFilePath( i, files[i] );
                }

                // 上記以外の場合
                else
                {
                    // NOP
                }
            }

            // ファイルを2つ以上D&Dした場合
            else
            {
                var id = sender.GetId();
                for ( var index = 0; index < paths.Length; index++, id++ )
                    _model.SetFilePath( id, paths[index] );
            }
        }

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドラッグした時の処理
        /// </summary>
        public void FileNameTextBox_DragEnter( object sender, DragEventArgs e )
        {
            if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 再生ボタンを押下した時の処理
        /// </summary>
        public void PlayButton_Click( object sender, EventArgs e )
        {
            if ( _model.IsPlayable( sender ) )
                _model.PlayBack( sender, _viewProvider.ParallelPlayBackCheckBox.Checked );
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e ) => _model.Stop( sender, _viewProvider.ParallelPlayBackCheckBox.Checked );

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e ) => _model.SwitchLoopMode( sender );

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e ) => _model.ClearSettings( sender );

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void VolumeBar_Scroll( object sender, EventArgs e ) => _model.SetVolume( sender );

        /// <summary>
        /// ALLクリアーボタン押下時の処理
        /// </summary>
        public void ClearAllButton_Click( object sender, EventArgs e ) => _model.ClearAllSettings();

        #endregion イベントハンドラ
    }
}