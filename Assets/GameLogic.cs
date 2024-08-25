using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameLogic : MonoBehaviour
{
  
    public Text scoreText;
    public Text lifeText;
 
    public GameObject gameOverDisplay;
    public GameObject[] hearts; 
    
    SpriteSpawn timerPausedLogic;


    // Game Constants - do not change during runtime

    public float DeadZoneY = 4.9f;
    public float Ceiling = 15.3f;
    public float JumpHeight = 3f;

    public float moveSpeed = 2.5f;
    public float deadZoneX = -14f;


    // Game variables - changes during runtime
    public int lifeCounter = 3;

    public float SpawnRate;
    public float HeightOffset = 4.5f;

    private int playerScore;
    private int numGameOverRun = 0;
    private int numberOfPlayers = 1;


    void Start()
    {
        timerPausedLogic = GameObject.FindGameObjectWithTag("SpriteSpawnLogic").GetComponent<SpriteSpawn>(); // used to allow access to the class PipeSpawn
            
    }

    
    public float RandomSpawn()
    {
        // Randomly generates a spawn rate for pipes - pipes spawn at different distances to each other
        SpawnRate = UnityEngine.Random.Range(2,6);
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

        // if the number of times game over is run is equal to the number of players selected, the game is ended.
        // ensures all players have died and there is no remaining player.
        if (numGameOverRun == numberOfPlayers)
        {
            timerPausedLogic.timerPaused = true; // stops game timer to stop pipes spawning
            gameOverDisplay.SetActive(true); // displays gameover screen 
            
            destroyAllLives(); // destroys all hearts on screen 
        }
    }

    public void removeLife()
    {
        // removes the next heart each time the procedure is called

        lifeCounter -= 1; // removes one life from life counter
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
      
        if (lifeCounter == 2)
        {
            Destroy(hearts[0]);
        }
        else if (lifeCounter == 1) 
        {
            Destroy(hearts[1]);
        }
        else
        {
            Destroy(hearts[2]);
        }
    }


    private void destroyAllLives()
    {
        // is used as player may fall below view before losing all lives
        // all active hearts would not be deleted, this procedure ensures that they will be

        lifeCounter = 0; // sets lives to 0
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
        
        for (int i = 0; i <= 2; i++) // loops through array to destroy all active hearts
        {
            Destroy(hearts[i]);
        }
    }

}
