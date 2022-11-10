using System.Collections;
using UnityEngine;
using System;
using System.IO;

public class FileManager : MonoBehaviour
{
    [SerializeField]
    private string fileName;

    private string[] songNames;
    private string[] songNotes;
 
    public void initSongFile() {
        string workdir = Directory.GetCurrentDirectory();
        char separator = Path.DirectorySeparatorChar;
        string songFilePath = String.Format("{0}{1}Assets{2}Songs{3}songs.txt", workdir, separator, separator, separator);
        try {
            string[] lines = System.IO.File.ReadAllLines(songFilePath);
            int numberOfSongs = Int32.Parse(lines[0]);
            this.songNotes = new string[numberOfSongs];
            this.songNames = new string[numberOfSongs];
            for (int i = 1; i < lines.Length; i++) {
                string[] song = lines[i].Split("-");
                string songName = song[0];
                string songNotes = song[1];
                this.songNotes[i-1] = songNotes;
                this.songNames[i-1] = songName;
            }
        } catch (FileNotFoundException e) {
            Debug.Log(String.Format("File {0} not found!\nMessage:", songFilePath, e.Message));
        }
    }

    public string getNotesFromFile(int songIndex) {
        //Debug.Log(String.Format("Playing song {0}, with notes {1}", this.songNames[songIndex], this.songNotes[songIndex]));
        return this.songNotes[songIndex];
    }
}
