using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverDisplay;
    PipeSpawn timerPausedLogic;

    void Start()
    {
        timerPausedLogic = GameObject.FindGameObjectWithTag("PipeSpawnLogic").GetComponent<PipeSpawn>();
    }

    [ContextMenu("Increase Score")]
    public void increaseScore(int incrementValue)
    {
        playerScore = playerScore + incrementValue;
        scoreText.text = playerScore.ToString();

    }

    public void restartGame()
    {
        Debug.Log("Restarted game");
        Debug.Log("Pre Game Scene is loaded");
        SceneManager.LoadScene("PreGameScene");
    }

    public void gameOver()
    {
        gameOverDisplay.SetActive(true);
        timerPausedLogic.timerPaused = true;

    }
}
