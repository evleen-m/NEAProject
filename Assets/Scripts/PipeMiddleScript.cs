using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : Sprite
{
    GameLogic logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }
    private void Update()
    {
        
    }

    //checks bird passed through pipes and increases score by 1
    public override void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.layer == 3) { 
            logic.IncreaseScore(1);
            
        }
    }
    
}
