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
    float immunityCounter = 0f;
    bool hasImmunity = false;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        CheckToResetImmunity();

        immunityCounter += Time.deltaTime;
        
        if (isPlayerAlive)
        {
           ValidPlayerMovement();
        }


       // if bird falls below player view it is destroyed
        if (logic.DeadZoneY > transform.position.y)
        {
            DestroyBird();
        }

    }

    private void ValidPlayerMovement()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if (immunity) { return; }
        if (hasImmunity)
        {
            return;
        }
        else
        {
            //causes bird to lose a life when it hits foreign object
            logic.RemoveLife();
            //used for debugging ans identifying which object the player came into contact with
            IdentifyObjectCollision();
            hasImmunity = true;
        }

        //if player life is 0 or below, isPlayerALive is set to false
        //this stops the bird from taking inputs from user causes it to fall below screen - leads to game over
        if (logic.lifeCounter <= 0)
        {
            isPlayerAlive = false;
        }
    }

    public void CheckToResetImmunity()
    {
        if (immunityCounter == 3 && hasImmunity)
        {
            hasImmunity = false;
        }
    }

    public void DestroyBird()
    {
        Debug.Log("bird deleted");
        Destroy(gameObject);

        // once bird is destroyed it calls gameOver() - displays the end screen
        Debug.Log("game over run");
        logic.GameOver();
    }

    private void IdentifyObjectCollision()
    {
        if (gameObject.CompareTag("Pipe"))
        {
            Debug.Log("lost life - hit pipe. lives remaining: " + logic.lifeCounter);
        }
        else if (gameObject.CompareTag("Bat"))
        {
            Debug.Log("lost life - hit bat. lives remaining: " + logic.lifeCounter);
        }
    }
}


