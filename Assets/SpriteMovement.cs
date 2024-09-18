using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class SpriteMovement : MonoBehaviour
{

    public GameLogic logic;

    void Start()
    {

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }
    //moves sprites across the screen and deletes them when out of view
    void Update()
    {   
        
        transform.position = transform.position + (Vector3.left * logic.moveSpeed) * Time.deltaTime;
        //Time.deltaTime returns the amount of time in seconds that elapsed since the last frame completed.
        //This value varies depending on the frames per second(FPS)rate at which your game or app is running.
        //Used to ensure the movement speed of objects is the same across different devices


        // checks if object is out of frame and deletes it if it is
        if (logic.deadZoneX > transform.position.x)
        {
            IdentifyObjectDeleted();
            Destroy(gameObject);
        }

    }
    
    // additional checks to identify the type of object that was destroyed
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
