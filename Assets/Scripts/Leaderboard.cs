using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text leaderboardText;
    private void Start()
    {
        var x = Stats.Leaderboard();
        x = "SCORE: USERNAME: \n" + x;
        leaderboardText.text = x.ToString();

    }

}
