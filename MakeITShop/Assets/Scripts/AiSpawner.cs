using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawner : MonoBehaviour
{

    public GameObject aIPrefab;
    public Transform spawnPoint;
    
    public float spawnRate;

    public int maxCostumersAmount;
    public List<GameObject> costumers = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            if (costumers.Count < maxCostumersAmount)
            {
                GameObject ai = Instantiate(aIPrefab, spawnPoint.position, spawnPoint.rotation);
                costumers.Add(ai);
                spawnRate = Random.Range(8f, 20f);
                print(spawnRate);
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
