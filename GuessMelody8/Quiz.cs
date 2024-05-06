using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace GuessMelody8
{
    static class Quiz
    {
        static public List<String> list = new List<String>();
        static public int gameDuration = 60;
        static public int musicDuration = 10;
        static public bool randomStart = false;
        static public string lastFolder = "";
        static public bool allDirectories = false;
        static public string answer = "";

        static public void ReadMusic()
        {
            try
            {
                string[] musicFiles = Directory.GetFiles(
                    lastFolder, 
                    "*.mp3", 
                    allDirectories?SearchOption.AllDirectories:SearchOption.TopDirectoryOnly
                );
                list.Clear();
                list.AddRange(musicFiles);
            } 
            catch 
            { 

            }
        }
        static string readKeyName = "Software\\MyCompanyName\\MusicQuiz";

        static public void WriteParam()
        {
            RegistryKey registryKey = null;
            try
            {
                registryKey = Registry.CurrentUser.CreateSubKey(readKeyName);
                if (registryKey == null)
                    return;
                registryKey.SetValue("LastFolder", lastFolder);
                registryKey.SetValue("RandomStart", randomStart);
                registryKey.SetValue("GameDuration", gameDuration);
                registryKey.SetValue("MusicDuration", musicDuration);
                registryKey.SetValue("AllDirectories", allDirectories);
            }
            finally
            {
                if(registryKey != null) 
                    registryKey.Close();
            }
        }

        static public void ReadParam()
        {
            RegistryKey registryKey = null;
            try
            {
                registryKey = Registry.CurrentUser.OpenSubKey(readKeyName);
                if (registryKey != null)
                {
                    lastFolder = (string)registryKey.GetValue("LastFolder");
                    gameDuration = (int)registryKey.GetValue("GameDuration");
                    randomStart = Convert.ToBoolean(registryKey.GetValue("RandomStart", false));
                    musicDuration = (int)registryKey.GetValue("MusicDuration");
                    allDirectories = Convert.ToBoolean(registryKey.GetValue("AllDirectories", false));
                }
            }
            finally
            {
                if (registryKey != null) registryKey.Close();
            }
        }
    }
}
