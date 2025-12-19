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
    void Awake() {
        
    
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0); 
        }

        if (!PlayerPrefs.HasKey("R"))
        {
            PlayerPrefs.SetInt("R", 255); 
        }


        // Sauvegarde immédiatement les valeurs
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
