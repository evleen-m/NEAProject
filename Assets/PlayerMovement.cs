using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    GameLogic logic;
    public bool isPlayerAlive = true;
    public float immunityCounter = 0f;
    public bool hasImmunity = false;

    public float DeadZoneY = 4.9f;
    public float deadZoneX = -14f;
    public float Ceiling = 15.3f;

    public float JumpHeight = 3f;
    public float moveSpeed = 2.5f;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        CheckToResetImmunity();
        immunityCounter += Time.deltaTime;

        //if player life is 0 or below, isPlayerALive is set to false
        //this stops the bird from taking inputs from user causes it to fall below screen - leads to game over
        if (logic.lifeCounter > 0)
        {
            ValidPlayerMovement();
        }

        // if bird falls below player view it is destroyed
        if (DeadZoneY > transform.position.y)
        {        //destroys the bird
            Debug.Log("bird deleted");
            Destroy(gameObject);
            logic.GameOver();
        }

    }

    private void ValidPlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerRigidbody.velocity = Vector2.up * JumpHeight;
            //moves player up by jumpheight when W is pressed and bird is in play
        }

        if (transform.position.y > Ceiling)
        {
            transform.position = new Vector3(transform.position.x, Ceiling, 0);
            Debug.Log("bird position transform.position.y: " + transform.position.y);
            //moves bird down to if it goes above ceiling - stops players from flying off screen and over pipes
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.DetectedCollision();
    }
    public void CheckToResetImmunity()
    {
        if (immunityCounter >= 3 && hasImmunity)
        {
            hasImmunity = false;
            logic.SetBackgroundNormal();
            Debug.Log("Reset immunity");
        }
    }

}


