using UnityEngine;

public class AiBuying : AiBase
{
    public override void EnterState(AiManager ai)
    {

        
        
    }

    public override void UpdateState(AiManager ai)
    {

        for (int i = 0; i < ai.npcWaitingPoints.Count; i++)
        {
            if(ai.npcWaitingPoints[i].transform.childCount == 0)
            {
                ai.navMeshAgent.destination = ai.npcWaitingPoints[i].transform.position;
                ai.npcWaitingPoints.Remove(ai.npcWaitingPoints[i]);
                i--;

            }
            else
            {
                break;
            }
        }


        if (ai.payed)
        {
            // Improve this later to make it more dynamic
            for (int i = 0; i < ai.itemsHolding.Count; i++)
            {

                if (ai.itemsHolding[i] == null)
                {
                    ai.itemsHolding.Remove(ai.itemsHolding[i]);
                }
            }
        }

        if (Vector3.Distance(ai.transform.position, ai.buingPoint.transform.position) < 1f)
        {
            
            if (ai.paying == false)
            {
                ai.StartCoroutine(ai.buyingProducts());
            }
        }



        if (Vector3.Distance(ai.transform.position, ai.navMeshAgent.destination) < 1f)
        {

        }


        if (ai.itemsHolding.Count == 0)
        {
            ai.switchState(new AiLeaving());
        }
    }

    public override void OnCollisionEnterState(AiManager ai, Collider other)
    {
        
    }

}
