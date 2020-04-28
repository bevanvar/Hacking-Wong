using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    HealthBar playerHealth;
    Player_Movement healthValues;

    public float healthBonus = 50f;

    void Start(){
        playerHealth = FindObjectOfType<HealthBar>();
        healthValues = FindObjectOfType<Player_Movement>();
    }

    void OnTriggerEnter2D(Collider2D col){
        

        if (col.CompareTag("Player")){ 
            if(healthValues.currentHealth<healthValues.maxHealth){
                Destroy(gameObject);
                healthValues.currentHealth = healthValues.currentHealth + healthBonus;
                healthValues.currentHealth = healthValues.currentHealth > 100 ? 100 : healthValues.currentHealth;
                playerHealth.SetHealth(healthValues.currentHealth);
        }
    }
    }


}
