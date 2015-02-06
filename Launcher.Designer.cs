namespace NsOL
{
    partial class Launcher
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.Launch = new System.Windows.Forms.Button();
            this.FullScreen = new System.Windows.Forms.CheckBox();
            this.DisplayMode = new System.Windows.Forms.FlowLayoutPanel();
            this.DMode1 = new System.Windows.Forms.RadioButton();
            this.DMode2 = new System.Windows.Forms.RadioButton();
            this.DMode3 = new System.Windows.Forms.RadioButton();
            this.DMode4 = new System.Windows.Forms.RadioButton();
            this.Label = new System.Windows.Forms.Label();
            this.DisplayMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // Launch
            // 
            this.Launch.Location = new System.Drawing.Point(25, 110);
            this.Launch.Margin = new System.Windows.Forms.Padding(1);
            this.Launch.Name = "Launch";
            this.Launch.Size = new System.Drawing.Size(96, 24);
            this.Launch.TabIndex = 0;
            this.Launch.Text = "Launch";
            this.Launch.UseVisualStyleBackColor = true;
            this.Launch.Click += new System.EventHandler(this.Launch_Click);
            // 
            // FullScreen
            // 
            this.FullScreen.AutoSize = true;
            this.FullScreen.Location = new System.Drawing.Point(21, 90);
            this.FullScreen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.FullScreen.Name = "FullScreen";
            this.FullScreen.Size = new System.Drawing.Size(84, 16);
            this.FullScreen.TabIndex = 1;
            this.FullScreen.Text = "FullScreen";
            this.FullScreen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.FullScreen.UseVisualStyleBackColor = true;
            // 
            // DisplayMode
            // 
            this.DisplayMode.Controls.Add(this.DMode1);
            this.DisplayMode.Controls.Add(this.DMode2);
            this.DisplayMode.Controls.Add(this.DMode3);
            this.DisplayMode.Controls.Add(this.DMode4);
            this.DisplayMode.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.DisplayMode.Location = new System.Drawing.Point(20, 19);
            this.DisplayMode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.DisplayMode.Name = "DisplayMode";
            this.DisplayMode.Size = new System.Drawing.Size(106, 70);
            this.DisplayMode.TabIndex = 2;
            // 
            // DMode1
            // 
            this.DMode1.AutoSize = true;
            this.DMode1.Checked = true;
            this.DMode1.Location = new System.Drawing.Point(0, 0);
            this.DMode1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.DMode1.Name = "DMode1";
            this.DMode1.Size = new System.Drawing.Size(77, 16);
            this.DMode1.TabIndex = 1;
            this.DMode1.TabStop = true;
            this.DMode1.Text = "1280×720";
            this.DMode1.UseVisualStyleBackColor = true;
            // 
            // DMode2
            // 
            this.DMode2.AutoSize = true;
            this.DMode2.Location = new System.Drawing.Point(0, 17);
            this.DMode2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.DMode2.Name = "DMode2";
            this.DMode2.Size = new System.Drawing.Size(77, 16);
            this.DMode2.TabIndex = 2;
            this.DMode2.Text = "1600×900";
            this.DMode2.UseVisualStyleBackColor = true;
            // 
            // DMode3
            // 
            this.DMode3.AutoSize = true;
            this.DMode3.Location = new System.Drawing.Point(0, 34);
            this.DMode3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.DMode3.Name = "DMode3";
            this.DMode3.Size = new System.Drawing.Size(83, 16);
            this.DMode3.TabIndex = 3;
            this.DMode3.Text = "1920×1080";
            this.DMode3.UseVisualStyleBackColor = true;
            // 
            // DMode4
            // 
            this.DMode4.AutoSize = true;
            this.DMode4.Location = new System.Drawing.Point(0, 51);
            this.DMode4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.DMode4.Name = "DMode4";
            this.DMode4.Size = new System.Drawing.Size(83, 16);
            this.DMode4.TabIndex = 4;
            this.DMode4.Text = "2560×1440";
            this.DMode4.UseVisualStyleBackColor = true;
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(19, 4);
            this.Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(107, 12);
            this.Label.TabIndex = 3;
            this.Label.Text = "Choose Resolution";
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(146, 140);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.DisplayMode);
            this.Controls.Add(this.FullScreen);
            this.Controls.Add(this.Launch);
            this.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Launcher";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NsOL Launcher";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.DisplayMode.ResumeLayout(false);
            this.DisplayMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Launch;
        private System.Windows.Forms.CheckBox FullScreen;
        private System.Windows.Forms.FlowLayoutPanel DisplayMode;
        private System.Windows.Forms.RadioButton DMode1;
        private System.Windows.Forms.RadioButton DMode2;
        private System.Windows.Forms.RadioButton DMode3;
        private System.Windows.Forms.RadioButton DMode4;
        private System.Windows.Forms.Label Label;
    }
}

