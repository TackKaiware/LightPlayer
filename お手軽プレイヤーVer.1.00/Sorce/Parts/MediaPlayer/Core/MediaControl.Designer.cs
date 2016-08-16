namespace LightPlayer
{
    partial class MediaControl
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panel_ControlsGroup;

        private System.Windows.Forms.TrackBar trackBar_VolumeBar;

        private System.Windows.Forms.Button button_Clear;

        private System.Windows.Forms.CheckBox checkBox_Loop;

        private System.Windows.Forms.Button button_Stop;

        private System.Windows.Forms.Button button_Play;

        private System.Windows.Forms.TextBox textBox_FileName;

        private System.Windows.Forms.Panel panel_VolumeBar;

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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_ControlsGroup = new System.Windows.Forms.Panel();
            this.panel_VolumeBar = new System.Windows.Forms.Panel();
            this.trackBar_VolumeBar = new System.Windows.Forms.TrackBar();
            this.button_Clear = new System.Windows.Forms.Button();
            this.checkBox_Loop = new System.Windows.Forms.CheckBox();
            this.button_Stop = new System.Windows.Forms.Button();
            this.button_Play = new System.Windows.Forms.Button();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.panel_ControlsGroup.SuspendLayout();
            this.panel_VolumeBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_VolumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_ControlsGroup
            // 
            this.panel_ControlsGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_ControlsGroup.Controls.Add(this.panel_VolumeBar);
            this.panel_ControlsGroup.Controls.Add(this.button_Clear);
            this.panel_ControlsGroup.Controls.Add(this.checkBox_Loop);
            this.panel_ControlsGroup.Controls.Add(this.button_Stop);
            this.panel_ControlsGroup.Controls.Add(this.button_Play);
            this.panel_ControlsGroup.Controls.Add(this.textBox_FileName);
            this.panel_ControlsGroup.Location = new System.Drawing.Point(3, 3);
            this.panel_ControlsGroup.Name = "panel_ControlsGroup";
            this.panel_ControlsGroup.Size = new System.Drawing.Size(452, 27);
            this.panel_ControlsGroup.TabIndex = 1;
            // 
            // panel_VolumeBar
            // 
            this.panel_VolumeBar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_VolumeBar.Controls.Add(this.trackBar_VolumeBar);
            this.panel_VolumeBar.Location = new System.Drawing.Point(286, 0);
            this.panel_VolumeBar.Name = "panel_VolumeBar";
            this.panel_VolumeBar.Size = new System.Drawing.Size(108, 26);
            this.panel_VolumeBar.TabIndex = 2;
            // 
            // trackBar_VolumeBar
            // 
            this.trackBar_VolumeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.trackBar_VolumeBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBar_VolumeBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBar_VolumeBar.Location = new System.Drawing.Point(0, 0);
            this.trackBar_VolumeBar.Maximum = 100;
            this.trackBar_VolumeBar.Name = "trackBar_VolumeBar";
            this.trackBar_VolumeBar.Size = new System.Drawing.Size(108, 26);
            this.trackBar_VolumeBar.SmallChange = 5;
            this.trackBar_VolumeBar.TabIndex = 5;
            this.trackBar_VolumeBar.TickFrequency = 10;
            this.trackBar_VolumeBar.Value = 80;
            // 
            // button_Clear
            // 
            this.button_Clear.AutoSize = true;
            this.button_Clear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Clear.BackColor = System.Drawing.Color.Gainsboro;
            this.button_Clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Clear.Font = new System.Drawing.Font("メイリオ", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Clear.ForeColor = System.Drawing.Color.Blue;
            this.button_Clear.Location = new System.Drawing.Point(400, 2);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(44, 24);
            this.button_Clear.TabIndex = 4;
            this.button_Clear.Text = "クリア";
            this.button_Clear.UseVisualStyleBackColor = false;
            // 
            // checkBox_Loop
            // 
            this.checkBox_Loop.AutoSize = true;
            this.checkBox_Loop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_Loop.Font = new System.Drawing.Font("メイリオ", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_Loop.Location = new System.Drawing.Point(236, 6);
            this.checkBox_Loop.Name = "checkBox_Loop";
            this.checkBox_Loop.Size = new System.Drawing.Size(53, 18);
            this.checkBox_Loop.TabIndex = 3;
            this.checkBox_Loop.Text = "ループ";
            this.checkBox_Loop.UseVisualStyleBackColor = true;
            // 
            // button_Stop
            // 
            this.button_Stop.AutoSize = true;
            this.button_Stop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Stop.BackColor = System.Drawing.Color.Black;
            this.button_Stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Stop.Font = new System.Drawing.Font("Webdings", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.button_Stop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button_Stop.Location = new System.Drawing.Point(204, 1);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(26, 25);
            this.button_Stop.TabIndex = 2;
            this.button_Stop.Text = "<";
            this.button_Stop.UseVisualStyleBackColor = false;
            // 
            // button_Play
            // 
            this.button_Play.AutoSize = true;
            this.button_Play.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Play.BackColor = System.Drawing.Color.Black;
            this.button_Play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Play.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Play.Font = new System.Drawing.Font("Webdings", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.button_Play.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_Play.Location = new System.Drawing.Point(172, 1);
            this.button_Play.Name = "button_Play";
            this.button_Play.Size = new System.Drawing.Size(26, 25);
            this.button_Play.TabIndex = 1;
            this.button_Play.Text = "4";
            this.button_Play.UseVisualStyleBackColor = false;
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.AllowDrop = true;
            this.textBox_FileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(34)))), ((int)(((byte)(0)))));
            this.textBox_FileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_FileName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_FileName.Font = new System.Drawing.Font("メイリオ", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_FileName.ForeColor = System.Drawing.Color.YellowGreen;
            this.textBox_FileName.Location = new System.Drawing.Point(3, 3);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.ReadOnly = true;
            this.textBox_FileName.Size = new System.Drawing.Size(163, 21);
            this.textBox_FileName.TabIndex = 0;
            this.textBox_FileName.TabStop = false;
            // 
            // MediaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_ControlsGroup);
            this.Name = "MediaControl";
            this.Size = new System.Drawing.Size(458, 31);
            this.panel_ControlsGroup.ResumeLayout(false);
            this.panel_ControlsGroup.PerformLayout();
            this.panel_VolumeBar.ResumeLayout(false);
            this.panel_VolumeBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_VolumeBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion コンポーネント デザイナーで生成されたコード
    }
}