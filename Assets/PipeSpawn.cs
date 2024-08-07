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
    [SerializeField]private bool getSpawnNotRun = true;



    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();
    }

    

    void Update()
    {
        if (!timerPaused)
        {
            if (getSpawnNotRun)
            {
                spawnRate = logic.RandomSpawn();
                getSpawnNotRun = false;
            }

            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                spawnPipe();
                timer = 0;
                getSpawnNotRun = true;
            }
        }

        void spawnPipe()
        {

            //spawns pipes at varying heights
            float lowestPoint = transform.position.y - logic.HeightOffset;
            float highestPoint = transform.position.y + logic.HeightOffset;

            Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        }

    }
}
