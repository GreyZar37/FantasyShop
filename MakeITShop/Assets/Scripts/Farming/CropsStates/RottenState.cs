
using UnityEngine;
public class RottenState : CropBaseState
{
    public override void EnterState(CropManagerState crop)
    {
        crop.isRotten = true;
        crop.isWhole = false;

        foreach (Transform child in crop.transform)
        {
          child.GetComponentInChildren<MeshRenderer>().material = crop.rotMaterial;

        }
       
    }

    public override void UpdateState(CropManagerState crop)
    {

    }

    public override void OnCollisionEnterState(CropManagerState crop, Collider colider)
    {

    }
}
