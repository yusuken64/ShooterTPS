using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Player Player;
    public List<GameObject> EnemyPrefabs;

    public void GenerateEnemies()
    {
        if (Player == null || EnemyPrefabs == null || EnemyPrefabs.Count == 0)
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

            GameObject prefab = EnemyPrefabs[UnityEngine.Random.Range(0, EnemyPrefabs.Count)];

            GameObject enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Optional: face the player
            enemy.transform.LookAt(center);
        }
    }
    float spawnTimer;
	private int aliveEnemies;

	private void Update()
	{
        UpdateSpawning();
	}

	void UpdateSpawning()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer > 0)
            return;
        
        spawnTimer = 5f;
        SpawnEnemy(1);
    }

    void SpawnEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = GetSpawnPosition();

            GameObject prefab = EnemyPrefabs[
                UnityEngine.Random.Range(0, EnemyPrefabs.Count)
            ];

            GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);

            enemy.transform.LookAt(Player.transform.position);

            aliveEnemies++;

            // Hook into death
            enemy.GetComponent<Enemy>().OnDeath += () =>
            {
                aliveEnemies--;
            };
        }
    }

    Vector3 GetSpawnPosition()
    {
        float minDistance = 10f;
        float maxDistance = 25f;

        for (int i = 0; i < 10; i++)
        {
            Vector2 rand = UnityEngine.Random.insideUnitCircle.normalized;
            float dist = UnityEngine.Random.Range(minDistance, maxDistance);

            Vector3 candidate = Player.transform.position +
                                new Vector3(rand.x, 0, rand.y) * dist;

            // Check if visible
            Vector3 dir = (candidate - Player.transform.position).normalized;

            if (!Physics.Raycast(Player.transform.position, dir, dist))
            {
                return candidate;
            }
        }

        return Player.transform.position + UnityEngine.Random.onUnitSphere * 15f;
    }
}
