using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float force = 15f;
    public Transform crosshair;
    public Animator anim;
    Vector2 shootDirection;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootDirection = crosshair.position - shootPoint.position;
            if (shootDirection.x > 0)
            {
                anim.SetBool("isRight", true);
            }
            else if (shootDirection.x < 0)
            {
                anim.SetBool("isRight", false);
            }
            anim.SetTrigger("Shoot");
            Shoot();
        }
    }

    void Shoot()
    {
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg; //add -90f if bullet direction is off
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.Euler(0,0,angle));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shootDirection.normalized * force, ForceMode2D.Impulse);
    }
}
