using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class StatCanvas : MonoBehaviour
{
    public TextMeshProUGUI score, combo, B2B, cheeseTime;
    public Board board;

    public void Refresh()
    {
        score.text = board.score.ToString();
        B2B.text = board.B2BLvl.ToString();
        combo.text = board.combo.ToString();
    }
    private void Update()
    {
        if (board.cheeseQueue == 0)
            cheeseTime.text = Math.Round(board.cheeseTime - Time.time, 3).ToString();
        else if (board.meterGrid.coolDown)
            cheeseTime.text = ((int)board.cheeseDelay).ToString();
        else
            cheeseTime.text = "0";
    }
}