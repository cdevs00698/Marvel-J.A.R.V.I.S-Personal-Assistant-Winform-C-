namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class MarvelJPA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarvelJPA));
            this.label2 = new System.Windows.Forms.Label();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.tmrSpeech = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.verticleProgressBar16 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar15 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar14 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar13 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar12 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar11 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar10 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar9 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar8 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar7 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar6 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar5 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar4 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar3 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar2 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            this.verticleProgressBar1 = new Marvel_J.A.R.V.I.S_Personal_Assistant.VerticleProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(176, 617);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "Language :";
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.ForeColor = System.Drawing.Color.White;
            this.lblLanguage.Location = new System.Drawing.Point(288, 617);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(100, 25);
            this.lblLanguage.TabIndex = 22;
            this.lblLanguage.Text = "Language";
            this.lblLanguage.Click += new System.EventHandler(this.lblLanguage_Click);
            // 
            // cbVoice
            // 
            this.cbVoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbVoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVoice.ForeColor = System.Drawing.Color.White;
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(143, 543);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(251, 24);
            this.cbVoice.TabIndex = 23;
            this.cbVoice.SelectedIndexChanged += new System.EventHandler(this.cbVoice_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.Jarvis_clock_main;
            this.pictureBox1.Location = new System.Drawing.Point(2, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(523, 467);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // verticleProgressBar16
            // 
            this.verticleProgressBar16.Location = new System.Drawing.Point(384, 573);
            this.verticleProgressBar16.Maximum = 50;
            this.verticleProgressBar16.Name = "verticleProgressBar16";
            this.verticleProgressBar16.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar16.TabIndex = 16;
            // 
            // verticleProgressBar15
            // 
            this.verticleProgressBar15.Location = new System.Drawing.Point(368, 573);
            this.verticleProgressBar15.Maximum = 60;
            this.verticleProgressBar15.Name = "verticleProgressBar15";
            this.verticleProgressBar15.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar15.TabIndex = 15;
            // 
            // verticleProgressBar14
            // 
            this.verticleProgressBar14.Location = new System.Drawing.Point(352, 573);
            this.verticleProgressBar14.Maximum = 60;
            this.verticleProgressBar14.Name = "verticleProgressBar14";
            this.verticleProgressBar14.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar14.TabIndex = 14;
            // 
            // verticleProgressBar13
            // 
            this.verticleProgressBar13.Location = new System.Drawing.Point(336, 573);
            this.verticleProgressBar13.Maximum = 50;
            this.verticleProgressBar13.Name = "verticleProgressBar13";
            this.verticleProgressBar13.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar13.TabIndex = 13;
            // 
            // verticleProgressBar12
            // 
            this.verticleProgressBar12.Location = new System.Drawing.Point(320, 573);
            this.verticleProgressBar12.Name = "verticleProgressBar12";
            this.verticleProgressBar12.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar12.TabIndex = 12;
            // 
            // verticleProgressBar11
            // 
            this.verticleProgressBar11.Location = new System.Drawing.Point(304, 573);
            this.verticleProgressBar11.Maximum = 80;
            this.verticleProgressBar11.Name = "verticleProgressBar11";
            this.verticleProgressBar11.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar11.TabIndex = 11;
            // 
            // verticleProgressBar10
            // 
            this.verticleProgressBar10.Location = new System.Drawing.Point(287, 573);
            this.verticleProgressBar10.Maximum = 50;
            this.verticleProgressBar10.Name = "verticleProgressBar10";
            this.verticleProgressBar10.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar10.TabIndex = 10;
            // 
            // verticleProgressBar9
            // 
            this.verticleProgressBar9.Location = new System.Drawing.Point(271, 573);
            this.verticleProgressBar9.Maximum = 60;
            this.verticleProgressBar9.Name = "verticleProgressBar9";
            this.verticleProgressBar9.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar9.TabIndex = 9;
            // 
            // verticleProgressBar8
            // 
            this.verticleProgressBar8.Location = new System.Drawing.Point(255, 573);
            this.verticleProgressBar8.Maximum = 50;
            this.verticleProgressBar8.Name = "verticleProgressBar8";
            this.verticleProgressBar8.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar8.TabIndex = 8;
            // 
            // verticleProgressBar7
            // 
            this.verticleProgressBar7.Location = new System.Drawing.Point(239, 573);
            this.verticleProgressBar7.Maximum = 30;
            this.verticleProgressBar7.Name = "verticleProgressBar7";
            this.verticleProgressBar7.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar7.TabIndex = 7;
            // 
            // verticleProgressBar6
            // 
            this.verticleProgressBar6.Location = new System.Drawing.Point(223, 573);
            this.verticleProgressBar6.Name = "verticleProgressBar6";
            this.verticleProgressBar6.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar6.TabIndex = 6;
            // 
            // verticleProgressBar5
            // 
            this.verticleProgressBar5.Location = new System.Drawing.Point(207, 573);
            this.verticleProgressBar5.Maximum = 70;
            this.verticleProgressBar5.Name = "verticleProgressBar5";
            this.verticleProgressBar5.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar5.TabIndex = 5;
            // 
            // verticleProgressBar4
            // 
            this.verticleProgressBar4.Location = new System.Drawing.Point(191, 573);
            this.verticleProgressBar4.Name = "verticleProgressBar4";
            this.verticleProgressBar4.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar4.TabIndex = 4;
            // 
            // verticleProgressBar3
            // 
            this.verticleProgressBar3.Location = new System.Drawing.Point(175, 573);
            this.verticleProgressBar3.Name = "verticleProgressBar3";
            this.verticleProgressBar3.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar3.TabIndex = 3;
            // 
            // verticleProgressBar2
            // 
            this.verticleProgressBar2.Location = new System.Drawing.Point(159, 573);
            this.verticleProgressBar2.Name = "verticleProgressBar2";
            this.verticleProgressBar2.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar2.TabIndex = 2;
            // 
            // verticleProgressBar1
            // 
            this.verticleProgressBar1.Location = new System.Drawing.Point(143, 573);
            this.verticleProgressBar1.Name = "verticleProgressBar1";
            this.verticleProgressBar1.Size = new System.Drawing.Size(10, 38);
            this.verticleProgressBar1.TabIndex = 1;
            // 
            // MarvelJPA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(530, 662);
            this.Controls.Add(this.cbVoice);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.verticleProgressBar16);
            this.Controls.Add(this.verticleProgressBar15);
            this.Controls.Add(this.verticleProgressBar14);
            this.Controls.Add(this.verticleProgressBar13);
            this.Controls.Add(this.verticleProgressBar12);
            this.Controls.Add(this.verticleProgressBar11);
            this.Controls.Add(this.verticleProgressBar10);
            this.Controls.Add(this.verticleProgressBar9);
            this.Controls.Add(this.verticleProgressBar8);
            this.Controls.Add(this.verticleProgressBar7);
            this.Controls.Add(this.verticleProgressBar6);
            this.Controls.Add(this.verticleProgressBar5);
            this.Controls.Add(this.verticleProgressBar4);
            this.Controls.Add(this.verticleProgressBar3);
            this.Controls.Add(this.verticleProgressBar2);
            this.Controls.Add(this.verticleProgressBar1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MarvelJPA";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private VerticleProgressBar verticleProgressBar1;
        private VerticleProgressBar verticleProgressBar2;
        private VerticleProgressBar verticleProgressBar3;
        private VerticleProgressBar verticleProgressBar4;
        private VerticleProgressBar verticleProgressBar5;
        private VerticleProgressBar verticleProgressBar6;
        private VerticleProgressBar verticleProgressBar7;
        private VerticleProgressBar verticleProgressBar8;
        private VerticleProgressBar verticleProgressBar9;
        private VerticleProgressBar verticleProgressBar10;
        private VerticleProgressBar verticleProgressBar11;
        private VerticleProgressBar verticleProgressBar12;
        private VerticleProgressBar verticleProgressBar13;
        private VerticleProgressBar verticleProgressBar14;
        private VerticleProgressBar verticleProgressBar15;
        private VerticleProgressBar verticleProgressBar16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Timer tmrSpeech;
    }
}

