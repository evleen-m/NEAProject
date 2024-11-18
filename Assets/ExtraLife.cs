using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : Sprite
{

    GameLogic logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

    }
    private void Update()
    {
        
    }
    //checks bird passed through star and increases score
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.AddLife();
            Stats.Record.PowerupsUsed++;
            Destroy(gameObject);
        }
    }
}
