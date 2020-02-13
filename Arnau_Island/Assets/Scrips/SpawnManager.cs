using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnRange = 10;
    public float minMassRange = 0.5f;
    public float maxMassRange = 2.5f;
    int waves = 1;

    public GameObject powerUpPrefab;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waves);
        Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);

        player = GameObject.Find("Player");
        initialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesCount = FindObjectsOfType<Enemy>().Length;
        if (enemiesCount == 0)
        {
            waves++;
            SpawnEnemyWave(waves);
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }
        if(player.transform.position.y == -10)
        {
            //Destroy(player.gameObject);
            player.transform.position = initialPosition;
        }

        if (Input.anyKeyDown && player == null)
        {
            Instantiate(player, transform.position, player.transform.rotation);
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float spawnRandomX = Random.Range(-spawnRange, spawnRange);
        float spawnRandomZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnRandomPosition = new Vector3(spawnRandomX, 0, spawnRandomZ);
        Debug.Log("Random position" + spawnRandomPosition);
        return spawnRandomPosition;
    }

    void SpawnEnemyWave(int enmiesToSpawn)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateRandomPosition(), enemyPrefab.transform.rotation);
            Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();
            enemyRB.mass = Random.Range(minMassRange, maxMassRange);
            float currentMass = enemyRB.mass;
            enemy.transform.localScale = new Vector3(currentMass, currentMass, currentMass);
        }
    }
}
