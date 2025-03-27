using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D bulletRB;
    public float speed;

    public float damage;

    [SerializeField] GameObject sparkgameobject; //the spark sprite that spawns when hitting an object

    private bool withSpark = true;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.velocity = Vector3.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the object the bullet collided with has a DamageController script
        if (collision.gameObject.GetComponent<TakeDamage>() != null)
        {
            TakeDamage collidedEnemy = collision.gameObject.GetComponent<TakeDamage>();
            collidedEnemy.TakeHit(damage);
        }

        if(collision.gameObject.GetComponent<ReflectBullet>() != null && collision.gameObject.GetComponent<ReflectBullet>().reflecting) 
        {
            bulletRB.velocity = (transform.position - collision.gameObject.transform.position) * speed;
        }
        else Destroy(gameObject);

        if(collision.gameObject.tag == "Bullet Collider (No Spark FX)")
        {
            withSpark = false;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); //destroys the bullets when they go off-screen
    }

    private void OnDestroy() //spawns a spark animation when hitting an enemy or destructable, with a slightly randomized pos
    {
        if (withSpark)
        {
            Vector3 randSpawnPos = new Vector3(transform.position.x + Random.Range(0, .1f), transform.position.y + Random.Range(0, .1f), 0);
            Instantiate(sparkgameobject, randSpawnPos, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
