using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnRange = 10;
    public float minMassRange = 0.5f;
    public float maxMassRange = 2.5f;
    int waves = 1;

    public GameObject powerUpPrefab;
    GameObject player;
    bool isGameOver = false;
    Vector3 initialPosition;
    GameObject Canvas;
    public Text gameOverText;

    private AudioSource playerAudio;

    public AudioClip crashSound;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waves);
        Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);

        player = GameObject.Find("Player");
        initialPosition = player.transform.position;
        Canvas = GameObject.Find("Player");
        /*string lifes = "3";
         int lifesInt = int.Parse(lifes); // function to change form string to Integer
         lifesInt = lifesInt - 2; */
        initialPosition = player.transform.position;
        Canvas = GameObject.Find("Canvas");
        gameOverText = Canvas.transform.GetChild(0).GetComponent<Text>();
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        int enemiesCount = FindObjectsOfType<Enemy>().Length;
        if (enemiesCount == 0 && !isGameOver)
        {
            waves++;
            SpawnEnemyWave(waves);
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }
        if(player.transform.position.y <= -10)
        {
            isGameOver = true;
            gameOverText.enabled = true;
            if (Input.GetKeyDown("space"))
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        player.transform.position = initialPosition;
        waves = 1;
        SpawnEnemyWave(waves);
        isGameOver = false;
        gameOverText.enabled = false;
        int powerupCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        if (powerupCount == 0)
        {
            Instantiate(powerUpPrefab, GenerateRandomPosition(), powerUpPrefab.transform.rotation);
        }
        else if (powerupCount > 1)
        {
            for (int i = 0; i < powerupCount; i++)
            {
                GameObject PowerupToDestroy = GameObject.FindGameObjectWithTag("Powerup");
                Destroy(PowerupToDestroy);
            }
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
