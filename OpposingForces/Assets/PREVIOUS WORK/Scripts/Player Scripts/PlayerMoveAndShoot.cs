using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveAndShoot : MonoBehaviour
{
    /*
     * Script used for:
     * Player Movement Input
     * Player Shooting Input
     * Player Dashing
     
     */
    
    public static Transform playerTransform;

    public static Rigidbody2D playerRB;
    private Vector2 moveInputValue;

    public GameObject bulletPrefab;
    private Transform bulletSpawn;

    //variables to calculate acceleration/deceleration
    private float acceleration;
    private float deceleration;
    public static float maxSpeed;
    public static float timeToReachMaxSpeed = 0.1f;
    public static float timeToDecelerate = 0.1f;

    //variables to handle shooting
    bool isShooting;
    public float bulletFrequency;
    bool canShoot = true;

    //variables to handle dashing
    [SerializeField] bool unlockDash; //only for testing through inspector
    public static bool dashUnlocked; //use this to unlock and check ability through code
    private bool isDashing;
    public float dashSpeed;
    private float dashTimer;
    public float dashDuration;
    private TrailRenderer trailRenderer;

    //variables to handle maxSpeed throughout different states
    //to set in inspector
    [SerializeField] private float maxSpeedNormal; //=6?
    [SerializeField] private float maxSpeedDash;
    [SerializeField] private float maxSpeedKnockback;
    //to reference in other scripts
    public static float maxSpeedNormalStatic;
    public static float maxSpeedDashStatic;
    public static float maxSpeedKnockbackStatic;

    void Start()
    {
        playerTransform = transform;
        //calculates acceleration and deceleration

        //get reference to components
        playerRB = GetComponent<Rigidbody2D>();

        //shoots from a specific point of the sprite, not just (0,0,0)
        Transform[] spawns = GetComponentsInChildren<Transform>();
        foreach (Transform t in spawns)
        {
            if(t.gameObject.name == "BulletSpawn")
            {
                bulletSpawn = t;
            } 
        }

        //get reference to trail renderer
        trailRenderer = GetComponent<TrailRenderer>();

        maxSpeed = maxSpeedNormal;
        maxSpeedNormalStatic = maxSpeedNormal;
        maxSpeedDashStatic = maxSpeedDash;
        maxSpeedKnockbackStatic = maxSpeedKnockback;

        dashUnlocked = unlockDash;
    }

    private void Update()
    {
        //calculates acceleration and deceleration every frame
        acceleration = maxSpeed / timeToReachMaxSpeed;
        deceleration = maxSpeed / timeToDecelerate;
    }

    void FixedUpdate()
    {
        MovementUpdate();

        if(isShooting && canShoot) //if the player is holding the "shoot" button and they can
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    //controls player movement physics
    private void MovementUpdate()
    {
        //increases velocity by player input
        playerRB.velocity += moveInputValue * acceleration * Time.deltaTime;

        //prevents player's speed from surpassing max speed
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxSpeed);

        //decelerates if there's no player input
        if (moveInputValue == Vector2.zero)
        {
                playerRB.velocity -= Vector2.ClampMagnitude((playerRB.velocity.normalized) * deceleration * Time.deltaTime, playerRB.velocity.magnitude);
        }

        //if the player is dashing
        if (isDashing)
        {
            //begins timer to end dash
            dashTimer += Time.deltaTime;

            //if dash timer exceeds the dash duration
            if (dashTimer >= dashDuration)
            {
                //return maxSpeed to normal and stop dashing
                maxSpeed = maxSpeedNormalStatic;
                isDashing = false;
            }

            //turns trail renderer on
            trailRenderer.time = Mathf.Clamp(trailRenderer.time + Time.deltaTime, 0f, .2f);
        }
        else 
        {
            dashTimer = 0; //resets timer to zero when not dashing

            //turns trail renderer off
            trailRenderer.time = Mathf.Clamp(trailRenderer.time - Time.deltaTime, 0f, .2f);
        } 
    }

  
    private void OnMove(InputValue value)
    {
        //takes player input into variable we can edit
        moveInputValue = value.Get<Vector2>();
    }

    private void OnStartShoot()
    {
        //player starts shooting
        isShooting = true;
    }
    private void OnEndShoot()
    {
        //player ends shooting
        isShooting = false;
    }

    private void OnDash()
    {
        //if the player has unlocked the dash, isn't already dashing, and has enough stamina
        if (dashUnlocked && !isDashing)
        {
            //add maxSpeed to dashSpeed and start dashing
            maxSpeed = maxSpeedDashStatic;
            isDashing = true;
        }
    }

    IEnumerator Shoot()
    {
        Vector3 bulletSpawnPos = new Vector3 (bulletSpawn.position.x, bulletSpawn.position.y + .1f, bulletSpawn.position.z);
        Instantiate(bulletPrefab, bulletSpawnPos, transform.rotation); //shoots one bullet
        yield return new WaitForSeconds(.02f); //wait just a bit to offset the bullets
        bulletSpawnPos.y -= .2f;
        Instantiate(bulletPrefab, bulletSpawnPos, transform.rotation); //shoots the second bullet slightly lower
        yield return new WaitForSeconds(bulletFrequency); //how long until the player can shoot another burst of bullets
        canShoot = true;
    }

}
