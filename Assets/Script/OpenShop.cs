using UnityEngine;

public class OpenShop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject shopPanel;
    public void open()
    {
        shopPanel.SetActive(true);
    }
    public void close()
    {
        shopPanel.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
