using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class GameLogic : MonoBehaviour
{
    SpriteSpawn spriteSpawn;
    PlayerMovement player;

    public Text scoreText;
    public Text lifeText;
    public Text summaryText;

    public GameObject gameOverDisplay;
    public GameObject extraLife;
    public GameObject[] hearts;

    public SpriteRenderer BackgroundImage;

    // Game variables - changes during runtime
    public int lifeCounter = 3;
    private int playerScore;
    
    private int numGameOverRun = 0;
    private int numberOfPlayers = 1;


    void Start()
    {
        spriteSpawn = GameObject.FindGameObjectWithTag("SpriteSpawnLogic").GetComponent<SpriteSpawn>(); // used to allow access to the class PipeSpawn
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //Stats.Initialise();
    }

    public void IncreaseScore(int incrementValue)
    {
        // Increases playerScore by one when called
        // Displays the updated score
        playerScore = playerScore + incrementValue;
        scoreText.text = playerScore.ToString();
        Stats.Record.GameScore = Stats.Record.GameScore + incrementValue;


    }

    public void GameOver()
    {
        numGameOverRun += 1;
        Debug.Log("game over ran:"+ numGameOverRun);

        // if the number of times game over is run is equal to the number of players selected, the game is ended.
        // ensures all players have died and there is no remaining player.
        if (numGameOverRun == numberOfPlayers)//added so that can make game multiplayr in the future
        {
            spriteSpawn.timerPaused = true; // stops game timer to stop pipes spawning

            Stats.SaveToFile();
            var stats = Stats.Compare();
            Debug.Log(stats.ToString());

            summaryText.text = stats;

            gameOverDisplay.SetActive(true); // displays gameover screen 
            
            DestroyAllLives(); // destroys all hearts on screen 


            //log to stats
            

           // string filePath = Stats.CheckForFile("output.txt");
            //Stats.AppendTextToFile(filePath);
        }

    
    }

    public void RemoveSingleLife()
    {
        
        // removes the next heart each time the procedure is called

        lifeCounter--; // removes one life from life counter
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
        hearts[lifeCounter].SetActive(false);

        Debug.Log("lost life");
    }

    public void AddLife()
    {
        if (lifeCounter == 9)
        {
            
        }
        else
        {
           // displays the players remaining lives next to the hearts
            
            hearts[lifeCounter].SetActive(true);
            Stats.Record.HeartsGained++;
            lifeCounter++;
            lifeText.text = lifeCounter.ToString();
        }
    }

    private void DestroyAllLives()
    {
        // is used as player may fall below view before losing all lives
        // all active hearts would not be deleted, this procedure ensures that they will be

        lifeCounter = 0; // sets lives to 0
        lifeText.text = lifeCounter.ToString(); // displays the players remaining lives next to the hearts
        
        for (int i = 0; i <= 9; i++) // loops through array to destroy all active hearts
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
            Stats.Record.HeartsLost++;
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
