using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageAndParry : MonoBehaviour
{
    /* 
     * Script used for:
     * Player health
     * Player taking damage
     * Player knockback when taking damage
     
     */

    public int maxHealth;
    public int health;

    public Slider healthSlider;

    SpriteRenderer tempSpriteRenderer; //used rn for changing player sprite colour to illustrate different states

    public float knockbackForce; //how far the player is knocked back when taking damage
    public float knockbackTime; //how long a player can be in knockback
    public float knockbackTimer; //keeps track of how long a player has been in knockback
    public static bool inKnockback = false;

    public float invincibleTime; 
    bool isInvincible = false;

    public GameObject gameOverScreen;

    void Start()
    {
        health = maxHealth;

        tempSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if (inKnockback)
        {
            knockbackTimer += Time.deltaTime;
            PlayerMoveAndShoot.maxSpeed = PlayerMoveAndShoot.maxSpeedKnockbackStatic;
            PlayerMoveAndShoot.timeToReachMaxSpeed = 0.1f * knockbackForce;
            PlayerMoveAndShoot.timeToDecelerate = 0.1f * knockbackForce;
            //change time to accelerate/decelerate to something longer;
            if (knockbackTimer >= knockbackTime) 
            {
                knockbackTimer = 0;
                PlayerMoveAndShoot.maxSpeed = PlayerMoveAndShoot.maxSpeedNormalStatic;
                PlayerMoveAndShoot.timeToReachMaxSpeed = 0.1f;
                PlayerMoveAndShoot.timeToDecelerate = 0.1f;
                inKnockback = false;
            }
        }
        
        healthSlider.value = health;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
        }

       
        
    }

    private void OnParry()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 15)
        {
            TakeDamage(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }


    //everything below here is really wonky !!!!!
    //we need to figure out why the knocback is being clamped
    //and a way to show i-frames
    void TakeDamage(Collider2D collision)
    {
        if (!isInvincible)
        {
            if (collision.tag != "Enemy" && collision.gameObject.layer != 8)
            {
                Destroy(collision.gameObject);
            }
            StartCoroutine("InvincibleFlash");
            
            //knockback when taking damage
            inKnockback = true;
            Vector3 toCollision = (transform.position - collision.transform.position); //direction of collision
            Vector2 pushbackDirection = toCollision; //turning into a vector 2 (not sure if this is the best way to handle this)
            PlayerMoveAndShoot.playerRB.velocity += pushbackDirection.normalized * knockbackForce; //applying force

        

            health--;
        }
    }

    IEnumerator InvincibleFlash()
    {
        isInvincible = true;
        yield
        return new WaitForSeconds(invincibleTime);
        yield
        return new WaitForSeconds(invincibleTime);
        yield
        return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}
