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
        private ViewProvider _provider;

        /// <summary>
        /// メディアプレイヤーモデルへの参照
        /// </summary>
        private MediaPlayerModel _model;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediaPlayerController( ViewProvider provider, MediaPlayerModel model )
        {
            _provider = provider;
            _model = model;
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
        /// ファイル名テキストボックスにアイテムをドロップした時の処理
        /// </summary>
        public void FileNameTextBox_DragDrop( object sender, DragEventArgs e )
        {
            var paths = ( string[] )e.Data.GetData( DataFormats.FileDrop, false );

            if ( paths.Count() > 1 )

                // ２つ以上のファイルをD&Dした場合
                DroppedMultiFiles( sender, paths );
            else

                // 1つのファイルをD&Dした場合
                DroppedSingleFile( sender, paths.First() );
        }

        /// <summary>
        /// 再生ボタンを押下した時の処理
        /// </summary>
        public void PlayButton_Click( object sender, EventArgs e )
        {
            if ( _model.IsPlayable( sender.GetId() ) )
                _model.PlayBack( sender.GetId(), _provider.ParallelPlayBackCheckBox.Checked );
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e ) => _model.Stop( sender.GetId(), _provider.ParallelPlayBackCheckBox.Checked );

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e ) => _model.SwitchLoopMode( sender.GetId() );

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e ) => _model.ClearSettings( sender.GetId() );

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void VolumeBar_Scroll( object sender, EventArgs e ) => _model.SetVolume( sender.GetId(), ( ( TrackBar )sender ).Value );

        /// <summary>
        /// ALLクリアーボタン押下時の処理
        /// </summary>
        public void ClearAllButton_Click( object sender, EventArgs e ) => _model.ClearAllSettings();

        #endregion イベントハンドラ

        #region 非公開メソッド

        /// <summary>
        /// １つのファイルがドラッグ＆ドロップされた場合の処理
        /// </summary>
        private void DroppedSingleFile( object sender, string path )
        {
            // ドロップされたアイテムがファイルの場合でかつ、
            // メディアプレイヤーに設定済みでない場合
            if ( File.Exists( path ) && !_model.Exists( path ) )
                _model.SetFilePath( sender.GetId(), path );

            // フォルダの場合
            else if ( Directory.Exists( path ) )
            {
                // メディアプレイヤーをすべて初期化する
                _model.ClearAllSettings();

                // フォルダの内容から対応可能な音声ファイルのみ抽出
                // ※再帰的にサブフォルダの内容は取得しない
                var id = 0;
                Directory.GetFiles( path )
                         .Where( p => MediaPlayer.AVAILABLE_FILE_TYPES.Contains( new FileInfo( p ).Extension ) )
                         .ToList()
                         .ForEach( f => _model.SetFilePath( id++, f ) );
            }
        }

        /// <summary>
        /// 複数のファイルがドラッグ＆ドロップされた場合の処理
        /// </summary>
        private void DroppedMultiFiles( object sender, string[] paths )
        {
            var id = sender.GetId();
            for ( var index = 0; index < paths.Length; index++, id++ )
                _model.SetFilePath( id, paths[index] );
        }

        #endregion 非公開メソッド
    }
}