using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraPathFollow : MonoBehaviour
{
    Camera mainCamera;

    public GameObject cameraTransitionParent;
    private Transform[] waypoints; 
    public int nextWaypoint = 1;
    public WaypointScript nextWaypointScript;

    public bool setConsistentSpeed;
    public float consistentCameraSpeed;

    public int waypointToStartAt;

    private Vector3 toNextWaypoint;

    public Vector3 currentVelocity;
    public float intendedSpeed;

    private float acceleration;
    public float deceleration;
    public float timeToReachMaxSpeed;
    public float timeToDecelerate;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        waypoints = cameraTransitionParent.GetComponentsInChildren<Transform>();

        if (waypointToStartAt != 0)
        {
            nextWaypoint = waypointToStartAt;
            transform.position = waypoints[waypointToStartAt].position;
        }
        
        nextWaypointScript = waypoints[nextWaypoint].gameObject.GetComponent<WaypointScript>(); //fetch waypoint script of current waypoint
        intendedSpeed = nextWaypointScript.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (setConsistentSpeed)
        {
            SetConsistentSpeed(consistentCameraSpeed);
        }

        if (nextWaypoint < waypoints.Length) //if current waypoint is within the index of the list
        {
            //if the waypoint is unlocked, move towards it
            //if the waypoint's is locked, the camera will not move until it is changed

                //get vector to nextwaypoint
                toNextWaypoint = waypoints[nextWaypoint].position - transform.position;
                //currentVelocity = nextWaypointScript.speed * toNextWaypoint.normalized;

                if (toNextWaypoint.magnitude < 0.5)
                {
                    //switch to next waypoint
                    if ((nextWaypoint + 1) != waypoints.Length)
                    {
                        //move to next waypoint in list
                        nextWaypoint++;
                        nextWaypointScript = waypoints[nextWaypoint].gameObject.GetComponent<WaypointScript>(); //fetch waypoint script of current waypoint
                        intendedSpeed = nextWaypointScript.speed;
                        Debug.Log("SWITCH");
                    } else
                    {
                        //at the end of the waypoint list
                        intendedSpeed = 0;
                        Debug.Log("STOP CAMERA");
                    }
                }

                //intendedSpeed = nextWaypointScript.speed;

                //acceleration/deceleration calculations
                acceleration = intendedSpeed / timeToReachMaxSpeed;
                deceleration = intendedSpeed / timeToDecelerate;

                //accelerate
                if (intendedSpeed > currentVelocity.magnitude)
                {
                    currentVelocity += acceleration * Time.deltaTime * toNextWaypoint.normalized;
                    currentVelocity = Vector3.ClampMagnitude(currentVelocity, nextWaypointScript.speed);
                    Debug.Log("accelerating");
                }

                //decelerate
                if (intendedSpeed < currentVelocity.magnitude)
                {
                    currentVelocity -= Vector3.ClampMagnitude((currentVelocity.normalized) * deceleration * Time.deltaTime, currentVelocity.magnitude);
                    Debug.Log("decelerating");
                }

                //apply changes to camera transform
                mainCamera.transform.position += currentVelocity * Time.deltaTime;
            
        }
    }
    public void WaypointDetour(Transform detourWaypoint)
    {
        waypoints[nextWaypoint] = detourWaypoint;
    }

    public void SetConsistentSpeed(float speed)
    {
        for (int i = 1; i < waypoints.Length; i++)
        {
            waypoints[i].gameObject.GetComponent<WaypointScript>().speed = speed;
        }
    }


}
