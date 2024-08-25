using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameLogic logic;
    private bool moveTargetIsHigher;
    private float lowestPoint = 5.2f;
    private float highestPoint = 15.5f;
    [SerializeField] private float timer = 0;

    [SerializeField] private float spawnRate = 2;

    void Start()
    {

        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > spawnRate )
        {
            transform.Rotate(Vector3.forward * -90);
            transform.Rotate(Vector3.forward * 90);
            timer = 0;
        }
        else
        { // increases timer by the time taken to get from last frame to current frame (in seconds)
            timer += Time.deltaTime;
        }


        if (moveTargetIsHigher)
        {
            moveBatUp();

            if (transform.position.y > highestPoint)
            {
                moveTargetIsHigher = false;
            }
        }

        else
        {
            moveBatDown();
            if (transform.position.y < lowestPoint)
            {
                moveTargetIsHigher = true;
            }
        }

    }

    private void moveBatUp()
    {
        transform.position = transform.position + (Vector3.up * logic.moveSpeed) * Time.deltaTime;
    }
    private void moveBatDown()
    {
        transform.position = transform.position + (Vector3.down * logic.moveSpeed) * Time.deltaTime;
    }
}
