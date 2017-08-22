namespace Marvel_J.A.R.V.I.S_Personal_Assistant
{
    partial class Name
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Name));
            this.enternamebtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.namebox = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nametxt = new System.Windows.Forms.TextBox();
            this.namebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enternamebtn
            // 
            this.enternamebtn.Location = new System.Drawing.Point(0, 0);
            this.enternamebtn.Name = "enternamebtn";
            this.enternamebtn.Size = new System.Drawing.Size(75, 23);
            this.enternamebtn.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 20;
            // 
            // closebtn
            // 
            this.closebtn.BackColor = System.Drawing.Color.Transparent;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.closebtn.Location = new System.Drawing.Point(516, 22);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(47, 38);
            this.closebtn.TabIndex = 16;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = false;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // namebox
            // 
            this.namebox.AutoSize = true;
            this.namebox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.namebox.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namebox.ForeColor = System.Drawing.Color.White;
            this.namebox.Location = new System.Drawing.Point(117, 112);
            this.namebox.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(328, 47);
            this.namebox.TabIndex = 17;
            this.namebox.Text = "Enter Your Name";
            this.namebox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(13, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 37);
            this.label2.TabIndex = 18;
            this.label2.Text = "Marvel Jarvis PA";
            // 
            // nametxt
            // 
            this.nametxt.Location = new System.Drawing.Point(63, 188);
            this.nametxt.Multiline = true;
            this.nametxt.Name = "nametxt";
            this.nametxt.Size = new System.Drawing.Size(438, 50);
            this.nametxt.TabIndex = 22;
            this.nametxt.Text = "Enter Here";
            // 
            // namebtn
            // 
            this.namebtn.BackColor = System.Drawing.Color.Black;
            this.namebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.namebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.namebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namebtn.ForeColor = System.Drawing.Color.Teal;
            this.namebtn.Location = new System.Drawing.Point(193, 257);
            this.namebtn.Name = "namebtn";
            this.namebtn.Size = new System.Drawing.Size(153, 51);
            this.namebtn.TabIndex = 24;
            this.namebtn.Text = "Click Save";
            this.namebtn.UseVisualStyleBackColor = false;
            this.namebtn.Click += new System.EventHandler(this.namebtn_Click);
            // 
            // Name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(577, 334);
            this.Controls.Add(this.namebtn);
            this.Controls.Add(this.nametxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enternamebtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Name";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Panel";
            this.Load += new System.EventHandler(this.Domain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enternamebtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.Label namebox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nametxt;
        private System.Windows.Forms.Button namebtn;
    }
}