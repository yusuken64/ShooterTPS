using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public List<GameObject> TerrainChunk;
    public GameObject InteractablePrefab;

    public int Rows;
    public int Columns;

    public float ChunkSize = 10f; // size of each chunk in world units

    public Transform Container;
    public NavMeshSurface Surface;

    [ContextMenu("Create Terrain")]
    public void CreateTerrain()
    {
        ClearTerrain();
        GenerateTerrain();
    }

	private void ClearTerrain()
	{
        foreach(Transform child in Container)
		{
            DestroyImmediate(child.gameObject);
		}
	}

	private void GenerateTerrain()
    {
        if (TerrainChunk == null || TerrainChunk.Count == 0)
        {
            Debug.LogWarning("No terrain chunks assigned.");
            return;
        }

        float totalWidth = Columns * ChunkSize;
        float totalHeight = Rows * ChunkSize;

        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                // pick a random chunk prefab
                GameObject prefab = TerrainChunk[Random.Range(0, TerrainChunk.Count)];

                if( x == Columns/2 &&
                    y == Rows/2)
				{
                    prefab = InteractablePrefab;
				}

                Vector3 position = new Vector3(
                    x * ChunkSize - totalWidth / 2f + ChunkSize / 2f,
                    0f,
                    y * ChunkSize - totalHeight / 2f + ChunkSize / 2f
                );

                int rotationIndex = Random.Range(0, 4); // 0,1,2,3
                Quaternion rotation = Quaternion.Euler(0f, rotationIndex * 90f, 0f);
                GameObject chunk = Instantiate(prefab, position, rotation, Container);

                chunk.name = $"Chunk_{x}_{y}";
            }
        }

        if (Surface != null)
        {
            Surface.BuildNavMesh();
        }
    }
}