    using UnityEngine;

public class Shop_Boutton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int type;
    public int tank;
    public int price;
    public int R;
    public int G;
    public int B;

    public void buy()
    {   if(price<=PlayerPrefs.GetInt("coins")){
            if (type == 0 && PlayerPrefs.GetInt("tank")!=tank)
            {
            PlayerPrefs.SetInt("tank",tank);
            PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins")-price);
            }
            else
            {
                if (type == 1)
                 if (PlayerPrefs.GetInt("R") != R || PlayerPrefs.GetInt("G") != G ||PlayerPrefs.GetInt("R") != B)
                    {

                        PlayerPrefs.SetInt("R",R);
                        PlayerPrefs.SetInt("G",G);
                        PlayerPrefs.SetInt("B",B);
                        PlayerPrefs.SetInt("coins",PlayerPrefs.GetInt("coins")-price);
                
                    }
                
            }

            print("R" + PlayerPrefs.GetInt("R"));
            print("G" + PlayerPrefs.GetInt("G"));
            print("B" + PlayerPrefs.GetInt("B"));
            print(PlayerPrefs.GetInt("tank"));
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
