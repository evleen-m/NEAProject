using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenScripting : MonoBehaviour
{
    void Awake()
    {
        ScaleBackground();
    }

    private void ScaleBackground()
    {
        // gets the spriteRenderer of the attached background
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //orthographicSize * 2 give full vertical size of camera
        float cameraHeight = Camera.main.orthographicSize * 2;

        // aspect is camera aspect ratio - width divided by height
        // width = aspect*cameraHieght
        // store cameraSize as vector 2 - reps both width and height of camera
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);

        // gets width and height of background image 
        // stored in vector - original size of background image
        Vector2 backgroundSize = spriteRenderer.sprite.bounds.size;


        // set scaling of the background image to current scale of image
        Vector2 scale = transform.localScale;

        // checks if the game is in portrait or landscape mode
        // adujst the background image scale accordingly - opens in full screen mode
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / backgroundSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / backgroundSize.y;
        }

        // sets background image center to 0,0 and correct scale.
        transform.position = Vector2.zero;
        transform.localScale = scale;
    }
}

