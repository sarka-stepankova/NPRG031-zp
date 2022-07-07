using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PacMan
{
    enum PressedDirection { no, left, up, right, down };
    public enum State { notStarted, running, win, loss };
    public enum GhostState { chase, scatter, frightened, eaten }

    class Pacman
    {
        public int x;
        public int y;
        public Map map;
        public int rectHeight = 17;
        public int rectWidth = 17;
        public PressedDirection smer = PressedDirection.no;
        public Pacman(int x, int y, Map map)
        {
            this.x = x;
            this.y = y;
            this.map = map;
        }

        public void redrawPacman(Graphics g)
        {
            switch (smer)
            {
                case PressedDirection.left:
                    g.DrawImage(Properties.Resources._1sx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case PressedDirection.right:
                    g.DrawImage(Properties.Resources._1dx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case PressedDirection.up:
                    g.DrawImage(Properties.Resources._1up, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case PressedDirection.down:
                    g.DrawImage(Properties.Resources._1down, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources._1sx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
            }

        }

        bool isFree(int a, int b)
        {
            if (map.board[a][b] != 'B')
            {
                return true;
            }
            return false;
        }

        public void movePacman(PressedDirection docasnySmer)
        {
            // TODO: smer doprava plne nefunguje
            if (this.x == 18 && this.y == 10 && this.smer == PressedDirection.right)
            {
                this.x = 1;
            }
            else if (this.x == 0 && this.y == 10 && this.smer == PressedDirection.left)
            {
                this.x = 18;
            }

            else if (this.isFree(this.y - 1, this.x) && docasnySmer == PressedDirection.up)
            {
                this.map.board[this.y][this.x] = ' ';
                this.y -= 1;
                this.smer = PressedDirection.up;
            }
            else if (this.isFree(this.y + 1, this.x) && docasnySmer == PressedDirection.down)
            {
                this.map.board[this.y][this.x] = ' ';
                this.y += 1;
                this.smer = PressedDirection.down;
            }
            else if (this.isFree(this.y, this.x - 1) && docasnySmer == PressedDirection.left)
            {
                this.map.board[this.y][this.x] = ' ';
                this.x -= 1;
                this.smer = PressedDirection.left;
            }
            else if (this.isFree(this.y, this.x + 1) && docasnySmer == PressedDirection.right)
            {
                this.map.board[this.y][this.x] = ' ';
                this.x += 1;
                this.smer = PressedDirection.right;
            }
            else if (this.isFree(this.y - 1, this.x) && this.smer == PressedDirection.up)
            {
                this.map.board[this.y][this.x] = ' ';
                this.y -= 1;
            }
            else if (this.isFree(this.y + 1, this.x) && this.smer == PressedDirection.down)
            {
                this.map.board[this.y][this.x] = ' ';
                this.y += 1;
            }
            else if (this.isFree(this.y, this.x - 1) && this.smer == PressedDirection.left)
            {
                this.map.board[this.y][this.x] = ' ';
                this.x -= 1;
            }
            else if (this.isFree(this.y, this.x + 1) && this.smer == PressedDirection.right)
            {
                this.map.board[this.y][this.x] = ' ';
                this.x += 1;
            }
        }
    }

    // Duch - pozice, barva (if pinky target = ...,)
    class Blinky  // red ghost, target is pacman
    {
        public int x;
        public int y;
        public PressedDirection dir;
        public GhostState state = GhostState.chase; //ale pak scatter
        public int counter = 0;
        Random rnd;
        int targetX;
        int targetY;

        public Blinky (int x, int y, PressedDirection dir, Random rnd)
        {
            this.x = x;  // x=9 x y=8
            this.y = y;
            this.dir = dir;
            this.rnd = rnd;
        }

        public void redrawBlinky(Graphics g)
        {
            int rectHeight = 17;
            int rectWidth = 17; 
            // podle state
            switch (state)
            {
                case GhostState.chase:
                    g.DrawImage(Properties.Resources.rsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                case GhostState.frightened:
                    g.DrawImage(Properties.Resources.crazy, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                case GhostState.eaten:
                    g.DrawImage(Properties.Resources.msx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources.rsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
            }
        }

        bool isFree(int a, int b, Pacman pac)
        {
            if (pac.map.board[a][b] != 'B')
            {
                return true;
            }
            return false;
        }

        double distanceBetween2(int x, int y, int targetX, int targetY)
        {
            double a = Math.Abs(x - targetX);
            double b = Math.Abs(y - targetY);
            return Math.Sqrt(a * a + b * b);
        }

        void findTargetByState(Pacman pac)
        {
            targetX = pac.x;
            targetY = pac.y;
            if (state == GhostState.frightened)
            {
                targetX = rnd.Next(19);
                targetY = rnd.Next(22);
            }
        }
        public void moveGhost(Pacman pac)
        {
            if (pac.smer == PressedDirection.no) { return; }
            if (state == GhostState.frightened) { counter += 1; }
            if (counter == 50) { state = GhostState.chase; counter = 0; }
            if (counter == 1) { turnAround(); return; } // return;

            findTargetByState(pac);

            double distance = double.PositiveInfinity;
            PressedDirection docdir = PressedDirection.up;

            // go through tunnel
            if (x == 18 && y == 10 && dir == PressedDirection.right)
            {
                x = 0;
                return;
            }
            if (x == 0 && y == 10 && dir == PressedDirection.left)
            {
                x = 18;
                return;
            }

            // check the directions and compute distance between ghost move and target
            if ((dir != PressedDirection.down) && isFree(y-1,x, pac))  //nahoru
            {
                distance = distanceBetween2(x, y - 1, targetX, targetY);
                docdir = PressedDirection.up;
                //this.y = y - 1;
            }
            if ((dir != PressedDirection.right) && isFree(y, x-1, pac))  //doleva
            {
                double tempDistance = distanceBetween2(x-1, y, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = PressedDirection.left;
                }
            }
            if ((dir != PressedDirection.up) && isFree(y+1, x, pac))  //dolu
            {
                double tempDistance = distanceBetween2(x, y+1, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = PressedDirection.down;
                }
            }
            if ((dir != PressedDirection.left) && isFree(y, x+1, pac))  //doprava
            {
                double tempDistance = distanceBetween2(x+1,y, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = PressedDirection.right;
                }
            }

            if (docdir == PressedDirection.up && dir !=PressedDirection.down) { this.y = y - 1; dir = PressedDirection.up; }
            else if (docdir == PressedDirection.left && dir != PressedDirection.right) { this.x -= 1; dir = PressedDirection.left; }
            else if (docdir == PressedDirection.down && dir != PressedDirection.up) { this.y += 1; dir = PressedDirection.down; }
            else if (docdir == PressedDirection.right && dir != PressedDirection.left) { this.x += 1; dir = PressedDirection.right; }

        }

        public void turnAround()
        {
            switch (dir)
            {
                case PressedDirection.left:
                    dir = PressedDirection.right;
                    break;
                case PressedDirection.up:
                    dir = PressedDirection.down;
                    break;
                case PressedDirection.right:
                    dir = PressedDirection.left;
                    break;
                case PressedDirection.down:
                    dir = PressedDirection.up;
                    break;
                default:
                    break;
            }
        }
    }

    internal class Map
    {
        public List<List<char>> board;
        int widthCount = 19;
        int heightCount = 22;
        public int rectHeight = 17;
        public int rectWidth = 17;

        public State state = State.notStarted;

        public Map(string file)
        {
            board = loadMap(file);
        }
 
        List<List<char>> loadMap(string path)
        {
            List<List<char>> charMap = new List<List<char>>();
            string text = File.ReadAllText(path);  //"board.txt"

            List<char> list = new List<char>();
            foreach (char c in text)
            {
                if (c != '\n')
                {
                    list.Add(c);
                }
            }

            for (int i = 0; i < 22; i++)
            {
                List<char> newList = new List<char>();
                for (int j = 0; j < 19; j++)
                {
                    newList.Add(list[20 * i + j]);
                }
                charMap.Add(newList);
            }
            return charMap;
        }

        public void redrawMap(List<List<char>> updatedMap, Graphics g)  // tady se nemeni pacman, ale jenom mizi coins, ty ted neresim
        {
            for (int y = 0; y < heightCount; y++)
            {
                for (int x = 0; x < widthCount; x++)
                {
                    char c = updatedMap[y][x];
                    switch (c)
                    {
                        case 'B':
                            g.FillRectangle(Brushes.DarkGray, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            //g.DrawImage(brick, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case 'C':
                            g.FillEllipse(Brushes.Yellow, x * rectWidth + 4, y * rectHeight + 4, rectWidth - 10, rectHeight - 10);
                            //g.FillRectangle(Brushes.Green, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            //g.DrawImage(Properties.Resources.coin, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case 'T':
                            g.FillEllipse(Brushes.Orange, x * rectWidth + 2, y * rectHeight + 2, rectWidth - 7, rectHeight - 7);
                            //g.DrawImage(Properties.Resources.token, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        //case 'P':
                        //    g.DrawImage(Properties.Resources._1sx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                        //    break;
                        default:
                            g.FillRectangle(Brushes.Black, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            //g.DrawImage(Properties.Resources.empty, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                }
            }
        }
    }
}
