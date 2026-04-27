using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Player Player;
    public bool GenerateTerrainOnStart;
    public bool SpawnEnemiesOnStart;
    public TerrainGenerator TerrainGenerator;

    public List<GameObject> EnemyPrefab;

    public ObjectiveManager ObjectiveManager;

    void Start()
    {
        if (GenerateTerrainOnStart)
        {
            TerrainGenerator.CreateTerrain();
        }

        var objectives = FindObjectsByType<ObjectiveInteractable>(FindObjectsSortMode.None).ToList();
        ObjectiveManager.SetObjectives(objectives);

        if (SpawnEnemiesOnStart)
        {
            GenerateEnemies();
        }
    }

    private void GenerateEnemies()
    {
        if (Player == null || EnemyPrefab == null || EnemyPrefab.Count == 0)
        {
            Debug.LogWarning("Missing Player or Enemy Prefabs.");
            return;
        }

        int enemyCount = 30;     // how many enemies in the ring
        float radius = 15f;     // distance from player

        Vector3 center = Player.transform.position;

        for (int i = 0; i < enemyCount; i++)
        {
            float angle = (i / (float)enemyCount) * Mathf.PI * 2f;

            Vector3 offset = new Vector3(
                Mathf.Cos(angle),
                0f,
                Mathf.Sin(angle)
            ) * radius;

            Vector3 spawnPosition = center + offset;

            GameObject prefab = EnemyPrefab[UnityEngine.Random.Range(0, EnemyPrefab.Count)];

            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Optional: face the player
            enemy.transform.LookAt(center);
        }
    }
}
