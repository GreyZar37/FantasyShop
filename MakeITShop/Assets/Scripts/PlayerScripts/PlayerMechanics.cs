using UnityEngine;
using TMPro;

public enum GameState
{
    MainMenu,
    Game,
    GameOver,
    shopMenu,
}

public class PlayerMechanics : MonoBehaviour
{

    public static GameState gameState;

    //Stats
    public int coins;
    public TextMeshProUGUI coinText;

    //storage
    public int potionsAmount;
    public int vegetables;
    public int meat;
    public int jewerly;

    // UI elements
    public GameObject UIRefillManager;

    //Test objects
    public audioManager audioManager_;
    public GameObject buyerPreab;

    // Vision and Interaction
    public float interactionDistance = 2f;

    public GameObject pressEText;
    
    public GameObject interactableObject;

    public GameObject coinsPrefab;

    // Farming
    public GameObject rake;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Game;

    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Game)
        {
            vision();
            
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(buyerPreab, transform.position, transform.rotation);
        }
   

        coinText.text =  "Coins: " + coins.ToString();
    }
    public void vision()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform.tag == "Interactable")
            {
                interactableObject = hit.transform.gameObject;
                interactableObject.transform.GetComponent<ShelveController>().animator.SetTrigger("MouseEnter");
                UIRefillManager.GetComponent<RefillOptions>().getShelveStorage(interactableObject.transform.gameObject);
              
                pressEText.SetActive(true);
              
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UIRefillManager.GetComponent<RefillOptions>().getStatsFromPlayer(this);
                    UIRefillManager.GetComponent<RefillOptions>().refillOptionPanel.SetActive(true);
                    gameState = GameState.shopMenu;

                }
            }
           
            if (hit.transform.tag == "item")
            {
                hit.transform.GetComponentInParent<Animator>().SetTrigger("MouseEnter");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    audioManager_.playSellSound();

                    hit.transform.GetComponent<ItemStats>().itemSold(coinsPrefab);
                }
            }
            
            if (hit.transform.tag == "Coins")
            {
                hit.transform.GetComponentInParent<Animator>().SetTrigger("MouseEnter");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    audioManager_.playCollectMoneySound();
                    coins += hit.transform.GetComponentInParent<CoinsAndMoney>().coinsValue;
                    Destroy(hit.transform.parent.gameObject);
                }
            }

        }
        else
        {
            pressEText.SetActive(false);

        }

       

  
    }
}

