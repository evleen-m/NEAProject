using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SpriteSpawn : MonoBehaviour
{
    public GameObject pipe;
    public GameObject star;
    public GameObject bat;
    public GameObject lightning;
    public GameObject extraLife;

    public float SpawnRate;


    private float timer = 0.5f;
    public bool timerPaused = false;
    private float spawnRate = 3;

    //[SerializeField]
    //private bool toGetSpawnRate = true;

    [SerializeField]
    private float lowestPoint;

    [SerializeField]
    private float highestPoint;
    public float HeightOffset = 4.5f;

    private GameObject spriteToSpawn;


    private List<int> listSpritesToSpawn = new List<int>();

    GameLogic logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    void Update()
    {
        if (!timerPaused)
        {
            if (!listSpritesToSpawn.Any()) // checks if list is empty
            {
                GenerateListToSpawn();
            }

            if (timer < spawnRate)
            {
                // increases timer by the time taken to get from last frame to current frame (in seconds)
                timer += Time.deltaTime;
            }
            else // runs when timer is greater than spawnRate
            {
                // first save into variable so can be used for switch case
                int gamePiece = listSpritesToSpawn[0];

                // spawn corresopnding game object
                switch(gamePiece){
                    case 0:
                        spriteToSpawn = pipe;
                        break;
                    case 1:
                        spriteToSpawn = star;
                        break;
                    case 2:
                        spriteToSpawn = bat;
                        break;
                    case 3:
                        spriteToSpawn = lightning;
                        break;
                    case 4:
                        spriteToSpawn = extraLife;
                        break;
                }
                SpawnSprite(spriteToSpawn);

                // then remove it from the list
                listSpritesToSpawn.RemoveAt(0);

                // resets the timer
                timer = 0;
                // Gets a new spawn rate after each object is spawed - varies the distance of new object to the object in front
                // is only run after each object has been spawned
                spawnRate = RandomSpawnRate();
            }

        }
    }
    public float RandomSpawnRate()
    {
        // Randomly generates a spawn rate for pipes - pipes spawn at different distances to each other
        SpawnRate = UnityEngine.Random.Range(2, 6);
        return SpawnRate;
    }

    private void GenerateListToSpawn()
    {
        //adds four seperate 0s to the list (represents pipes)
        for (int i = 1; i <= 4; i++)
        {
            listSpritesToSpawn.Add(0);
        }

        //adds two seperate 2s to the list (represents bats)
        for (int i = 1; i <= 2; i++)
        {
            listSpritesToSpawn.Add(2);
        }
        //adds a 1 to the end of list (represents star)
        listSpritesToSpawn.Add(1);

        //adds a 3 to the end of list (represents lightning)
        listSpritesToSpawn.Add(3);
        //4 = extra life
        listSpritesToSpawn.Add(4);

        listSpritesToSpawn = listSpritesToSpawn.OrderBy(x => Guid.NewGuid()).ToList(); // this randomises the sprites in the list - ensures they spawn in random order 
        Debug.Log(listSpritesToSpawn.Count()); //used to check there are items in list
    }

    private void SetHeightLimits()
    {
        // sets the lowest and highest points that pipe can spawn at
        lowestPoint = transform.position.y - HeightOffset;
        highestPoint = transform.position.y + HeightOffset;

    }

    private void SpawnSprite(GameObject sprite)
    {
        SetHeightLimits();
        Instantiate(sprite, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint),0), transform.rotation);
        // spawns a new instance of pipe at the randomly selected height
    }

}
