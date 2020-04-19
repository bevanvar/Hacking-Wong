using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boar_Movement : MonoBehaviour
{
    private float speed = 10f;
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
                speed = 0f;
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", 0f);
                anim.SetFloat("Speed", 0f);
                if(Vector2.Distance(rb.position, target.position)<2)
                {
                    StartCoroutine(waiter());
                }
                else if(Vector2.Distance(rb.position, target.position)<=12)
                {
                    state = State.Chasing;
                    firstTimePath = true;
                }
            break;
            case State.Chasing:
                speed = 10f;
                if (Vector2.Distance(rb.position, target.position)>=2 && Vector2.Distance(rb.position, target.position)<=12)
                {
                    if (firstTimePath)
                    {
                        InvokeRepeating("UpdatePath", 0f, 0.5f);
                        firstTimePath = false;
                        currentWaypoint = 0;
                    }
                    if (Vector2.Distance(rb.position, target.position)<6) speed = 18f;
                    else speed = 10f;
                    Pathfinding();
                }
                if (Vector2.Distance(rb.position, target.position)<2 || Vector2.Distance(rb.position, target.position)>12)
                {
                    state = State.Idle;
                    firstTimePath = false;
                }
            break;
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        state = State.Chasing;
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
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, 8);
    }
}
