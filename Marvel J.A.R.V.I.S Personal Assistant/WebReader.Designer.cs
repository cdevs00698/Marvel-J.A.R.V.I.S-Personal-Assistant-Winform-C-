namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class WebReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebReader));
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.inputurltxt = new System.Windows.Forms.TextBox();
            this.webreaderbrow = new System.Windows.Forms.WebBrowser();
            this.labelnet = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.stopbtn = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.convertedtxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._lblSpeed = new System.Windows.Forms.Label();
            this.voicespeed = new System.Windows.Forms.TrackBar();
            this.searchbtn = new System.Windows.Forms.Button();
            this.yousaidtxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.minbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "Web";
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(1220, 15);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 28;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // inputurltxt
            // 
            this.inputurltxt.AcceptsReturn = true;
            this.inputurltxt.BackColor = System.Drawing.Color.Black;
            this.inputurltxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputurltxt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputurltxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.inputurltxt.Location = new System.Drawing.Point(25, 70);
            this.inputurltxt.Margin = new System.Windows.Forms.Padding(4);
            this.inputurltxt.Multiline = true;
            this.inputurltxt.Name = "inputurltxt";
            this.inputurltxt.Size = new System.Drawing.Size(1150, 48);
            this.inputurltxt.TabIndex = 35;
            this.inputurltxt.TextChanged += new System.EventHandler(this.inputurltxt_TextChanged);
            // 
            // webreaderbrow
            // 
            this.webreaderbrow.Location = new System.Drawing.Point(25, 126);
            this.webreaderbrow.Margin = new System.Windows.Forms.Padding(4);
            this.webreaderbrow.MinimumSize = new System.Drawing.Size(27, 25);
            this.webreaderbrow.Name = "webreaderbrow";
            this.webreaderbrow.Size = new System.Drawing.Size(1241, 411);
            this.webreaderbrow.TabIndex = 36;
            this.webreaderbrow.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webreaderbrow_DocumentCompleted);
            // 
            // labelnet
            // 
            this.labelnet.AutoSize = true;
            this.labelnet.BackColor = System.Drawing.Color.Black;
            this.labelnet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labelnet.Location = new System.Drawing.Point(153, 654);
            this.labelnet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelnet.Name = "labelnet";
            this.labelnet.Size = new System.Drawing.Size(57, 20);
            this.labelnet.TabIndex = 39;
            this.labelnet.Text = "Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(28, 654);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 20);
            this.label7.TabIndex = 38;
            this.label7.Text = "Connected :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(24, 691);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 43;
            this.label2.Text = "Website Reader ";
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Transparent;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPlay.Location = new System.Drawing.Point(259, 656);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(143, 31);
            this.btnPlay.TabIndex = 44;
            this.btnPlay.Text = "Start Reading";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.BackColor = System.Drawing.Color.Transparent;
            this.stopbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.stopbtn.Location = new System.Drawing.Point(596, 655);
            this.stopbtn.Margin = new System.Windows.Forms.Padding(4);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(143, 31);
            this.stopbtn.TabIndex = 45;
            this.stopbtn.Text = "Stop ";
            this.stopbtn.UseVisualStyleBackColor = false;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnPause.Location = new System.Drawing.Point(427, 656);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(143, 31);
            this.btnPause.TabIndex = 46;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDownload.Location = new System.Drawing.Point(760, 655);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(175, 31);
            this.btnDownload.TabIndex = 47;
            this.btnDownload.Text = "Convert Text To Audio";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // convertedtxt
            // 
            this.convertedtxt.BackColor = System.Drawing.Color.Black;
            this.convertedtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.convertedtxt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertedtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.convertedtxt.Location = new System.Drawing.Point(24, 728);
            this.convertedtxt.Margin = new System.Windows.Forms.Padding(4);
            this.convertedtxt.Multiline = true;
            this.convertedtxt.Name = "convertedtxt";
            this.convertedtxt.ReadOnly = true;
            this.convertedtxt.Size = new System.Drawing.Size(1242, 131);
            this.convertedtxt.TabIndex = 51;
            this.convertedtxt.Text = "Drag And Drop Text Or Simply copy The Text On Web Broswer . . .";
            this.convertedtxt.TextChanged += new System.EventHandler(this.convertedtxt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(87, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 25);
            this.label4.TabIndex = 52;
            this.label4.Text = "Compiler . . .";
            // 
            // _lblSpeed
            // 
            this._lblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblSpeed.AutoSize = true;
            this._lblSpeed.Location = new System.Drawing.Point(951, 578);
            this._lblSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblSpeed.Name = "_lblSpeed";
            this._lblSpeed.Size = new System.Drawing.Size(53, 17);
            this._lblSpeed.TabIndex = 58;
            this._lblSpeed.Text = "Speed:";
            // 
            // voicespeed
            // 
            this.voicespeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.voicespeed.Location = new System.Drawing.Point(1029, 572);
            this.voicespeed.Margin = new System.Windows.Forms.Padding(4);
            this.voicespeed.Name = "voicespeed";
            this.voicespeed.Size = new System.Drawing.Size(237, 56);
            this.voicespeed.TabIndex = 57;
            this.voicespeed.Value = 5;
            // 
            // searchbtn
            // 
            this.searchbtn.BackColor = System.Drawing.Color.Transparent;
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.searchbtn.Location = new System.Drawing.Point(1184, 69);
            this.searchbtn.Margin = new System.Windows.Forms.Padding(4);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(83, 49);
            this.searchbtn.TabIndex = 60;
            this.searchbtn.Text = "Go";
            this.searchbtn.UseVisualStyleBackColor = false;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click);
            // 
            // yousaidtxt
            // 
            this.yousaidtxt.AcceptsReturn = true;
            this.yousaidtxt.BackColor = System.Drawing.Color.Black;
            this.yousaidtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yousaidtxt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yousaidtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.yousaidtxt.Location = new System.Drawing.Point(137, 601);
            this.yousaidtxt.Margin = new System.Windows.Forms.Padding(4);
            this.yousaidtxt.Multiline = true;
            this.yousaidtxt.Name = "yousaidtxt";
            this.yousaidtxt.Size = new System.Drawing.Size(1129, 28);
            this.yousaidtxt.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(29, 604);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 20);
            this.label5.TabIndex = 63;
            this.label5.Text = "You Said :";
            // 
            // cbVoice
            // 
            this.cbVoice.BackColor = System.Drawing.Color.Black;
            this.cbVoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbVoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cbVoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Location = new System.Drawing.Point(23, 553);
            this.cbVoice.Margin = new System.Windows.Forms.Padding(4);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(1243, 33);
            this.cbVoice.TabIndex = 70;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // minbtn
            // 
            this.minbtn.BackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.minbtn.Location = new System.Drawing.Point(1165, 15);
            this.minbtn.Margin = new System.Windows.Forms.Padding(4);
            this.minbtn.Name = "minbtn";
            this.minbtn.Size = new System.Drawing.Size(47, 38);
            this.minbtn.TabIndex = 71;
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
            this.panel1.Size = new System.Drawing.Size(1296, 878);
            this.panel1.TabIndex = 72;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // WebReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1296, 878);
            this.Controls.Add(this.minbtn);
            this.Controls.Add(this.cbVoice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.yousaidtxt);
            this.Controls.Add(this.searchbtn);
            this.Controls.Add(this._lblSpeed);
            this.Controls.Add(this.voicespeed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.convertedtxt);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelnet);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.webreaderbrow);
            this.Controls.Add(this.inputurltxt);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WebReader";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebReader";
            this.Load += new System.EventHandler(this.WebReader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.TextBox inputurltxt;
        private System.Windows.Forms.WebBrowser webreaderbrow;
        private System.Windows.Forms.Label labelnet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox convertedtxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _lblSpeed;
        private System.Windows.Forms.TrackBar voicespeed;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.TextBox yousaidtxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button minbtn;
        private System.Windows.Forms.Panel panel1;
    }
}