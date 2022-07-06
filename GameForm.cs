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
        Graphics g;
        Pacman pac;
        private void playGame2_Click(object sender, EventArgs e)
        {
            pacMan.Visible = false;
            playGame2.Visible = false;
            pictureBox2.Visible = false;
            pictureBox1.Visible = false;
            Quit.Visible = false;

            map = new Map("board.txt");
            pac = new Pacman(9, 16);

            mainTimer.Enabled = true;
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // updatovat - vykreslit board, + vykreslit Pacmana, (+ vykreslit Duchy)
            pac.rdup = false;
            pac.rddown = false;
            pac.rdleft = false;
            pac.rdright = false;

            // TADY PREKRESLOVAT PACMANA
            //pac.redrawPacman(g);

            //switch (map.stav)
            //{
            //    case Stav.bezi:
            //        map.PohniVsemiPrvky(stisknutaSipka);
            //        map.VykresliSe(g, ClientSize.Width, ClientSize.Height);
            //        break;
            //    case Stav.vyhra:
            //        mainTimer.Enabled = false;
            //        MessageBox.Show("Game Win!");
            //        break;
            //    case Stav.prohra:
            //        mainTimer.Enabled = false;
            //        MessageBox.Show("Game Over!");
            //        break;
            //    default:
            //        break;
            //}
            this.Refresh();
        }

        //PressedDirection stisknutaSipka = PressedDirection.no;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Up)
            {
                if (!pac.rdup && pac.isFree(map, pac.y-1, pac.x))  // rd = redraw
                {
                    map.board[pac.y][pac.x] = ' ';
                    map.board[pac.y - 1][pac.x] = 'P';
                    pac.y -= 1;
                    pac.rdup = true;
                }
                return true;
            }
            if (keyData == Keys.Down)
            {
                if (!pac.rddown && pac.isFree(map, pac.y + 1, pac.x))  // rd = redraw
                {
                    map.board[pac.y][pac.x] = ' ';
                    map.board[pac.y + 1][pac.x] = 'P';
                    pac.y += 1;
                    pac.rddown = true;
                }
                return true;
            }
            if (keyData == Keys.Left)
            {
                if (!pac.rdleft && pac.isFree(map, pac.y, pac.x - 1))  // rd = redraw
                {
                    map.board[pac.y][pac.x] = ' ';
                    map.board[pac.y][pac.x - 1] = 'P';
                    pac.x -= 1;
                    pac.rdleft = true;
                }
                return true;
            }
            if (keyData == Keys.Right)
            {
                if (!pac.rdright && pac.isFree(map, pac.y, pac.x + 1))  // rd = redraw
                {
                    map.board[pac.y][pac.x] = ' ';
                    map.board[pac.y][pac.x + 1] = 'P';
                    pac.x += 1;
                    pac.rdright = true;
                }
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
            map.redrawMap(map.board, e.Graphics);
            
        }

        //private void Form1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    stisknutaSipka = PressedDirection.no;
        //}
    }
}
