using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject pipe;
    public GameLogic logic;

    private float timer = 0.5f;
    public bool timerPaused = false;
    private float spawnRate = 3;
    [SerializeField]private bool gotSpawnRate = true;



    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    

    void Update()
    {
        if (!timerPaused) 
        {
            if (gotSpawnRate)
            {
                // Gets a new spawn rate for each pipe - varies the distance of new pipe to be spawned from the pipe in front
                // is only run after each pipe has been spawned
                spawnRate = logic.RandomSpawn();
                gotSpawnRate = false;
            }

            if (timer < spawnRate)
            {
                // increases timer by the time taken to get from last frame to current frame (in seconds)
                timer += Time.deltaTime;
            }

            else
            {
                // runs when timer is greater than spawnRate
                // calls spawnPipe and resets the timer and gotSpawnRate
                spawnPipe();
                timer = 0;
                gotSpawnRate = true;
            }
        }

        void spawnPipe()
        {

            // sets the lowest and highest points that pipe can spawn at
            float lowestPoint = transform.position.y - logic.HeightOffset;
            float highestPoint = transform.position.y + logic.HeightOffset;

            // randomises the height and spawns pipe at that position
            Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        }

    }
}
