using UnityEngine;

public class Mouvement_Camera : MonoBehaviour
{
    GameObject tank;
    float lastKnownZ;
    bool tankDestroyed = false;

    void Start()
    {
        tank = GameObject.FindWithTag("tank");
        if (tank == null)
        {
            Debug.LogError("Tank object not found!");
        }
        else
        {
            lastKnownZ = tank.transform.position.z;
        }
    }

    void LateUpdate()
    {
        if (tank != null)
        {
            lastKnownZ = tank.transform.position.z;
            Vector3 newPosition = transform.position;
            newPosition.z = lastKnownZ + 18;
            transform.position = newPosition;
        }
        else if (!tankDestroyed)
        {
            tankDestroyed = true;
        }
    }
}
