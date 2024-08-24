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
        //Used to ensure the movement speed of pipe is the same across different devices


        if (logic.deadZoneX > transform.position.x)
        {
            Debug.Log("Pipe deleted transform.position.x: " + transform.position.x);
            Destroy(gameObject);
        }

    }
}
