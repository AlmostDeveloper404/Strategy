using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectableObject
{
    public NavMeshAgent NavMeshAgent;
    public int Price;

    public override void ClickOnGround(Vector3 point)
    {
        base.ClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }
}
