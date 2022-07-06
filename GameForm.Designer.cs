namespace PacMan
{
    partial class GameForm
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.playGame2 = new System.Windows.Forms.PictureBox();
            this.Quit = new System.Windows.Forms.PictureBox();
            this.pacMan = new System.Windows.Forms.PictureBox();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playGame2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacMan)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::PacMan.Properties.Resources.pacman_landing_page;
            this.pictureBox2.Location = new System.Drawing.Point(2, 198);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(429, 197);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PacMan.Properties.Resources.mup1;
            this.pictureBox1.Location = new System.Drawing.Point(195, 540);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // playGame2
            // 
            this.playGame2.Image = global::PacMan.Properties.Resources.playgame2;
            this.playGame2.Location = new System.Drawing.Point(116, 152);
            this.playGame2.Name = "playGame2";
            this.playGame2.Size = new System.Drawing.Size(165, 40);
            this.playGame2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playGame2.TabIndex = 3;
            this.playGame2.TabStop = false;
            this.playGame2.Click += new System.EventHandler(this.playGame2_Click);
            this.playGame2.MouseEnter += new System.EventHandler(this.playGame2_MouseEnter);
            this.playGame2.MouseLeave += new System.EventHandler(this.playGame2_MouseLeave);
            // 
            // Quit
            // 
            this.Quit.Image = global::PacMan.Properties.Resources.exit;
            this.Quit.Location = new System.Drawing.Point(146, 490);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(92, 33);
            this.Quit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Quit.TabIndex = 2;
            this.Quit.TabStop = false;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            this.Quit.MouseEnter += new System.EventHandler(this.Quit_MouseEnter);
            this.Quit.MouseLeave += new System.EventHandler(this.Quit_MouseLeave);
            // 
            // pacMan
            // 
            this.pacMan.Image = global::PacMan.Properties.Resources.logo;
            this.pacMan.Location = new System.Drawing.Point(116, 63);
            this.pacMan.Name = "pacMan";
            this.pacMan.Size = new System.Drawing.Size(200, 50);
            this.pacMan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pacMan.TabIndex = 0;
            this.pacMan.TabStop = false;
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 50;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(432, 603);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playGame2);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.pacMan);
            this.DoubleBuffered = true;
            this.Name = "GameForm";
            this.Text = "PacMan";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playGame2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacMan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pacMan;
        private System.Windows.Forms.PictureBox Quit;
        private System.Windows.Forms.PictureBox playGame2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer mainTimer;
    }
}

