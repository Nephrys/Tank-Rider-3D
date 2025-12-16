using UnityEngine;
using UnityEngine.InputSystem;
public class Mouvement_Tank : MonoBehaviour
{
    public float speed;
    public InputActionReference moveAction;
    public GameObject bulletPrefab;

    public InputActionReference fireAction;
    //public GameObject gameovercanva;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, 1f);
        transform.position += direction * speed * Time.deltaTime;

        if (fireAction.action.WasPressedThisFrame())
        {
            Instantiate(bulletPrefab, this.transform.position + this.transform.forward, this.transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "enemy")
        {
            Destroy(this.gameObject);
            Destroy(collision.collider.gameObject);
            //gameovercanva.SetActive(true);
        }
    }

    
}
