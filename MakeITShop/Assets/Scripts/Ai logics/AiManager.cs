using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiManager : MonoBehaviour
{

    public AiUnit aiUnit = new AiUnit();
    AiSpawner aiSpawner;


    [Header("Ai Unit")]
    public Transform itemHoalder;
    public List<GameObject> itemsHolding = new List<GameObject>();


    [Header("Sound and Animation")]

    public audioManager audioManager_;
    public float cooldownTime = 0.5f;
    public float currentTime;

    [Header("Components")]
    public NavMeshAgent navMeshAgent;


    [Header("WorldNavigation")]
   
    public GameObject currentBuyingStation;
    public List<GameObject> npcStandingPoints = new List<GameObject>();
    public List<GameObject> npcWaitingPoints = new List<GameObject>();
    public Transform buingPoint;
    public Transform entrencePoint;
    public Collider buyingArea;


    [Header("States")]

    public bool paying;
    public bool payed;


    AiBase currentState;

    public AiWalking aiWalkingState = new AiWalking();
    public AiBuying aiBuyingState = new AiBuying();
    public AiLeaving aiLeavingState = new AiLeaving();
    public AiLooking aiLookingState = new AiLooking();
    public AiTakeProduct aiTakeProductState = new AiTakeProduct();


    // Start is called before the first frame update
    void Start()
    {
        aiUnit.maxItemsToHold = Random.Range(1, 6);
        aiUnit.maxMoney = Random.Range(10, 100);
        aiUnit.race = aiUnit.races[Random.Range(0, aiUnit.races.Length)];

        print(aiUnit.maxItemsToHold);
        print(aiUnit.maxMoney);
        print(aiUnit.race);


        npcWaitingPoints.AddRange(GameObject.FindGameObjectsWithTag("WaitingPoint"));
        npcWaitingPoints.Add(GameObject.FindGameObjectWithTag("buyingPoint"));


        navMeshAgent = GetComponent<NavMeshAgent>();
        audioManager_ = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();

        buyingArea = GameObject.Find("TradingArea").GetComponent<Collider>();
        npcStandingPoints.AddRange(GameObject.FindGameObjectsWithTag("standingPoint"));
        buingPoint = GameObject.FindGameObjectWithTag("buyingPoint").transform;
        
        
        entrencePoint = GameObject.FindGameObjectWithTag("entrancePoint").transform;
        aiSpawner = GameObject.FindObjectOfType<AiSpawner>();

    




        transform.position = entrencePoint.position;

        currentState = aiWalkingState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "standingPoint")
        {
            currentBuyingStation = other.transform.parent.gameObject;
        }
        currentState.OnCollisionEnterState(this, other);

    }

    public void switchState(AiBase aiState)
    {
        currentState = aiState;
        currentState.EnterState(this);
    }

    public void playWalkingSound()
    {
        if (navMeshAgent.velocity.x != 0 || navMeshAgent.velocity.z != 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                audioManager_.playStepsSoundStone();
                currentTime = cooldownTime;
            }
        }
    }

    public IEnumerator looking()
    {
        yield return new WaitForSeconds(4f);

        if (currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count <= 0 && npcStandingPoints.Count <= 0)
        {
            switchState(new AiLooking());
        }
        else if (currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count > 0)
        {
            int itemsToBuy = Random.Range(0, ((aiUnit.maxItemsToHold + 1) - aiUnit.itemsHeld));

            for (int i = 0; i < itemsToBuy; i++)
            {

                if (currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count > 0)
                {

                    StartCoroutine(currentBuyingStation.GetComponent<ShelveController>().takeItem(this));
                    aiUnit.itemsHeld++;
                    yield return new WaitForSeconds(1f);
                }

            }

        }

        foreach (Transform standingPoint in currentBuyingStation.transform)
        {
            if (standingPoint.tag == "standingPoint")
            {
                npcStandingPoints.Remove(standingPoint.gameObject);
            }
        }

        yield return new WaitForSeconds(2f);

        if (aiUnit.itemsHeld == 0 && npcStandingPoints.Count <= 0)
        {

            switchState(new AiLeaving());

        }
        else if (aiUnit.itemsHeld == aiUnit.maxItemsToHold || npcStandingPoints.Count <= 0)
        {
            switchState(new AiBuying());

        }
        else
        {
            switchState(new AiWalking());
        }

    }
    public IEnumerator buyingProducts()
    {

        paying = true;


        Vector3 colliderPos = buyingArea.transform.position;

        for (int i = 0; i < aiUnit.itemsHeld; i++)
        {

            float randomXPos = Random.Range(colliderPos.x - buyingArea.bounds.size.x / 2, colliderPos.x + buyingArea.bounds.size.x / 2);
            float randomZPos = Random.Range(colliderPos.z - buyingArea.bounds.size.z / 2, colliderPos.z + buyingArea.bounds.size.z / 2);


            audioManager_.playItemPlacementSound();
            itemsHolding[i].transform.parent = buyingArea.transform.parent;
            itemsHolding[i].transform.position = new Vector3(randomXPos, colliderPos.y, randomZPos);

            yield return new WaitForSeconds(1f);
        }
        payed = true;

    }
    public void destory()
    {
        aiSpawner.costumers.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
