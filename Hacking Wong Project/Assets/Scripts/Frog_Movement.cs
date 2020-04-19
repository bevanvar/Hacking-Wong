using Pathfinding;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System;

public class Frog_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    private int currentWaypoint;
    Seeker seeker;
    public Animator anim;
    private bool firstTimePath = false;
    private float timeTracker;

    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float explosionRadius = 4f;
    public float damage = 100f;

    public LayerMask layerMask;
    public Collider2D selfCollider;

    private enum State
    {
        Idle,
        Chasing,
        Explode,
    }
    private State state;

    private void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        target = g.transform;
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
                if (Vector2.Distance(rb.position, target.position)<chaseRange)
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
                if (Vector2.Distance(rb.position, target.position)< attackRange)
                {
                    firstTimePath = true;
                    state = State.Explode;
                    return;
                }
                Pathfinding();
                break;
            case State.Explode:
                anim.SetBool("isRight", rb.position.x > target.position.x);
                anim.SetFloat("horizontal", 0f);
                anim.SetFloat("vertical", 0f);
                anim.SetFloat("speed", 0f);
                if (firstTimePath)
                {
                    firstTimePath = false;
                    timeTracker = Time.time + 1.0f;
                } else
                {
                    if (timeTracker <= Time.time)
                    {
                        Explode();
                    }
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

    private void Explode()
    {
        firstTimePath = true;
        selfCollider.enabled = false;
        //stop moving
        Collider2D[] hitBy = Physics2D.OverlapCircleAll(rb.position, explosionRadius, layerMask);
        anim.SetTrigger("hurt");
        foreach (Collider2D hit in hitBy)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Take_Damage>().DamageTaken(damage);
            } else if(hit.tag == "Player")
            {
                Debug.Log("Player hurt");
            } else
            {
                //bullet hit
                Destroy(hit);
            }
        }
        Destroy(gameObject, 0.8f);
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
        Gizmos.DrawWireSphere(rb.position, attackRange);
        Gizmos.DrawWireSphere(rb.position, explosionRadius);
    }
}
