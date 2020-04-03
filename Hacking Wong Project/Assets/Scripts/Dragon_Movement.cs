using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        //FOR TESTING ONLY - PLEASE CHANGE AFTER ADDING AI!
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x < 0) animator.SetBool("isRight", false);
        else if (movement.x > 0) animator.SetBool("isRight", true);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
