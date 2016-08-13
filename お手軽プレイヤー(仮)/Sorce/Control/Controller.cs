﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using static LightPlayer.MediaPlayerUtility;

namespace LightPlayer
{
    /// <summary>
    /// コントロールクラス
    /// </summary>
    public class Controller
    {
        #region フィールド
        List<MediaPlayer> _mediaPlayerList;
        Model _model;
        MediaPlayerState _state;
        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Controller( View view, Model model )
        {
            _mediaPlayerList = view.MediaPlayerList;
            _model = model;
            _state = new MediaPlayerState( _mediaPlayerList );
        }

        #endregion コンストラクタ

        #region 公開メソッド（コントロールのイベント）

        /// <summary>
        /// ファイル名テキストボックスにアイテムをドロップした時の処理
        /// </summary>
        public void FileNameTextBox_DragDrop( object sender, DragEventArgs e )
        {
            var filePath = ( ( string[] )e.Data.GetData( DataFormats.FileDrop, false ) ).First();
            _mediaPlayerList[GetId( sender )].SetFilePath( filePath );
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
            var id = _mediaPlayerList.GetPlayingId();
            _state.SetState( id, MediaPlayerStateEnum.StopFromPlaying );
            _mediaPlayerList[id].Player.Stop();

            // 指定したメディアプレイヤーを再生する
            var mediaPlayer = _mediaPlayerList[GetId( sender )];
            mediaPlayer.Player.Play();
            _state.SetState( mediaPlayer.Id, MediaPlayerStateEnum.Playing );
        }

        /// <summary>
        /// 停止ボタンを押下した時の処理
        /// </summary>
        public void StopButton_Click( object sender, EventArgs e )
        {
            var index = GetId( sender );

            _state.SetState( index, MediaPlayerStateEnum.Stop );
            _mediaPlayerList[index].Player.Stop();
        }

        /// <summary>
        /// ループ再生チェックボックスを変更した時の処理
        /// </summary>
        public void LoopCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            _mediaPlayerList[GetId( sender )].SwitchLoopMode();
        }

        /// <summary>
        /// クリアボタンを押下した時の処理
        /// </summary>
        public void ClearButton_Click( object sender, EventArgs e )
        {
            _mediaPlayerList[GetId( sender )].ClearSettings();
        }

        /// <summary>
        /// 音量バーをスクロールした時の処理
        /// </summary>
        public void trackBar_VolumeBar_Scroll( object sender, EventArgs e )
        {
            var volume = ( ( TrackBar )sender ).Value;
            _mediaPlayerList[GetId( sender )].SetVolume( volume );
        }

        /// <summary>
        /// フォームを開く時の処理
        /// </summary>
        public void View_Load( object sender, EventArgs e )
        {
            try
            {
                // 設定情報をメディアプレイヤーに読み込む
                _model.SettingManager.Load( _mediaPlayerList );
            }
            catch ( FileNotFoundException )
            {
                // 初回起動時はXMLファイルが存在しないため新規作成する
                _model.SettingManager.Save( _mediaPlayerList );
            }
            catch ( InvalidOperationException )
            {
                // 読み込み失敗時の処理
                _model.SettingManager.Save( _mediaPlayerList );
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
                _model.SettingManager.Save( _mediaPlayerList );
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