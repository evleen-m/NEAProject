using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class GameLogic : MonoBehaviour
{
    SpriteSpawn timerPausedLogic;
    PlayerMovement player;
    void Start()
    {
        timerPausedLogic = GameObject.FindGameObjectWithTag("SpriteSpawnLogic").GetComponent<SpriteSpawn>(); // used to allow access to the class PipeSpawn
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public Text scoreText;
    public Text lifeText;
 
    public GameObject gameOverDisplay;
    public GameObject extraLife;
    public GameObject[] hearts;

    public SpriteRenderer BackgroundImage;

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
        Stats.GameProgress++;

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
            
            // log to stats
            //var stats = new Stats();
            string filePath = @"c:\\temp\\cs\\output.txt";
            string textToWrite = "Hello, this is the game file!";
            Stats.CreateFile(filePath, textToWrite);
            Stats.Save(filePath);
        }

    
    }

    public void RemoveSingleLife()
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

    public void AddLife()
    {
        if (lifeCounter == 3)
        {
            
        }
        else
        {
            Stats.GameLives++;
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

    public void SetBackgroundRed()
    {
        BackgroundImage.color = new Color(1f, 0f, 0f, 0.8313726f);
    }
    public void SetBackgroundNormal()
    {
        BackgroundImage.color = new Color(1f, 1f, 1f, 0.8313726f);
    }

    public void DetectedCollision()
    {
        //checks if the player bird has immunity - if it does no further damage is taken.
        if (player.hasImmunity)
        {
            return;
        }

        else
        {
            //player does no have immunity so collision causes bird to lose a life when foriegn object is hit
            RemoveSingleLife();

            IdentifyObjectCollision();

            //sets the player immunity
            player.hasImmunity = true;
            player.immunityCounter = 0;

            //used to indicate damage and immunity to the player
            SetBackgroundRed();

            //used for developer debugging
            Debug.Log("Immunity is active");
        }
    }

    public void CheckToResetImmunity()
    {
        if (player.immunityCounter >= 3 && player.hasImmunity)
        {
            player.hasImmunity = false;
            SetBackgroundNormal();
            Debug.Log("Reset immunity");
        }
    }

    //used for debugging ans identifying which object the player came into contact with
    public void IdentifyObjectCollision()
    {
        string tag = gameObject.tag;

        switch (tag)
        {
            case "Pipe":
                Debug.Log("Pipe");
                break;
            case "Star":
                Debug.Log("Star");
                break;
            case "Bat":
                Debug.Log("Bat");
                break;
            case "Lightning":
                Debug.Log("Lightning");
                break;
            case "ExtraLife":
                Debug.Log("ExtraLife");
                break;
        }
    }
}
