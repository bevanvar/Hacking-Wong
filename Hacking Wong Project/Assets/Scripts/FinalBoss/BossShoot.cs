using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : StateMachineBehaviour
{
    Transform target;
    public GameObject missile;
    Transform shootPoint;
    public float timeBwShoot = 0.3f;
    float timer = 0;
    public float force = 10f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        shootPoint = animator.transform.GetChild(1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timer + timeBwShoot > Time.time)
        {
            return;
        }
        timer = Time.time;
        Vector2 shootDirection = target.position - shootPoint.position;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        GameObject missileProjectile = Instantiate(missile, shootPoint.position, Quaternion.Euler(0, 0, angle));
        missileProjectile.GetComponent<Rigidbody2D>().AddForce(force * shootDirection.normalized, ForceMode2D.Impulse);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
