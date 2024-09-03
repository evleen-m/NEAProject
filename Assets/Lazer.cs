using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Lazer : MonoBehaviour
{
    public GameLogic logic;
    [SerializeField] private float timer = 0;

    [SerializeField] private float inactiveTime = 5;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        if (timer > inactiveTime)
        {
            SetLazerActive();
            timer = 0;
        }
        else
        { // increases timer by the time taken to get from last frame to current frame (in seconds)
            timer += Time.deltaTime;
        }
    }

    private void SetLazerActive()
    {
        spriteRenderer.color = new Color(0.4150943f, 0.04894979f, 0.04894979f, 1f);

        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.RemoveLife();
            
        }
    }
}
