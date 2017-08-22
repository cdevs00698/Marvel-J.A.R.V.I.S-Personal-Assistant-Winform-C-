namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class MediaPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MediaPlayer));
            this.closebtn = new System.Windows.Forms.Button();
            this.playlist = new System.Windows.Forms.ListBox();
            this.playbacktimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnvolumedown = new System.Windows.Forms.Button();
            this.btnvolumeup = new System.Windows.Forms.Button();
            this.btnfullscreen = new System.Windows.Forms.Button();
            this.btnnext = new System.Windows.Forms.Button();
            this.btnpre = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnpause = new System.Windows.Forms.Button();
            this.btnaddplay = new System.Windows.Forms.Button();
            this.btnplay = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fastreversebutton = new System.Windows.Forms.Button();
            this.fastfarwordbuttton = new System.Windows.Forms.Button();
            this.voicespeed = new System.Windows.Forms.TrackBar();
            this.minbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(1029, 11);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 11;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // playlist
            // 
            this.playlist.BackColor = System.Drawing.Color.Black;
            this.playlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playlist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playlist.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playlist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.playlist.FormattingEnabled = true;
            this.playlist.ItemHeight = 19;
            this.playlist.Location = new System.Drawing.Point(-2, 584);
            this.playlist.Margin = new System.Windows.Forms.Padding(4);
            this.playlist.Name = "playlist";
            this.playlist.Size = new System.Drawing.Size(1111, 190);
            this.playlist.TabIndex = 0;
            this.playlist.SelectedIndexChanged += new System.EventHandler(this.playlist_SelectedIndexChanged);
            // 
            // playbacktimer
            // 
            this.playbacktimer.Tick += new System.EventHandler(this.playbacktimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label2.Location = new System.Drawing.Point(115, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 31);
            this.label2.TabIndex = 26;
            this.label2.Text = "Jarvis Me";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(235, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 31);
            this.label1.TabIndex = 27;
            this.label1.Text = "dia Player . . .";
            // 
            // btnvolumedown
            // 
            this.btnvolumedown.BackColor = System.Drawing.Color.Transparent;
            this.btnvolumedown.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.mediaplayervolumedown;
            this.btnvolumedown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnvolumedown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnvolumedown.FlatAppearance.BorderSize = 0;
            this.btnvolumedown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnvolumedown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnvolumedown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnvolumedown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnvolumedown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnvolumedown.Location = new System.Drawing.Point(944, 645);
            this.btnvolumedown.Margin = new System.Windows.Forms.Padding(4);
            this.btnvolumedown.Name = "btnvolumedown";
            this.btnvolumedown.Size = new System.Drawing.Size(53, 49);
            this.btnvolumedown.TabIndex = 38;
            this.btnvolumedown.UseVisualStyleBackColor = false;
            this.btnvolumedown.Click += new System.EventHandler(this.btnvolumedown_Click);
            // 
            // btnvolumeup
            // 
            this.btnvolumeup.BackColor = System.Drawing.Color.Transparent;
            this.btnvolumeup.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.mediaplayervolumeupp;
            this.btnvolumeup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnvolumeup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnvolumeup.FlatAppearance.BorderSize = 0;
            this.btnvolumeup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnvolumeup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnvolumeup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnvolumeup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnvolumeup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnvolumeup.Location = new System.Drawing.Point(764, 508);
            this.btnvolumeup.Margin = new System.Windows.Forms.Padding(4);
            this.btnvolumeup.Name = "btnvolumeup";
            this.btnvolumeup.Size = new System.Drawing.Size(53, 49);
            this.btnvolumeup.TabIndex = 37;
            this.btnvolumeup.UseVisualStyleBackColor = false;
            this.btnvolumeup.Click += new System.EventHandler(this.btnvolumeup_Click);
            // 
            // btnfullscreen
            // 
            this.btnfullscreen.BackColor = System.Drawing.Color.Transparent;
            this.btnfullscreen.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.mediaplayerfullscreen;
            this.btnfullscreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnfullscreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnfullscreen.FlatAppearance.BorderSize = 0;
            this.btnfullscreen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnfullscreen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnfullscreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfullscreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfullscreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnfullscreen.Location = new System.Drawing.Point(1005, 508);
            this.btnfullscreen.Margin = new System.Windows.Forms.Padding(4);
            this.btnfullscreen.Name = "btnfullscreen";
            this.btnfullscreen.Size = new System.Drawing.Size(53, 49);
            this.btnfullscreen.TabIndex = 36;
            this.btnfullscreen.UseVisualStyleBackColor = false;
            this.btnfullscreen.Click += new System.EventHandler(this.btnfullscreen_Click);
            // 
            // btnnext
            // 
            this.btnnext.BackColor = System.Drawing.Color.Transparent;
            this.btnnext.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_next;
            this.btnnext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnnext.FlatAppearance.BorderSize = 0;
            this.btnnext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnnext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnnext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnnext.Location = new System.Drawing.Point(545, 508);
            this.btnnext.Margin = new System.Windows.Forms.Padding(4);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(53, 49);
            this.btnnext.TabIndex = 35;
            this.btnnext.UseVisualStyleBackColor = false;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // btnpre
            // 
            this.btnpre.BackColor = System.Drawing.Color.Transparent;
            this.btnpre.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_previous;
            this.btnpre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnpre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnpre.FlatAppearance.BorderSize = 0;
            this.btnpre.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnpre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnpre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnpre.Location = new System.Drawing.Point(300, 506);
            this.btnpre.Margin = new System.Windows.Forms.Padding(4);
            this.btnpre.Name = "btnpre";
            this.btnpre.Size = new System.Drawing.Size(53, 49);
            this.btnpre.TabIndex = 34;
            this.btnpre.UseVisualStyleBackColor = false;
            this.btnpre.Click += new System.EventHandler(this.btnpre_Click);
            // 
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.Transparent;
            this.btnstop.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_btnstop;
            this.btnstop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnstop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnstop.FlatAppearance.BorderSize = 0;
            this.btnstop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnstop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnstop.Location = new System.Drawing.Point(484, 506);
            this.btnstop.Margin = new System.Windows.Forms.Padding(4);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(53, 49);
            this.btnstop.TabIndex = 33;
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // btnpause
            // 
            this.btnpause.BackColor = System.Drawing.Color.Transparent;
            this.btnpause.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_btnpause;
            this.btnpause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnpause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnpause.FlatAppearance.BorderSize = 0;
            this.btnpause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnpause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnpause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnpause.Location = new System.Drawing.Point(423, 508);
            this.btnpause.Margin = new System.Windows.Forms.Padding(4);
            this.btnpause.Name = "btnpause";
            this.btnpause.Size = new System.Drawing.Size(53, 49);
            this.btnpause.TabIndex = 32;
            this.btnpause.UseVisualStyleBackColor = false;
            this.btnpause.Click += new System.EventHandler(this.btnpause_Click);
            // 
            // btnaddplay
            // 
            this.btnaddplay.BackColor = System.Drawing.Color.Transparent;
            this.btnaddplay.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.mediaplayeraddplaylist;
            this.btnaddplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnaddplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnaddplay.FlatAppearance.BorderSize = 0;
            this.btnaddplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnaddplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnaddplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnaddplay.Location = new System.Drawing.Point(672, 509);
            this.btnaddplay.Margin = new System.Windows.Forms.Padding(4);
            this.btnaddplay.Name = "btnaddplay";
            this.btnaddplay.Size = new System.Drawing.Size(53, 49);
            this.btnaddplay.TabIndex = 30;
            this.btnaddplay.UseVisualStyleBackColor = false;
            this.btnaddplay.Click += new System.EventHandler(this.btnaddplay_Click);
            // 
            // btnplay
            // 
            this.btnplay.BackColor = System.Drawing.Color.Transparent;
            this.btnplay.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_btnplay;
            this.btnplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnplay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnplay.FlatAppearance.BorderSize = 0;
            this.btnplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnplay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnplay.Location = new System.Drawing.Point(361, 506);
            this.btnplay.Margin = new System.Windows.Forms.Padding(4);
            this.btnplay.Name = "btnplay";
            this.btnplay.Size = new System.Drawing.Size(53, 49);
            this.btnplay.TabIndex = 31;
            this.btnplay.UseVisualStyleBackColor = false;
            this.btnplay.Click += new System.EventHandler(this.btnplay_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.mediaplayericon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(21, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 48);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // fastreversebutton
            // 
            this.fastreversebutton.BackColor = System.Drawing.Color.Transparent;
            this.fastreversebutton.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_btnfastreverse;
            this.fastreversebutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.fastreversebutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fastreversebutton.FlatAppearance.BorderSize = 0;
            this.fastreversebutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.fastreversebutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.fastreversebutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fastreversebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastreversebutton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fastreversebutton.Location = new System.Drawing.Point(239, 506);
            this.fastreversebutton.Margin = new System.Windows.Forms.Padding(4);
            this.fastreversebutton.Name = "fastreversebutton";
            this.fastreversebutton.Size = new System.Drawing.Size(53, 49);
            this.fastreversebutton.TabIndex = 40;
            this.fastreversebutton.UseVisualStyleBackColor = false;
            this.fastreversebutton.Click += new System.EventHandler(this.fastforwardbutton_Click);
            // 
            // fastfarwordbuttton
            // 
            this.fastfarwordbuttton.BackColor = System.Drawing.Color.Transparent;
            this.fastfarwordbuttton.BackgroundImage = global::Marvel_J.A.R.V.I.S_Personal_Assistant.Properties.Resources.media_btnfastfarword;
            this.fastfarwordbuttton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.fastfarwordbuttton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fastfarwordbuttton.FlatAppearance.BorderSize = 0;
            this.fastfarwordbuttton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.fastfarwordbuttton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.fastfarwordbuttton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fastfarwordbuttton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastfarwordbuttton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.fastfarwordbuttton.Location = new System.Drawing.Point(607, 508);
            this.fastfarwordbuttton.Margin = new System.Windows.Forms.Padding(4);
            this.fastfarwordbuttton.Name = "fastfarwordbuttton";
            this.fastfarwordbuttton.Size = new System.Drawing.Size(53, 49);
            this.fastfarwordbuttton.TabIndex = 41;
            this.fastfarwordbuttton.UseVisualStyleBackColor = false;
            this.fastfarwordbuttton.Click += new System.EventHandler(this.fastreversebutton_Click);
            // 
            // voicespeed
            // 
            this.voicespeed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.voicespeed.LargeChange = 10;
            this.voicespeed.Location = new System.Drawing.Point(826, 509);
            this.voicespeed.Margin = new System.Windows.Forms.Padding(4);
            this.voicespeed.Maximum = 100;
            this.voicespeed.Name = "voicespeed";
            this.voicespeed.Size = new System.Drawing.Size(171, 56);
            this.voicespeed.TabIndex = 100;
            this.voicespeed.TickFrequency = 0;
            this.voicespeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.voicespeed.Value = 100;
            this.voicespeed.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // minbtn
            // 
            this.minbtn.BackColor = System.Drawing.Color.Transparent;
            this.minbtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minbtn.FlatAppearance.BorderSize = 0;
            this.minbtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minbtn.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.minbtn.Location = new System.Drawing.Point(990, 4);
            this.minbtn.Margin = new System.Windows.Forms.Padding(4);
            this.minbtn.Name = "minbtn";
            this.minbtn.Size = new System.Drawing.Size(49, 46);
            this.minbtn.TabIndex = 101;
            this.minbtn.Text = "-";
            this.minbtn.UseVisualStyleBackColor = false;
            this.minbtn.Click += new System.EventHandler(this.minbtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 772);
            this.panel1.TabIndex = 103;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 57);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(821, 344);
            this.axWindowsMediaPlayer1.TabIndex = 102;
            // 
            // MediaPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1089, 772);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.minbtn);
            this.Controls.Add(this.voicespeed);
            this.Controls.Add(this.fastfarwordbuttton);
            this.Controls.Add(this.fastreversebutton);
            this.Controls.Add(this.btnvolumeup);
            this.Controls.Add(this.btnfullscreen);
            this.Controls.Add(this.btnnext);
            this.Controls.Add(this.btnpre);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnpause);
            this.Controls.Add(this.btnplay);
            this.Controls.Add(this.btnaddplay);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.playlist);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.btnvolumedown);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MediaPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediaPlayer";
            this.Load += new System.EventHandler(this.MediaPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.voicespeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.ListBox playlist;
        private System.Windows.Forms.Timer playbacktimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnvolumedown;
        private System.Windows.Forms.Button btnvolumeup;
        private System.Windows.Forms.Button btnfullscreen;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Button btnpre;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Button btnpause;
        private System.Windows.Forms.Button btnplay;
        private System.Windows.Forms.Button btnaddplay;
        private System.Windows.Forms.Button fastreversebutton;
        private System.Windows.Forms.Button fastfarwordbuttton;
        private System.Windows.Forms.TrackBar voicespeed;
        private System.Windows.Forms.Button minbtn;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Panel panel1;
    }
}