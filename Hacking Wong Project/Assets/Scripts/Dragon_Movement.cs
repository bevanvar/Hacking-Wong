using Pathfinding;
using UnityEngine;

public class Dragon_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    private int currentWaypoint;
    Seeker seeker;
    public Animator anim;
    public float attackRange = 15f;
    public float shootRange = 6f;
    private bool firstTimePath = false;

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
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void Update()
    {
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
                    firstTimePath = false;
                    currentWaypoint = 0;
                }
                Pathfinding();
                break;
            case State.Shooting:
                Debug.Log("Currently Shooting");
                if (target.position.x < rb.position.x)
                {
                    anim.SetBool("isRight", false);
                } else if (target.position.x > rb.position.x)
                {
                    anim.SetBool("isRight", true);
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
            state = State.Shooting;
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
            return;
        }

        if(Vector2.Distance(target.position, rb.position) >= attackRange)
        {
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, attackRange);
    }
}
