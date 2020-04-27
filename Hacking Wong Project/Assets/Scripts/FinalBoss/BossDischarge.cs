using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDischarge : StateMachineBehaviour
{
    public GameObject missile;
    Transform shootPoint;
    public float force = 10f;
    public int numOfBullets = 10;
    double timeBwShots;
    float timer = 0;
    Transform target;
    Vector2 directionOfProjectile;
    float angleDeviation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shootPoint = animator.transform.GetChild(0);
        timeBwShots = 0.833 / (numOfBullets);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        directionOfProjectile = target.position - shootPoint.position;
        angleDeviation = 360 / numOfBullets * Mathf.Deg2Rad;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer + timeBwShots > Time.time)
        {
            return;
        }
        timer = Time.time;
        GameObject missileProjectile = Instantiate(missile, shootPoint.position, Quaternion.identity);
        missileProjectile.GetComponent<Rigidbody2D>().AddForce(force * directionOfProjectile.normalized, ForceMode2D.Impulse);
        //change angle
        float cosAng = Mathf.Cos(angleDeviation);
        float sinAng = Mathf.Sin(angleDeviation);
        directionOfProjectile = new Vector2(directionOfProjectile.x * cosAng - directionOfProjectile.y * sinAng, directionOfProjectile.x * sinAng + directionOfProjectile.y * cosAng);
    }
}
