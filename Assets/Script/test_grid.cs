using UnityEngine;

public class test_grid : MonoBehaviour
{
    const int width = 20;
    const int height = 7;
    const int shift = 4;

    public float difficulty = 1f;
    public float time = 3f;
    int[] pathY = new int[width];

    int[,] grid = new int[width, height];

    int currentPathY; 
    bool initialized = false;

    void Start()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                grid[x, y] = 1;

        currentPathY = Random.Range(0, height);
        initialized = true;
        int startY = Random.Range(0, height);

        for (int x = 0; x < width; x++)
        {
            pathY[x] = startY;
        }
    }

    void Update()
    {
        if (time < 0)
        {
            // SHIFT À GAUCHE
            for (int x = 0; x < width - shift; x++)
{
                for (int y = 0; y < height; y++)
                {
                    grid[x, y] = grid[x + shift, y];
                }

                pathY[x] = pathY[x + shift];
            }

            float oneProbability = Mathf.Lerp(0.9f, 0.3f, difficulty);

            for (int x = width - shift; x < width; x++)
            {
                int delta = Random.Range(-1, 2);
                pathY[x] = Mathf.Clamp(pathY[x - 1] + delta, 0, height - 1);

                for (int y = 0; y < height; y++)
                {
                    if (y == pathY[x])
                        grid[x, y] = 1;
                    else
                        grid[x, y] = Random.value < oneProbability ? 1 : 0;
                }
            }

            // PRINT GRID
            string output = "";
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    output += grid[x, y] + " ";
                }
                output += "\n";
            }

            Debug.Log(output);

            time = 3f;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}


