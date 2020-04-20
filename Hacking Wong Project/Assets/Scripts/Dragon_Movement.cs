using Pathfinding;
using System;
using System.Net.Sockets;
using UnityEngine;

public class Dragon_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    private int currentWaypoint;
    Seeker seeker;
    public Animator anim;
    public float attackRange = 15f;
    public float shootRange = 6f;
    private bool firstTimePath = false;

    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    public Transform shootPoint;
    public GameObject firePrefab;
    public float force = 10f;

    private enum State
    {
        Idle,
        Chasing,
        Shooting,
    }
    private State state;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        state = State.Idle;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        target = g.transform;
        if (target.position.x <= rb.position.x) anim.SetBool("isRight", false);
        else anim.SetBool("isRight", true);
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
            default:
            case State.Idle:
                if(Vector2.Distance(rb.position, target.position)< attackRange)
                {
                    state = State.Chasing;
                    firstTimePath = true;
                }
                break;
            case State.Chasing:
                if (firstTimePath)
                {
                    InvokeRepeating("UpdatePath", 0f, 0.5f);
                    firstTimePath = false;
                    currentWaypoint = 0;
                }
                Pathfinding();
                break;
            case State.Shooting:
                if (target.position.x < rb.position.x)
                {
                    anim.SetBool("isRight", false);
                } else if (target.position.x > rb.position.x)
                {
                    anim.SetBool("isRight", true);
                }
                if (Time.time > nextFireTime)
                {
                    Shoot();
                }
                if (Vector2.Distance(rb.position, target.position) > shootRange)
                {
                    firstTimePath = true;
                    state = State.Chasing;
                }
                break;
        }   
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

        if(Vector2.Distance(target.position, rb.position) <= shootRange)
        {
            CancelInvoke("UpdatePath");
            state = State.Shooting;
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            return;
        }

        if(Vector2.Distance(target.position, rb.position) >= attackRange)
        {
            CancelInvoke("UpdatePath");
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            state = State.Idle;
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
        anim.SetFloat("Speed", movement.sqrMagnitude);
        if (movement.x > 0)
        {
            anim.SetBool("isRight", true);
        } else if (movement.x < 0)
        {
            anim.SetBool("isRight", false);
        }
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(rb.position, attackRange);
    //}

    private void Shoot()
    {
        anim.SetTrigger("shoot");
        Vector2 shootDirection = (Vector2)target.position - rb.position;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        GameObject fireProjectile = Instantiate(firePrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
        fireProjectile.GetComponent<Rigidbody2D>().AddForce(force* shootDirection.normalized, ForceMode2D.Impulse);
        nextFireTime = Time.time + 1/fireRate;
    }
}
