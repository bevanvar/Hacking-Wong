﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    private IInteractable interactable;

    static public float maxHealth = 100f;
    static public float currentHealth = maxHealth;
    public HealthBar healthbar;

    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            movement = Vector2.zero;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        animator.SetFloat("Horizontal", movement.x); //necessary for blend tree in Animator
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        if (movement.x < 0) animator.SetBool("isRight", false);
        else if(movement.x >0) animator.SetBool("isRight", true); //for left persistence in idle position
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    public void Interact(){
        if(interactable!=null){
            interactable.Interact();
        }

    }

    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Interactable"){
            interactable = collision.GetComponent<IInteractable>();
        }
    }
    public void OnTriggerExit2D(Collider2D collision){
        if (collision.tag == "Interactable")
        {
            if (interactable !=null)
            {
                interactable.StopInteract();
                interactable = null;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt")) return;
        if(damage >= currentHealth)
        {
            animator.SetTrigger("Hurt");
            currentHealth = 0;
            StartCoroutine(DeadLoad());
        } else
        {
            currentHealth = currentHealth - damage;
            animator.SetTrigger("Hurt");
        }
        healthbar.SetHealth(currentHealth);
    }

    IEnumerator DeadLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("DeathScene");
    }

}
