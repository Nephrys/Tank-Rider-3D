//using UnityEngine;

//public class Value_Tile : MonoBehaviour
//{
//    public float size;
//    private Transform player;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        player = GameObject.FindGameObjectWithTag("tank")?.transform;

//        if (player == null)
//        {
//            Debug.LogError("Player introuvable (tag 'tank')");
//            enabled = false;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (transform.position.z < player.position.z - 100f)
//        {
//            Destroy(gameObject);
//        }
//    }
//}
