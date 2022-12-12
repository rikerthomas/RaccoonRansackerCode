using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Chasing : MonoBehaviour
{
    public NavMeshAgent enemy;
    public GameObject Player;
    public float enemyDistance = 4.0f;

    public Transform returnToPatrol;
    public GameObject bullet;
    public int hits;
    public GameObject enemY;
    private float timeToDisappear = 3f;
    public Animator animator;
    public GameObject bulletSpawn;
    public AudioSource audioSource;
    public AudioClip clip;

    public Collider player;

    private float waitTime;

    public Transform target;
    public float speed = 4f;


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Audio", 0f, 3f); 
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        bulletSpawn = GetComponent<GameObject>();
        bullet = GetComponent<GameObject>();
        enemy = GetComponent<NavMeshAgent>();
        enemY = GetComponent<GameObject>();

    }

        // Update is called once per frame
        void Update()
        {
        waitTime = Random.Range(20f, 200f);
        if (hits >= 6)
            {
                StopCoroutine(MonsterRoar());
                Destroy(bulletSpawn);
                enemy.SetDestination(transform.position);
                animator.SetBool("isDead", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", false);
                StartCoroutine(DisappearCoroutine(gameObject));
                return;
            }
            float distance = Vector3.Distance(transform.position, Player.transform.position);
            //If the enemy is less than 4 spaces away from the player, the enemy will change color to blue, to indicate chasing, and will chase the player. 
            if (distance < enemyDistance)
            {
                //This brings the enemy towards the player
                Vector3 dirToPlayer = transform.position - Player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                enemy.SetDestination(newPos);
                GetComponent<MeshRenderer>().material.color = Color.blue; //sets the enemy color to blue to indicate that their state has changed. 
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
                transform.LookAt(target);
        }
            //If the player is more than 4 units away, the enemy will return to it's original color. 
            if (distance > enemyDistance)
            {
                enemy.SetDestination(transform.position);

                animator.SetBool("isIdle", true);
                animator.SetBool("isRunning", false);
            }

        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == player)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("bullet"))
        {
            hits++;
        }
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void Audio()
    {
        StartCoroutine(MonsterRoar());
    }

    private IEnumerator DisappearCoroutine(GameObject gameObject)
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }

    private IEnumerator MonsterRoar()
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.PlayOneShot(clip);
    }
}
