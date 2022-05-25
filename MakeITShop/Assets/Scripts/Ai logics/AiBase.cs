using UnityEngine;

public abstract class AiBase 
{
    public abstract void EnterState(AiManager ai);
    public abstract void UpdateState(AiManager ai);
    public abstract void OnCollisionEnterState(AiManager ai, Collider other);

}
