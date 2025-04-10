using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); //get rigidbody
        rb.AddTorque(speed, ForceMode2D.Impulse); //apply torque
        rb.angularDrag = 0f; //set to no drag

    }

}
