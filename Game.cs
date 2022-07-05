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
    public class Board
    {

    }
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            // vytvor mapu
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

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Close();
            // nacti mapu();
            List<List<char>> mapFromFile = loadMap("board.txt");
            createMap(mapFromFile);
        }

        void createMap(List<List<char>> mapFromFile)
        {
            int rectHeight = 17;
            int rectWidth = 17;
            int widthCount = 19;
            int heightCount = 22;
            for (int y = 0; y < heightCount; y++)
            {
                for (int x = 0; x < widthCount; x++)
                {
                    char c = mapFromFile[y][x];
                    switch (c)
                    {
                        case 'B':
                            PictureBox p = new PictureBox
                            {
                                Name = "brick",
                                Size = new Size(rectWidth, rectHeight),
                                Location = new Point(x * rectWidth, y * rectHeight),
                                Image = Properties.Resources.brick,
                            };
                            this.Controls.Add(p);
                            break;
                        case ' ':
                            PictureBox q = new PictureBox
                            {
                                Name = "empty",
                                Size = new Size(rectWidth, rectHeight),
                                Location = new Point(x * rectWidth, y * rectHeight),
                                Image = Properties.Resources.empty,
                            };
                            this.Controls.Add(q);
                            break;
                        case 'P':
                            PictureBox r = new PictureBox
                            {
                                Name = "pacman",
                                Size = new Size(rectWidth, rectHeight),
                                Location = new Point(x * rectWidth, y * rectHeight),
                                Image = Properties.Resources.pacdx,
                                SizeMode = PictureBoxSizeMode.StretchImage,
                            };
                            this.Controls.Add(r);
                            break;
                        default:
                            PictureBox s = new PictureBox
                            {
                                Name = "brick",
                                Size = new Size(rectWidth, rectHeight),
                                Location = new Point(x * rectWidth, y * rectHeight),
                                Image = Properties.Resources.brick,
                            };
                            this.Controls.Add(s);
                            break;
                    }
                }
            }
        }
    }
}
