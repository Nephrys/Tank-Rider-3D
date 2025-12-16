using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    public float speed = 50.0f;
    public float acceleration = 0.6f;

    public GameObject explosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += acceleration * Time.deltaTime;
        float move = speed * Time.deltaTime;
        this.transform.Translate(0, 0, move);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            Destroy(collision.collider.gameObject);
            Destroy(this.gameObject);
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
