using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class KeyBinds : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public TextMeshProUGUI moveLeft, moveRight, softDrop, hardDrop, rotateRight, rotateLeft, rotate180, swapHeld, forfeit;
    public Slider SDF;
    public Slider ARR;
    public Slider DAS;
    private GameObject button;
    private GameObject previous;
    private Color32 textColor = new Color32(82, 129, 201, 255);
    public void Start()
    {
        keys.Add("LeftInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftInput", "LeftArrow")));
        keys.Add("RightInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightInput", "RightArrow")));
        keys.Add("SoftDropInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SoftDropInput", "DownArrow")));
        keys.Add("HardDropInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("HardDropInput", "C")));
        keys.Add("RotateRightInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateRightInput", "UpArrow")));
        keys.Add("RotateLeftInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateLeftInput", "X")));
        keys.Add("Rotate180Input", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Rotate180Input", "Z")));
        keys.Add("SwapHeldInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwapHeldInput", "LeftShift")));
        keys.Add("ForfeitInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForfeitInput", "Escape")));

        moveLeft.text = keys["LeftInput"].ToString().ToLower();
        moveRight.text = keys["RightInput"].ToString().ToLower();
        softDrop.text = keys["SoftDropInput"].ToString().ToLower();
        hardDrop.text = keys["HardDropInput"].ToString().ToLower();
        rotateRight.text = keys["RotateRightInput"].ToString().ToLower();
        rotateLeft.text = keys["RotateLeftInput"].ToString().ToLower();
        rotate180.text = keys["Rotate180Input"].ToString().ToLower();
        swapHeld.text = keys["SwapHeldInput"].ToString().ToLower();
        forfeit.text = keys["ForfeitInput"].ToString().ToLower();

        SDF.value = PlayerPrefs.GetFloat("SDF");
        ARR.value = PlayerPrefs.GetFloat("ARR");
        DAS.value = PlayerPrefs.GetFloat("DAS");

        TextMeshProUGUI[] allText = new TextMeshProUGUI[] { moveLeft, moveRight, softDrop, hardDrop, rotateRight, rotateLeft, rotate180, swapHeld, forfeit };
        foreach (TextMeshProUGUI text in allText)
            if (text.text == "none")
                text.color = Color.red;
            else
                text.color = textColor;
    }
    public void ResetText()
    {
        if (button != null && button.GetComponentInChildren<TextMeshProUGUI>().text != keys[button.name].ToString().ToLower())
            button.GetComponentInChildren<TextMeshProUGUI>().text = keys[button.name].ToString().ToLower();
        button = null;
        previous = null;
    }
    private void OnGUI()
    {
        if (button != null)
        {
            previous = button;
            Event e = Event.current;
            button.GetComponentInChildren<TextMeshProUGUI>().color = textColor;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "[press key]";
            if (e.isKey)
            {
                if (!keys.ContainsValue(e.keyCode))
                {
                    keys[button.name] = e.keyCode;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = keys[button.name].ToString().ToLower();
                    button = null;
                }
                else if (e.keyCode == keys[button.name])
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = keys[button.name].ToString().ToLower();
                    button = null;
                }
                else
                {
                    keys[button.name] = KeyCode.None;
                    button.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    button.GetComponentInChildren<TextMeshProUGUI>().text = keys[button.name].ToString().ToLower();
                    button = null;
                }
            }
        }
        float SDF = this.SDF.value;
        float ARR = this.ARR.value;
        float DAS = this.DAS.value;
        PlayerPrefs.SetFloat("SDF", SDF);
        PlayerPrefs.SetFloat("ARR", ARR);
        PlayerPrefs.SetFloat("DAS", DAS);
    }
    public void BindKey(GameObject clicked)
    {
        if (previous != null || (previous != null && button == clicked))
            previous.GetComponentInChildren<TextMeshProUGUI>().text = keys[previous.name].ToString().ToLower();

        button = clicked;
    }
    public void SaveBinds()
    {
        foreach (var key in keys)
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        PlayerPrefs.Save();
    }
    public void ResetBinds()
    {
        PlayerPrefs.DeleteAll();
        keys.Clear();
        PlayerPrefs.SetFloat("SDF", 1);
        PlayerPrefs.SetFloat("ARR", 1);
        PlayerPrefs.SetFloat("DAS", 167);
        Start();
    }
}