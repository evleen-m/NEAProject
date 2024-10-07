using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Lightning : MonoBehaviour
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
            SetLightningActive();
            timer = 0;
        }
        else
        { // increases timer by the time taken to get from last frame to current frame (in seconds)
            timer += Time.deltaTime;
        }

        //if (transform.position.z >= 5f)
        //{
        //    transform.Rotate(Vector3.forward * 1f); //very slightly roates the bat by 0.5 each frame
        //}
        //else
        //{
        //    transform.Rotate(Vector3.forward * -1f); //very slightly roates the bat by 0.5 each frame
        //}
    }

    private void SetLightningActive()
    {
        spriteRenderer.color = new Color(1f, 0.5506037f, 0f, 1f);

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
