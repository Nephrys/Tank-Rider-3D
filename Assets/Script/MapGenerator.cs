using UnityEngine;

public class TileRunnerZ_Simple : MonoBehaviour
{
    [Header("Tile Settings")]
    public GameObject[] tilePrefabs;

    [Header("Spawn Settings")]
    public Transform player;
    public Transform spawnStartPoint;
    public float spawnAheadDistance = 200f;

    private float nextSpawnZ;
    private float lastPlayerZ;
    private bool playerDestroyed = false;

    void Start()
    {
        if (player != null)
        {
            lastPlayerZ = player.position.z;
        }
        else
        {
            lastPlayerZ = spawnStartPoint.position.z;
            Debug.LogWarning("TileRunnerZ_Simple: Player reference is null at start");
        }

        nextSpawnZ = spawnStartPoint.position.z;
        SpawnTile();
    }

    void Update()
    {
        float currentPlayerZ;

        if (player != null)
        {
            // Player exists, use its position
            currentPlayerZ = player.position.z;
            lastPlayerZ = currentPlayerZ;
        }
        else
        {
            if (!playerDestroyed)
            {
                playerDestroyed = true;
            }
            // Player destroyed, use last known position
            currentPlayerZ = lastPlayerZ;
        }

        if (nextSpawnZ - currentPlayerZ < spawnAheadDistance)
        {
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        int rand = Random.Range(0, tilePrefabs.Length);
        GameObject prefab = tilePrefabs[rand];

        GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        Renderer renderer = tile.GetComponentInChildren<Renderer>();
        float tileLengthZ = renderer.bounds.size.z;

        // Positionner la tile pour que SON BAS touche nextSpawnZ
        Vector3 pos = spawnStartPoint.position;
        pos.z = nextSpawnZ + tileLengthZ / 2f;

        tile.transform.position = pos;

        nextSpawnZ += tileLengthZ;
    }
}
