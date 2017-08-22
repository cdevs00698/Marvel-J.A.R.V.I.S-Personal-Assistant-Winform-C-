namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class Reminder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reminder));
            this.openmusicbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.btnset = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.time_n = new System.Windows.Forms.Label();
            this.time_s = new System.Windows.Forms.Label();
            this.time_now = new System.Windows.Forms.Timer(this.components);
            this.time_set = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.settimetxt = new System.Windows.Forms.TextBox();
            this.filename = new System.Windows.Forms.Label();
            this.musicnamebox = new System.Windows.Forms.Label();
            this.timereplacetxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.reminderset = new System.Windows.Forms.Label();
            this.remindermsgtxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.resetbtn = new System.Windows.Forms.Button();
            this.daysofweek = new System.Windows.Forms.TextBox();
            this.labelnet = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.newstxt = new System.Windows.Forms.ListBox();
            this.speakbtn = new System.Windows.Forms.Button();
            this.spokentxt = new System.Windows.Forms.TextBox();
            this.autoselectvoice = new System.Windows.Forms.ComboBox();
            this.minbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Media = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.Media)).BeginInit();
            this.SuspendLayout();
            // 
            // openmusicbtn
            // 
            this.openmusicbtn.BackColor = System.Drawing.Color.Transparent;
            this.openmusicbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openmusicbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openmusicbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.openmusicbtn.Location = new System.Drawing.Point(3, 94);
            this.openmusicbtn.Margin = new System.Windows.Forms.Padding(4);
            this.openmusicbtn.Name = "openmusicbtn";
            this.openmusicbtn.Size = new System.Drawing.Size(785, 38);
            this.openmusicbtn.TabIndex = 8;
            this.openmusicbtn.Text = "Open music file for Wakeup Alarm";
            this.openmusicbtn.UseVisualStyleBackColor = false;
            this.openmusicbtn.Click += new System.EventHandler(this.openmusicbtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 31);
            this.label1.TabIndex = 9;
            this.label1.Text = "Jarvis Digital";
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(1201, 17);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 10;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // btnset
            // 
            this.btnset.BackColor = System.Drawing.Color.Transparent;
            this.btnset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnset.Location = new System.Drawing.Point(469, 581);
            this.btnset.Margin = new System.Windows.Forms.Padding(4);
            this.btnset.Name = "btnset";
            this.btnset.Size = new System.Drawing.Size(320, 38);
            this.btnset.TabIndex = 12;
            this.btnset.Text = "Set  Alarm  And  Reminder";
            this.btnset.UseVisualStyleBackColor = false;
            this.btnset.Click += new System.EventHandler(this.btnset_Click);
            // 
            // btnstop
            // 
            this.btnstop.BackColor = System.Drawing.Color.Transparent;
            this.btnstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnstop.Location = new System.Drawing.Point(5, 581);
            this.btnstop.Margin = new System.Windows.Forms.Padding(4);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(201, 38);
            this.btnstop.TabIndex = 13;
            this.btnstop.Text = "Stop  Reminder";
            this.btnstop.UseVisualStyleBackColor = false;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // time_n
            // 
            this.time_n.AutoSize = true;
            this.time_n.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.time_n.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_n.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.time_n.Location = new System.Drawing.Point(118, 240);
            this.time_n.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.time_n.Name = "time_n";
            this.time_n.Size = new System.Drawing.Size(158, 31);
            this.time_n.TabIndex = 15;
            this.time_n.Text = "00:00:00:00";
            // 
            // time_s
            // 
            this.time_s.AutoSize = true;
            this.time_s.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.time_s.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time_s.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.time_s.Location = new System.Drawing.Point(123, 362);
            this.time_s.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.time_s.Name = "time_s";
            this.time_s.Size = new System.Drawing.Size(158, 31);
            this.time_s.TabIndex = 16;
            this.time_s.Text = "00:00:00:00";
            // 
            // time_now
            // 
            this.time_now.Enabled = true;
            this.time_now.Interval = 1000;
            this.time_now.Tick += new System.EventHandler(this.time_now_Tick);
            // 
            // time_set
            // 
            this.time_set.Enabled = true;
            this.time_set.Tick += new System.EventHandler(this.time_set_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(97, 192);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 31);
            this.label2.TabIndex = 19;
            this.label2.Text = "Time Date Now ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(65, 316);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 31);
            this.label3.TabIndex = 20;
            this.label3.Text = "Alarm And Reminder";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(467, 297);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 31);
            this.label4.TabIndex = 21;
            this.label4.Text = "Set Alarm  :";
            // 
            // settimetxt
            // 
            this.settimetxt.BackColor = System.Drawing.Color.Black;
            this.settimetxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settimetxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.settimetxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settimetxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.settimetxt.Location = new System.Drawing.Point(469, 332);
            this.settimetxt.Margin = new System.Windows.Forms.Padding(4);
            this.settimetxt.Multiline = true;
            this.settimetxt.Name = "settimetxt";
            this.settimetxt.Size = new System.Drawing.Size(319, 43);
            this.settimetxt.TabIndex = 22;
            this.settimetxt.Text = "00:00:00:AM";
            // 
            // filename
            // 
            this.filename.AutoSize = true;
            this.filename.BackColor = System.Drawing.Color.Black;
            this.filename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filename.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.filename.Location = new System.Drawing.Point(2, 134);
            this.filename.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(147, 31);
            this.filename.TabIndex = 23;
            this.filename.Text = "File name :";
            // 
            // musicnamebox
            // 
            this.musicnamebox.AutoSize = true;
            this.musicnamebox.BackColor = System.Drawing.Color.Black;
            this.musicnamebox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.musicnamebox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.musicnamebox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.musicnamebox.Location = new System.Drawing.Point(167, 144);
            this.musicnamebox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.musicnamebox.Name = "musicnamebox";
            this.musicnamebox.Size = new System.Drawing.Size(279, 20);
            this.musicnamebox.TabIndex = 24;
            this.musicnamebox.Text = "File name will gose here . . .";
            // 
            // timereplacetxt
            // 
            this.timereplacetxt.BackColor = System.Drawing.Color.Black;
            this.timereplacetxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timereplacetxt.ForeColor = System.Drawing.Color.Black;
            this.timereplacetxt.Location = new System.Drawing.Point(57, 529);
            this.timereplacetxt.Margin = new System.Windows.Forms.Padding(4);
            this.timereplacetxt.Multiline = true;
            this.timereplacetxt.Name = "timereplacetxt";
            this.timereplacetxt.Size = new System.Drawing.Size(320, 32);
            this.timereplacetxt.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Black;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(465, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 31);
            this.label6.TabIndex = 28;
            this.label6.Text = "Set Reminder  :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(139, 442);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 31);
            this.label7.TabIndex = 30;
            this.label7.Text = "Reminder  ";
            // 
            // reminderset
            // 
            this.reminderset.AutoSize = true;
            this.reminderset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reminderset.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reminderset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.reminderset.Location = new System.Drawing.Point(49, 481);
            this.reminderset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.reminderset.Name = "reminderset";
            this.reminderset.Size = new System.Drawing.Size(322, 39);
            this.reminderset.TabIndex = 31;
            this.reminderset.Text = "02/11/2015 3:33 PM";
            // 
            // remindermsgtxt
            // 
            this.remindermsgtxt.BackColor = System.Drawing.Color.Black;
            this.remindermsgtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remindermsgtxt.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remindermsgtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.remindermsgtxt.Location = new System.Drawing.Point(471, 415);
            this.remindermsgtxt.Margin = new System.Windows.Forms.Padding(4);
            this.remindermsgtxt.Multiline = true;
            this.remindermsgtxt.Name = "remindermsgtxt";
            this.remindermsgtxt.Size = new System.Drawing.Size(315, 146);
            this.remindermsgtxt.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(469, 379);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(223, 31);
            this.label9.TabIndex = 33;
            this.label9.Text = "Write Reminder  :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(187, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(205, 31);
            this.label8.TabIndex = 34;
            this.label8.Text = "Alarm Clock . . .";
            // 
            // resetbtn
            // 
            this.resetbtn.BackColor = System.Drawing.Color.Transparent;
            this.resetbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.resetbtn.Location = new System.Drawing.Point(229, 581);
            this.resetbtn.Margin = new System.Windows.Forms.Padding(4);
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.Size = new System.Drawing.Size(201, 38);
            this.resetbtn.TabIndex = 35;
            this.resetbtn.Text = "Reset  Reminder";
            this.resetbtn.UseVisualStyleBackColor = false;
            this.resetbtn.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // daysofweek
            // 
            this.daysofweek.BackColor = System.Drawing.Color.Black;
            this.daysofweek.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.daysofweek.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.daysofweek.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.daysofweek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.daysofweek.Location = new System.Drawing.Point(469, 246);
            this.daysofweek.Margin = new System.Windows.Forms.Padding(4);
            this.daysofweek.Multiline = true;
            this.daysofweek.Name = "daysofweek";
            this.daysofweek.Size = new System.Drawing.Size(319, 43);
            this.daysofweek.TabIndex = 36;
            this.daysofweek.Text = "DAY OF WEEK";
            // 
            // labelnet
            // 
            this.labelnet.AutoSize = true;
            this.labelnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labelnet.Location = new System.Drawing.Point(134, 545);
            this.labelnet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelnet.Name = "labelnet";
            this.labelnet.Size = new System.Drawing.Size(99, 17);
            this.labelnet.TabIndex = 38;
            this.labelnet.Text = "Current Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(3, 545);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 17);
            this.label5.TabIndex = 37;
            this.label5.Text = "Connection Status :";
            // 
            // newstxt
            // 
            this.newstxt.BackColor = System.Drawing.Color.Black;
            this.newstxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newstxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.newstxt.FormattingEnabled = true;
            this.newstxt.ItemHeight = 16;
            this.newstxt.Location = new System.Drawing.Point(3, 635);
            this.newstxt.Margin = new System.Windows.Forms.Padding(4);
            this.newstxt.Name = "newstxt";
            this.newstxt.Size = new System.Drawing.Size(1259, 130);
            this.newstxt.TabIndex = 39;
            // 
            // speakbtn
            // 
            this.speakbtn.BackColor = System.Drawing.Color.Transparent;
            this.speakbtn.FlatAppearance.BorderSize = 0;
            this.speakbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speakbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speakbtn.ForeColor = System.Drawing.Color.Black;
            this.speakbtn.Location = new System.Drawing.Point(1087, 132);
            this.speakbtn.Margin = new System.Windows.Forms.Padding(4);
            this.speakbtn.Name = "speakbtn";
            this.speakbtn.Size = new System.Drawing.Size(168, 12);
            this.speakbtn.TabIndex = 40;
            this.speakbtn.Text = "Speak";
            this.speakbtn.UseVisualStyleBackColor = false;
            this.speakbtn.Click += new System.EventHandler(this.speakbtn_Click);
            // 
            // spokentxt
            // 
            this.spokentxt.BackColor = System.Drawing.Color.Black;
            this.spokentxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spokentxt.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spokentxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.spokentxt.Location = new System.Drawing.Point(3, 773);
            this.spokentxt.Margin = new System.Windows.Forms.Padding(4);
            this.spokentxt.Multiline = true;
            this.spokentxt.Name = "spokentxt";
            this.spokentxt.Size = new System.Drawing.Size(1259, 76);
            this.spokentxt.TabIndex = 41;
            // 
            // autoselectvoice
            // 
            this.autoselectvoice.BackColor = System.Drawing.Color.Black;
            this.autoselectvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.autoselectvoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.autoselectvoice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.autoselectvoice.FormattingEnabled = true;
            this.autoselectvoice.Location = new System.Drawing.Point(812, 95);
            this.autoselectvoice.Margin = new System.Windows.Forms.Padding(4);
            this.autoselectvoice.Name = "autoselectvoice";
            this.autoselectvoice.Size = new System.Drawing.Size(448, 33);
            this.autoselectvoice.TabIndex = 42;
            // 
            // minbtn
            // 
            this.minbtn.BackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.minbtn.Location = new System.Drawing.Point(1146, 17);
            this.minbtn.Margin = new System.Windows.Forms.Padding(4);
            this.minbtn.Name = "minbtn";
            this.minbtn.Size = new System.Drawing.Size(47, 38);
            this.minbtn.TabIndex = 43;
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
            this.panel1.Size = new System.Drawing.Size(1266, 862);
            this.panel1.TabIndex = 46;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // Media
            // 
            this.Media.Enabled = true;
            this.Media.Location = new System.Drawing.Point(812, 144);
            this.Media.Name = "Media";
            this.Media.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Media.OcxState")));
            this.Media.Size = new System.Drawing.Size(192, 418);
            this.Media.TabIndex = 45;
            // 
            // Reminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1266, 862);
            this.Controls.Add(this.Media);
            this.Controls.Add(this.minbtn);
            this.Controls.Add(this.autoselectvoice);
            this.Controls.Add(this.spokentxt);
            this.Controls.Add(this.speakbtn);
            this.Controls.Add(this.newstxt);
            this.Controls.Add(this.labelnet);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.daysofweek);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.remindermsgtxt);
            this.Controls.Add(this.reminderset);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timereplacetxt);
            this.Controls.Add(this.musicnamebox);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.settimetxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.time_s);
            this.Controls.Add(this.time_n);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnset);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openmusicbtn);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Reminder";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AlarmClock";
            this.Load += new System.EventHandler(this.Reminder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Media)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openmusicbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Button btnset;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Label time_n;
        private System.Windows.Forms.Label time_s;
        private System.Windows.Forms.Timer time_now;
        private System.Windows.Forms.Timer time_set;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox settimetxt;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Label musicnamebox;
        private System.Windows.Forms.TextBox timereplacetxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label reminderset;
        private System.Windows.Forms.TextBox remindermsgtxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button resetbtn;
        private System.Windows.Forms.TextBox daysofweek;
        private System.Windows.Forms.Label labelnet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox newstxt;
        private System.Windows.Forms.Button speakbtn;
        private System.Windows.Forms.TextBox spokentxt;
        private System.Windows.Forms.ComboBox autoselectvoice;
        private System.Windows.Forms.Button minbtn;
        private AxWMPLib.AxWindowsMediaPlayer Media;
        private System.Windows.Forms.Panel panel1;
    }
}