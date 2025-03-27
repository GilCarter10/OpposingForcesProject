using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUnlocker : MonoBehaviour
{
    public Transform associatedWaypoint;

    private WaypointScript waypointScript;
    private TakeDamage damageScript;
    
    public bool multipleTargets;
    Transform[] childrenObjects;
    
    void Start()
    {
        damageScript = gameObject.GetComponent<TakeDamage>();
        waypointScript = associatedWaypoint.gameObject.GetComponent<WaypointScript>();

    }

    void Update()
    {
        if (!multipleTargets)
        {
            if (damageScript.health <= 0) //if dead
            {
                //unlock the waypoint
                waypointScript.locked = false;
            }

        } else if (multipleTargets)
        {
            childrenObjects = GetComponentsInChildren<Transform>(); //get a list of all the child game objects

            if (childrenObjects.Length == 1) //if there are no more children
            {
                //unlock the waypoint
                waypointScript.locked = false;
            }

        }

    }

}
