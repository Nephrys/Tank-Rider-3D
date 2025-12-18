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

    void Start()
    {
        

        nextSpawnZ = spawnStartPoint.position.z;
        SpawnTile();
    }



    void Update()
    {
        if (nextSpawnZ - player.position.z < spawnAheadDistance)
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
