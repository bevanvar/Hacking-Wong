using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public float attackRate;
    public float attackDamage;
    public LayerMask enemyLayer;
    float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }           
    }

    void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            return;
        }
        animator.SetTrigger("Attack"); //include attackTimer later

        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        //hurt enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.gameObject.tag == "Enemy")
            {
                enemy.GetComponent<Take_Damage>().DamageTaken(attackDamage);
            } else if(enemy.gameObject.tag == "Boss")
            {
                enemy.GetComponent<BossDamage>().Damage(attackDamage);
            } else
            {
                Destroy(enemy);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {

        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
