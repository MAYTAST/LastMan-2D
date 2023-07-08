using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private float spawnInterval = 1f;

    [Tooltip("Minimum distance at which enemy should be spawning from player")]
    [SerializeField,Range(1f,10f)] private float minimumSpawnDistance;

    [Tooltip("Maximum distance at which enemy should be spawning from player")]
    [SerializeField,Range(1f,10f)] private float maximumSpawnDistance;


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
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)//Condition at which it should stop spawning like : if we are playing the level and the screen is not pasued.
        {
            SpawnEnemy();
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
        float xRange1 = Random.Range(-minimumSpawnDistance, minimumSpawnDistance);
        float xRange2 = Random.Range(-maximumSpawnDistance, maximumSpawnDistance);
        float x = Random.Range(xRange1, xRange2);


        float yRange1 = Random.Range(-minimumSpawnDistance, minimumSpawnDistance);
        float yRange2 = Random.Range(-maximumSpawnDistance, maximumSpawnDistance);
        float y = Random.Range(yRange1, yRange2);

        return new Vector3 (x,y,0);

    }

}
