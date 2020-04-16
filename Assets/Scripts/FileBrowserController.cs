using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SmartDLL;

public class FileBrowserController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private bool readFile = false;
    //string path;

    public SmartFileExplorer fileExplorer = new SmartFileExplorer();

    private void Update()
    {
        if(fileExplorer.resultOK && readFile)
        {
            GetAudio(fileExplorer.fileName);
            readFile = false;
        }
    }

    public void OpenExplorer()
    {
        //path = EditorUtility.OpenFilePanel("Open Mp3 File", "", "mp3");
        string initialDir = @"C:\";
        bool restoreDir = true;
        string title = "Open Mp3 File";
        string defExt = "mp3";
        string filter = "mp3 files (*.mp3)|*.mp3";

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);

        readFile = true;
    }

    private void GetAudio(string path)
    {
        if(path != null)
        {
            UpdateAudio(path);
        }
    }

    private void UpdateAudio(string path)
    {
        WWW www = new WWW("file://" + path);
        _audioSource.clip = NAudioPlayer.FromMp3Data(www.bytes);
    }
}
