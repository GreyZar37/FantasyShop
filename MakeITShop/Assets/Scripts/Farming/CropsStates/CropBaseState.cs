using UnityEngine;

public abstract class CropBaseState
{
    public abstract void EnterState(CropManagerState crop);
    public abstract void UpdateState(CropManagerState crop);
    public abstract void OnCollisionEnterState(CropManagerState crop, Collider colider);


}
