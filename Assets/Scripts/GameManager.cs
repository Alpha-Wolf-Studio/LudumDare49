using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Action OnResetLevel;
    public Player player;

    [Header("Platforms Configurations")]
    [SerializeField] Transform cameraTransform = null;
    [SerializeField] GameObject startingPlatformPrefab = null;
    [SerializeField] List<GameObject> randomPlatformsPrefabs = null;
    [SerializeField] float minDistanceToSpawn = 20f;
    [SerializeField] float maxDistanceToSpawn = 30f;
    [SerializeField] float minHeightToSpawn = -5f;
    [SerializeField] float maxHeightToSpawn = 5f;
    [SerializeField] float spawnDistanceXFromCamera = 50f;

    [Header("Theme Configurations")]
    [SerializeField] List<ColorTheme> posibleThemeColors = null;
    public Color currentThemeColor { get; set; }

    IEnumerator PlatformSpawnCoroutine =  null;
    IEnumerator ColorThemeCoroutine = null;
    List<PlatformBase> activePlatforms = new List<PlatformBase>();


    void Start()
    {
        player.onDie += PlayerDie;
        PlatformSpawnCoroutine = PlatformSpawn();
        Instantiate(startingPlatformPrefab, transform.position, Quaternion.identity, transform);
        StartCoroutine(PlatformSpawnCoroutine);
        ColorThemeCoroutine = UpdateColorTheme();
        StartCoroutine(ColorThemeCoroutine);
    }
    void Update()
    {

    }
    void PlayerDie()
    {
        ResetGame();
    }

    IEnumerator UpdateColorTheme() 
    {
        int currentColorIndex = 0;
        int nextColorIndex = 1;
        float t = 0;
        float cameraStartingPosX = cameraTransform.position.x;
        float cameraFinalPosX = cameraStartingPosX + posibleThemeColors[currentColorIndex].distanceToChange;
        while (true) 
        {
            while (cameraTransform.position.x < cameraFinalPosX)
            {
                t = (cameraTransform.position.x - cameraStartingPosX) / (cameraFinalPosX - cameraStartingPosX);
                currentThemeColor = Color.Lerp(posibleThemeColors[currentColorIndex].color, posibleThemeColors[nextColorIndex].color, t);
                yield return null;
            }
            currentColorIndex = nextColorIndex;
            nextColorIndex = nextColorIndex + 1 == posibleThemeColors.Count ? 0 : currentColorIndex + 1;
            cameraStartingPosX = cameraTransform.position.x;
            cameraFinalPosX = cameraStartingPosX + posibleThemeColors[currentColorIndex].distanceToChange;
            t = 0;
        }
    }
    IEnumerator PlatformSpawn() 
    {
        GameObject go;
        PlatformBase platform;
        Vector3 cameraStartingPos = cameraTransform.position;
        float randomSpawnDistance = UnityEngine.Random.Range(minDistanceToSpawn, maxDistanceToSpawn);
        float randomSpawnHeight = UnityEngine.Random.Range(minHeightToSpawn, maxHeightToSpawn);
        while (true) 
        {
            if (Vector3.Distance(cameraStartingPos, cameraTransform.position) > randomSpawnDistance)
            {
                var randomIndex = UnityEngine.Random.Range(0, randomPlatformsPrefabs.Count);
                Vector3 spawnPos = new Vector3(cameraTransform.position.x + spawnDistanceXFromCamera, cameraTransform.position.y + randomSpawnHeight, 0);
                CreateNewPlatform(out go, out platform, randomIndex, spawnPos);
                cameraStartingPos = cameraTransform.position;
                randomSpawnDistance = UnityEngine.Random.Range(minDistanceToSpawn, maxDistanceToSpawn);
                randomSpawnHeight = UnityEngine.Random.Range(minHeightToSpawn, maxHeightToSpawn);
            }
            yield return null;
        }
    }

    private void CreateNewPlatform(out GameObject go, out PlatformBase platform, int randomIndex, Vector3 spawnPos)
    {
        go = Instantiate(randomPlatformsPrefabs[randomIndex], spawnPos, Quaternion.identity, transform);
        platform = go.GetComponent<PlatformBase>();
        platform.OnDestroy += OnPlatformDestroy;
        activePlatforms.Add(platform);
    }

    private void DestroyAllActivePlatforms()
    {
        foreach (var platform in activePlatforms)
        {
            Destroy(platform.gameObject);
        }
        activePlatforms.Clear();
    }

    void OnPlatformDestroy(PlatformBase platform) 
    {
        activePlatforms.Remove(platform);
    }

    private void ResetGame()
    {
        DestroyAllActivePlatforms();
        StopCoroutine(PlatformSpawnCoroutine);
        PlatformSpawnCoroutine = PlatformSpawn();
        StartCoroutine(PlatformSpawnCoroutine);

        StopCoroutine(ColorThemeCoroutine);
        ColorThemeCoroutine = UpdateColorTheme();
        StartCoroutine(ColorThemeCoroutine);
    }

}