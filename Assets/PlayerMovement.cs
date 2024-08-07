using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    //public GameObject PlayerSprite1;
    public GameLogic logic;
    bool isPlayer1Alive = true;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        //makes player move up(jump) when W is pressed only when bird is in play
        if (isPlayer1Alive)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerRigidbody.velocity = Vector2.up * logic.JumpHeight;
            }

            if (transform.position.y > logic.Ceiling)
            {
                transform.position = new Vector3(transform.position.x, logic.Ceiling, 0);
                Debug.Log("bird position transform.position.y: " + transform.position.y);
            }
        }


        //stops bird move when fallen below view
        if (logic.DeadZoneY > transform.position.y) 
        {
            //Debug.Log("bird position transform.position.y: " + transform.position.y);
            Debug.Log("game over - player off screen(y)");
            
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
        isPlayer1Alive = false;

    }


}


