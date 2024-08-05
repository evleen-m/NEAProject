using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float jumpHeight;
  
    void Start()
    {

    }

    void Update()
    {
        //makes player move up(jump) when W is pressed
        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            playerRigidbody.velocity = Vector2.up * jumpHeight;
        }
        //if (Input.GetKeyDown(KeyCode.S) == true)
        //{
        //    playerRigidbody.velocity = Vector2.down * jumpHeight;
        //}
        //if (Input.GetKeyDown(KeyCode.D) == true)
        //{
        //    playerRigidbody.velocity = Vector2.right * jumpHeight;
        //}
        //if (Input.GetKeyDown(KeyCode.A) == true)
        //{
        //    playerRigidbody.velocity = Vector2.left * jumpHeight;
        //}

        


    }
}
