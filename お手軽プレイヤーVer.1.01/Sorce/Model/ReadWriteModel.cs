using System;
using System.IO;
using System.Windows.Forms;

using static System.Windows.Forms.MessageBoxButtons;
using static System.Windows.Forms.MessageBoxIcon;

namespace LightPlayer
{
    /// <summary>
    /// プログラム開始時に設定を読み込み、
    /// プロギラム終了時に設定の書き込むモデルの抽象クラス
    /// </summary>
    public abstract class ReadWriteModel
    {
        #region 定数

        /// <summary>
        /// 設定ファイルの保存先ディレクトリ　
        /// </summary>
        private static readonly string XML_DIRECTORY_PATH = Environment.CurrentDirectory;

        #endregion 定数

        #region フィールド

        /// <summary>
        /// 設定のR/W用オブジェクト
        /// </summary>
        protected object _settings;

        /// <summary>
        /// 設定ファイルのフルパス　
        /// </summary>
        private readonly string _xmlPath;

        /// <summary>
        /// 読み書きする設定のクラス型
        /// </summary>
        private readonly Type _settingsType;

        #endregion フィールド

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected ReadWriteModel( string xmlFileName, object settings, Type settingsType )
        {
            _xmlPath = @XML_DIRECTORY_PATH + "\\" + xmlFileName;
            _settings = settings;
            _settingsType = settingsType;
        }

        #endregion コンストラクタ

        #region 公開メソッド

        /// <summary>
        /// プログラム開始時の処理（設定の読み込み処理）
        /// </summary>
        public void StartProcess()
        {
            try
            {
                ReadSettings();
            }
            catch ( FileNotFoundException )
            {
                WriteSettings();
            }
            catch ( InvalidOperationException )
            {
                WriteSettings();
            }
            finally
            {
                FinallyAtStartProcess();
            }
        }

        #endregion 公開メソッド

        /// <summary>
        /// プログラム終了時の処理（設定の書き込み処理）
        /// </summary>
        public void EndProcess()
        {
            try
            {
                WriteSettings();
            }
            catch ( InvalidOperationException )
            {
                MessageBox.Show( "設定の保存に失敗しました。", "エラー", OK, Error );
            }
            finally
            {
                FinallyAtEndProcess();
            }
        }

        #region 非公開メソッド

        /// <summary>
        /// 設定を読み込む
        /// </summary>
        private void ReadSettings()
        {
            //- 読み込むのXMLファイルの存在をチェックする
            //- 存在しない場合は例外をスローする
            if ( !File.Exists( _xmlPath ) )
                throw new FileNotFoundException();

            //- XMLファイルから設定情報を読み込む
            //- XmlAccesser.Read()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            _settings = Convert.ChangeType( XmlAccesser.Read( _xmlPath, _settingsType ), _settingsType );

            //- 読み込んだ設定を反映する
            //- このメソッドは派生先でオーバライドする(やりたい処理を書く)
            LoadSettings();
        }

        /// <summary>
        /// 設定を書き込む
        /// </summary>
        private void WriteSettings()
        {
            //- 書き込み先のXMLファイルの存在をチェックする
            //- 存在しない場合は新規作成する
            if ( !File.Exists( _xmlPath ) )
                using ( var stream = File.Create( _xmlPath ) )
                {
                }

            //- 現在の情報を設定に反映する
            //- このメソッドは派生先でオーバライドする(やりたい処理を書く)
            StoreSettings();

            //- 設定情報をXMLファイルに書き込む
            //- XmlAccesser.Write()から例外がスローされた場合は
            //- ここでcatchせずにそのままコルー元に伝播させる
            XmlAccesser.Write( _xmlPath, _settingsType, _settings );
        }

        #endregion 非公開メソッド

        #region 抽象メソッド

        /// <summary>
        /// 開始処理にてfinally節で実行する処理
        /// </summary>
        protected abstract void FinallyAtStartProcess();

        /// <summary>
        /// 終了処理にてfinally節で実行する処理
        /// </summary>

        protected abstract void FinallyAtEndProcess();

        /// <summary>
        /// R/W用オブジェクトから設定を反映する
        /// </summary>
        protected abstract void LoadSettings();

        /// <summary>
        /// 現在の設定をR/W用オブジェクトに格納する
        /// </summary>
        protected abstract void StoreSettings();

        #endregion 抽象メソッド
    }
}