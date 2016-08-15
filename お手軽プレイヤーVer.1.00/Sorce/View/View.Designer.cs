namespace LightPlayer
{
    partial class View
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private System.Windows.Forms.TabControl tabControl;

        private System.Windows.Forms.TabPage tabPage_PlayBack;

        private System.Windows.Forms.TabPage tabPage_Configuration;

        private System.Windows.Forms.GroupBox groupBox_PlayBackConfig;

        private System.Windows.Forms.GroupBox groupBox_ViewConfig;

        private System.Windows.Forms.CheckBox checkBox_Opacity;

        private System.Windows.Forms.CheckBox checkBox_TopMost;

        private System.Windows.Forms.CheckBox checkBox_ParallelPlayBack;

        private System.Windows.Forms.Label label_ClearAll;

        private System.Windows.Forms.Button button_ClearAll;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( View ) );
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_PlayBack = new System.Windows.Forms.TabPage();
            this.tabPage_Configuration = new System.Windows.Forms.TabPage();
            this.groupBox_ViewConfig = new System.Windows.Forms.GroupBox();
            this.checkBox_Opacity = new System.Windows.Forms.CheckBox();
            this.checkBox_TopMost = new System.Windows.Forms.CheckBox();
            this.groupBox_PlayBackConfig = new System.Windows.Forms.GroupBox();
            this.label_ClearAll = new System.Windows.Forms.Label();
            this.button_ClearAll = new System.Windows.Forms.Button();
            this.checkBox_ParallelPlayBack = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabPage_PlayBack.SuspendLayout();
            this.tabPage_Configuration.SuspendLayout();
            this.groupBox_ViewConfig.SuspendLayout();
            this.groupBox_PlayBackConfig.SuspendLayout();
            this.SuspendLayout();

            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 100F ) );
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point( 3, 3 );
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.RowStyles.Add( new System.Windows.Forms.RowStyle( System.Windows.Forms.SizeType.Percent, 10F ) );
            this.tableLayoutPanel1.Size = new System.Drawing.Size( 532, 411 );
            this.tableLayoutPanel1.TabIndex = 0;

            //
            // tabControl
            //
            this.tabControl.Controls.Add( this.tabPage_PlayBack );
            this.tabControl.Controls.Add( this.tabPage_Configuration );
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font( "メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.tabControl.Location = new System.Drawing.Point( 0, 0 );
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size( 546, 447 );
            this.tabControl.TabIndex = 4;

            //
            // tabPage_PlayBack
            //
            this.tabPage_PlayBack.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_PlayBack.Controls.Add( this.tableLayoutPanel1 );
            this.tabPage_PlayBack.Location = new System.Drawing.Point( 4, 26 );
            this.tabPage_PlayBack.Name = "tabPage_PlayBack";
            this.tabPage_PlayBack.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage_PlayBack.Size = new System.Drawing.Size( 538, 417 );
            this.tabPage_PlayBack.TabIndex = 0;
            this.tabPage_PlayBack.Text = "再生画面";

            //
            // tabPage_Configuration
            //
            this.tabPage_Configuration.BackColor = System.Drawing.Color.Transparent;
            this.tabPage_Configuration.Controls.Add( this.groupBox_ViewConfig );
            this.tabPage_Configuration.Controls.Add( this.groupBox_PlayBackConfig );
            this.tabPage_Configuration.Location = new System.Drawing.Point( 4, 26 );
            this.tabPage_Configuration.Name = "tabPage_Configuration";
            this.tabPage_Configuration.Padding = new System.Windows.Forms.Padding( 3 );
            this.tabPage_Configuration.Size = new System.Drawing.Size( 538, 417 );
            this.tabPage_Configuration.TabIndex = 1;
            this.tabPage_Configuration.Text = "コンフィグレーション";

            //
            // groupBox_ViewConfig
            //
            this.groupBox_ViewConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox_ViewConfig.Controls.Add( this.checkBox_Opacity );
            this.groupBox_ViewConfig.Controls.Add( this.checkBox_TopMost );
            this.groupBox_ViewConfig.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.groupBox_ViewConfig.Location = new System.Drawing.Point( 8, 6 );
            this.groupBox_ViewConfig.Name = "groupBox_ViewConfig";
            this.groupBox_ViewConfig.Size = new System.Drawing.Size( 217, 77 );
            this.groupBox_ViewConfig.TabIndex = 1;
            this.groupBox_ViewConfig.TabStop = false;
            this.groupBox_ViewConfig.Text = "表示の設定";

            //
            // checkBox_Opacity
            //
            this.checkBox_Opacity.AutoSize = true;
            this.checkBox_Opacity.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_Opacity.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.checkBox_Opacity.Location = new System.Drawing.Point( 6, 50 );
            this.checkBox_Opacity.Name = "checkBox_Opacity";
            this.checkBox_Opacity.Size = new System.Drawing.Size( 99, 22 );
            this.checkBox_Opacity.TabIndex = 4;
            this.checkBox_Opacity.Text = "半透明にする";
            this.checkBox_Opacity.UseVisualStyleBackColor = true;

            //
            // checkBox_TopMost
            //
            this.checkBox_TopMost.AutoSize = true;
            this.checkBox_TopMost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_TopMost.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.checkBox_TopMost.Location = new System.Drawing.Point( 6, 23 );
            this.checkBox_TopMost.Name = "checkBox_TopMost";
            this.checkBox_TopMost.Size = new System.Drawing.Size( 111, 22 );
            this.checkBox_TopMost.TabIndex = 3;
            this.checkBox_TopMost.Text = "常に手前に表示";
            this.checkBox_TopMost.UseVisualStyleBackColor = true;

            //
            // groupBox_PlayBackConfig
            //
            this.groupBox_PlayBackConfig.Controls.Add( this.label_ClearAll );
            this.groupBox_PlayBackConfig.Controls.Add( this.button_ClearAll );
            this.groupBox_PlayBackConfig.Controls.Add( this.checkBox_ParallelPlayBack );
            this.groupBox_PlayBackConfig.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.groupBox_PlayBackConfig.Location = new System.Drawing.Point( 8, 89 );
            this.groupBox_PlayBackConfig.Name = "groupBox_PlayBackConfig";
            this.groupBox_PlayBackConfig.Size = new System.Drawing.Size( 217, 84 );
            this.groupBox_PlayBackConfig.TabIndex = 0;
            this.groupBox_PlayBackConfig.TabStop = false;
            this.groupBox_PlayBackConfig.Text = "再生の設定";

            //
            // label_ClearAll
            //
            this.label_ClearAll.AutoSize = true;
            this.label_ClearAll.Location = new System.Drawing.Point( 7, 52 );
            this.label_ClearAll.Name = "label_ClearAll";
            this.label_ClearAll.Size = new System.Drawing.Size( 164, 18 );
            this.label_ClearAll.TabIndex = 6;
            this.label_ClearAll.Text = "プレイヤー設定を初期化する";

            //
            // button_ClearAll
            //
            this.button_ClearAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ClearAll.BackColor = System.Drawing.Color.Gainsboro;
            this.button_ClearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ClearAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ClearAll.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.button_ClearAll.ForeColor = System.Drawing.Color.Red;
            this.button_ClearAll.Location = new System.Drawing.Point( 177, 50 );
            this.button_ClearAll.Name = "button_ClearAll";
            this.button_ClearAll.Size = new System.Drawing.Size( 33, 22 );
            this.button_ClearAll.TabIndex = 4;
            this.button_ClearAll.Text = "実行";
            this.button_ClearAll.UseVisualStyleBackColor = false;

            //
            // checkBox_ParallelPlayBack
            //
            this.checkBox_ParallelPlayBack.AutoSize = true;
            this.checkBox_ParallelPlayBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_ParallelPlayBack.Font = new System.Drawing.Font( "メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 128 ) ) );
            this.checkBox_ParallelPlayBack.Location = new System.Drawing.Point( 6, 23 );
            this.checkBox_ParallelPlayBack.Name = "checkBox_ParallelPlayBack";
            this.checkBox_ParallelPlayBack.Size = new System.Drawing.Size( 99, 22 );
            this.checkBox_ParallelPlayBack.TabIndex = 5;
            this.checkBox_ParallelPlayBack.Text = "同時再生する";
            this.checkBox_ParallelPlayBack.UseVisualStyleBackColor = true;

            //
            // View
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size( 546, 447 );
            this.Controls.Add( this.tabControl );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
            this.MaximizeBox = false;
            this.Name = "View";
            this.Text = "お手軽プレイヤーVer.1.00";
            this.tabControl.ResumeLayout( false );
            this.tabPage_PlayBack.ResumeLayout( false );
            this.tabPage_Configuration.ResumeLayout( false );
            this.groupBox_ViewConfig.ResumeLayout( false );
            this.groupBox_ViewConfig.PerformLayout();
            this.groupBox_PlayBackConfig.ResumeLayout( false );
            this.groupBox_PlayBackConfig.PerformLayout();
            this.ResumeLayout( false );
        }

        #endregion Windows フォーム デザイナーで生成されたコード
    }
}