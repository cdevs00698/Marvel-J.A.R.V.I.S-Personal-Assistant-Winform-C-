namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class WakeUpAlarm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WakeUpAlarm));
            this.screentimelbl = new System.Windows.Forms.Label();
            this.templbl = new System.Windows.Forms.Label();
            this.dayofweeklbl = new System.Windows.Forms.Label();
            this.compeletdatelbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.sunsetlbl = new System.Windows.Forms.Label();
            this.sunriselbl = new System.Windows.Forms.Label();
            this.lowlbl = new System.Windows.Forms.Label();
            this.highlbl = new System.Windows.Forms.Label();
            this.windspeedlbl = new System.Windows.Forms.Label();
            this.humiditylbl = new System.Windows.Forms.Label();
            this.conditionlbl = new System.Windows.Forms.Label();
            this.labelnet = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timetimer = new System.Windows.Forms.Timer(this.components);
            this.ampmlbl = new System.Windows.Forms.Label();
            this.weatherimg = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.weatherbg = new System.Windows.Forms.WebBrowser();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tomorrowforcast = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.weatherimg)).BeginInit();
            this.SuspendLayout();
            // 
            // screentimelbl
            // 
            this.screentimelbl.AutoSize = true;
            this.screentimelbl.BackColor = System.Drawing.Color.Transparent;
            this.screentimelbl.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.screentimelbl.ForeColor = System.Drawing.Color.White;
            this.screentimelbl.Location = new System.Drawing.Point(1363, 279);
            this.screentimelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.screentimelbl.Name = "screentimelbl";
            this.screentimelbl.Size = new System.Drawing.Size(214, 51);
            this.screentimelbl.TabIndex = 1;
            this.screentimelbl.Text = "00:00:00";
            // 
            // templbl
            // 
            this.templbl.AutoSize = true;
            this.templbl.BackColor = System.Drawing.Color.Transparent;
            this.templbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templbl.ForeColor = System.Drawing.Color.White;
            this.templbl.Location = new System.Drawing.Point(525, 119);
            this.templbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.templbl.Name = "templbl";
            this.templbl.Size = new System.Drawing.Size(129, 91);
            this.templbl.TabIndex = 2;
            this.templbl.Text = "00";
            // 
            // dayofweeklbl
            // 
            this.dayofweeklbl.AutoSize = true;
            this.dayofweeklbl.BackColor = System.Drawing.Color.Transparent;
            this.dayofweeklbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayofweeklbl.ForeColor = System.Drawing.Color.White;
            this.dayofweeklbl.Location = new System.Drawing.Point(536, 247);
            this.dayofweeklbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dayofweeklbl.Name = "dayofweeklbl";
            this.dayofweeklbl.Size = new System.Drawing.Size(182, 31);
            this.dayofweeklbl.TabIndex = 4;
            this.dayofweeklbl.Text = "Day Of  Week";
            this.dayofweeklbl.Click += new System.EventHandler(this.label3_Click);
            // 
            // compeletdatelbl
            // 
            this.compeletdatelbl.AutoSize = true;
            this.compeletdatelbl.BackColor = System.Drawing.Color.Transparent;
            this.compeletdatelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compeletdatelbl.ForeColor = System.Drawing.Color.White;
            this.compeletdatelbl.Location = new System.Drawing.Point(536, 294);
            this.compeletdatelbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.compeletdatelbl.Name = "compeletdatelbl";
            this.compeletdatelbl.Size = new System.Drawing.Size(202, 31);
            this.compeletdatelbl.TabIndex = 5;
            this.compeletdatelbl.Text = "Complete Date ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(192, 452);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Humidity : ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(192, 415);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "Condition : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(192, 528);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 25);
            this.label7.TabIndex = 9;
            this.label7.Text = "High :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(192, 492);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 25);
            this.label8.TabIndex = 8;
            this.label8.Text = "Wind Speed : ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(189, 603);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 25);
            this.label9.TabIndex = 11;
            this.label9.Text = "Sunrise : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(192, 566);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 25);
            this.label10.TabIndex = 10;
            this.label10.Text = "Low : ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(189, 639);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 25);
            this.label11.TabIndex = 12;
            this.label11.Text = "Sunset : ";
            // 
            // sunsetlbl
            // 
            this.sunsetlbl.AutoSize = true;
            this.sunsetlbl.BackColor = System.Drawing.Color.Transparent;
            this.sunsetlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sunsetlbl.ForeColor = System.Drawing.Color.White;
            this.sunsetlbl.Location = new System.Drawing.Point(287, 639);
            this.sunsetlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sunsetlbl.Name = "sunsetlbl";
            this.sunsetlbl.Size = new System.Drawing.Size(70, 25);
            this.sunsetlbl.TabIndex = 19;
            this.sunsetlbl.Text = "sunset";
            // 
            // sunriselbl
            // 
            this.sunriselbl.AutoSize = true;
            this.sunriselbl.BackColor = System.Drawing.Color.Transparent;
            this.sunriselbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sunriselbl.ForeColor = System.Drawing.Color.White;
            this.sunriselbl.Location = new System.Drawing.Point(287, 603);
            this.sunriselbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sunriselbl.Name = "sunriselbl";
            this.sunriselbl.Size = new System.Drawing.Size(75, 25);
            this.sunriselbl.TabIndex = 18;
            this.sunriselbl.Text = "sunrise";
            // 
            // lowlbl
            // 
            this.lowlbl.AutoSize = true;
            this.lowlbl.BackColor = System.Drawing.Color.Transparent;
            this.lowlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowlbl.ForeColor = System.Drawing.Color.White;
            this.lowlbl.Location = new System.Drawing.Point(267, 566);
            this.lowlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lowlbl.Name = "lowlbl";
            this.lowlbl.Size = new System.Drawing.Size(41, 25);
            this.lowlbl.TabIndex = 17;
            this.lowlbl.Text = "low";
            // 
            // highlbl
            // 
            this.highlbl.AutoSize = true;
            this.highlbl.BackColor = System.Drawing.Color.Transparent;
            this.highlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highlbl.ForeColor = System.Drawing.Color.White;
            this.highlbl.Location = new System.Drawing.Point(267, 528);
            this.highlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.highlbl.Name = "highlbl";
            this.highlbl.Size = new System.Drawing.Size(49, 25);
            this.highlbl.TabIndex = 16;
            this.highlbl.Text = "high";
            // 
            // windspeedlbl
            // 
            this.windspeedlbl.AutoSize = true;
            this.windspeedlbl.BackColor = System.Drawing.Color.Transparent;
            this.windspeedlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windspeedlbl.ForeColor = System.Drawing.Color.White;
            this.windspeedlbl.Location = new System.Drawing.Point(344, 492);
            this.windspeedlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.windspeedlbl.Name = "windspeedlbl";
            this.windspeedlbl.Size = new System.Drawing.Size(111, 25);
            this.windspeedlbl.TabIndex = 15;
            this.windspeedlbl.Text = "wind speed";
            // 
            // humiditylbl
            // 
            this.humiditylbl.AutoSize = true;
            this.humiditylbl.BackColor = System.Drawing.Color.Transparent;
            this.humiditylbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humiditylbl.ForeColor = System.Drawing.Color.White;
            this.humiditylbl.Location = new System.Drawing.Point(304, 452);
            this.humiditylbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.humiditylbl.Name = "humiditylbl";
            this.humiditylbl.Size = new System.Drawing.Size(84, 25);
            this.humiditylbl.TabIndex = 14;
            this.humiditylbl.Text = "humidity";
            // 
            // conditionlbl
            // 
            this.conditionlbl.AutoSize = true;
            this.conditionlbl.BackColor = System.Drawing.Color.Transparent;
            this.conditionlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionlbl.ForeColor = System.Drawing.Color.White;
            this.conditionlbl.Location = new System.Drawing.Point(304, 415);
            this.conditionlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.conditionlbl.Name = "conditionlbl";
            this.conditionlbl.Size = new System.Drawing.Size(90, 25);
            this.conditionlbl.TabIndex = 13;
            this.conditionlbl.Text = "condition";
            // 
            // labelnet
            // 
            this.labelnet.AutoSize = true;
            this.labelnet.BackColor = System.Drawing.Color.Transparent;
            this.labelnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelnet.ForeColor = System.Drawing.Color.White;
            this.labelnet.Location = new System.Drawing.Point(1607, 897);
            this.labelnet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelnet.Name = "labelnet";
            this.labelnet.Size = new System.Drawing.Size(64, 25);
            this.labelnet.TabIndex = 21;
            this.labelnet.Text = "status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1463, 897);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 25);
            this.label2.TabIndex = 20;
            this.label2.Text = "Connection : ";
            // 
            // timetimer
            // 
            this.timetimer.Enabled = true;
            this.timetimer.Tick += new System.EventHandler(this.timetimer_Tick);
            // 
            // ampmlbl
            // 
            this.ampmlbl.AutoSize = true;
            this.ampmlbl.BackColor = System.Drawing.Color.Transparent;
            this.ampmlbl.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ampmlbl.ForeColor = System.Drawing.Color.White;
            this.ampmlbl.Location = new System.Drawing.Point(1597, 279);
            this.ampmlbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ampmlbl.Name = "ampmlbl";
            this.ampmlbl.Size = new System.Drawing.Size(70, 51);
            this.ampmlbl.TabIndex = 41;
            this.ampmlbl.Text = "AM";
            // 
            // weatherimg
            // 
            this.weatherimg.Location = new System.Drawing.Point(155, 60);
            this.weatherimg.Margin = new System.Windows.Forms.Padding(4);
            this.weatherimg.Name = "weatherimg";
            this.weatherimg.Size = new System.Drawing.Size(373, 345);
            this.weatherimg.TabIndex = 42;
            this.weatherimg.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(663, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 91);
            this.label1.TabIndex = 43;
            this.label1.Text = "°F";
            // 
            // weatherbg
            // 
            this.weatherbg.Location = new System.Drawing.Point(543, 362);
            this.weatherbg.Margin = new System.Windows.Forms.Padding(4);
            this.weatherbg.MinimumSize = new System.Drawing.Size(27, 25);
            this.weatherbg.Name = "weatherbg";
            this.weatherbg.Size = new System.Drawing.Size(1129, 521);
            this.weatherbg.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1071, 119);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(600, 91);
            this.label3.TabIndex = 45;
            this.label3.Text = "Weather Report";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(192, 688);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(244, 25);
            this.label4.TabIndex = 57;
            this.label4.Text = "Weekly  Weather Forcast :";
            // 
            // tomorrowforcast
            // 
            this.tomorrowforcast.BackColor = System.Drawing.Color.Black;
            this.tomorrowforcast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tomorrowforcast.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tomorrowforcast.ForeColor = System.Drawing.Color.White;
            this.tomorrowforcast.Location = new System.Drawing.Point(195, 725);
            this.tomorrowforcast.Margin = new System.Windows.Forms.Padding(4);
            this.tomorrowforcast.Multiline = true;
            this.tomorrowforcast.Name = "tomorrowforcast";
            this.tomorrowforcast.Size = new System.Drawing.Size(333, 157);
            this.tomorrowforcast.TabIndex = 56;
            // 
            // WakeUpAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1744, 960);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tomorrowforcast);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ampmlbl);
            this.Controls.Add(this.labelnet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sunsetlbl);
            this.Controls.Add(this.sunriselbl);
            this.Controls.Add(this.lowlbl);
            this.Controls.Add(this.highlbl);
            this.Controls.Add(this.windspeedlbl);
            this.Controls.Add(this.humiditylbl);
            this.Controls.Add(this.conditionlbl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.compeletdatelbl);
            this.Controls.Add(this.dayofweeklbl);
            this.Controls.Add(this.weatherimg);
            this.Controls.Add(this.templbl);
            this.Controls.Add(this.screentimelbl);
            this.Controls.Add(this.weatherbg);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WakeUpAlarm";
            this.Opacity = 0.8D;
            this.Text = "WakeUpAlarm";
            this.Load += new System.EventHandler(this.WakeUpAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.weatherimg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label screentimelbl;
        private System.Windows.Forms.Label templbl;
        private System.Windows.Forms.PictureBox weatherimg;
        private System.Windows.Forms.Label dayofweeklbl;
        private System.Windows.Forms.Label compeletdatelbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label sunsetlbl;
        private System.Windows.Forms.Label sunriselbl;
        private System.Windows.Forms.Label lowlbl;
        private System.Windows.Forms.Label highlbl;
        private System.Windows.Forms.Label windspeedlbl;
        private System.Windows.Forms.Label humiditylbl;
        private System.Windows.Forms.Label conditionlbl;
        private System.Windows.Forms.Label labelnet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timetimer;
        private System.Windows.Forms.Label ampmlbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser weatherbg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tomorrowforcast;
    }
}