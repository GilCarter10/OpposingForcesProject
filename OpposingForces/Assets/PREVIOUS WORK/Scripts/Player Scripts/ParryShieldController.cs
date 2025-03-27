using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerDamageAndParry;

public class ParryShieldController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy Projectile")
        {
            if (parryUnlocked && collision.gameObject.GetComponent<ProjectileController>() != null)
            {
                if(collision.gameObject.GetComponent<ProjectileController>().isParriable)
                {
                    ProjectileController parriedbullet = collision.gameObject.GetComponent<ProjectileController>();
                    parriedbullet.ChangeToParried(); //switches the bullet direction and speed
                }
            }
        }
    }

}
