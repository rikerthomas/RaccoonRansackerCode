using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    public bool isDead;
    public GameObject spawner;
    public Transform[] SpawnPoints;
    public bool spawning;


    public float spawnTimer;
    public float spawnEnd;



    // Start is called before the first frame update
    void Start()
    {
        spawning = false;
        spawnEnd = 20f;
    }

    private void Update()
    {
        if (spawning == true)
        {
            spawnTimer += Time.deltaTime;
        }
        if (spawnTimer > spawnEnd)
        {
            Destroy(spawner);
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawning = true;
            Debug.Log("Enemys are spawnignasignsda");
            StartCoroutine(SpawnMoreEnemies());
        }
    }

    IEnumerator SpawnMoreEnemies()
    {
        while (spawning == true)
        {
            yield return new WaitForSeconds(3.0f);
            EnemySpawner();
        }
    }

    private void EnemySpawner()
    {
        int randomNumber = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length - 1));

        Instantiate(enemy, SpawnPoints[randomNumber].transform.position, Quaternion.identity);
    }
}
