using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : MonoBehaviour
{
    public float damage = 10f;
    public GameObject explosion;
    public AudioClip playerHit;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy" || other.tag == "Projectile_Enemy" || other.tag == "Gun" || other.tag == "Boss" || other.tag == "GameController")
        {
            return;
        }
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(playerHit, Camera.main.transform.position);
            other.GetComponent<Player_Movement>().TakeDamage(damage);
            Destroy(Instantiate(explosion, other.transform.position, Quaternion.identity), 0.45f);
        }
        else
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.45f);
        Destroy(gameObject);
    }
}
