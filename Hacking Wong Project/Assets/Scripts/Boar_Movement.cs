using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boar_Movement : MonoBehaviour
{
    private float speed = 6f;
    public Rigidbody2D rb;
    Transform target;
    public float nextWaypointDistance = 2f;
    Path path;
    private int currentWaypoint;
    Seeker seeker;
    public Animator anim;
    private bool firstTimePath = false;
    public float chaseRange = 12f;
    public float attackRange;
    private bool waiting = false;
    public GameObject attackAnim;
    public float damage;
    private GameObject player;

    private enum State
    {
        Idle,
        Chasing,
    }
    private State state;

    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        seeker = GetComponent<Seeker>();
        state = State.Idle;
    }

    private void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Dead") || anim.GetCurrentAnimatorStateInfo(0).IsTag("Hurt"))
        {
            rb.velocity = Vector2.zero;
            return;
        }
        switch (state)
        {
            case State.Idle:
                CancelInvoke();
                speed = 0f;
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", 0f);
                anim.SetFloat("Speed", 0f);
                anim.SetBool("isRight", false);
                if(waiting)
                {
                    break;
                }
                /*if (Vector2.Distance(rb.position, target.position)<attackRange)
                {
                    StartCoroutine(waiter());
                }*/
                else if(Vector2.Distance(rb.position, target.position)<=chaseRange)
                {
                    state = State.Chasing;
                    firstTimePath = true;
                }
            break;
            case State.Chasing:
                speed = 6f;
                float distance = Vector2.Distance(rb.position, target.position);
                if (distance>attackRange && distance<=chaseRange)
                {
                    if (firstTimePath)
                    {
                        InvokeRepeating("UpdatePath", 0f, 0.5f);
                        firstTimePath = false;
                        currentWaypoint = 0;
                    }
                    if (distance<6) speed = 8f;
                    else speed = 6f;
                    Pathfinding();
                }
                else if (distance>chaseRange)
                {
                    state = State.Idle;
                    firstTimePath = false;
                }
                else if (distance<=attackRange)
                {
                    StartCoroutine(Attack());
                }
            break;
        }
    }

    IEnumerator Attack()
    {
        GameObject clone = (GameObject) Instantiate(attackAnim, new Vector3((target.position.x + rb.position.x) / 2, (target.position.y + rb.position.y) / 2, 2), Quaternion.identity);
        Destroy(clone, 1.0f);
        player.GetComponent<Player_Movement>().TakeDamage(damage);
        waiting = true;
        state = State.Idle;
        yield return new WaitForSeconds(2);
        state = State.Chasing;
        waiting = false;
        firstTimePath = true;
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Pathfinding()
    {
        if (path == null)
        {
            return;
        }
        //Debug.Log(currentWaypoint + " " + path.vectorPath.Count);
        //if (currentWaypoint > path.vectorPath.Count)
        //{
        //    currentWaypoint = 0;
        //}
        Vector2 movement = ((Vector2)path.vectorPath[currentWaypoint] - rb.position);
        Vector2 direction = movement.normalized;
        rb.MovePosition(direction * speed * Time.deltaTime + rb.position);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance <= nextWaypointDistance)
        {
            currentWaypoint++;
            if (currentWaypoint == path.vectorPath.Count)
            {
                currentWaypoint = 0;
            }
        }
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", speed);
        if (movement.x<=0) anim.SetBool("isRight", false);
        else anim.SetBool("isRight", true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, chaseRange);
    }
}
