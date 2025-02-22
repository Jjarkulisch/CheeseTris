using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveMenu : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameOverMenu gameOverMenu;
    public void Save()
    {
        string userName = inputField.text;

        if (string.IsNullOrEmpty(userName))
            return;

        string score = gameOverMenu.score;
        DateTime date = DateTime.Now;
        
        string path = "data\\scores.txt";

        if (!File.Exists(path))
            File.Create(path);

        string userInfo = string.Format("{0} {1} {2} \r\n", userName, score, date);
        File.AppendAllText(path, userInfo);

        SceneManager.LoadScene("Start");
    }
}