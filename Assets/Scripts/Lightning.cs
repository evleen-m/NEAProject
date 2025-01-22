using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Lightning : Sprite
{
    GameLogic logic;
    [SerializeField] private float timer = 0;

    [SerializeField] private float inactiveTime = 5;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        SpriteAction(gameObject);
    }
    public override void SpriteAction(GameObject gameObject)
    {

        if (timer > inactiveTime)
        {
            SetLightningActive();
            timer = 0;
        }
        else
        { // increases timer by the time taken to get from last frame to current frame (in seconds)
            timer += Time.deltaTime;
        }
    }
    
    private void SetLightningActive()
    {
        spriteRenderer.color = new Color(1f, 0.5506037f, 0f, 1f);

        GetComponent<Collider2D>().enabled = true;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.DetectedCollision();
        }
    }
}
