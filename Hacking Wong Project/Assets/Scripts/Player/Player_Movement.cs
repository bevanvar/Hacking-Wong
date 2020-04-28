using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    private IInteractable interactable;

    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthbar;

    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
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
            currentHealth = 0;
            Debug.Log("Player dead");
        } else
        {
            currentHealth = currentHealth - damage;
            animator.SetTrigger("Hurt");
        }
        healthbar.SetHealth(currentHealth);
    }

}
