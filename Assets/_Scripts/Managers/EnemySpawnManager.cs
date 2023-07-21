using System.Collections;
using UnityEngine;

public class EnemySpawnManager : BaseSpawnManager
{

    public float spawnInterval = 2.0f;
    public float spawnDistance = 10.0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        GameManager.Instance.OnGame += StartSpawnEnemy;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (GameManager.Instance.state == GameStates.Game)
        {
            
            Vector3 spawnDirection = GetRandomSpawnDirection();

           
            Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(spawnDirection);
            spawnPosition.z = 0;
            spawnPosition = spawnPosition.normalized * spawnDistance;

            
            var enemy = EnemyPool.Instance.GetObject();
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnDirection()
    {
        int randomSide = Random.Range(0, 4);

        switch (randomSide)
        {
            case 0: return new Vector3(0, Random.value, spawnDistance);        
            case 1: return new Vector3(1, Random.value, spawnDistance);       
            case 2: return new Vector3(Random.value, 0, spawnDistance);        
            case 3: return new Vector3(Random.value, 1, spawnDistance);        
            default: return Vector3.zero;
        }
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(SpawnEnemies());
    }

   
}
