using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    

    public Rigidbody2D playerRigidbody;
    public float jumpHeight;
    public GameLogic logic;
    float deadZoneY = 5.6f;
    bool isPlayerAlive = true;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        //makes player move up(jump) when W is pressed only when bird is in play
        if (isPlayerAlive)
        {
            if (Input.GetKeyDown(KeyCode.W) == true)
            {
                playerRigidbody.velocity = Vector2.up * jumpHeight;
            }
        }

        //stops bird move when fallen below view
        if (deadZoneY > transform.position.y)
        {
            Debug.Log("bird position transform.position.y: " + transform.position.y);
            Debug.Log("game over - player off screen(y)");
            logic.gameOver();
        }

        //destroys the bird when it is off screen
        if (transform.position.y < -15)
        {
            Debug.Log("bird deleted");
            Destroy(gameObject);
        }
    }

    //stops bird movement when hits pipe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        logic.gameOver();
        Debug.Log("game over - hit pipe");
        isPlayerAlive = false;

    }


}


