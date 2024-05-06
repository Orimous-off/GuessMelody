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

namespace GuessMelody8
{
    public partial class FParams : Form
    {
        public FParams()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Quiz.allDirectories = cdAllDirectory.Checked;
            Quiz.gameDuration = Convert.ToInt32(cdGameDuration.Text);
            Quiz.musicDuration = Convert.ToInt32(cbMusicDuraction.Text);
            Quiz.randomStart = cbRamdomStart.Checked;


            Quiz.WriteParam();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Set();
            this.Hide();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBorwserDialog = new FolderBrowserDialog();
            if(folderBorwserDialog.ShowDialog() == DialogResult.OK)
            {
                string[] music_list = Directory.GetFiles(
                    folderBorwserDialog.SelectedPath, 
                    "*.mp3", 
                    cdAllDirectory.Checked?SearchOption.AllDirectories:SearchOption.TopDirectoryOnly
                );
                Quiz.lastFolder = folderBorwserDialog.SelectedPath;
                listBox1.Items.Clear();
                listBox1.Items.AddRange(music_list);
                Quiz.list.Clear();
                Quiz.list.AddRange(music_list);
            }
            folderBorwserDialog.ShowDialog();
        }

        void Set()
        {
            cdAllDirectory.Checked = Quiz.allDirectories;
            cdGameDuration.Text = Quiz.gameDuration.ToString();
            cbMusicDuraction.Text = Quiz.musicDuration.ToString();
            cbRamdomStart.Checked = Quiz.randomStart;
        }

        private void FParams_Load(object sender, EventArgs e)
        {
            Set();
            listBox1.Items.Clear();
            listBox1.Items.AddRange(Quiz.list.ToArray());
        }
    }
}
