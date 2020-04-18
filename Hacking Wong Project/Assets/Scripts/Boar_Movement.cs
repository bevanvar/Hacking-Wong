﻿using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar_Movement : MonoBehaviour
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
        seeker = GetComponent<Seeker>();
        state = State.Idle;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                if ( Vector2.Distance(rb.position, target.position)<10)
                {
                    state = State.Chasing;
                    firstTimePath = true;
                }
                if (target.position.x < rb.position.x)
                {
                    anim.SetBool("isRight", false);
                }
                else if (target.position.x > rb.position.x)
                {
                    anim.SetBool("isRight", true);
                }
                break;
            case State.Chasing:
                if (firstTimePath)
                {
                    InvokeRepeating("UpdatePath", 0f, 0.5f);
                    firstTimePath = false;
                    currentWaypoint = 0;
                }
                if(Vector2.Distance(rb.position, target.position)<2)
                {
                    StartCoroutine(waiter());
                }
                Pathfinding();
            break;
        }
    }

    IEnumerator waiter()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(4);
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
        anim.SetFloat("Speed", movement.sqrMagnitude);
        if (target.position.x < rb.position.x)
        {
            anim.SetBool("isRight", false);
        }
        else if (target.position.x > rb.position.x)
        {
            anim.SetBool("isRight", true);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, 8);
    }
}
