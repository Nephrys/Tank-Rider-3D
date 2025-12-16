using UnityEngine;

public class Mouvement_Camera : MonoBehaviour
{
    GameObject tank;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tank = GameObject.FindWithTag("tank");
        if (tank == null)
        {
            Debug.LogError("Tank object not found!");
        }
    }

    void LateUpdate()
    {
        print("Late Update Camera");
        print(tank);
        if (tank != null)
        {
            //Follow only its position on the z axis
            Vector3 newPosition = transform.position;
            newPosition.z = tank.transform.position.z + 18;
            transform.position = newPosition;
        }
    }
}
