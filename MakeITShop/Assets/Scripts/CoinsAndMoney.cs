using UnityEngine;

public class CoinsAndMoney : MonoBehaviour
{
    public int coinsValue;
    
    public GameObject lowValueCoins;
    public GameObject mediumValueCoins;
    public GameObject highValueCoins;

    public void setValue(int sellPrise)
    {
        coinsValue = sellPrise;

    }

    private void Update()
    {
        if (coinsValue > 0 && coinsValue <= 20)
        {
            lowValueCoins.SetActive(true);
            mediumValueCoins.SetActive(false);
            highValueCoins.SetActive(false);
        }
        else if (coinsValue > 20 && coinsValue <= 100)
        {
            lowValueCoins.SetActive(false);
            mediumValueCoins.SetActive(true);
            highValueCoins.SetActive(false);
        }
        else if (coinsValue > 100)
        {
            lowValueCoins.SetActive(false);
            mediumValueCoins.SetActive(false);
            highValueCoins.SetActive(true);
        }
      
    }



}
