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
    [SerializeField] private float wobbleTimer = 0;

    [SerializeField] private float wobbleRate = 2;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.forward * -0.5f); //very slightly roates the bat by 0.5 each frame

        if (moveTargetIsHigher) // following code is executed if the target position for the bat to be moved to is higher than the current bat position 
        {
            MoveBatUp();
            if (transform.position.y > highestPoint)
            {
                moveTargetIsHigher = false; // sets the target position for bat to be moved to as the 
            }
        }
        else
        {
            MoveBatDown();
            if (transform.position.y < lowestPoint)
            {
                moveTargetIsHigher = true;
            }
        }
    }

    private void MoveBatUp()
    {
        transform.position = transform.position + (Vector3.up * logic.moveSpeed) * Time.deltaTime;
    }

    private void MoveBatDown()
    {
        transform.position = transform.position + (Vector3.down * logic.moveSpeed) * Time.deltaTime;
    }
}
