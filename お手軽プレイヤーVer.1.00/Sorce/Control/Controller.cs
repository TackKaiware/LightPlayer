﻿using System;
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

        /// <summary>
        /// ビューへの参照
        /// </summary>
        private View _view;

        /// <summary>
        /// メディアプレイヤーモデルへの参照
        /// </summary>
        private MediaPlayerModel _playerModel;

        /// <summary>
        /// コンフィグレーションモデルへの実体
        /// </summary>
        private ConfigurationModel _configModel;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Controller( View view, MediaPlayerModel playerModel, ConfigurationModel configModel )
        {
            _view = view;
            _playerModel = playerModel;
            _configModel = configModel;
        }

        #endregion コンストラクタ

        #region 公開メソッド（コントロールのイベント）

        #region メディアプレイヤー

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
                    if ( !_playerModel.Exists( path ) )
                        _playerModel.SetFilePath( sender, path );
                }

                // フォルダの場合
                else if ( Directory.Exists( path ) )
                {
                    // メディアプレイヤーをすべて初期化する
                    _playerModel.ClearAllSettings();

                    // フォルダの内容から対応可能な音声ファイルのみ抽出
                    // ※再帰的にサブフォルダの内容は取得しない
                    var files = Directory.GetFiles( path )
                                         .Where( x => x.IsSoundFile() )
                                         .ToArray();

                    for ( var i = 0; i < files.Length; i++ )
                        _playerModel.SetFilePath( i, files[i] );
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
                    _playerModel.SetFilePath( id, paths[index] );
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
            if ( _playerModel.IsPlayable( sender ) )
            {
                _playerModel.PlayBack( sender, _configModel.IsParallelPlayback );
            }
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e )
        {
            _playerModel.Stop( sender, _configModel.IsParallelPlayback );
        }

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _playerModel.SwitchLoopMode( sender );
        }

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e )
        {
            _playerModel.ClearSettings( sender );
        }

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void VolumeBar_Scroll( object sender, EventArgs e )
        {
            _playerModel.SetVolume( sender );
        }

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e )
        {
            // モデルを初期化する
            _playerModel.StartProcess();
            _configModel.StartProcess();
        }

        /// <summary>
        /// フォームを閉じる時の処理
        /// </summary>
        public void View_FormClosing( object sender, FormClosingEventArgs e )
        {
            // モデルの終了処理を実行する
            _playerModel.EndProcces();
            _configModel.EndProcces();
        }

        #endregion メディアプレイヤー

        #region コンフィグレーション

        /// <summary>
        /// 常に手前に表示のチェックを変更した時の処理
        /// </summary>
        public void TopMostCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _configModel.SetTopMost( ( ( CheckBox )sender ).Checked );
        }

        /// <summary>
        /// 半透明にするのチェックを変更した時の処理
        /// </summary>
        public void TranslucentCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _configModel.SetOpacity( ( ( CheckBox )sender ).Checked );
        }

        /// <summary>
        /// 同時再生するのチェックを変更した時の処理
        /// </summary>
        public void ParallelPlayBackCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _configModel.SetParallelPlayBack( ( ( CheckBox )sender ).Checked );

            // 変更時はのメディアプレーヤーを停止させる
            _playerModel.StopAll();
        }

        /// <summary>
        /// ALLクリアーボタン押下時の処理
        /// </summary>
        public void ClearAllButton_Click( object sender, EventArgs e )
        {
            _playerModel.ClearAllSettings();
        }

        #endregion コンフィグレーション

        #endregion 公開メソッド（コントロールのイベント）
    }
}