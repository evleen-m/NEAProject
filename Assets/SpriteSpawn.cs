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
    public GameLogic logic;


    private float timer = 0.5f;
    public bool timerPaused = false;
    private float spawnRate = 3;

    [SerializeField]
    private bool gotSpawnRate = true;

    [SerializeField]
    private float lowestPoint;

    [SerializeField]
    private float highestPoint;



    public List<int> toSpawn = new List<int>();


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>(); // used to allow access to the class GameLogic
    }



    void Update()
    {
        if (!timerPaused)
        {
            if (!toSpawn.Any()) // checks if list is empty
            {
                generateListToSpawn();
            }

            if (gotSpawnRate)
            {
                // Gets a new spawn rate after each object is spawed - varies the distance of new object to the object in front
                // is only run after each object has been spawned
                spawnRate = logic.RandomSpawn();
                gotSpawnRate = false;
            }

            if (timer < spawnRate)
            {
                // increases timer by the time taken to get from last frame to current frame (in seconds)
                timer += Time.deltaTime;
            }
            else // runs when timer is greater than spawnRate
            {
                // first save into variable
                int gamePiece = toSpawn[0];

                // spawn corresopnding game object
                switch(gamePiece){
                    case 0:
                        spawnPipe();
                        break;
                    case 1:
                        spawnStar();
                        break;
                }

                // then remove it from the list
                toSpawn.RemoveAt(0);

                // resets the timer and gotSpawnRate
                timer = 0;
                gotSpawnRate = true;
            }

        }
    }

    private void generateListToSpawn()
    {
        for (int i = 0; i <=3; i++)
        {
            toSpawn.Add(0);
        }
        toSpawn.Add(1);

        toSpawn = toSpawn.OrderBy(x => Guid.NewGuid()).ToList(); // this randomises the sprites in the list - ensures they spawn in random order 

        //for (int i = toSpawn.Count - 1; i > 0; i--)
        //{
        //    var j = UnityEngine.Random.Range(i + 1, toSpawn.Count - i);
        //    var k = toSpawn[j];
        //    toSpawn[j] = toSpawn[i];
        //    toSpawn[i] = k;
        //}

        Debug.Log(toSpawn.Count());
    }

    private void setHeightLimits()
    {
        // sets the lowest and highest points that pipe can spawn at
        lowestPoint = transform.position.y - logic.HeightOffset;
        highestPoint = transform.position.y + logic.HeightOffset;
    }

    private void spawnPipe()
    {
        setHeightLimits();
        Instantiate(pipe, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of pipe at the randomly selected height
    }

    private void spawnStar()
    {
        setHeightLimits();
        Instantiate(star, new Vector3(transform.position.x, UnityEngine.Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        // spawns a new instance of star at the randomly selected height
    }


}
