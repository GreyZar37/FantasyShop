using UnityEngine;

public class GrowingState : CropBaseState
{
    public override void EnterState(CropManagerState crop)
    {
    }

    public override void UpdateState(CropManagerState crop)
    {
        crop.transform.localScale += new Vector3(0.004f, 0.004f, 0.004f) * Time.deltaTime;
        if(crop.transform.localScale.x >= 1.0f)
        {
            crop.switchState(new WholeState());
            
        }
    }

    public override void OnCollisionEnterState(CropManagerState crop, Collider colider)
    {

    }
}
