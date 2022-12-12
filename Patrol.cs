using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float speed = 5f;
    private float waitTime = 5.28f;
    private float waitCounter = 0f;
    private bool waiting = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Patrolling();
    }

    private void Patrolling()
    {
        if (waiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                return;
            waiting = false;
        }
        Transform wp = waypoints[currentWaypointIndex];
        if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        {

            waitCounter = 0f;
            waiting = true;
            animator.SetBool("isLooking", true);
            animator.SetBool("isWalking", false);

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, speed * Time.deltaTime);
            transform.LookAt(wp.position);
            animator.SetBool("isWalking", true);
            animator.SetBool("isLooking", false);
        }
    }
}
