namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class DateTimeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeForm));
            this.labeltime2 = new System.Windows.Forms.Label();
            this.labeltime = new System.Windows.Forms.Label();
            this.labelmonth = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datetimer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labeltime2
            // 
            this.labeltime2.AutoSize = true;
            this.labeltime2.BackColor = System.Drawing.Color.Transparent;
            this.labeltime2.Font = new System.Drawing.Font("Impact", 24F);
            this.labeltime2.ForeColor = System.Drawing.Color.White;
            this.labeltime2.Location = new System.Drawing.Point(230, 84);
            this.labeltime2.Name = "labeltime2";
            this.labeltime2.Size = new System.Drawing.Size(69, 48);
            this.labeltime2.TabIndex = 26;
            this.labeltime2.Text = "PM";
            // 
            // labeltime
            // 
            this.labeltime.AutoSize = true;
            this.labeltime.BackColor = System.Drawing.Color.Transparent;
            this.labeltime.Font = new System.Drawing.Font("Impact", 24F);
            this.labeltime.ForeColor = System.Drawing.Color.White;
            this.labeltime.Location = new System.Drawing.Point(63, 84);
            this.labeltime.Name = "labeltime";
            this.labeltime.Size = new System.Drawing.Size(161, 48);
            this.labeltime.TabIndex = 25;
            this.labeltime.Text = "02:00:00";
            // 
            // labelmonth
            // 
            this.labelmonth.AutoSize = true;
            this.labelmonth.BackColor = System.Drawing.Color.Transparent;
            this.labelmonth.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelmonth.ForeColor = System.Drawing.Color.White;
            this.labelmonth.Location = new System.Drawing.Point(190, 25);
            this.labelmonth.Name = "labelmonth";
            this.labelmonth.Size = new System.Drawing.Size(130, 48);
            this.labelmonth.TabIndex = 24;
            this.labelmonth.Text = "Month ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(38, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 48);
            this.label1.TabIndex = 23;
            this.label1.Text = "Month : ";
            // 
            // datetimer
            // 
            this.datetimer.Enabled = true;
            this.datetimer.Tick += new System.EventHandler(this.datetimer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(374, 162);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // DateTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(374, 162);
            this.Controls.Add(this.labeltime2);
            this.Controls.Add(this.labeltime);
            this.Controls.Add(this.labelmonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DateTimeForm";
            this.Opacity = 0.7D;
            this.Text = "DateTime";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Load += new System.EventHandler(this.DateTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labeltime2;
        private System.Windows.Forms.Label labeltime;
        private System.Windows.Forms.Label labelmonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer datetimer;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}