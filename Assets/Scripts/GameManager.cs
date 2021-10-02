using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Player player;
    [Header("Platforms configurations")]
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] GameObject startingPlatformPrefab = null;
    [SerializeField] List<GameObject> randomPlatformsPrefabs = null;
    [SerializeField] float minDistanceToSpawn = 20f;
    [SerializeField] float maxDistanceToSpawn = 30f;
    [SerializeField] float minHeightToSpawn = -5f;
    [SerializeField] float maxHeightToSpawn = 5f;
    [SerializeField] float spawnDistanceXFromCamera = 50f;

    void Start()
    {
        player.onDie += PlayerDie;
        StartCoroutine(PlatformSpawnCoroutine());
    }
    void Update()
    {
        
    }
    void PlayerDie()
    {

    }
    IEnumerator PlatformSpawnCoroutine() 
    {
        Instantiate(startingPlatformPrefab, transform.position, Quaternion.identity, transform);
        Vector3 cameraStartingPos = cameraTransform.position;
        float randomSpawnDistance = Random.Range(minDistanceToSpawn, maxDistanceToSpawn);
        float randomSpawnHeight = Random.Range(minHeightToSpawn, maxHeightToSpawn);
        while (true) 
        {
            if (Vector3.Distance(cameraStartingPos, cameraTransform.position) > randomSpawnDistance) 
            {
                var randomIndex = Random.Range(0, randomPlatformsPrefabs.Count);
                Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistanceXFromCamera, cameraTransform.position.y + randomSpawnHeight, 0);
                Instantiate(randomPlatformsPrefabs[randomIndex], spawnPos, Quaternion.identity, transform);
                cameraStartingPos = cameraTransform.position;
                randomSpawnDistance = Random.Range(minDistanceToSpawn, maxDistanceToSpawn);
                randomSpawnHeight = Random.Range(minHeightToSpawn, maxHeightToSpawn);
            }
            yield return null;
        }
    }
}