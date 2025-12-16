using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    public float speed = 50.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = speed * Time.deltaTime;
        this.transform.Translate(0, 0, move);
    }
}
