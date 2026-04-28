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

    public ObjectiveManager ObjectiveManager;
    public EnemySpawner EnemySpawner;

    void Start()
    {
        if (GenerateTerrainOnStart)
        {
            TerrainGenerator.CreateTerrain();
        }

        ObjectiveManager.SpawnObjective();

        if (SpawnEnemiesOnStart)
        {
            EnemySpawner.GenerateEnemies();
        }
    }
}
