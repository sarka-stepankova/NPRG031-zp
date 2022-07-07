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
        Blinky b;
        PressedDirection docasnySmer = PressedDirection.no;
        private void playGame2_Click(object sender, EventArgs e)
        {
            pacMan.Visible = false;
            playGame2.Visible = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            Quit.Visible = false;

            map = new Map("board.txt");
            pac = new Pacman(9, 16, map);
            b = new Blinky(9, 8, PressedDirection.left);

            mainTimer.Enabled = true;
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // pak neco jako pohni duchy for each ghost v duchove
            b.moveGhost(pac);
            pac.movePacman(docasnySmer);

            //switch (map.stav)

            this.Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Up)
            {
                //pac.smer = PressedDirection.up;
                docasnySmer = PressedDirection.up;
                return true;
            }
            if (keyData == Keys.Down)
            {
                docasnySmer = PressedDirection.down;
                return true;
            }
            if (keyData == Keys.Left)
            {
                docasnySmer = PressedDirection.left;
                return true;
            }
            if (keyData == Keys.Right)
            {
                docasnySmer = PressedDirection.right;
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
            b.redrawBlinky(e.Graphics);
        }

    }
}
