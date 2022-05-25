
using UnityEngine;

public class AiWalking : AiBase
{
    public override void EnterState(AiManager ai)
    {
        ai.navMeshAgent.destination = ai.npcStandingPoints[Random.Range(0, ai.npcStandingPoints.Count)].transform.position;
    }

    public override void UpdateState(AiManager ai)
    {
        ai.playWalkingSound();


       
        if (Vector3.Distance(ai.transform.position, ai.navMeshAgent.destination) < 1f)
        {
            ai.switchState(new AiLooking());
        }


    }

    public override void OnCollisionEnterState(AiManager ai, Collider other)
    {

    }
   
    
}
