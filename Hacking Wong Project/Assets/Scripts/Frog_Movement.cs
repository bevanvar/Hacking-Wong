using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Frog_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    private int currentWaypoint;
    Seeker seeker;
    public Animator anim;
    private bool firstTimePath = false;

    private enum State
    {
        Idle,
        Chasing,
    }
    private State state;

    private void Start()
    {
        if (target.position.x <= rb.position.x) anim.SetBool("isRight", false);
        else anim.SetBool("isRight", true);
        seeker = GetComponent<Seeker>();
        state = State.Idle;
    }

    private void Update()
    {
        if (target.position.x <= rb.position.x) anim.SetBool("isRight", false);
        else anim.SetBool("isRight", true);
        switch (state)
        {
            case State.Idle:
                anim.SetFloat("horizontal", 0f);
                anim.SetFloat("vertical", 0f);
                anim.SetFloat("speed", 0f);
                if (Vector2.Distance(rb.position, target.position)<12)
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
                /*if (Vector2.Distance(rb.position, target.position)< *INSERT DISTANCE)
                {
                    explode
                }
                */
                Pathfinding();
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
        if (target.position.x <= rb.position.x) anim.SetBool("isRight", false);
        else anim.SetBool("isRight", true);
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
        anim.SetFloat("horizontal", movement.x);
        anim.SetFloat("vertical", movement.y);
        anim.SetFloat("speed", speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, 8);
    }
}
