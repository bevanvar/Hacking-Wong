using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }   
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); //include attackTimer later

        //Detect enemies in range

        //hurt enemies
    }
}
