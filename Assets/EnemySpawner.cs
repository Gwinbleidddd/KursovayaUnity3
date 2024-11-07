using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Random2 = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    private GameObject player;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject SpeedPrefab;
    public GameObject DurabilityPrefab;
    public GameObject AKPrefab;
    public GameObject DrobPrefab;
    public GameObject BazukaPrefab;
    public float spawnTime = 8f;
    public int enemySpawned = 0;
    public float spawnRadius = 15f;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 0.5f;
    private int wave = 0;
    int randomBit = GenerateRandomBit();
    Random random = new Random();
    public float timeSinceLastSpawn = 0f;
    public float timeSinceLastSpeedSpawn = 0f;
    public float timeSinceLastDurSpawn = 0f;
    public float timeSinceLastAKSpawn = 0f;
    int randomBit1 = GenerateRandomBit1();
    public float SpeedSpawnTime = 8f;
    public float DurabilitySpawnTime = 8f;
    public float AKSpawnTime = 8f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnWave());
    }
    public static int GenerateRandomBit()
    {
        Random random = new Random();
        return random.Next(0, 2);
    }
    public static int GenerateRandomBit1()
    {
        Random random = new Random();
        return random.Next(0, 2);
    }
    IEnumerator SpawnWave()
    {
        while (true)
        {
            for (int i = 0; i < wave + 1; i++)
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), Random2.Range(-size.y / 2, size.y / 2), Random2.Range(-size.z / 2, size.z / 2));
                randomBit = GenerateRandomBit();
                if (randomBit == 0) {
                    
                    if (Vector3.Distance(spawnPosition,playerPosition) > spawnRadius)
                    {
                        Instantiate(enemy1Prefab, spawnPosition, Quaternion.identity);
                    }
                } 
                else 
                {
                    if (Vector3.Distance(spawnPosition,playerPosition) > spawnRadius)
                    {
                        Instantiate(enemy2Prefab, spawnPosition, Quaternion.identity);
                    }
                }
            }
            wave++;
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
    void Update()
    {
        timeSinceLastSpeedSpawn += Time.deltaTime;
        timeSinceLastDurSpawn += Time.deltaTime;
        timeSinceLastAKSpawn += Time.deltaTime;
        if (timeSinceLastSpeedSpawn >= SpeedSpawnTime)
        {
            SpawnSpeed();
            timeSinceLastSpeedSpawn = 0f;
        }
        
        if (timeSinceLastDurSpawn >= DurabilitySpawnTime)
        {
            SpawnDurability();
            timeSinceLastDurSpawn = 0f;
        }
        if (timeSinceLastAKSpawn >= AKSpawnTime)
        {
            randomBit1 = GenerateRandomBit1();
            if (randomBit1 == 0) {
                    
                SpawnAK();
            } 
            else if (randomBit1 == 1)
            {
                SpawnDrob();
            }
            else 
            {
                SpawnBazuka();
            }
            timeSinceLastAKSpawn = 0f;
        }
    }
    void SpawnSpeed()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), 1, Random2.Range(-size.z / 2, size.z / 2));
        Instantiate(SpeedPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnDurability()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), 1, Random2.Range(-size.z / 2, size.z / 2));
        Instantiate(DurabilityPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnAK()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), 1, Random2.Range(-size.z / 2, size.z / 2));
        Instantiate(AKPrefab, spawnPosition, Quaternion.identity);
    }
    void SpawnDrob()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), 1, Random2.Range(-size.z / 2, size.z / 2));
        Instantiate(DrobPrefab, spawnPosition, Quaternion.identity);
    }
    void SpawnBazuka()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition = center + new Vector3(Random2.Range(-size.x / 2, size.x / 2), 1, Random2.Range(-size.z / 2, size.z / 2));
        Instantiate(BazukaPrefab, spawnPosition, Quaternion.identity);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center,size);
    }
}