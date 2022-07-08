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
        Blinky blinky;
        Pinky pinky;
        Inky inky;
        Clyde clyde;
        List<Ghost> ghosts;
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
            ghosts = new List<Ghost>();
            blinky = new Blinky(9, 8, Direction.no, rnd); ghosts.Add(blinky);
            pinky = new Pinky(8, 10, Direction.no, rnd); ghosts.Add(pinky);
            inky = new Inky(9, 10, Direction.no, rnd); ghosts.Add(inky);
            clyde = new Clyde(10, 10, Direction.no, rnd); ghosts.Add(clyde);
           
            tempDir = Direction.no;
            scoreBox.Text = pac.score.ToString();

            mainTimer.Enabled = true;
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool onTheSamePlace(Pacman pac, Ghost g)
        {
            return ( pac.x == g.x && pac.y == g.y );
        }

        private bool switchedPlaces(Pacman pac, Ghost g, int prevpX, int prevpY, int prevbX, int prevbY)
        {
            return (g.x == prevpX && g.y == prevpY && pac.x == prevbX && pac.y == prevbY);
        }

        private void switchToWinState(object sender, EventArgs e)
        {
            if (pac.coins == 0)
            {
                mainTimer.Enabled = false;
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
        }
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            int prevpX = pac.x; int prevpY = pac.y;

            // pro vsechny duchy
            // prev hodnoty prevest primo na ducha jako prevX, prevY, duchove pak zdedi
            foreach (Ghost ghost in ghosts)
            {
                ghost.prevX = ghost.x;
                ghost.prevY = ghost.y;
                ghost.moveGhost(pac);
            }
            //int prevbX = blinky.x; int prevbY = blinky.y;
            //blinky.moveGhost(pac);

            pac.movePacman(tempDir);
            scoreBox.Text = pac.score.ToString();

            if (pac.coins == 0)
            {
                switchToWinState(sender, e);
            }

            if (pac.map.board[pac.y][pac.x] == 'T')
            {
                foreach (Ghost ghost in ghosts)
                {
                    ghost.state = GhostState.frightened;
                    ghost.counter = 0;
                    ghost.turnAround();
                }
            }

            foreach (Ghost ghost in ghosts)
            {
                // for each ghost do this
                if (onTheSamePlace(pac, ghost) || switchedPlaces(pac, ghost, prevpX, prevpY, ghost.prevX, ghost.prevY))
                {
                    if (ghost.state == GhostState.chase)
                    {
                        numOfLifes -= 1;
                        if (numOfLifes == 2)
                        {  // tady to prepsat na neco jako kdyz duch dostal bool yes, chycen a pak tenhle kod delat az pod foreach
                            // tady upravuju totiz stav cele hry
                            // nebo mozna ne, tady upravuju jen mensi stav hry, ale musim vsechny duchy presunout na jejich mista
                            this.Refresh();
                            firstLife.Visible = false;
                            pac.x = 9; pac.y = 16; pac.direction = Direction.no;
                            ghost.x = 9; ghost.y = 8; // tady SPATNE !!!
                            tempDir = Direction.no;
                            mainTimer.Enabled = false;
                            MessageBox.Show("You lost 1 life");
                            mainTimer.Enabled = true;
                        }
                        if (numOfLifes == 1)
                        {
                            //tady taky upravuju stav cele hry, taky ne, takze spis presunout vsechny duchy misto jednoho
                            // nejaka funkce na presunuti vsech duchu
                            secondLife.Visible = false;
                            pac.x = 9; pac.y = 16; pac.direction = Direction.no;
                            ghost.x = 9; ghost.y = 8;
                            tempDir = Direction.no;
                            mainTimer.Enabled = false;
                            MessageBox.Show("You lost 1 life");
                            mainTimer.Enabled = true;
                        }
                        if (numOfLifes <= 0)
                        {
                            // tady taky upravuju stav cele hry
                            // a to musim delat az projdu vsechny duchy
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
                    else if (ghost.state == GhostState.frightened)
                    {
                        ghost.state = GhostState.eaten;
                        pac.score += 10;
                    }
                }

                // tohle funfuje dobre, separatni pro kazdeho ducha
                if (ghost.state == GhostState.eaten && ghost.x == 9 && ghost.y == 8)
                {
                    ghost.state = GhostState.chase;
                }
            }
            

            //switch (map.stav)

            this.Refresh();
        }

        // Hlidani stisknutych sipek
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

        // Funkce co prekresluje obrazovku pokazde, kdyz se pohnou pohyblive prvky.
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            if (!mainTimer.Enabled) { return; }
            e.Graphics.Clear(Color.Black);
            pac.map.redrawMap(pac.map.board, e.Graphics);
            pac.redrawPacman(e.Graphics);
            foreach (Ghost ghost in ghosts)
            {
                ghost.redrawGhost(e.Graphics);
            }
        }
    }
}
