using UnityEngine;
using System.Collections;

public class MapSpawner : MonoBehaviour
{
    public GameObject map_segment;
    public int zPos = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpanwerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpanwerRoutine()
    {
        while (true) //if on the plane just before, regens a new one
        {
            Instantiate(map_segment, new Vector3(0,0, zPos), Quaternion.identity);
            zPos += 50;
            yield return new WaitForSeconds(5);
        }
    }
}
