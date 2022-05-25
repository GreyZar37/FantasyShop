using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelveController : MonoBehaviour
{

    public Transform[] slots;
    public Animator animator;

    public bool filling;

    public GameObject itemParrent;
    public audioManager audioManager_;

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> itemsInShelve = new List<GameObject>();

    PlayerMechanics playerMechanics;

    public bool isFilling;

    void Start()
    {
        playerMechanics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        audioManager_ = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public IEnumerator refillStock(RefillOptions data, int itemID)
    {
        isFilling = true;
        for (int i = 0; i < slots.Length; i++)
        {

            int randomNum = Random.Range(0, items.Count);
            
            if (slots[i].childCount == 0 && data.refillAmount > 0)
            {
                switch (itemID)
                {
                    case 1:
                        playerMechanics.vegetables--;
                        data.refillAmount--;
                        break;

                    case 2:
                        playerMechanics.potionsAmount--;
                        data.refillAmount--;


                        break;
                    case 3:
                        playerMechanics.meat--;
                        data.refillAmount--;
                        break;

                    case 4:
                        playerMechanics.jewerly--;
                        data.refillAmount--;

                        break;
                    default:
                        break;
                }
                itemsInShelve.Add(Instantiate(items[randomNum], slots[i].position, slots[i].rotation, slots[i].transform));
                audioManager_.playItemPlacementSound();
                data.getStatsFromPlayer(playerMechanics);

                yield return new WaitForSeconds(0.5f);

            }

        }

        isFilling = false;

    }

    public IEnumerator takeItem(AiManager data)
    {
        if (itemsInShelve.Count > 0)
        {
            
            int randomItemNum = Random.Range(0, itemsInShelve.Count);
            data.itemsHolding.Add(itemsInShelve[randomItemNum]);

            itemsInShelve[randomItemNum].transform.parent = data.transform;
            itemsInShelve[randomItemNum].transform.position = data.itemHoalder.position;

            itemsInShelve.Remove(itemsInShelve[randomItemNum]);
            
            yield return new WaitForSeconds(0.2f);

        }




    }

}
