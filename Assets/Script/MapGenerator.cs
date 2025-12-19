using UnityEngine;

public class TileRunnerZ_Simple : MonoBehaviour
{
    [Header("Tile Settings")]
    public GameObject[] tilePrefabs;

    [Header("Spawn Settings")]
    public Transform spawnStartPoint;
    public float spawnAheadDistance = 200f;
    public string tankTag = "tank";

    private float nextSpawnZ;
    private float lastKnownZ;

    void Start()
    {
        lastKnownZ = spawnStartPoint.position.z;
        nextSpawnZ = spawnStartPoint.position.z;

        SpawnTile();
    }

    void Update()
    {
        float currentPlayerZ = GetMaxTankZ();

        if (nextSpawnZ - currentPlayerZ < spawnAheadDistance)
        {
            SpawnTile();
        }
    }

    float GetMaxTankZ()
    {
        GameObject[] tanks = GameObject.FindGameObjectsWithTag(tankTag);

        float maxZ = lastKnownZ;
        bool tankFound = false;

        foreach (GameObject tank in tanks)
        {
            if (tank != null && tank.activeInHierarchy)
            {
                maxZ = Mathf.Max(maxZ, tank.transform.position.z);
                tankFound = true;
            }
        }

        if (tankFound)
        {
            lastKnownZ = maxZ;
        }

        return lastKnownZ;
    }

    void SpawnTile()
    {
        int rand = Random.Range(0, tilePrefabs.Length);
        GameObject prefab = tilePrefabs[rand];

        GameObject tile = Instantiate(prefab);

        Renderer renderer = tile.GetComponentInChildren<Renderer>();
        float tileLengthZ = renderer.bounds.size.z;

        Vector3 pos = spawnStartPoint.position;
        pos.z = nextSpawnZ + tileLengthZ / 2f;
        tile.transform.position = pos;

        nextSpawnZ += tileLengthZ;
    }
}
