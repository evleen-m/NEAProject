using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.U2D;
public class Sprite: MonoBehaviour
{

    private float deadZoneX = -12.5f;
    public float moveSpeed;

    SpriteSpawn spriteSpawn;

    private void Start()
    {
        spriteSpawn = GameObject.FindGameObjectWithTag("SpriteSpawnLogic").GetComponent<SpriteSpawn>();
        moveSpeed = spriteSpawn.GetMoveSpeed();
    }

    private void Update()
    {
        ValidSpriteMovement(gameObject);
        //moveSpeed = spriteSpawn.getMoveSpeed();
    }

    //moves sprites across the screen and deletes them when out of view
    public virtual void ValidSpriteMovement(GameObject gameObject)
    {
        
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        //Time.deltaTime returns the amount of time in seconds that elapsed since the last frame completed.
        //This value varies depending on the frames per second(FPS)rate at which your game or app is running.
        //Used to ensure the movement speed of objects is the same across different devices

        // checks if object is out of frame and deletes it if it is
        if (deadZoneX > transform.position.x)
        {
            Destroy(gameObject);
        }
    }




    public void RotateSprite(float rotationAngle)
    {
        
        transform.Rotate(Vector3.forward * rotationAngle); //very slightly roates the sprite by 0.5 each frame
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //allows for different actions to be set when the player collides with a sprite

    }
    public virtual void SpriteAction(GameObject gameObject)
    {
        // allows for different actions to be set when the player collides with a sprite
    }

    private void IdentifyObjectDeleted()
    {
        if (gameObject.CompareTag("Pipe"))
        {
            Debug.Log("Pipe deleted transform.position.x: " + transform.position.x);
        }
        else if (gameObject.CompareTag("Star"))
        {
            Debug.Log("Star deleted transform.position.x: " + transform.position.x);
        }
        else if (gameObject.CompareTag("Bat"))
        {
            Debug.Log("Bat deleted transform.position.x: " + transform.position.x);
        }
        else if (gameObject.CompareTag("Lightning"))
        {
            Debug.Log("Lightning deleted transform.position.x: " + transform.position.x);
        }
    }
}

