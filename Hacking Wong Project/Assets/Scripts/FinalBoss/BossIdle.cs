using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    public float idleTime = 3f;
    float timer;
    Transform target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time >= timer + idleTime)
        {
            int random = Mathf.FloorToInt(Random.Range(0, 2));
            switch (random)
            {
                case 0: 
                    animator.SetTrigger("Discharge");
                    break;
                case 1: 
                    Transform boss = animator.transform;
                    if(boss.position.x > target.position.x+2)
                    {
                        animator.SetBool("playerIsRight", false);
                    } else if(boss.position.x + 2 < target.position.x)
                    {
                        animator.SetBool("playerIsRight", true);
                    }
                    animator.SetTrigger("Shoot");
                    break;
            }
        }
        //check if health is zero and then trigger death
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
