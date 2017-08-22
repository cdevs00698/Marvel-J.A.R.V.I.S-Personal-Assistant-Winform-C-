namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class YoutubePlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoutubePlayer));
            this.closebtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.searchtxt = new System.Windows.Forms.TextBox();
            this.searchbtn = new System.Windows.Forms.Button();
            this.youtubebrowser = new System.Windows.Forms.WebBrowser();
            this.stopbtn = new System.Windows.Forms.Button();
            this.prebtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.minbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(1233, 15);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 11;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(535, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "Youtube";
            // 
            // searchtxt
            // 
            this.searchtxt.BackColor = System.Drawing.Color.Black;
            this.searchtxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchtxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.searchtxt.Location = new System.Drawing.Point(16, 69);
            this.searchtxt.Margin = new System.Windows.Forms.Padding(4);
            this.searchtxt.Multiline = true;
            this.searchtxt.Name = "searchtxt";
            this.searchtxt.Size = new System.Drawing.Size(1123, 38);
            this.searchtxt.TabIndex = 26;
            this.searchtxt.Text = "Search by key words";
            // 
            // searchbtn
            // 
            this.searchbtn.BackColor = System.Drawing.Color.Transparent;
            this.searchbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.searchbtn.Location = new System.Drawing.Point(1148, 69);
            this.searchbtn.Margin = new System.Windows.Forms.Padding(4);
            this.searchbtn.Name = "searchbtn";
            this.searchbtn.Size = new System.Drawing.Size(132, 38);
            this.searchbtn.TabIndex = 28;
            this.searchbtn.Text = "Search";
            this.searchbtn.UseVisualStyleBackColor = false;
            this.searchbtn.Click += new System.EventHandler(this.searchbtn_Click);
            // 
            // youtubebrowser
            // 
            this.youtubebrowser.Location = new System.Drawing.Point(17, 134);
            this.youtubebrowser.Margin = new System.Windows.Forms.Padding(4);
            this.youtubebrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.youtubebrowser.Name = "youtubebrowser";
            this.youtubebrowser.Size = new System.Drawing.Size(1263, 548);
            this.youtubebrowser.TabIndex = 29;
            // 
            // stopbtn
            // 
            this.stopbtn.BackColor = System.Drawing.Color.Transparent;
            this.stopbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.stopbtn.Location = new System.Drawing.Point(1137, 702);
            this.stopbtn.Margin = new System.Windows.Forms.Padding(4);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(143, 31);
            this.stopbtn.TabIndex = 48;
            this.stopbtn.Text = "Stop ";
            this.stopbtn.UseVisualStyleBackColor = false;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // prebtn
            // 
            this.prebtn.BackColor = System.Drawing.Color.Transparent;
            this.prebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.prebtn.Location = new System.Drawing.Point(975, 702);
            this.prebtn.Margin = new System.Windows.Forms.Padding(4);
            this.prebtn.Name = "prebtn";
            this.prebtn.Size = new System.Drawing.Size(143, 31);
            this.prebtn.TabIndex = 49;
            this.prebtn.Text = "Previous ";
            this.prebtn.UseVisualStyleBackColor = false;
            this.prebtn.Click += new System.EventHandler(this.prebtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(648, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 31);
            this.label2.TabIndex = 50;
            this.label2.Text = "Play";
            // 
            // minbtn
            // 
            this.minbtn.BackColor = System.Drawing.Color.Transparent;
            this.minbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.minbtn.Location = new System.Drawing.Point(1179, 15);
            this.minbtn.Margin = new System.Windows.Forms.Padding(4);
            this.minbtn.Name = "minbtn";
            this.minbtn.Size = new System.Drawing.Size(47, 38);
            this.minbtn.TabIndex = 51;
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
            this.panel1.Size = new System.Drawing.Size(1296, 752);
            this.panel1.TabIndex = 52;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // YoutubePlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1296, 752);
            this.Controls.Add(this.minbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prebtn);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.youtubebrowser);
            this.Controls.Add(this.searchbtn);
            this.Controls.Add(this.searchtxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "YoutubePlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MediaPlayer";
            this.Load += new System.EventHandler(this.YoutubePlayer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchtxt;
        private System.Windows.Forms.Button searchbtn;
        private System.Windows.Forms.WebBrowser youtubebrowser;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button prebtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button minbtn;
        private System.Windows.Forms.Panel panel1;
    }
}