using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public List<GameObject> TerrainChunk;
    public GameObject EmptyPlanePrefab;
    public GameObject WallPrefab;

    public int Rows;
    public int Columns;

    public float ChunkSize = 10f; // size of each chunk in world units

    public Transform Container;
    public NavMeshSurface Surface;
    private List<Vector2Int> reservedSpots = new();

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

        reservedSpots = new List<Vector2Int>
        {
            new(1, 1),
            new(Columns - 1 - 1, 1),
            new(1, Rows - 1 - 1),
            new(Columns - 1 - 1, Rows - 1 - 1),
            new(Columns / 2, Rows / 2),
        };

        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
			{
				Vector2Int coord = new(x, y);

				GameObject prefab;

				if (reservedSpots.Contains(coord))
				{
					prefab = EmptyPlanePrefab;
				}
                else if (x == 0 || y == 0 ||
                    x == Columns - 1 || y == Rows - 1)
				{
                    prefab = WallPrefab;
				}
				else
				{
					prefab = TerrainChunk[Random.Range(0, TerrainChunk.Count)];
				}

				SpawnPrefab(x, y, prefab);
			}
		}

        Surface?.BuildNavMesh();
    }

	private void SpawnPrefab(int x, int y, GameObject prefab)
	{
		float totalWidth = Columns * ChunkSize;
		float totalHeight = Rows * ChunkSize;

		Vector3 position = new Vector3(
			x * ChunkSize - totalWidth / 2f + ChunkSize / 2f,
			0f,
			y * ChunkSize - totalHeight / 2f + ChunkSize / 2f
		);

		int rotationIndex = Random.Range(0, 4);
		Quaternion rotation = Quaternion.Euler(0f, rotationIndex * 90f, 0f);

		GameObject chunk = Instantiate(prefab, position, rotation, Container);
		chunk.name = $"Chunk_{x}_{y}";
	}

    public GameObject SpawnObjective(GameObject objectivePrefab)
    {
        if (reservedSpots == null || reservedSpots.Count == 0)
        {
            Debug.LogWarning("No reserved spots left!");
            return null;
        }

        int index = Random.Range(0, reservedSpots.Count);
        Vector2Int chosenSpot = reservedSpots[index];

        reservedSpots.RemoveAt(index); // consume it

        Vector3 position = GridToWorld(chosenSpot);

        return Instantiate(objectivePrefab, position, Quaternion.identity);
    }

    private Vector3 GridToWorld(Vector2Int coord)
    {
        float totalWidth = Columns * ChunkSize;
        float totalHeight = Rows * ChunkSize;

        return new Vector3(
            coord.x * ChunkSize - totalWidth / 2f + ChunkSize / 2f,
            0f,
            coord.y * ChunkSize - totalHeight / 2f + ChunkSize / 2f
        );
    }
}