using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;


public class SpriteSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;
    [SerializeField]

    private GameObject star;
    [SerializeField]

    private GameObject bat;
    [SerializeField]

    private GameObject lightning;
    [SerializeField]

    private GameObject extraLife;


    private float moveSpeed = 2.4f;


    private float spawnTimer = 0.5f;



    private float spawnRate = 3;
    private double increaseSpeedTimer = 0;

    private float maxRange = 6f;

    //[SerializeField]
    //private bool toGetSpawnRate = true;

    [SerializeField]
    private float lowestPoint;

    [SerializeField]
    private float highestPoint;
    private float HeightOffset = 4.5f;

    private GameObject spriteToSpawn;

    private bool notIncreased;

    private CyclicQueue spriteQueue = new CyclicQueue(9);

    //needs getter/setter methods
    [SerializeField]
    private bool timerPaused = false;

    GameLogic logic;
    void Start()
    {
        GenerateListToSpawn();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        if (!timerPaused)
        {
            if (spriteQueue.isEmpty()) // checks if list is empty
            {
                GenerateListToSpawn();
            }

            if (spawnTimer < spawnRate)
            {
                // increases timer by the time taken to get from last frame to current frame (in seconds)
                spawnTimer += Time.deltaTime;
            }
            else // runs when timer is greater than spawnRate
            {
                // first save into variable so can be used for switch case
                int gamePiece = spriteQueue.getValueInHead();

                //Debug.Log(spriteQueue.getQueue());
                //Debug.Log(spriteQueue.getHead());
                //Debug.Log(spriteQueue.getValueInHead());


                // spawn corresopnding game object
                switch (gamePiece)
                {
                    case 0:
                        spriteToSpawn = pipe;
                        Debug.Log("sprite to spawn set: pipe");
                        break;
                    case 1:
                        spriteToSpawn = star;
                        Debug.Log("sprite to spawn set: star");
                        break;
                    case 2:
                        spriteToSpawn = bat;
                        Debug.Log("sprite to spawn set: bat");
                        break;
                    case 3:
                        spriteToSpawn = lightning;
                        Debug.Log("sprite to spawn set: light");
                        break;
                    case 4:
                        spriteToSpawn = extraLife;
                        Debug.Log("sprite to spawn set: life");
                        break;
                }
                SpawnSprite(spriteToSpawn);

                // then remove it from the list
                spriteQueue.dequeue();
                // resets the timer
                spawnTimer = 0;
                increaseSpeedTimer++;
                DecreaseMaxRange();
                // Gets a new spawn rate after each object is spawed
                // -> varies the distance of new object to the object in front
                // is only run after each object has been spawned
                spawnRate = RandomSpawnRate();
            }


        }
    }

    // decreases the max value for the range of spawn rate
    public void DecreaseMaxRange()
    { 
        if (increaseSpeedTimer % 10 == 0) // run every 10 seconds
        {
            if (maxRange > 2) // max range value muste be greater than 2
            {
                maxRange -= 0.5f; //descrease
                Debug.Log("Decreased max range to" + maxRange.ToString());
            }
        }
    }

    // calculates the speed at which sprites must move at based on game progess
    public float GetMoveSpeed()
    {
        if (increaseSpeedTimer % 10 == 0 & notIncreased == true) // run every 10 seconds and if this has not been run previously
        {
            if (moveSpeed < 6) // moveSpeed must be below 6
            {
                moveSpeed += 0.5f; // increase
                Debug.Log("Increased move speed to: " + moveSpeed.ToString());
                notIncreased = false; // sets the flag to false
            }
        }
        notIncreased = true; // flag set to true - indicates that fucntion has not been run
        return moveSpeed;
    }
    public float RandomSpawnRate()
    {
        float SpawnRate;

        // Randomly generates a spawn rate for pipes - pipes spawn at different distances to each other
        SpawnRate = UnityEngine.Random.Range(1, maxRange);
        return SpawnRate;
    }

    public bool GetTimer()
    {
        return timerPaused;
    }

    public void SetTimer(bool active)
    {
        this.timerPaused = active;
    }

    private void GenerateListToSpawn()
    {
        //adds four seperate 0s to the list (represents pipes)
        for (int i = 1; i <= 4; i++)
        {
            spriteQueue.enqueue(0);
        }

        //adds two seperate 2s to the list (represents bats)
        for (int i = 1; i <= 2; i++)
        {
            spriteQueue.enqueue(2);
        }
        //adds a 1 to the end of list (represents star)
        spriteQueue.enqueue(1);

        //adds a 3 to the end of list (represents lightning)
        spriteQueue.enqueue(3);
        //4 = extra life
        spriteQueue.enqueue(4);

        spriteQueue.randomise(); // this randomises the sprites in the list - ensures they spawn in random order 
        Debug.Log(spriteQueue.getQueue()); //used to check there are items in list
    }

    private void SetHeightLimits()
    {
        // sets the lowest and highest points that pipe can spawn at
        lowestPoint = transform.position.y - HeightOffset;
        highestPoint = transform.position.y + HeightOffset;

    }

    private void SpawnSprite(GameObject sprite1)
    {
        SetHeightLimits();
        Instantiate(sprite1, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of pipe at the randomly selected height
    }

}
