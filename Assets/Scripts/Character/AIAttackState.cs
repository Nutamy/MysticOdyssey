using UnityEngine;

public class AIAttackState : AIBaseState
{
    public override void EnterState(EnemyController enemy)
    {
        
        enemy.movementCmp.StopMovingAgent();
    }
    public override void UpdateState(EnemyController enemy)
    {
        if (enemy.player == null)
        {
            Debug.Log("enemy.combatCmp.CancelAttack();");
            enemy.combatCmp.CancelAttack();
            return;
            //enemy.SwitchState(enemy.retreatState);
        }

        if (enemy.distanceFromPlayer > enemy.attackRange)
        {
            enemy.combatCmp.CancelAttack();
            enemy.SwitchState(enemy.chaseState);
            return;
        }

        if (enemy.hasUIOpened) return;

        enemy.combatCmp.StartAttack();
        enemy.transform.LookAt(enemy.player.transform);
        //Debug.Log("Attack player");
    }
}
