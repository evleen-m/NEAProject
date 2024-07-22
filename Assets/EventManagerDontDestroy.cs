using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// DontDestroyOnLoad - Event Manager
// ensures there is only one event manager at any one time
public class DontDestroy : MonoBehaviour
{
    void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("EventsManager");
         if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}