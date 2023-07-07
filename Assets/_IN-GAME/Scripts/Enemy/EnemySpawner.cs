using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 1f;

    [Tooltip("Minimum distance at which enemy should be spawning from player")]
    [SerializeField,Range(1f,10f)] private float spawnDistance;


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
        /*while (currentEnemyPos == lastEnemyPos)
        {
            currentEnemyPos = GetRandomEdgePosition();
        }*/

        spawnRotation = Quaternion.identity;
        Instantiate(enemyPrefab, currentSpawnPos, spawnRotation);



    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns the random world position of enemy</returns>
    private Vector3 GetRandomEdgePosition()
    {
        Vector3 randomOffset = Random.insideUnitSphere * spawnDistance;
        Vector3 spawnPos = playerTransform.position + randomOffset;
        return spawnPos;
    }

}
