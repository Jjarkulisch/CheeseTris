using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public List<string[]> scores;
    public List<TextMeshProUGUI[]> text;
    public ScrollRect scrollRect;
    public GameObject lineEmpty;

    public void Awake()
    {
        if (!Directory.Exists("data"))
            return;

        scores = new List<string[]>();
        string[] rawData = File.ReadAllLines("data\\scores.txt");
        RectTransform rt = (RectTransform)lineEmpty.transform;
        Vector2 contentSize = new Vector2(0, rawData.Count() * (1.8f*rt.rect.height));

        scrollRect.content.sizeDelta = contentSize;
        lineEmpty.transform.localPosition = new Vector3(rt.rect.width/2, 0 - (rt.rect.height / 2));

        List<string> sortedData = rawData.ToList();
        sortedData.Sort(CompareScore);
        sortedData.Reverse();
        rawData = sortedData.ToArray();

        for (int y = 0; y < rawData.Length; y++)
        {
            GameObject newLine = Instantiate(lineEmpty, lineEmpty.transform.position - new Vector3(0, y * rt.rect.height), new Quaternion(0, 0, 0, 0), scrollRect.content.transform);
            TextMeshProUGUI[] text = newLine.GetComponentsInChildren<TextMeshProUGUI>();
            string line = rawData[y];
            
            for (int x = 0; x < 3; x++)
                text[x].text = line.Split()[x];

            newLine.SetActive(true);
        }
    }
    private static int CompareScore(string x, string y)
    {
        int retval = int.Parse(x.Split()[1]).CompareTo(int.Parse(y.Split()[1]));

        if (retval != 0)
        {
            return retval;
        }
        else
        {
            return x.CompareTo(y);
        }
    }
}

