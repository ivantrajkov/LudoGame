namespace LudoGame
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.two = new System.Windows.Forms.RadioButton();
            this.three = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.four = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("MV Boli", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.button1.Location = new System.Drawing.Point(83, 233);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(534, 135);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("MV Boli", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label2.Location = new System.Drawing.Point(134, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 110);
            this.label2.TabIndex = 2;
            this.label2.Text = "LudoGame";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // two
            // 
            this.two.AutoSize = true;
            this.two.BackColor = System.Drawing.Color.Transparent;
            this.two.Font = new System.Drawing.Font("MV Boli", 10.1F, System.Drawing.FontStyle.Bold);
            this.two.ForeColor = System.Drawing.Color.Black;
            this.two.Location = new System.Drawing.Point(6, 46);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(135, 26);
            this.two.TabIndex = 3;
            this.two.TabStop = true;
            this.two.Text = "Two players";
            this.two.UseVisualStyleBackColor = false;
            // 
            // three
            // 
            this.three.AutoSize = true;
            this.three.BackColor = System.Drawing.Color.Transparent;
            this.three.Font = new System.Drawing.Font("MV Boli", 10.1F, System.Drawing.FontStyle.Bold);
            this.three.ForeColor = System.Drawing.Color.Black;
            this.three.Location = new System.Drawing.Point(172, 46);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(148, 26);
            this.three.TabIndex = 4;
            this.three.TabStop = true;
            this.three.Text = "Three players";
            this.three.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.four);
            this.groupBox1.Controls.Add(this.two);
            this.groupBox1.Controls.Add(this.three);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("MV Boli", 10.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(83, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Number of players!";
            // 
            // four
            // 
            this.four.AutoSize = true;
            this.four.BackColor = System.Drawing.Color.Transparent;
            this.four.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.four.Font = new System.Drawing.Font("MV Boli", 10.1F, System.Drawing.FontStyle.Bold);
            this.four.ForeColor = System.Drawing.Color.Black;
            this.four.Location = new System.Drawing.Point(351, 45);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(147, 27);
            this.four.TabIndex = 5;
            this.four.TabStop = true;
            this.four.Text = "Four players";
            this.four.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LudoGame.Properties.Resources.blurred_green_gradient_3d_wallpaper_preview;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(729, 395);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton two;
        private System.Windows.Forms.RadioButton three;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton four;
    }
}

