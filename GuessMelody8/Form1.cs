using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessMelody8
{
    public partial class Form1 : Form
    {
        FParams formParameters = new FParams();
        FGame formGame = new FGame();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnParams_Click(object sender, EventArgs e)
        {
            formParameters.ShowDialog();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            formGame.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Quiz.ReadParam();
            Quiz.ReadMusic();
        }
    }
}
