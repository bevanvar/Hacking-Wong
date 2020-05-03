using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    public EnemyHealth healthBar;
    public float maxHealth = 50f;
    float currentHealth;
    public Animator anim;
    public float delayDestroy = 1f;
    BossBattle bb;
    public AudioSource bossHit;

    // Start is called before the first frame update
    void Start()
    {
        bb = GameObject.Find("GameManager").GetComponent<BossBattle>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void Damage(float damage)
    {
        bossHit.Play();
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                
                anim.SetTrigger("Dead");
                anim.SetBool("isDead", true);
                Destroy(gameObject, delayDestroy);
                bb.BossIsDead();
                bb.InstantiateExplosions();
            }
        }
    }
}
