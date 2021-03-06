﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    HealthBar playerHealth;

    public float healthBonus = 50f;
    public Player_Movement healthValues;
    public AudioClip potionCollect;

    void Start(){
        playerHealth = FindObjectOfType<HealthBar>();
        healthValues = FindObjectOfType<Player_Movement>();
    }

    void OnTriggerEnter2D(Collider2D col){
        

        if (col.CompareTag("Player")){ 
            if(Player_Movement.currentHealth<Player_Movement.maxHealth){
                AudioSource.PlayClipAtPoint(potionCollect, new Vector3(0, 0, 0));
                Destroy(gameObject);
                Player_Movement.currentHealth = Player_Movement.currentHealth + healthBonus;
                Player_Movement.currentHealth = Player_Movement.currentHealth > 100 ? 100 : Player_Movement.currentHealth;
                playerHealth.SetHealth(Player_Movement.currentHealth);
            }
        }
    }



}
