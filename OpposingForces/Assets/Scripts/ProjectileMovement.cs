using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    private Vector2 direction;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = (PlayerMoveAndShoot.playerTransform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion newRotation = transform.rotation;
        newRotation.eulerAngles = new Vector3(0, 0, angle + 90);
        transform.rotation = newRotation;
    }

    private void Update()
    {   
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D whoCollided)
    {
        Destroy(gameObject);
    }

}


