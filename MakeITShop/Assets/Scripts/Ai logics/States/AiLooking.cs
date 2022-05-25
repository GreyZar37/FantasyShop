
using UnityEngine;

public class AiLooking : AiBase
{
    public override void EnterState(AiManager ai)
    {
        ai.StartCoroutine(ai.looking());
    }

    public override void UpdateState(AiManager ai)
    {
        
    }

    public override void OnCollisionEnterState(AiManager ai, Collider other)
    {

    }

   

}
