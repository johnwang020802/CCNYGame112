using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    
    [Tooltip("Drag your specific Terrain object here from the Hierarchy.")]
    public Terrain targetTerrain; 
    
    public int density = 200; 
    public float respawnTimer = 20f;
    public float heightOffset = 1f; 

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine ()
    {
        while (true)
        {
            ClearEnemies();
            SpawnEnemies();
            yield return new WaitForSeconds(respawnTimer);
        }
    }

    void SpawnEnemies()
    {
        // Use the manually assigned terrain, or fallback to the active one if the slot is empty
        Terrain currentTerrain = targetTerrain != null ? targetTerrain : Terrain.activeTerrain;

        if (currentTerrain == null)
        {
            Debug.LogWarning("No terrain found! Please assign your Terrain object in the Inspector.");
            return;
        }

        TerrainData tData = currentTerrain.terrainData;
        Vector3 tPos = currentTerrain.transform.position;

        for (int i = 0; i < density; i++)
        {
            float randomX = Random.Range(0f, tData.size.x);
            float randomZ = Random.Range(0f, tData.size.z);

            float worldX = tPos.x + randomX;
            float worldZ = tPos.z + randomZ;

            // Sample the exact height of the assigned terrain
            float terrainHeight = currentTerrain.SampleHeight(new Vector3(worldX, 0, worldZ)) + tPos.y;

            Vector3 spawnPosition = new Vector3(worldX, terrainHeight + heightOffset, worldZ);

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            activeEnemies.Add(newEnemy);
        }
    }

    void ClearEnemies()
    {
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null) 
            {
                Destroy(enemy);
            }
        }
        activeEnemies.Clear();
    }
}