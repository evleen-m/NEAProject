using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// DontDestroyOnLoad - Event Manager
// ensures there is only one event manager at any one time
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {

        GameObject[] eventSystem = GameObject.FindGameObjectsWithTag("EventsManager");
         if (eventSystem.Length > 1) //check how many event systems are present - max is 1 in a single scene
        {
            Destroy(this.gameObject); //destroy duplicate event systems
        }
        DontDestroyOnLoad(this.gameObject);
    }

}