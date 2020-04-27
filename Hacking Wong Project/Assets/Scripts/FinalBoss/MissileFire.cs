using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : MonoBehaviour
{
    public float damage = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Projectile_Enemy" || other.tag == "Gun")
        {
            return;
        }
        if (other.tag == "Player")
        {
            other.GetComponent<Player_Movement>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
