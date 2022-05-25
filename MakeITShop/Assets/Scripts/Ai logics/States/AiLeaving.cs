using UnityEngine;

public class AiLeaving : AiBase
{
  
    public override void EnterState(AiManager ai)
    {
        ai.navMeshAgent.destination = ai.entrencePoint.position;
    }

    public override void UpdateState(AiManager ai)
    {
 
        if (Vector3.Distance(ai.transform.position, ai.navMeshAgent.destination) < 1f)
        {

            ai.destory();
        }
    }
    
    public override void OnCollisionEnterState(AiManager ai, Collider other)
    {

    }


}
