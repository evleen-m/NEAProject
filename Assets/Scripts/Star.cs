using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Sprite
{
    GameLogic logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

    }

    void Update()
    {
        RotateSprite(1f);
    }

    //checks bird passed through star and increases score
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.IncreaseScore(3);
            Stats.Record.PowerupsUsed++;
            Destroy(gameObject);
        }
    }

}
