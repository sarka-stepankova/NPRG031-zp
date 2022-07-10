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

            // set size of clickable components
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

        private void changeVisibilityAfterLeaveStartScreen()
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
        }
        private void setStartObjectsAndVars()
        {
            map = new Map("board.txt");
            pac = new Pacman(9, 16, map);
            ghosts = new List<Ghost>();
            blinky = new Blinky(9, 8, Direction.no, rnd); ghosts.Add(blinky);
            pinky = new Pinky(8, 10, Direction.no, rnd); ghosts.Add(pinky);
            inky = new Inky(9, 10, Direction.no, rnd); ghosts.Add(inky);
            clyde = new Clyde(10, 10, Direction.no, rnd); ghosts.Add(clyde);
            pac.map.numOfLives = 3;
            tempDir = Direction.no;
            scoreBox.Text = pac.score.ToString();
            firstLife.Visible = true; secondLife.Visible = true; thirdLife.Visible = true;
        }
        private void playGame2_Click(object sender, EventArgs e)
        {
            changeVisibilityAfterLeaveStartScreen();
            setStartObjectsAndVars();
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

        // vymenil si Pacman misto s nejakym duchem?
        private bool switchPlaces(Pacman pac, Ghost g, int prevpX, int prevpY, int prevX, int prevY)
        {
            return (g.x == prevpX && g.y == prevpY && pac.x == prevX && pac.y == prevY);
        }

        private void switchToWinState(object sender, EventArgs e)
        {
            if (pac.coins == 0)
            {
                mainTimer.Enabled = false;
                DialogResult dialogResult = MessageBox.Show("You win! Play again?", "Pacman", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    setStartObjectsAndVars();
                    mainTimer.Enabled = true;
                    //playGame2_Click(sender, e);
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Close();
                }
            }
        }
        private void switchStateToFrightened()
        {
            foreach (Ghost ghost in ghosts)
            {
                ghost.state = GhostState.frightened;
                ghost.counter = 0;
                ghost.turnAround();
            }
        }
        private void refreshGame()
        {
            this.Refresh();
            if (pac.map.numOfLives == 2) firstLife.Visible = false;
            if (pac.map.numOfLives == 1) secondLife.Visible = false;
            pac.x = 9; pac.y = 16; pac.direction = Direction.no;
            foreach (Ghost g in ghosts)
            {
                g.goBackHome();
            }
            tempDir = Direction.no;
            mainTimer.Enabled = false;
            MessageBox.Show("You lost 1 life");
            mainTimer.Enabled = true;
        }
        private void endGame()
        {
            thirdLife.Visible = false;
            this.Refresh();
            mainTimer.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("You lose! Play again?", "Pacman", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                setStartObjectsAndVars();
                mainTimer.Enabled = true;
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }
        private void changeGhostsStates(int prevpX, int prevpY)
        {
            foreach (Ghost ghost in ghosts)
            {
                // Kdyz se potka Pacman a Duch, bud v jednom stavu na jednom policku, nebo si vymeni mista
                if (onTheSamePlace(pac, ghost) || switchPlaces(pac, ghost, prevpX, prevpY, ghost.prevX, ghost.prevY))
                {
                    if (ghost.state == GhostState.chase)
                    {
                        pac.map.numOfLives -= 1;
                        if (pac.map.numOfLives == 2)
                        {
                            refreshGame();
                        }
                        else if (pac.map.numOfLives == 1)
                        {
                            refreshGame();
                        }
                        else if (pac.map.numOfLives <= 0)
                        {
                            endGame();
                        }
                    }
                    else if (ghost.state == GhostState.frightened)
                    {
                        ghost.state = GhostState.eaten;
                        // Kdyz sni Pacman ducha, hrac ziska 10 bodu navic
                        pac.score += 10;
                    }
                }

                // Kdyz byl duch snedeny a vrati se k domecku (doprostred kde zacinal cerveny duch), zmeni se jeho stav na chase
                if (ghost.state == GhostState.eaten && ghost.x == 9 && ghost.y == 8)
                {
                    ghost.state = GhostState.chase;
                }
            }
        }
        
        // Hlavni kontrola a zmena stavu
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // Zapamatuji si predchozi hodnoty, kde byl Pacman a duchove
            // kvuli hlidani prohozeni si mist (snezeni nekoho nekym)
            int prevpX = pac.x; int prevpY = pac.y;
            foreach (Ghost ghost in ghosts)
            {
                ghost.prevX = ghost.x;
                ghost.prevY = ghost.y;
                ghost.moveGhost(pac);
            }

            pac.movePacman(tempDir);
            scoreBox.Text = pac.score.ToString();

            // kdyz Pacman sesbira vsechny dukaty
            if (pac.coins == 0) switchToWinState(sender, e);

            // kdyz sni Pacman power pellet (token), muze sezrat duchy a ziskat body navic
            if (pac.map.board[pac.y][pac.x] == 'T') switchStateToFrightened();

            changeGhostsStates(prevpX, prevpY);

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

        // Funkce co prekresluje obrazovku pokazde, kdyz se pohnou pohyblive prvky
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
