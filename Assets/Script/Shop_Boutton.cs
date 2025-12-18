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
    {
        print(price + R + G + B);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
