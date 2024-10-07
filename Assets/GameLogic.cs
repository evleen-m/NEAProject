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
    public GameObject extraLife;
    public GameObject[] hearts;

    public SpriteRenderer BackgroundImage;
    
    SpriteSpawn timerPausedLogic;


    // Game Constants - do not change during runtime can be placed into the classes??
    public float DeadZoneY = 4.9f;
    public float deadZoneX = -14f;
    public float Ceiling = 15.3f;

    public float JumpHeight = 3f;
    public float moveSpeed = 2.5f;


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


    public float RandomSpawnRate()
    {
        // Randomly generates a spawn rate for pipes - pipes spawn at different distances to each other
        SpawnRate = UnityEngine.Random.Range(2,6);
        return SpawnRate;
    }


    public void IncreaseScore(int incrementValue)
    {
        // Increases playerScore by one when called
        // Displays the updated score
        playerScore = playerScore + incrementValue;
        scoreText.text = playerScore.ToString();

    }


    public void RestartGame()
    {
        // Loads the pre game scene when button is clicked
        Debug.Log("Restarted game");
        Debug.Log("Pre Game Scene is loaded");
        SceneManager.LoadScene("PreGameScene");
    }


    public void GameOver()
    {
        numGameOverRun += 1;
        Debug.Log("game over ran:"+ numGameOverRun);

        // if the number of times game over is run is equal to the number of players selected, the game is ended.
        // ensures all players have died and there is no remaining player.
        if (numGameOverRun == numberOfPlayers)
        {
            timerPausedLogic.timerPaused = true; // stops game timer to stop pipes spawning
            gameOverDisplay.SetActive(true); // displays gameover screen 
            
            DestroyAllLives(); // destroys all hearts on screen 
        }
    }


    public void RemoveLife()
    {
        // removes the next heart each time the procedure is called

        lifeCounter -= 1; // removes one life from life counter
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
      
        if (lifeCounter == 2)
        {
            hearts[0].SetActive(false);

        }
        else if (lifeCounter == 1) 
        {
            hearts[1].SetActive(false);

        }
        else if (lifeCounter == 0) 
        {
            hearts[2].SetActive(false);

        }

        Debug.Log("lost life");
    }

    public void SetBackgroundRed()
    {
        BackgroundImage.color = new Color(1f, 0f, 0f, 0.8313726f);
    } 
    public void SetBackgroundNormal()
    {
        BackgroundImage.color = new Color(1f, 1f, 1f, 0.8313726f);
    }


    public void AddLife()
    {
        if (lifeCounter == 3)
        {

        }
        else
        {
            lifeCounter = 3;
            lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
            
            for (int i = 0; i <= 2; i++) // loops through array to add all  hearts
            {
                hearts[i].SetActive(true);
            }

        }
    }

    private void DestroyAllLives()
    {
        // is used as player may fall below view before losing all lives
        // all active hearts would not be deleted, this procedure ensures that they will be

        lifeCounter = 0; // sets lives to 0
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
        
        for (int i = 0; i <= 2; i++) // loops through array to destroy all active hearts
        {
            hearts[i].SetActive(false);
        }
    }
}
