using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    //TODO:
    // 1. Randomize spawn time.
    // 2. Keep enemy count and add maximum number of enemies that can be present at a time.


    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header("Wave system")]
    [SerializeField,Range(0f,5f)] private float minSpawnInterval = 5f;
    [SerializeField,Range(0f,10f)] private float maxSpawnInterval = 10f;

    [Tooltip("Minimum distance at which enemy should be spawning from player")]
    [SerializeField,Range(1f,10f)] private float minimumSpawnDistance;

    [Tooltip("Maximum distance at which enemy should be spawning from player")]
    [SerializeField,Range(1f,10f)] private float maximumSpawnDistance;

    [SerializeField] private List<int> enemyPerWaves;

    //Properties



    //private variables (external references).
    private ObjectPooler enemyPooler;


    private Vector3 lastEnemyPos;
    private Vector3 currentSpawnPos;
    private Quaternion spawnRotation;


    private Transform playerTransform;


    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    private void Start()
    {
        enemyPooler = ObjectPooler.Instance;
        enemyPooler.InitializePool(enemyPrefab, 50, enemyParent);

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)//Condition at which it should stop spawning like : if we are playing the level and the screen is not pasued.
        {
            SpawnEnemy();
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        currentSpawnPos = GetRandomEdgePosition();
        //Debug.Log("Current spawn pos : "+ currentSpawnPos);
        /*while (currentEnemyPos == lastEnemyPos)
        {
            currentEnemyPos = GetRandomEdgePosition();
        }*/

        spawnRotation = Quaternion.identity;
        Instantiate(enemyPrefab, currentSpawnPos, spawnRotation,enemyParent);
 


    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns the random world position of enemy</returns>
    private Vector3 GetRandomEdgePosition()
    {
        Vector3 randomOffset = RandomMethod1();

        Vector3 spawnPos = playerTransform.position + randomOffset;
     
        return spawnPos;
    }


    private Vector3 RandomMethod1()
    {
        Vector3 randomOffset = Vector3.zero;
        float distance = 0f;
        do
        {

            float xRange1 = Random.Range(-minimumSpawnDistance, minimumSpawnDistance);
            float xRange2 = Random.Range(-maximumSpawnDistance, maximumSpawnDistance);
            float x = Random.Range(xRange1, xRange2);
            randomOffset.x = x;


            float yRange1 = Random.Range(-minimumSpawnDistance, minimumSpawnDistance);
            float yRange2 = Random.Range(-maximumSpawnDistance, maximumSpawnDistance);
            float y = Random.Range(yRange1, yRange2);
            randomOffset.y = y;

            distance = Vector2.Distance(randomOffset,playerTransform.position);

        } while (distance < minimumSpawnDistance);
        return randomOffset;

    }

}
