using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum npcState { moving, looking, buying, leaving }
public class NpcController : MonoBehaviour
{
    //state
    npcState npc = npcState.moving;
    bool destinationSet;
    public bool paying;
    public bool payed;


    audioManager audioManager_;
    float cooldownTime = 0.5f;
    float currentTime;

    //Npc components
    public List<GameObject> npcStandingPoints = new List<GameObject>();
    Transform buingPoint;
    Transform entrencePoint;
    
    NavMeshAgent navMeshAgent;
    public GameObject currentBuyingStation;
    
    public Transform itemHolder;
    

    //npc stats
    public int money;
    public int maxMoney;

    public int itemsHeld;
    public int maxItems;

    int itemsRemoved;

    public List<GameObject> itemsHolding = new List<GameObject>();

    // buying
    public Collider buyingArea;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioManager_ = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();

        buyingArea = GameObject.Find("TradingArea").GetComponent<Collider>();
        npcStandingPoints.AddRange(GameObject.FindGameObjectsWithTag("standingPoint"));
        buingPoint = GameObject.FindGameObjectWithTag("buyingPoint").transform;
        entrencePoint = GameObject.FindGameObjectWithTag("entrancePoint").transform;
        transform.position = entrencePoint.position;

        npc = npcState.moving;

    }

    // Update is called once per frame
    void Update()
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
        
        switch (npc)
        {
            case npcState.moving:
                movement();
                break;
            case npcState.buying:
                 buying();
                break;
            case npcState.leaving:
                 leaving();
                break;

        }


    }
    public void movement()
    {
       
        if (destinationSet == false)
        {
            navMeshAgent.destination = npcStandingPoints[Random.Range(0, npcStandingPoints.Count)].transform.position;
            destinationSet = true;
        }

        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1f && destinationSet == true)
        {
            StartCoroutine(looking());
            npc = npcState.looking;

        }


    }


    public void buying()
    {

        if (payed)
        {
            // Improve this later to make it more dynamic
            for (int i = 0; i < itemsHolding.Count; i++)
            {

                if (itemsHolding[i] == null)
                {
                    itemsHolding.Remove(itemsHolding[i]);
                    itemsRemoved++;
                }
            }
        }

        if (destinationSet == false && paying == false)
        {
            navMeshAgent.destination = buingPoint.position;
            destinationSet = true;
        }

        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1f && destinationSet == true)
        {
            
           if( paying == false)
            {
                StartCoroutine(buyingProducts());
            }

        }
        if (itemsHolding.Count == 0)
        {
            npc = npcState.leaving;
        }

    }
    public void leaving()
    {
        if (destinationSet == false)
        {
            navMeshAgent.destination = entrencePoint.position;
            destinationSet = true;
        }

        if (Vector3.Distance(transform.position, navMeshAgent.destination) < 1f && destinationSet == true)
        {
            Destroy(gameObject);

        }

    }


    public IEnumerator looking()
    {
        destinationSet = false;

        yield return new WaitForSeconds(4f);

        if (currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count <= 0 && npcStandingPoints.Count <= 0)
        {
            npc = npcState.moving;
        }
        else if (currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count > 0)
        {
            int itemsToBuy = Random.Range(0, ((maxItems + 1) - itemsHeld));

            for (int i = 0; i < itemsToBuy; i++)
            {

                if(currentBuyingStation.GetComponent<ShelveController>().itemsInShelve.Count > 0)
                {

                   // StartCoroutine(currentBuyingStation.GetComponent<ShelveController>().takeItem(this));
                    itemsHeld++;
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

        if(itemsHeld == 0 && npcStandingPoints.Count <= 0)
        {
            npc = npcState.leaving;
        }
        else if (itemsHeld == maxItems || npcStandingPoints.Count <= 0)
        {
            npc = npcState.buying;
        }
        else
        {
            npc = npcState.moving;
        }

    }

    public IEnumerator buyingProducts()
    {

        paying = true;
        destinationSet = false;

        Vector3 colliderPos = buyingArea.transform.position;

        for (int i = 0; i < itemsHeld; i++)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "standingPoint")
        {

            currentBuyingStation = other.transform.parent.gameObject;


        }
    }

  

}
