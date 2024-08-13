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
        //makes player move up(jump) when W is pressed only when bird is in play
        if (isPlayerAlive)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerRigidbody.velocity = Vector2.up * logic.JumpHeight;
            }

            if (transform.position.y > logic.Ceiling)
            {
                transform.position = new Vector3(transform.position.x, logic.Ceiling, 0);
                Debug.Log("bird position transform.position.y: " + transform.position.y);
            }
        }

        //destroys the bird when it is off screen
        else
        {
            destroyBird();
        }


        //stops bird move when fallen below view
        if (logic.DeadZoneY > transform.position.y)
        {
            //Debug.Log("bird position transform.position.y: " + transform.position.y);
            Debug.Log("game over - player off screen(y)");
            isPlayerAlive = false;
        }
    }

    //stops bird movement when hits pipe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("game over - hit pipe");
        isPlayerAlive = false;

    }

    public void destroyBird()
    {
        Debug.Log("bird1 deleted");
        Destroy(gameObject);
       
        Debug.Log("game over run");
        logic.gameOver();

    }


}


