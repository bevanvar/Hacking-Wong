using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public GameObject explosionEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosionEffect.gameObject, 0.8f);
        //damage script happens here
    }
}
