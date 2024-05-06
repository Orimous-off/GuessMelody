using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GuessMelody8
{
    public partial class FGame : Form
    {
        Random random = new Random();
        int musicDuration = Quiz.musicDuration;
        bool[] players = new bool[2];

        public FGame()
        {
            InitializeComponent();
        }

        void MakeMusic()
        {
            if(Quiz.list.Count == 0) 
            {
                EndGame();
                return;
            }
            else
            {
                musicDuration= Quiz.musicDuration;
                int n = random.Next(0, Quiz.list.Count);
                WindowsMediaPlayer.URL = Quiz.list[n];
                Quiz.answer = System.IO.Path.GetFileNameWithoutExtension(WindowsMediaPlayer.URL);
                //WindowsMediaPlayer.Ctlcontrols.play();
                Quiz.list.RemoveAt(n);
                lblMediaCount.Text = Quiz.list.Count.ToString();
                players[0] = false;
                players[1] = false;
            }

        }

        private void btnNex_Click(object sender, EventArgs e)
        {
            timer1.Start();
            MakeMusic();
        }

        private void FGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            WindowsMediaPlayer.Ctlcontrols.stop();
        }

        private void FGame_Load(object sender, EventArgs e)
        {
            lblMediaCount.Text = Quiz.list.Count.ToString();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = Quiz.gameDuration;
            lblMusicDuration.Text = musicDuration.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value++;
            musicDuration--;
            lblMusicDuration.Text = musicDuration.ToString();
            if(progressBar1.Value == progressBar1.Maximum)
            {
                EndGame();
            }
            if (musicDuration==0)
                MakeMusic();

        }

        private void EndGame()
        {
            timer1.Stop();
            WindowsMediaPlayer.Ctlcontrols.stop();
        }

        private void btnPouse_Click(object sender, EventArgs e)
        {
            GamePouse();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
           GamePlay();
        }

        void GamePouse()
        {
            timer1.Stop();
            WindowsMediaPlayer.Ctlcontrols.pause();
        }

        void GamePlay()
        {
            timer1.Start();
            WindowsMediaPlayer.Ctlcontrols.play();
        } 

        private void FGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (!timer1.Enabled) return;
            if(players[0]==false && e.KeyCode == Keys.A) 
            {
                GamePouse();
                fMessage fMessage = new fMessage();
                fMessage.Text = "Игрок 1";
                SoundPlayer soundPlayer = new SoundPlayer("Resources\\1.mp3");
                soundPlayer.PlaySync();
                players[0] = true; 
                if (fMessage.ShowDialog() == DialogResult.Yes)
                {
                    lblCounter1.Text = Convert.ToString(Convert.ToInt32(lblCounter1.Text)+1);
                    MakeMusic();
                }
                GamePlay();
            }
            if (players[1] == false && e.KeyCode == Keys.P)
            {
                GamePouse();
                fMessage fMessage = new fMessage();
                fMessage.Text = "Игрок 2";
                SoundPlayer soundPlayer = new SoundPlayer("Resources\\2.mp3");
                soundPlayer.PlaySync();
                players[1] = true;
                if (fMessage.ShowDialog() == DialogResult.Yes)
                {
                    lblCounter2.Text = Convert.ToString(Convert.ToInt32(lblCounter2.Text) + 1);
                    MakeMusic();
                }
                GamePlay();
            }
        }

        private void lblMusicDuration_Click(object sender, EventArgs e)
        {

        }

        private void WindowsMediaPlayer_Enter(object sender, EventArgs e)
        {

        }

        private void WindowsMediaPlayer_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if(Quiz.randomStart)
            {
                if(WindowsMediaPlayer.openState == WMPLib.WMPOpenState.wmposMediaOpen)
                {
                    WindowsMediaPlayer.Ctlcontrols.currentPosition = random.Next(0, (int)WindowsMediaPlayer.currentMedia.duration / 2);
                }

            }
        }

        private void lblCounter1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                (sender as Label).Text = Convert.ToString(Convert.ToInt32((sender as Label).Text) + 1);
            if(e.Button == MouseButtons.Right)
                (sender as Label).Text = Convert.ToString(Convert.ToInt32((sender as Label).Text) - 1);
        }

    }
}
