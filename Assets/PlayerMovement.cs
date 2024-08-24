using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    //public GameObject PlayerSprite1;
    public GameLogic logic;
    bool isPlayerAlive = true;



    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerRigidbody.velocity = Vector2.up * logic.JumpHeight;
                //moves player up by jumpheight when W is pressed and bird is in play
            }

            if (transform.position.y > logic.Ceiling)
            {
                transform.position = new Vector3(transform.position.x, logic.Ceiling, 0);
                Debug.Log("bird position transform.position.y: " + transform.position.y);
                //moves bird down to if it goes above ceiling - stops players from flying off screen and over pipes
            }
        }


       // if bird falls below player view it is destroyed
        if (logic.DeadZoneY > transform.position.y)
        {
            destroyBird();
        }

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //causes bird to lose a life when it hits foreign object

        logic.removeLife();
        Debug.Log("lost life - hit pipe. lives remaining: " +logic.lifeCounter);

        //if player life is 0 or below, isPlayerALive is set to false
        //this stops the bird from taking inputs from user causes it to fall below screen - leads to game over
        if (logic.lifeCounter <= 0)
        {
            isPlayerAlive = false;
        }

    }

    public void destroyBird()
    {
        
        Debug.Log("bird deleted");
        Destroy(gameObject);

        // once bird is destroyed it calls gameOver() - displays the end screen
        Debug.Log("game over run");
        logic.gameOver();

    }


}


