using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats: MonoBehaviour
{
    public int buyPrise;
    public int sellPrise;


    public string name_;
    public string tag_;

    public GameObject coinsSpawned;
    public void itemSold(GameObject coldToSpawn) 
    
    {
        coinsSpawned = Instantiate(coldToSpawn, transform.position, Quaternion.identity);
        coinsSpawned.GetComponent<CoinsAndMoney>().setValue(sellPrise);
        Destroy(transform.parent.gameObject);
    }

}
