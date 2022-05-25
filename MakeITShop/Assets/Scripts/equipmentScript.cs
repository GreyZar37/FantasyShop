using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class equipmentScript : MonoBehaviour
{

    public Texture itemImage;
    public string itemName;
    public string itemDescription;


    GameObject player;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            anim.SetTrigger("MouseEnter");
        }
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            Destroy(transform.parent.gameObject);
            player.GetComponent<InventorySystem>().AddItem(itemImage, itemName, itemDescription);

        }
    }
}
