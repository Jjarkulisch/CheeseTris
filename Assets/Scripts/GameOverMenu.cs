using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public string score;
    public void Start()
    {
        string path = "data\\temp.txt";
        score = File.ReadAllText(path);
        scoreText.text = score;
    }
    public void ToStart()
    {
        SceneManager.LoadScene("Start");
    }
}