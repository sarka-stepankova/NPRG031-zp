using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PacMan
{
    public partial class GameForm : Form
    {
        Random rnd = new Random();
        public GameForm()
        {
            InitializeComponent();
            Quit.Width = 92;
            Quit.Height = 33;
            playGame2.Width = 152;
            playGame2.Height = 33;
        }

        private void Quit_MouseEnter(object sender, EventArgs e)
        {
            Quit.Width = 95;
            Quit.Height = 35;
        }

        private void Quit_MouseLeave(object sender, EventArgs e)
        {
            Quit.Width = 92;
            Quit.Height = 33;
        }

        private void playGame2_MouseEnter(object sender, EventArgs e)
        {
            playGame2.Width = 155;
            playGame2.Height = 35;
        }

        private void playGame2_MouseLeave(object sender, EventArgs e)
        {
            playGame2.Width = 152;
            playGame2.Height = 33;
        }

        Map map;
        Pacman pac;
        Ghost blinky;
        Direction tempDir = Direction.no;
        int numOfLifes;
        private void playGame2_Click(object sender, EventArgs e)
        {
            pacMan.Visible = false;
            playGame2.Visible = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            Quit.Visible = false;
            firstLife.Visible = true;
            secondLife.Visible = true;
            thirdLife.Visible = true;
            scoreBox.Visible = true;
            scorePicture.Visible = true;
            numOfLifes = 3;

            map = new Map("board.txt");
            pac = new Pacman(9, 16, map);
            blinky = new Ghost(9, 8, Direction.no, rnd);
            tempDir = Direction.no;
            scoreBox.Text = pac.score.ToString();

            mainTimer.Enabled = true;
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool onTheSamePlace(Pacman pac, Ghost b)
        {
            return ( pac.x == b.x && pac.y == b.y );
        }

        private bool switchedPlaces(Pacman pac, Ghost b, int prevpX, int prevpY, int prevbX, int prevbY)
        {
            return (b.x == prevpX && b.y == prevpY && pac.x == prevbX && pac.y == prevbY);
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // kdyz score 150, tak prepni stav (asi u packmana pac.map) na stav.win a pak tady podle toho s tim operuj
            // jinak u stavu bezi budou podstavy pro duchy
            // pak neco jako pohni duchy for each ghost v duchove

            int prevpX = pac.x; int prevpY = pac.y;

            // pro vsechny duchy
            int prevbX = blinky.x; int prevbY = blinky.y;
            blinky.moveGhost(pac);

            pac.movePacman(tempDir);
            scoreBox.Text = pac.score.ToString();
            if (pac.coins == 0)
            {
                mainTimer.Enabled = false;
                thirdLife.Visible = false;
                DialogResult dialogResult = MessageBox.Show("You win! Play again?", "Pacman", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    playGame2_Click(sender, e);
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }

            // for each ghost
            if (pac.map.board[pac.y][pac.x] == 'T')
            {
                blinky.state = GhostState.frightened;
                blinky.counter = 0;
                blinky.turnAround();
            }

            // for each ghost do this
            if (onTheSamePlace(pac, blinky) || switchedPlaces(pac, blinky, prevpX, prevpY, prevbX, prevbY))
            {
                if (blinky.state == GhostState.chase)
                {
                    numOfLifes -= 1;
                    if (numOfLifes == 2)
                    {
                        this.Refresh();
                        firstLife.Visible = false;
                        pac.x = 9; pac.y = 16; pac.direction = Direction.no;
                        blinky.x = 9; blinky.y = 8;
                        tempDir = Direction.no;
                        mainTimer.Enabled = false;
                        MessageBox.Show("You lost 1 life");
                        mainTimer.Enabled = true;
                    }
                    if (numOfLifes == 1)
                    {
                        secondLife.Visible = false;
                        pac.x = 9; pac.y = 16; pac.direction = Direction.no;
                        blinky.x = 9; blinky.y = 8;
                        tempDir = Direction.no;
                        mainTimer.Enabled = false;
                        MessageBox.Show("You lost 1 life");
                        mainTimer.Enabled = true;
                    }
                    if (numOfLifes <= 0)
                    {
                        thirdLife.Visible = false;
                        this.Refresh();
                        mainTimer.Enabled = false;
                        DialogResult dialogResult = MessageBox.Show("You lose! Play again?", "Pacman", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            playGame2_Click(sender, e);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.Close();
                        }
                    }
                }
                else if (blinky.state == GhostState.frightened)
                {
                    blinky.state = GhostState.eaten;
                    pac.score += 10;
                }
            }

            // for each ghost too
            if (blinky.state == GhostState.eaten && blinky.x == 9 && blinky.y == 8)
            {
                blinky.state = GhostState.chase;
            }

            //switch (map.stav)

            this.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Up)
            {
                tempDir = Direction.up;
                return true;
            }
            if (keyData == Keys.Down)
            {
                tempDir = Direction.down;
                return true;
            }
            if (keyData == Keys.Left)
            {
                tempDir = Direction.left;
                return true;
            }
            if (keyData == Keys.Right)
            {
                tempDir = Direction.right;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            if (!mainTimer.Enabled)
            {
                return;
            }
            e.Graphics.Clear(Color.Black);
            pac.map.redrawMap(pac.map.board, e.Graphics);
            pac.redrawPacman(e.Graphics);

            // pak neco jako redraw duchy
            blinky.redrawGhost(e.Graphics);
        }
    }
}
