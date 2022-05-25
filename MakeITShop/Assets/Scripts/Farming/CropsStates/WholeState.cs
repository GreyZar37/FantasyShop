
using UnityEngine;

public class WholeState : CropBaseState
{
    float timeUntilRot = 1200;

    public override void EnterState(CropManagerState crop)
    {
       crop.isWhole = true;

    }

    public override void UpdateState(CropManagerState crop)
    {
        
        timeUntilRot -= Time.deltaTime;
        
        if (timeUntilRot <= 0)
        {
            crop.switchState(new RottenState());
        }
    }

    public override void OnCollisionEnterState(CropManagerState crop, Collider colider)
    {
        Debug.Log("Collision");
    }
    
}
