using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{

    // a redefinir, calculer size plane/size tank
    public int col;
    public int row;
    public int[,] Tile_binary;
    public GameObject obstacle; 
    public int cellSize;
    public float origin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tile_binary = new int[col, row];
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                //Tile_binary[x,y] = Random.Range(0,2);
                Tile_binary[x,y] = 0;
            }
        }
        
        // 2) Spawn a cube for each cell == 1
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                if (Tile_binary[x, y] == 0)
                {
                    // Compute world position on XZ-plane
                    Vector3 pos = new Vector3(x * cellSize, 0f, y * cellSize);

                    // Place and parent it
                    GameObject newObs = Instantiate(obstacle,pos, Quaternion.identity );
                    newObs.transform.SetParent(transform, worldPositionStays: true);
                }
            }
        }
        origin += 50;
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
