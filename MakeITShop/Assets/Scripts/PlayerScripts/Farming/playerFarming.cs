using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFarming : MonoBehaviour
{
    public GameObject[] crops;
    public GameObject[] cropsBluePrint;


    PlayerMechanics playerMechanics;

    RaycastHit hit;

    private void Start()
    {
        playerMechanics = GetComponentInParent<PlayerMechanics>();
    }

    private void Update()
    {

        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.transform.tag == "FarmPlot")
                {
                    if (playerMechanics.vegetables >= 1)
                    {
                        if (hit.transform.childCount == 1)
                        {
                            GameObject newCrop = Instantiate(crops[Random.Range(0, crops.Length)], hit.point, Quaternion.identity);
                            newCrop.transform.parent = hit.transform;
                            playerMechanics.vegetables--;
                        }
                        
                    }
                   
                }
                else if (hit.transform.tag == "Crop")
                {
                    hit.transform.GetComponent<CropManagerState>().collectVegetable();
               
                    
                }
            }
        }
    }

}
