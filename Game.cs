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

    class Pacman
    {
        public int x;
        public int y;
        public int rectHeight = 17;
        public int rectWidth = 17;
        // potrebuju smer
        public PressedDirection smer = PressedDirection.no;
        public Pacman(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        // + smer a mapa?

        public void redrawPacman(Graphics g)
        {
            g.DrawImage(Properties.Resources._1sx, this.x * rectWidth, this.y * rectHeight, rectWidth, rectHeight);
        }

        // je volno? ... tam kam chci jit
        public bool isFree(Map map, int a, int b)
        {
            if (map.board[a][b] != 'B')
            {
                return true;
            }
            return false;
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
            //redrawMap(board, g);
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
                            g.FillRectangle(Brushes.Black, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
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
