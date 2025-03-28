using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    /* 
     * Script used for:
     * setting a projectiles direction
     * whether it can be parried
     * whether it is homing
     */


    [SerializeField] float speed;
    
    //for predetermined shots
    Rigidbody2D rb;
    public Vector3 runDirection;

    //for parriable bullets
    public bool isParriable;
    
    //for bullets that just move using their transform.left
    public bool moveForward;

    //for homing bullets
    [SerializeField] bool isHoming;
    private Transform startPosition;
    private Vector3 bulletToPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform;
        bulletToPlayer = PlayerMoveAndShoot.playerTransform.position - startPosition.position;

        //direction is overwritten if homing
        //automatically calculate vector towards player
        if (isHoming)
        {
            runDirection = bulletToPlayer.normalized;
            Quaternion toPlayer = Quaternion.LookRotation(Vector3.forward, runDirection);
            transform.rotation = toPlayer; 
        }

        if(moveForward)
        {
            runDirection = -transform.right;
        }

    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * runDirection; //moves bullet
    }

    public void ChangeToParried()
    {
        //changes layer to collide with enemy and not player
        gameObject.layer = LayerMask.NameToLayer("Parried Projectile");
        
        //reverses direction and speeds up bullet
        runDirection *= -1; 
        speed *= 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the object the bullet collided with has a DamageController script && the bullet has been parried
        if (collision.gameObject.GetComponent<TakeDamage>() != null && gameObject.layer == LayerMask.NameToLayer("Parried Projectile"))
        {
            Debug.Log("collision happening");
            TakeDamage collidedEnemy = collision.gameObject.GetComponent<TakeDamage>();
            collidedEnemy.TakeHit(5);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("became invisible");
        Destroy(gameObject); //destroys the bullets when they go off-screen
    }
}
