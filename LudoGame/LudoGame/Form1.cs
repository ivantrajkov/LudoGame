using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LudoGame
{
    public partial class Form1 : Form
    {
        SoundPlayer music;
        int n = 0;
        public Form1()
        {
            InitializeComponent();
            string sPath = @"Assets\gameMusic.wav";
            music = new SoundPlayer(sPath);
            music.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (two.Checked)
            {
                n = 2;
            } else if (three.Checked)
            {
                n = 3;
            } else if (four.Checked)
            {
                n = 4;
            } else
            {
                MessageBox.Show("Select number of players!");
                return;
            }
            LudoMainGame game = new LudoMainGame(n);
            game.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
