using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RefillOptions : MonoBehaviour
{
    public int refillAmount;
    public GameObject refillOptionPanel;

    [Header("Refill Options for shelve")]
    public int playerPotionsAmount;
    public int playerVegetables;
    public int playerMeat;

    public int playerJewerly;

    public List<GameObject> potions = new List<GameObject>();
    public List<GameObject> vegetables = new List<GameObject>();
    public List<GameObject> meat = new List<GameObject>();

    public List<GameObject> jewerly = new List<GameObject>();

    ShelveController shelveScript;

    [Header("Refill Options for weaponStand")]


    public List<TextMeshProUGUI> itemAmountTxt = new List<TextMeshProUGUI>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        itemAmountTxt[0].text = "Refill " + "(" + playerVegetables.ToString() + ") left";
        itemAmountTxt[1].text = "Refill " + "(" + playerMeat.ToString() + ") left";

        itemAmountTxt[2].text = "Refill " + "(" + playerPotionsAmount.ToString() + ") left";
        itemAmountTxt[3].text = "Refill " + "(" + playerJewerly.ToString() + ") left";        
        
        if (refillOptionPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                refillOptionPanel.SetActive(false);
                PlayerMechanics.gameState = GameState.Game;
            }
        }
    }

    public void getStatsFromPlayer(PlayerMechanics data)
    {
        
        playerPotionsAmount = data.potionsAmount;
        playerVegetables = data.vegetables;
        playerMeat = data.meat;
        playerJewerly = data.jewerly;
    }
   

    public void getShelveStorage(GameObject shelveStorage)
    {
       shelveScript = shelveStorage.GetComponent<ShelveController>();

    }
    public void refill(int itemID)
    {
     
          
        if(shelveScript.isFilling == false)
        {
            switch (itemID)
            {
                case 1:
                    refillAmount = playerVegetables;
                    shelveScript.items = vegetables;

                    break;

                case 2:
                    refillAmount = playerPotionsAmount;
                    shelveScript.items = potions;

                    break;

                case 3:
                    refillAmount = playerMeat;
                    shelveScript.items = meat;
                        break;
                    
                case 4:
                    refillAmount = playerJewerly;
                    shelveScript.items = jewerly;

                    break;

                default:
                    break;
            }
            StartCoroutine(shelveScript.refillStock(this, itemID));
        }

    }

 
}
