using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawns")]
    [SerializeField]
    private GameObject enemyContainer = null;
    [SerializeField]
    private GameObject enemySpawn = null;

    [Header("PowerUp Spawns")]
    [SerializeField]
    private GameObject powerUpContainer = null;
    [SerializeField]
    private GameObject[] arrayOfPowerUps = null;

    [Header("Fixed Timer Spawns")]
    [SerializeField]
    private GameObject ammoPrefab = null;
    [SerializeField]
    private float ammoRespawnRate = 5.0f;

    private bool stopSpawning = false;
    private float leftBoundaryX = -9.25f;
    private float rightBoundaryX = 9.25f;
    private float startY = 8f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SpawnGameObjects()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnRandomPowerUp());
        StartCoroutine(SpawnFixedTimerPowerUps());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            if (enemySpawn != null)
            {
                GameObject newEnemy = Instantiate(enemySpawn, new Vector3(UnityEngine.Random.Range(leftBoundaryX, rightBoundaryX), startY, 0f), Quaternion.identity);
                newEnemy.transform.parent = enemyContainer.transform;
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 4));
        }
    }
    IEnumerator SpawnRandomPowerUp()
    {
        while (stopSpawning == false)
        {
            if (arrayOfPowerUps != null)
            {
                GameObject powerUpToSpawn = arrayOfPowerUps[UnityEngine.Random.Range(0, arrayOfPowerUps.Length)];
                Vector3 powerUpSpawnLocation = new Vector3(UnityEngine.Random.Range(leftBoundaryX, rightBoundaryX), startY, 0f);

                GameObject newPowerUp = Instantiate(powerUpToSpawn, powerUpSpawnLocation, Quaternion.identity);
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 9));
        }
    }

    IEnumerator SpawnFixedTimerPowerUps()
    {
        while(stopSpawning == false)
        {
            if(ammoPrefab != null)
            {
                Debug.Log("Ammo");
                Vector3 powerUpSpawnLocation = new Vector3(UnityEngine.Random.Range(leftBoundaryX, rightBoundaryX), startY, 0f);
                GameObject newPowerUp = Instantiate(ammoPrefab, powerUpSpawnLocation, Quaternion.identity);

            }
            yield return new WaitForSeconds(ammoRespawnRate);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }


}
