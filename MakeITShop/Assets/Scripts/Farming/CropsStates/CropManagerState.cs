using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManagerState : MonoBehaviour
{
    PlayerMechanics playerMechanics;

    public Material rotMaterial;

    int vegetableMultiplier = 2;
 
    public bool isWhole;
    public bool isRotten;


    CropBaseState currentState;

    public RottenState rottenState = new RottenState();
    public GrowingState growingState = new GrowingState();
    public WholeState wholeState = new WholeState();

    // Start is called before the first frame update
    void Start()
    {
        playerMechanics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();

        currentState = growingState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void switchState(CropBaseState cropState)
    {
        currentState = cropState;
        currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnCollisionEnterState(this, other);
    }

   
    public void collectVegetable()
    {
        if (isRotten)
        {
            Destroy(gameObject);
        }
        else if (isWhole)
        {
            Destroy(gameObject);
            playerMechanics.vegetables += (1 * vegetableMultiplier);

        }

    }
}
