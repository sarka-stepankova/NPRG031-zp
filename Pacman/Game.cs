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
    enum Direction { no, left, up, right, down };
    public enum GhostState { chase, frightened, eaten }
    internal class Map
    {
        public List<List<char>> board;
        int widthCount = 19;
        int heightCount = 22;
        public int rectHeight = 17;
        public int rectWidth = 17;
        public int numOfLives;

        public Map(string file)
        {
            board = loadMap(file);
        }

        List<List<char>> loadMap(string path)
        {
            List<List<char>> charMap = new List<List<char>>();
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            string text = sr.ReadToEnd();

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
                    newList.Add(list[19 * i + j]);
                }
                charMap.Add(newList);
            }
            return charMap;
        }

        public void redrawMap(List<List<char>> updatedMap, Graphics g)
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
                            break;
                        case 'C':
                            g.FillEllipse(Brushes.Yellow, x * rectWidth + 4, y * rectHeight + 4, rectWidth - 10, rectHeight - 10);
                            break;
                        case 'T':
                            g.FillEllipse(Brushes.Orange, x * rectWidth + 2, y * rectHeight + 2, rectWidth - 7, rectHeight - 7);
                            break;
                        default:
                            g.FillRectangle(Brushes.Black, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                }
            }
        }
    }

    class Pacman
    {
        public int x;
        public int y;
        public Map map;
        public int rectHeight = 17;
        public int rectWidth = 17;
        public int score = 0;
        public int coins = 150;
        public Direction direction = Direction.no;
        public Pacman(int x, int y, Map map)
        {
            this.x = x;
            this.y = y;
            this.map = map;
        }

        public void redrawPacman(Graphics g)
        {
            switch (direction)
            {
                case Direction.left:
                    g.DrawImage(Properties.Resources._1sx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case Direction.right:
                    g.DrawImage(Properties.Resources._1dx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case Direction.up:
                    g.DrawImage(Properties.Resources._1up, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                case Direction.down:
                    g.DrawImage(Properties.Resources._1down, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources._1sx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
                    break;
            }

        }

        // Pro pacmana, kdyz potrebuje zjistit, jestli je na policku, na ktere chce jit, volno
        bool isFree(int a, int b)
        {
            if (map.board[a][b] != 'B')
            {
                return true;
            }
            return false;
        }

        // Zjisti, kde je volno a kam muze pacman jit a pak ho tam presune
        public void movePacman(Direction tempDir)
        {
            // go through tunnel
            if (this.x == 18 && this.y == 10 && this.direction == Direction.right)
            {
                this.x = 1;
            }
            else if (this.x == 0 && this.y == 10 && this.direction == Direction.left)
            {
                this.x = 18;
            }

            // Prvne se pacman podiva na to, jestli nebyl zmacknuty jiny smer, kam by mohl jit
            else if (this.isFree(this.y - 1, this.x) && tempDir == Direction.up)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.y -= 1;
                this.direction = Direction.up;
            }
            else if (this.isFree(this.y + 1, this.x) && tempDir == Direction.down)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.y += 1;
                this.direction = Direction.down;
            }
            else if (this.isFree(this.y, this.x - 1) && tempDir == Direction.left)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.x -= 1;
                this.direction = Direction.left;
            }
            else if (this.isFree(this.y, this.x + 1) && tempDir == Direction.right)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.x += 1;
                this.direction = Direction.right;
            }

            // jinak pokracuje smerem ktery uz ma (popr. zataci, kdyz uz nemuze dal svym smerem) 
            else if (this.isFree(this.y - 1, this.x) && this.direction == Direction.up)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.y -= 1;
            }
            else if (this.isFree(this.y + 1, this.x) && this.direction == Direction.down)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.y += 1;
            }
            else if (this.isFree(this.y, this.x - 1) && this.direction == Direction.left)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.x -= 1;
            }
            else if (this.isFree(this.y, this.x + 1) && this.direction == Direction.right)
            {
                if (map.board[y][x] == 'C') { score += 1; coins -= 1; }
                this.map.board[this.y][this.x] = ' ';
                this.x += 1;
            }   
        }
    }

    abstract class Ghost
    {
        public int x;
        public int y;
        public Direction dir;
        public GhostState state = GhostState.chase;
        public int counter = 0;
        public Random rnd;
        public int targetX;
        public int targetY;
        public int prevX;
        public int prevY;

        public abstract void redrawGhost(Graphics g);

        bool isFree(int a, int b, Pacman pac)
        {
            if (pac.map.board[a][b] != 'B')
            {
                return true;
            }
            return false;
        }

        // Najdi vzdalenost mezi 2 body na mape (vzdusnou carou)
        public double findDistanceBetween2(int x, int y, int targetX, int targetY)
        {
            double a = Math.Abs(x - targetX);
            double b = Math.Abs(y - targetY);
            return Math.Sqrt(a * a + b * b);
        }

        public abstract void findTargetByState(Pacman pac);
        
        public void moveGhost(Pacman pac)
        {
            if (pac.direction == Direction.no) { return; }
            if (state == GhostState.frightened) { counter += 1; }
            if (counter == 50) { state = GhostState.chase; counter = 0; }

            findTargetByState(pac);

            double distance = double.PositiveInfinity;
            Direction docdir = Direction.up;

            // go through tunnel
            if (x == 18 && y == 10 && dir == Direction.right)
            {
                x = 0;
                return;
            }
            if (x == 0 && y == 10 && dir == Direction.left)
            {
                x = 18;
                return;
            }

            // check the directions and find the closest distance
            // between target and possible moves
            if ((dir != Direction.down) && isFree(y-1,x, pac))  //up
            {
                distance = findDistanceBetween2(x, y - 1, targetX, targetY);
                docdir = Direction.up;
            }
            if ((dir != Direction.right) && isFree(y, x-1, pac))  //left
            {
                double tempDistance = findDistanceBetween2(x-1, y, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = Direction.left;
                }
            }
            if ((dir != Direction.up) && isFree(y+1, x, pac))  //down
            {
                double tempDistance = findDistanceBetween2(x, y+1, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = Direction.down;
                }
            }
            if ((dir != Direction.left) && isFree(y, x+1, pac))  //right
            {
                double tempDistance = findDistanceBetween2(x+1,y, targetX, targetY);
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    docdir = Direction.right;
                }
            }

            if (docdir == Direction.up && dir !=Direction.down) { this.y = y - 1; dir = Direction.up; }
            else if (docdir == Direction.left && dir != Direction.right) { this.x -= 1; dir = Direction.left; }
            else if (docdir == Direction.down && dir != Direction.up) { this.y += 1; dir = Direction.down; }
            else if (docdir == Direction.right && dir != Direction.left) { this.x += 1; dir = Direction.right; }

        }

        public void turnAround()
        {
            switch (dir)
            {
                case Direction.left:
                    dir = Direction.right;
                    break;
                case Direction.up:
                    dir = Direction.down;
                    break;
                case Direction.right:
                    dir = Direction.left;
                    break;
                case Direction.down:
                    dir = Direction.up;
                    break;
                default:
                    dir = Direction.left;
                    break;
            }
        }

        public abstract void goBackHome();
    }

    // the red one
    class Blinky : Ghost 
    {
        public Blinky(int x, int y, Direction dir, Random rnd)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.rnd = rnd;
        }

        public override void redrawGhost(Graphics g)
        {
            int rectHeight = 17;
            int rectWidth = 17;
            switch (state)
            {
                case GhostState.chase:
                    // determine eyes (direction) and draw ghost with them
                    switch (dir)
                    {
                        case Direction.left:
                            g.DrawImage(Properties.Resources.rsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.right:
                            g.DrawImage(Properties.Resources.rdx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.down:
                            g.DrawImage(Properties.Resources.rdown, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.up:
                            g.DrawImage(Properties.Resources.rdx2, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.rsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                    break;
                case GhostState.frightened:
                    if (counter == 49 || counter == 47 || counter == 45 || counter == 43)
                    {
                        g.DrawImage(Properties.Resources.tempo, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    else
                    {
                        g.DrawImage(Properties.Resources.crazy, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    break;
                case GhostState.eaten:
                    g.DrawImage(Properties.Resources.msx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources.rsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
            }
        }

        public override void findTargetByState(Pacman pac)
        {
            targetX = pac.x;
            targetY = pac.y;
            if (state == GhostState.frightened)
            {
                targetX = rnd.Next(19);
                targetY = rnd.Next(22);
            }
            if (state == GhostState.eaten)
            {
                targetX = 9;
                targetY = 8;
            }
            // pro pinky - kdyz jde pacman nahoru, tak 4 nahoru a 4 doleva
        }

        public override void goBackHome()
        {
            this.x = 9;
            this.y = 8;
        }
    }

    // the pink one
    class Pinky : Ghost
    {
        public Pinky(int x, int y, Direction dir, Random rnd)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.rnd = rnd;
        }

        public override void redrawGhost(Graphics g)
        {
            int rectHeight = 17;
            int rectWidth = 17;
            switch (state)
            {
                case GhostState.chase:
                    // determine eyes (direction) and draw ghost with them
                    switch (dir)
                    {
                        case Direction.left:
                            g.DrawImage(Properties.Resources.vsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.right:
                            g.DrawImage(Properties.Resources.vdx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.down:
                            g.DrawImage(Properties.Resources.vdown, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.up:
                            g.DrawImage(Properties.Resources.vup, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.vsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                    break;
                case GhostState.frightened:
                    if (counter == 49 || counter == 47 || counter == 45 || counter == 43)
                    {
                        g.DrawImage(Properties.Resources.tempo, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    else
                    {
                        g.DrawImage(Properties.Resources.crazy, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    break;
                case GhostState.eaten:
                    g.DrawImage(Properties.Resources.msx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources.vsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
            }
        }

        public override void findTargetByState(Pacman pac)
        {
            targetX = pac.x;
            targetY = pac.y;
            if (pac.direction == Direction.right)
            {
                targetX = pac.x + 4;
                targetY = pac.y;
            } else if (pac.direction == Direction.left)
            {
                targetX = pac.x - 4;
                targetY = pac.y;
            } else if (pac.direction == Direction.up)
            {
                targetX = pac.x - 4;
                targetY = pac.y - 4;
            } else if (pac.direction == Direction.down)
            {
                targetX = pac.x;
                targetY = pac.y + 4;
            }
            if (pac.score < 10)
            {
                targetX = 10;
                targetY = 10;
            }

            if (state == GhostState.frightened)
            {
                targetX = rnd.Next(19);
                targetY = rnd.Next(22);
            }
            if (state == GhostState.eaten)
            {
                targetX = 9;
                targetY = 8;
            }
        }

        public override void goBackHome()
        {
            this.x = 8;
            this.y = 10;
        }
    }

    // the cyan one
    class Inky : Ghost
    {
        public Inky(int x, int y, Direction dir, Random rnd)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.rnd = rnd;
        }

        public override void redrawGhost(Graphics g)
        {
            int rectHeight = 17;
            int rectWidth = 17;
            switch (state)
            {
                case GhostState.chase:
                    // determine eyes (direction) and draw ghost with them
                    switch (dir)
                    {
                        case Direction.left:
                            g.DrawImage(Properties.Resources.asx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.right:
                            g.DrawImage(Properties.Resources.adx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.down:
                            g.DrawImage(Properties.Resources.adown, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.up:
                            g.DrawImage(Properties.Resources.aup, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.asx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                    break;
                case GhostState.frightened:
                    if (counter == 49 || counter == 47 || counter == 45 || counter == 43)
                    {
                        g.DrawImage(Properties.Resources.tempo, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    else
                    {
                        g.DrawImage(Properties.Resources.crazy, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    break;
                case GhostState.eaten:
                    g.DrawImage(Properties.Resources.msx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources.vsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
            }
        }

        public override void findTargetByState(Pacman pac)
        {
            targetX = pac.x;
            targetY = pac.y;
            
            if (findDistanceBetween2(x,y,pac.x,pac.y) < 8)
            {
                targetX = rnd.Next(19);
                targetY = rnd.Next(22);
            }

            if (state == GhostState.frightened)
            {
                targetX = rnd.Next(19);
                targetY = rnd.Next(22);
            }
            if (state == GhostState.eaten)
            {
                targetX = 9;
                targetY = 8;
            }

            if (pac.score < 15)
            {
                targetX = 10;
                targetY = 10;
            }
        }

        public override void goBackHome()
        {
            this.x = 9;
            this.y = 10;
        }
    }

    // the orange one
    class Clyde : Ghost
    {
        public Clyde(int x, int y, Direction dir, Random rnd)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.rnd = rnd;
        }

        public override void redrawGhost(Graphics g)
        {
            int rectHeight = 17;
            int rectWidth = 17;
            switch (state)
            {
                case GhostState.chase:
                    // determine eyes (direction) and draw ghost with them
                    switch (dir)
                    {
                        case Direction.left:
                            g.DrawImage(Properties.Resources.gsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.right:
                            g.DrawImage(Properties.Resources.gdx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.down:
                            g.DrawImage(Properties.Resources.gdown, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case Direction.up:
                            g.DrawImage(Properties.Resources.gup, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.gsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                    break;
                case GhostState.frightened:
                    if (counter == 49 || counter == 47 || counter == 45 || counter == 43)
                    {
                        g.DrawImage(Properties.Resources.tempo, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    else
                    {
                        g.DrawImage(Properties.Resources.crazy, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    }
                    break;
                case GhostState.eaten:
                    g.DrawImage(Properties.Resources.msx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
                default:
                    g.DrawImage(Properties.Resources.vsx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                    break;
            }
        }

        public override void findTargetByState(Pacman pac)
        {
            targetX = rnd.Next(19);
            targetY = rnd.Next(22);

            if (state == GhostState.eaten)
            {
                targetX = 9;
                targetY = 8;
            }

            if (pac.score < 20)
            {
                targetX = 11;
                targetY = 10;
            }
        }

        public override void goBackHome()
        {
            this.x = 10;
            this.y = 10;
        }
    }
}
