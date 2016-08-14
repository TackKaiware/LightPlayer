namespace LightPlayer
{
    partial class View
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox_TopMost = new System.Windows.Forms.CheckBox();
            this.checkBox_Translucent = new System.Windows.Forms.CheckBox();
            this.button_ClearAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 34);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 392);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // checkBox_TopMost
            // 
            this.checkBox_TopMost.AutoSize = true;
            this.checkBox_TopMost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_TopMost.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_TopMost.Location = new System.Drawing.Point(186, 8);
            this.checkBox_TopMost.Name = "checkBox_TopMost";
            this.checkBox_TopMost.Size = new System.Drawing.Size(104, 21);
            this.checkBox_TopMost.TabIndex = 1;
            this.checkBox_TopMost.Text = "常に手前に表示";
            this.checkBox_TopMost.UseVisualStyleBackColor = true;
            // 
            // checkBox_Translucent
            // 
            this.checkBox_Translucent.AutoSize = true;
            this.checkBox_Translucent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkBox_Translucent.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_Translucent.Location = new System.Drawing.Point(296, 8);
            this.checkBox_Translucent.Name = "checkBox_Translucent";
            this.checkBox_Translucent.Size = new System.Drawing.Size(93, 21);
            this.checkBox_Translucent.TabIndex = 2;
            this.checkBox_Translucent.Text = "半透明にする";
            this.checkBox_Translucent.UseVisualStyleBackColor = true;
            // 
            // button_ClearAll
            // 
            this.button_ClearAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_ClearAll.BackColor = System.Drawing.Color.Gainsboro;
            this.button_ClearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_ClearAll.Font = new System.Drawing.Font("メイリオ", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_ClearAll.ForeColor = System.Drawing.Color.Red;
            this.button_ClearAll.Location = new System.Drawing.Point(395, 6);
            this.button_ClearAll.Name = "button_ClearAll";
            this.button_ClearAll.Size = new System.Drawing.Size(75, 23);
            this.button_ClearAll.TabIndex = 3;
            this.button_ClearAll.Text = "ALLクリア";
            this.button_ClearAll.UseVisualStyleBackColor = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(482, 439);
            this.Controls.Add(this.button_ClearAll);
            this.Controls.Add(this.checkBox_Translucent);
            this.Controls.Add(this.checkBox_TopMost);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "View";
            this.Text = "お手軽プレイヤーVer.1.00";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows フォーム デザイナーで生成されたコード

        private System.Windows.Forms.CheckBox checkBox_TopMost;
        private System.Windows.Forms.CheckBox checkBox_Translucent;
        private System.Windows.Forms.Button button_ClearAll;
    }
}