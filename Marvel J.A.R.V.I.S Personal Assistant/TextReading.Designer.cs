namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class TextReading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextReading));
            this.txtWords = new System.Windows.Forms.RichTextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.stopbtn = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.openbtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.voicespeed = new System.Windows.Forms.TrackBar();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.minbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWords
            // 
            this.txtWords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWords.ForeColor = System.Drawing.Color.White;
            this.txtWords.Location = new System.Drawing.Point(15, 124);
            this.txtWords.Margin = new System.Windows.Forms.Padding(4);
            this.txtWords.Name = "txtWords";
            this.txtWords.Size = new System.Drawing.Size(1169, 337);
            this.txtWords.TabIndex = 0;
            this.txtWords.Text = "";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPlay.Location = new System.Drawing.Point(16, 562);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(283, 38);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.Text = "Start Reading";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPause.Location = new System.Drawing.Point(307, 562);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(288, 38);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.BackColor = System.Drawing.Color.Transparent;
            this.stopbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.stopbtn.Location = new System.Drawing.Point(603, 562);
            this.stopbtn.Margin = new System.Windows.Forms.Padding(4);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(283, 38);
            this.stopbtn.TabIndex = 3;
            this.stopbtn.Text = "Stop";
            this.stopbtn.UseVisualStyleBackColor = false;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDownload.Location = new System.Drawing.Point(893, 562);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(291, 38);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "Convert Text To Audio";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(441, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Text File";
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(1137, 18);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 6;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // openbtn
            // 
            this.openbtn.BackColor = System.Drawing.Color.Transparent;
            this.openbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.openbtn.Location = new System.Drawing.Point(15, 84);
            this.openbtn.Margin = new System.Windows.Forms.Padding(4);
            this.openbtn.Name = "openbtn";
            this.openbtn.Size = new System.Drawing.Size(580, 37);
            this.openbtn.TabIndex = 7;
            this.openbtn.Text = "Open Text File";
            this.openbtn.UseVisualStyleBackColor = false;
            this.openbtn.Click += new System.EventHandler(this.openbtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(556, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 31);
            this.label2.TabIndex = 9;
            this.label2.Text = "Reader . . . ";
            // 
            // lblSpeed
            // 
            this.lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblSpeed.Location = new System.Drawing.Point(28, 480);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(135, 17);
            this.lblSpeed.TabIndex = 11;
            this.lblSpeed.Text = "Select Voice Speed:";
            // 
            // voicespeed
            // 
            this.voicespeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.voicespeed.Location = new System.Drawing.Point(16, 500);
            this.voicespeed.Margin = new System.Windows.Forms.Padding(4);
            this.voicespeed.Name = "voicespeed";
            this.voicespeed.Size = new System.Drawing.Size(1168, 56);
            this.voicespeed.TabIndex = 10;
            this.voicespeed.Value = 5;
            this.voicespeed.ValueChanged += new System.EventHandler(this.voicespeed_ValueChanged);
            // 
            // cbVoice
            // 
            this.cbVoice.BackColor = System.Drawing.Color.Black;
            this.cbVoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbVoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbVoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(603, 85);
            this.cbVoice.Margin = new System.Windows.Forms.Padding(4);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(580, 33);
            this.cbVoice.TabIndex = 12;
            // 
            // minbtn
            // 
            this.minbtn.BackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.minbtn.Location = new System.Drawing.Point(1083, 18);
            this.minbtn.Margin = new System.Windows.Forms.Padding(4);
            this.minbtn.Name = "minbtn";
            this.minbtn.Size = new System.Drawing.Size(47, 38);
            this.minbtn.TabIndex = 14;
            this.minbtn.Text = "--";
            this.minbtn.UseVisualStyleBackColor = false;
            this.minbtn.Click += new System.EventHandler(this.minbtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 615);
            this.panel1.TabIndex = 15;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // TextReading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 615);
            this.Controls.Add(this.minbtn);
            this.Controls.Add(this.cbVoice);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.voicespeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.openbtn);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtWords);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TextReading";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "reading";
            this.Load += new System.EventHandler(this.reading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtWords;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Button openbtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar voicespeed;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Button minbtn;
        private System.Windows.Forms.Panel panel1;
    }
}