using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar_Movement : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D rb;
    public Vector2 movement;
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
        rb.MovePosition(rb.position + 4 * movement * speed * Time.fixedDeltaTime);
    }
}
