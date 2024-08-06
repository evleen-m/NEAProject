using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class PipeMovement : MonoBehaviour
{

    public float moveSpeed = 2;
        float deadZone = -12;


    void Start()
    {
        
    }
    //moves and destroys pipes
    void Update()
    {
        transform.position  = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        //Time.deltaTime returns the amount of time in seconds that elapsed since the last frame completed.
        //This value varies depending on the frames per second(FPS)rate at which your game or app is running.
        //Used to ensure the movement speed of pipe is the same across different devices

        
        if (deadZone > transform.position.x)
        {            
            Debug.Log("Pipe deleted transform.position.x: " + transform.position.x);            
            Destroy(gameObject);
        }

    }
}
