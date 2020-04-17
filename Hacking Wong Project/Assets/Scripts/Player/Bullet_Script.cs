using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public GameObject explosionEffect;
    public float bulletDamage = 20f;

    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            return;
        } else if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Take_Damage>().DamageTaken(bulletDamage);
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //damage script happens here
    }
}
