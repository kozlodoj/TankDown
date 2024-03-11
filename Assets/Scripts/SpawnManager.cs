using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 1;
    public int enemyWave;

    public float spawnPositionRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(enemyCount);
    }

    // Update is called once per frame
    void Update()
    {
        //track enemies count
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)

        {
            //add a new enemy
            enemyWave++;
            SpawnEnemy(enemyWave);
        }
       
    }
    //spawn enemy
    void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {

            Instantiate(enemyPrefab, GetRandomRange(), enemyPrefab.transform.rotation);
        }
    }
    //generate random position within range
    Vector3 GetRandomRange()
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnPositionRange, spawnPositionRange), 1f, Random.Range(-spawnPositionRange, spawnPositionRange));

        return randomPos;
     
    }
}
