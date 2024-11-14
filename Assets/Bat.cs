using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bat: Sprite
{

    private bool moveTargetIsHigher;
    private float lowestPoint = 5.2f;
    private float highestPoint = 15.5f;

    // Update is called once per frame
    void Update()
    {
        RotateSprite(-0.5f);
        ValidSpriteMovement(gameObject);

    }
    public override void ValidSpriteMovement(GameObject gameObject)
    {
        base.ValidSpriteMovement(gameObject);
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
        transform.position = transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
    }

    private void MoveBatDown()
    {
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
    }



}
