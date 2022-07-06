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
        PressedDirection docasnySmer = PressedDirection.no;
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
            // proste posouvat ducha na smer
            // duch ma nejaky smer a je zmacknute nejake tlacitko, kdyz muze jit tam 
            // kde je zmacknuty tlacitko, tak tam jde a ziska novy smer, jinak jde ve svem smeru
            if (pac.isFree(map, pac.y - 1, pac.x) && pac.smer == PressedDirection.up)
            {
                map.board[pac.y][pac.x] = ' ';
                pac.y -= 1;
            }
            if (pac.isFree(map, pac.y + 1, pac.x) && pac.smer == PressedDirection.down) 
            {
                map.board[pac.y][pac.x] = ' ';
                pac.y += 1;
            }
            if (pac.isFree(map, pac.y, pac.x - 1) && pac.smer == PressedDirection.left)
            {
                map.board[pac.y][pac.x] = ' ';
                pac.x -= 1;
            }
            if (pac.isFree(map, pac.y, pac.x + 1) && pac.smer == PressedDirection.right)  
            {
                map.board[pac.y][pac.x] = ' ';
                pac.x += 1;
            }

            //switch (map.stav)

            this.Refresh();
        }

        //PressedDirection stisknutaSipka = PressedDirection.no;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Up)
            {
                pac.smer = PressedDirection.up;
                return true;
            }
            if (keyData == Keys.Down)
            {
                pac.smer = PressedDirection.down;
                return true;
            }
            if (keyData == Keys.Left)
            {
                pac.smer = PressedDirection.left;
                return true;
            }
            if (keyData == Keys.Right)
            {
                pac.smer = PressedDirection.right;
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
            pac.redrawPacman(e.Graphics);
        }

        //private void Form1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    stisknutaSipka = PressedDirection.no;
        //}
    }
}
