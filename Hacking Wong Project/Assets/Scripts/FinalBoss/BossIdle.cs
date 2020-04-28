using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    public float idleTime = 3f;
    float timer;
    Transform target;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("isDead"))
        {
            Debug.Log("Explosions go booom"); //instantiate big boy explosion here
        }
        timer = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time >= timer + idleTime && !animator.GetBool("isDead"))
        {
            //int random = Mathf.FloorToInt(Random.Range(0, 3));
            int random = 2;
            Transform boss = animator.transform;
            switch (random)
            {
                case 0: 
                    animator.SetTrigger("Discharge");
                    break;
                case 1: 
                    if(boss.position.x > target.position.x+2)
                    {
                        animator.SetBool("playerIsRight", false);
                    } else if(boss.position.x + 2 < target.position.x)
                    {
                        animator.SetBool("playerIsRight", true);
                    }
                    animator.SetTrigger("Shoot");
                    break;
                case 2:
                    if (boss.position.x > target.position.x + 2)
                    {
                        animator.SetBool("playerIsRight", false);
                    }
                    else if (boss.position.x + 2 < target.position.x)
                    {
                        animator.SetBool("playerIsRight", true);
                    }
                    animator.SetTrigger("Tracker");
                    break;
            }
        }
    }
}
