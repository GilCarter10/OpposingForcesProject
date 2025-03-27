using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public bool locked;
    public float speed;
    private float intendedSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        intendedSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked)
        {
            speed = 0;
        } else
        {
            speed = intendedSpeed;
        }
    }
}
