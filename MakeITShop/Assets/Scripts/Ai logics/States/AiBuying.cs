using UnityEngine;

public class AiBuying : AiBase
{
    public override void EnterState(AiManager ai)
    {
       

    }

    public override void UpdateState(AiManager ai)
    {
      
        
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

        if (Vector3.Distance(ai.transform.position, ai.navMeshAgent.destination) < 1f)
        {
            /*
            if (ai.paying == false)
            {
                ai.StartCoroutine(ai.buyingProducts());
            }*/
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
