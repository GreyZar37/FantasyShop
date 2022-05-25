using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySystem : MonoBehaviour
{

    public List<GameObject> equipmentHolding = new List<GameObject>();
    public List<GameObject> equipment = new List<GameObject>();


    public List<string> itemName = new List<string>();


    public List<RawImage> slotsSelectOutline = new List<RawImage>();
    public List<RawImage> slotsImage = new List<RawImage>();

    bool alreadyEquipt;

    public void AddItem(Texture itemImage, string itemName, string itemDescription)
    {
        for (int i = 0; i < slotsSelectOutline.Count; i++)
        {
            slotsSelectOutline[i].enabled = false;
        }
        foreach (RawImage slot in slotsImage)
        {

            if (slot.texture == null)
            {
                this.itemName.Add(itemName);
                slot.enabled = true;
                slot.texture = itemImage;
                slot.color = Color.white;
                addItem(itemName);
                break;
            }
        }

    }

    private void Update()
    {
        equipController();
    }

    public void equipController()
    {
        for (int i = 0; i < 7; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {

                if(i - 1 < equipmentHolding.Count)
                {
                    if (equipmentHolding[i - 1].activeSelf == false)
                    {
                        alreadyEquipt = false;
                    }
                    else
                    {
                        alreadyEquipt = true;
                    }
                }
               

                for (int x = 0; x < equipmentHolding.Count; x++)
                {
                    equipmentHolding[x].SetActive(false);
                }

                
                if (i - 1 < equipmentHolding.Count)
                {
                    if (alreadyEquipt == false)
                    {
                        equipmentHolding[i - 1].SetActive(true);
                    }
                    else
                    {
                        equipmentHolding[i - 1].SetActive(false);

                    }


                }
            }
        }


    }
    
    public void addItem(string itemName)
    {
        switch (itemName)
        {
            case "Rake":
                equipmentHolding.Add(equipment[0]);
                break;
            case "Axe":
                equipmentHolding.Add(equipment[1]);
                break;

        }
    }
}
