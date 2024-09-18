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
    public GameLogic logic;


    private float timer = 0.5f;
    public bool timerPaused = false;
    private float spawnRate = 3;

    [SerializeField]
    private bool toGetSpawnRate = true;

    [SerializeField]
    private float lowestPoint;

    [SerializeField]
    private float highestPoint;



    public List<int> listSpritesToSpawn = new List<int>();


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>(); // used to allow access to the class GameLogic
    }



    void Update()
    {
        if (!timerPaused)
        {
            if (!listSpritesToSpawn.Any()) // checks if list is empty
            {
                GenerateListToSpawn();
            }

            if (toGetSpawnRate)
            {
                // Gets a new spawn rate after each object is spawed - varies the distance of new object to the object in front
                // is only run after each object has been spawned
                spawnRate = logic.RandomSpawn();
                toGetSpawnRate = false;
            }

            if (timer < spawnRate)
            {
                // increases timer by the time taken to get from last frame to current frame (in seconds)
                timer += Time.deltaTime;
            }
            else // runs when timer is greater than spawnRate
            {
                // first save into variable
                int gamePiece = listSpritesToSpawn[0];

                // spawn corresopnding game object
                switch(gamePiece){
                    case 0:
                        SpawnPipe();
                        break;
                    case 1:
                        SpawnStar();
                        break;
                    case 2:
                        SpawnBat();
                        break;
                    case 3:
                        SpawnLightning();
                        break;
                    case 4:
                        SpawnExtraLife();
                        break;
                }

                // then remove it from the list
                listSpritesToSpawn.RemoveAt(0);

                // resets the timer and gotSpawnRate
                timer = 0;
                toGetSpawnRate = true;
            }

        }
    }

    private void GenerateListToSpawn()
    {
        //adds four seperate 0s to the list (represents pipes)
        for (int i = 1; i <=4; i++)
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
        listSpritesToSpawn.Add(4);

        listSpritesToSpawn = listSpritesToSpawn.OrderBy(x => Guid.NewGuid()).ToList(); // this randomises the sprites in the list - ensures they spawn in random order 
        Debug.Log(listSpritesToSpawn.Count()); //used to check there are items in list
    }

    private void SetHeightLimits()
    {
        // sets the lowest and highest points that pipe can spawn at
        lowestPoint = transform.position.y - logic.HeightOffset;
        highestPoint = transform.position.y + logic.HeightOffset;

    }

    private void SpawnPipe()
    {
        SetHeightLimits();
        Instantiate(pipe, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint),0), transform.rotation);
        // spawns a new instance of pipe at the randomly selected height
    }
    private void SpawnBat()
    {
        SetHeightLimits();
        Instantiate(bat, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of bat at the randomly selected height
    }

    private void SpawnLightning()
    {
        SetHeightLimits();
        Instantiate(lightning, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of bat at the randomly selected height
    }
    private void SpawnStar()
    {
        SetHeightLimits();
        Instantiate(star, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of star at the randomly selected height
    }
    private void SpawnExtraLife()
    {
        SetHeightLimits();
        Instantiate(extraLife, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of extraLife at the randomly selected height
    }


}
