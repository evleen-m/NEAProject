using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class GameLogic : MonoBehaviour
{
    SpriteSpawn spriteSpawn;
    PlayerMovement player;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text lifeText;
    [SerializeField]
    private Text summaryText;

    [SerializeField]
    private GameObject gameOverDisplay;
    private GameObject extraLife;
    [SerializeField]
    private GameObject[] hearts;

    public SpriteRenderer BackgroundImage;

    // Game variables - changes during runtime
    private int lifeCounter = 3;
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
        // Increases playerScore by incrementValue when called
        // Displays the updated score
        // updates stats table
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
            spriteSpawn.SetTimer(true); // stops game timer to stop pipes spawning

            Stats.SaveToFile();
            var stats = Stats.Compare();
            Debug.Log(stats.ToString());

            summaryText.text = stats.ToString();

            gameOverDisplay.SetActive(true); // displays gameover screen 
            
            DestroyAllLives(); // destroys all hearts on screen 


            //log to stats
            

           // string filePath = Stats.CheckForFile("output.txt");
            //Stats.AppendTextToFile(filePath);
        }

    
    }



    public void AddLife()
    {
        if (lifeCounter == 9)
        {
            
        }
        else
        {
            
            hearts[lifeCounter].SetActive(true);
            // sets the current heart sprite to active (visible

            Stats.Record.HeartsGained++;
            
            lifeCounter++;
            // increases the player life by 1

            lifeText.text = lifeCounter.ToString();
            // displays the players remaining lives next to the hearts
        
        }
    }





    public void DetectedCollision()
    {
        //checks if the player bird has immunity - if it does no further damage is taken.
        if (player.GetHasImmunity())
        {
            return;
        }

        else
        {
            Stats.Record.HeartsLost++;
            //player does not have immunity so collision causes bird to lose a life when enemy object is hit
            RemoveSingleLife();

            IdentifyObjectCollision();

            //sets the player immunity to be true (acitvated)
            player.SetHasImmunity(true);
            player.SetImmunityCounter(0);

            //used to indicate damage and immunity to the player
            SetBackgroundRed();

            //used for developer debugging
            Debug.Log("Immunity is active");
        }
    }
    public void SetBackgroundNormal()
    {
        BackgroundImage.color = new Color(1f, 1f, 1f, 0.8313726f);
    }

    public int GetLifeCounter()
    {
        return lifeCounter;
    }

    private void SetBackgroundRed()
    {
        BackgroundImage.color = new Color(1f, 0f, 0f, 0.8313726f);
    }

    //used for debugging ans identifying which object the player came into contact with
    private void IdentifyObjectCollision()
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
    private void RemoveSingleLife()
    {

        // removes the next heart each time the procedure is called

        lifeCounter--;
        // removes one life from life counter
        
        lifeText.text = lifeCounter.ToString(); 
        // displays the players remaining lives next to the hearts -> prevent confusion
        
        hearts[lifeCounter].SetActive(false);
        //sets the sprite to be inactive - invisible

        Debug.Log("lost life"); //debugging
    }
}
