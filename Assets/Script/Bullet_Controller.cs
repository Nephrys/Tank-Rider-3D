using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{
    public float speed = 50.0f;
    private float inheritedSpeed = 0.0f;

    public GameObject explosionPrefab;
    
    public void setInheritedSpeed(float tankSpeed)
    {
        inheritedSpeed = tankSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float totalSpeed = speed + inheritedSpeed;
        float move = totalSpeed * Time.deltaTime;
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
