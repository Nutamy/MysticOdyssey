using UnityEngine;

public class AIDefeatedState : AIBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        Debug.Log("Defeat State Entering");
    }

    public override void UpdateState(EnemyController enemy)
    {

    }
}
