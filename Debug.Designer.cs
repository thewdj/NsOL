namespace NsOL
{
    partial class Debug
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Sender = new System.Windows.Forms.Timer(this.components);
            this.CheckSender = new System.Windows.Forms.CheckBox();
            this.TabCtrl = new System.Windows.Forms.TabControl();
            this.Tracks = new System.Windows.Forms.TabPage();
            this.Texts = new System.Windows.Forms.TabPage();
            this.Output = new System.Windows.Forms.TabPage();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.TrackBar3 = new System.Windows.Forms.TrackBar();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.TabCtrl.SuspendLayout();
            this.Tracks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // Sender
            // 
            this.Sender.Interval = 10;
            // 
            // CheckSender
            // 
            this.CheckSender.AutoSize = true;
            this.CheckSender.Location = new System.Drawing.Point(212, 233);
            this.CheckSender.Name = "CheckSender";
            this.CheckSender.Size = new System.Drawing.Size(60, 16);
            this.CheckSender.TabIndex = 0;
            this.CheckSender.Text = "Sender";
            this.CheckSender.UseVisualStyleBackColor = true;
            this.CheckSender.CheckedChanged += new System.EventHandler(this.CheckSender_CheckedChanged);
            // 
            // TabCtrl
            // 
            this.TabCtrl.Controls.Add(this.Tracks);
            this.TabCtrl.Controls.Add(this.Texts);
            this.TabCtrl.Controls.Add(this.Output);
            this.TabCtrl.HotTrack = true;
            this.TabCtrl.Location = new System.Drawing.Point(12, 12);
            this.TabCtrl.Multiline = true;
            this.TabCtrl.Name = "TabCtrl";
            this.TabCtrl.SelectedIndex = 0;
            this.TabCtrl.Size = new System.Drawing.Size(260, 215);
            this.TabCtrl.TabIndex = 1;
            // 
            // Tracks
            // 
            this.Tracks.Controls.Add(this.Label3);
            this.Tracks.Controls.Add(this.Label2);
            this.Tracks.Controls.Add(this.Label1);
            this.Tracks.Controls.Add(this.TrackBar3);
            this.Tracks.Controls.Add(this.TrackBar2);
            this.Tracks.Controls.Add(this.TrackBar1);
            this.Tracks.Location = new System.Drawing.Point(4, 22);
            this.Tracks.Name = "Tracks";
            this.Tracks.Size = new System.Drawing.Size(252, 189);
            this.Tracks.TabIndex = 0;
            this.Tracks.Text = "TrackBars";
            this.Tracks.UseVisualStyleBackColor = true;
            // 
            // Texts
            // 
            this.Texts.Location = new System.Drawing.Point(4, 22);
            this.Texts.Name = "Texts";
            this.Texts.Size = new System.Drawing.Size(252, 189);
            this.Texts.TabIndex = 1;
            this.Texts.Text = "TextBoxs";
            this.Texts.UseVisualStyleBackColor = true;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(4, 22);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(252, 189);
            this.Output.TabIndex = 2;
            this.Output.Text = "Output";
            this.Output.UseVisualStyleBackColor = true;
            // 
            // TrackBar1
            // 
            this.TrackBar1.Location = new System.Drawing.Point(0, 3);
            this.TrackBar1.Maximum = 360;
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(249, 45);
            this.TrackBar1.TabIndex = 3;
            this.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // TrackBar3
            // 
            this.TrackBar3.Location = new System.Drawing.Point(0, 125);
            this.TrackBar3.Maximum = 360;
            this.TrackBar3.Name = "TrackBar3";
            this.TrackBar3.Size = new System.Drawing.Size(249, 45);
            this.TrackBar3.TabIndex = 7;
            this.TrackBar3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBar3.Scroll += new System.EventHandler(this.TrackBar3_Scroll);
            // 
            // TrackBar2
            // 
            this.TrackBar2.Location = new System.Drawing.Point(0, 64);
            this.TrackBar2.Maximum = 360;
            this.TrackBar2.Name = "TrackBar2";
            this.TrackBar2.Size = new System.Drawing.Size(249, 45);
            this.TrackBar2.TabIndex = 6;
            this.TrackBar2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TrackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 51);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(0, 12);
            this.Label1.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(3, 112);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(0, 12);
            this.Label2.TabIndex = 8;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(3, 173);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(0, 12);
            this.Label3.TabIndex = 9;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.TabCtrl);
            this.Controls.Add(this.CheckSender);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Debug";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Debug";
            this.TabCtrl.ResumeLayout(false);
            this.Tracks.ResumeLayout(false);
            this.Tracks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Sender;
        private System.Windows.Forms.CheckBox CheckSender;
        private System.Windows.Forms.TabControl TabCtrl;
        private System.Windows.Forms.TabPage Tracks;
        private System.Windows.Forms.TabPage Texts;
        private System.Windows.Forms.TabPage Output;
        private System.Windows.Forms.TrackBar TrackBar3;
        private System.Windows.Forms.TrackBar TrackBar2;
        private System.Windows.Forms.TrackBar TrackBar1;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Label Label1;
    }
}