using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TrackerBullet : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    public float speed = 13f;
    public float rotateSpeed = 200f;
    public float damage = 10f;
    public GameObject explosion;
    void Start()
    {
        Destroy(gameObject, 5f);
        target = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = - rotateAmount * rotateSpeed;
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Projectile_Enemy" || other.tag == "Boss" || other.tag == "GameController")
        {
            return;
        }
        if (other.tag == "Player")
        {
            other.GetComponent<Player_Movement>().TakeDamage(damage);
            Destroy(Instantiate(explosion, other.transform.position, Quaternion.identity), 0.45f);
        }
        else
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.45f);
            if (other.tag == "Gun")
            {
                Destroy(other.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
