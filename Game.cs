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
    internal class Map
    {
        private List<List<char>> board;
        int widthCount = 19;
        int heightCount = 22;
        int rectHeight = 17;
        int rectWidth = 17;

        public State state = State.notStarted;

        public Map(string file, Graphics g)
        {
            board = loadMap(file);
            createMap(board, g);
        }
 
        public List<List<char>> loadMap(string path)
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

        void createMap(List<List<char>> mapFromFile, Graphics g)
        {
            for (int y = 0; y < heightCount; y++)
            {
                for (int x = 0; x < widthCount; x++)
                {
                    char c = mapFromFile[y][x];
                    switch (c)
                    {
                        case 'B':
                            //PictureBox p = new PictureBox
                            //{
                            //    Name = "brick",
                            //    Size = new Size(rectWidth, rectHeight),
                            //    Location = new Point(x * rectWidth, y * rectHeight),
                            //    Image = Properties.Resources.brick,
                            //};
                            //this.Controls.Add(p);
                            g.DrawImage(Properties.Resources.brick, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case ' ':
                            g.DrawImage(Properties.Resources.empty, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        case 'P':
                            g.DrawImage(Properties.Resources._1sx, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.empty, x * rectWidth, y * rectHeight, rectWidth, rectHeight);
                            break;
                    }
                }
            }
        }
    }
}
