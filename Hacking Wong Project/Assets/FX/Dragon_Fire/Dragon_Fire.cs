﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Dragon_Fire : MonoBehaviour
{
    public float damage;
    public GameObject explosion;
    public AudioClip playerHit;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Projectile_Enemy" || other.tag == "GameController")
        {
            return;
        }
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(playerHit, Camera.main.transform.position);
            other.GetComponent<Player_Movement>().TakeDamage(damage);
            Destroy(Instantiate(explosion, other.transform.position, Quaternion.identity), 0.65f);
        }
        else
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.65f);
        Destroy(gameObject);
    }
}
