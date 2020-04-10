using Pathfinding;
using UnityEngine;

public class Dragon_Movement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Transform target;
    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint;
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
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
                CancelInvoke();
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
                CancelInvoke();
                Debug.Log("Currently Shooting");
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance <= nextWaypointDistance)
        {
            currentWaypoint++;
        }
        anim.SetFloat("Horizontal", rb.velocity.x);
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
        if (rb.velocity.x > 0)
        {
            anim.SetBool("isRight", true);
        } else if (rb.velocity.x < 0)
        {
            anim.SetBool("isRight", false);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(rb.position, attackRange);
    }
}
