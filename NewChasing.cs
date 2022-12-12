using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewChasing : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;
    Rigidbody rb;
    public Collider player;
    private Animator animator;
    private float timeToDisappear = 3f;
    public AudioSource audioSource;
    public AudioClip clip;
    private float waitTime;

    public GameObject bullet;

    public int hits;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Audio", 0f, 3f);
        animator = GetComponent<Animator>();
        bullet = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        waitTime = Random.Range(5f, 30f);
        EnemyDead();

    }

    public void EnemyDead()
    {
        if (hits >= 3)
        {
            animator.SetBool("isDead", true);
            StopCoroutine(MonsterRoar());
            StartCoroutine(DisappearCoroutine(gameObject));

            return;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hits < 3)
        {
            {
                Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                rb.MovePosition(pos);
                transform.LookAt(target);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider == player)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            hits++;
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
