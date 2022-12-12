using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chasing : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject Player;
    public float enemyDistance = 4.0f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);
        //If the enemy is less than 4 spaces away from the player, the enemy will change color to blue, to indicate chasing, and will chase the player. 
        if (distance < enemyDistance)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
            //This brings the enemy towards the player
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            enemy.SetDestination(newPos);
        }
        //If the player is more than 4 units away, the enemy will return to it's original color. 
        else if (distance > enemyDistance)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isWalking", false);
            enemy.SetDestination(transform.position);
        }
    }
}
