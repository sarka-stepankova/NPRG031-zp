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
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreBox = new System.Windows.Forms.TextBox();
            this.scorePicture = new System.Windows.Forms.PictureBox();
            this.thirdLife = new System.Windows.Forms.PictureBox();
            this.secondLife = new System.Windows.Forms.PictureBox();
            this.firstLife = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.playGame2 = new System.Windows.Forms.PictureBox();
            this.Quit = new System.Windows.Forms.PictureBox();
            this.pacMan = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.scorePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playGame2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacMan)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 150;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // scoreBox
            // 
            this.scoreBox.BackColor = System.Drawing.Color.Black;
            this.scoreBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreBox.ForeColor = System.Drawing.Color.White;
            this.scoreBox.Location = new System.Drawing.Point(125, 475);
            this.scoreBox.Name = "scoreBox";
            this.scoreBox.Size = new System.Drawing.Size(165, 38);
            this.scoreBox.TabIndex = 9;
            this.scoreBox.Visible = false;
            // 
            // scorePicture
            // 
            this.scorePicture.Image = global::PacMan.Properties.Resources.score;
            this.scorePicture.Location = new System.Drawing.Point(28, 478);
            this.scorePicture.Name = "scorePicture";
            this.scorePicture.Size = new System.Drawing.Size(79, 33);
            this.scorePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.scorePicture.TabIndex = 10;
            this.scorePicture.TabStop = false;
            this.scorePicture.Visible = false;
            // 
            // thirdLife
            // 
            this.thirdLife.Image = global::PacMan.Properties.Resources._1sx;
            this.thirdLife.Location = new System.Drawing.Point(365, 473);
            this.thirdLife.Name = "thirdLife";
            this.thirdLife.Size = new System.Drawing.Size(20, 20);
            this.thirdLife.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thirdLife.TabIndex = 8;
            this.thirdLife.TabStop = false;
            this.thirdLife.Visible = false;
            // 
            // secondLife
            // 
            this.secondLife.Image = global::PacMan.Properties.Resources._1sx;
            this.secondLife.Location = new System.Drawing.Point(339, 473);
            this.secondLife.Name = "secondLife";
            this.secondLife.Size = new System.Drawing.Size(20, 20);
            this.secondLife.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.secondLife.TabIndex = 7;
            this.secondLife.TabStop = false;
            this.secondLife.Visible = false;
            // 
            // firstLife
            // 
            this.firstLife.Image = global::PacMan.Properties.Resources._1sx;
            this.firstLife.Location = new System.Drawing.Point(313, 473);
            this.firstLife.Name = "firstLife";
            this.firstLife.Size = new System.Drawing.Size(20, 20);
            this.firstLife.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.firstLife.TabIndex = 6;
            this.firstLife.TabStop = false;
            this.firstLife.Visible = false;
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
            this.pictureBox1.Image = global::PacMan.Properties.Resources.rsx;
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
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(432, 603);
            this.Controls.Add(this.scorePicture);
            this.Controls.Add(this.scoreBox);
            this.Controls.Add(this.thirdLife);
            this.Controls.Add(this.secondLife);
            this.Controls.Add(this.firstLife);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.playGame2);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.pacMan);
            this.DoubleBuffered = true;
            this.Name = "GameForm";
            this.Text = "PacMan";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.scorePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playGame2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Quit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacMan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pacMan;
        private System.Windows.Forms.PictureBox Quit;
        private System.Windows.Forms.PictureBox playGame2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.PictureBox firstLife;
        private System.Windows.Forms.PictureBox secondLife;
        private System.Windows.Forms.PictureBox thirdLife;
        private System.Windows.Forms.TextBox scoreBox;
        private System.Windows.Forms.PictureBox scorePicture;
    }
}

