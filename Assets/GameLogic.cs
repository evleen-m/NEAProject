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
    
    public int timeCounter;
    public Text timeText;



    // Game Constants
    public float DeadZoneY = 5.1f;
    public float Ceiling = 15.3f;
    public float JumpHeight = 20f;

    public float SpawnRate;
    public float HeightOffset = 4.5f;
    

    void Start()
    {
        timerPausedLogic = GameObject.FindGameObjectWithTag("PipeSpawnLogic").GetComponent<PipeSpawn>();
    }

    public float RandomSpawn()
    {
        SpawnRate = Random.Range(2,6);
        return SpawnRate;
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

    public void LeaderboardPressed()
    {
        Debug.Log("LeaderboardPressed");
        SceneManager.LoadScene("LeaderboardScene");
        Debug.Log("LeaderBoard scene loaded");
    }
}
