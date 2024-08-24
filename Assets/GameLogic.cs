using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
  
    public Text scoreText;
    public Text livesText;
 

    public GameObject gameOverDisplay;
    public GameObject[] hearts; 
    
    PipeSpawn timerPausedLogic;
    
    public int timeCounter;
    

    // Game Constants - do not change during runtime
    public float DeadZoneY = 5.1f;
    public float Ceiling = 15.3f;
    public float JumpHeight = 20f;

    // Game variables - changes during runtime
    public int playerScore;
    public int playerLife = 3;
    public float SpawnRate;
    public float HeightOffset = 4.5f;
    private int numGameOverRun = 0;
    public int numberOfPlayers = 1;


    void Start()
    {
        timerPausedLogic = GameObject.FindGameObjectWithTag("PipeSpawnLogic").GetComponent<PipeSpawn>();
    }

    
    public float RandomSpawn()
    {
        // Randomly generates a spawn rate for pipes - pipes spawn at different distances to each other
        SpawnRate = Random.Range(2,6);
        return SpawnRate;
    }


    public void increaseScore(int incrementValue)
    {
        // Increases playerScore by one when called
        // Displays the updated score
        playerScore = playerScore + incrementValue;
        scoreText.text = playerScore.ToString();

    }

    public void restartGame()
    {
        // Loads the pre game scene when button is clicked
        Debug.Log("Restarted game");
        Debug.Log("Pre Game Scene is loaded");
        SceneManager.LoadScene("PreGameScene");
    }

    public void gameOver()
    {

        
        numGameOverRun += 1;
        Debug.Log("game over ran:"+ numGameOverRun);

        // if the number of times game over is run is equal to the number of players in game the game is ended.
        // ensures all players have died and there is no remaining player.
        // displays the game over screen and stops timer to stop pipes spawning
        if (numGameOverRun == numberOfPlayers)
        {
            gameOverDisplay.SetActive(true);
            timerPausedLogic.timerPaused = true;
        }
    }

    public void removeLife()
    {
        playerLife -= 1;
        livesText.text = playerLife.ToString();

        Destroy(hearts[0]);

        if (playerLife == 1)
        {
            Destroy(hearts[1]);
        }
        else
        {
            Destroy(hearts[2]);
        }


    }
}
