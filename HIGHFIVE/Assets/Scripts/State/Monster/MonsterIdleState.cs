using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _speedModifier = 0;
        StartAnimation(_animData.IdleParameterHash);
        Debug.Log("Idle Enter");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.IdleParameterHash);
        Debug.Log("Idle Exit");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        bool isPlayerInRange = RangeInPlayer();
        //Debug.Log(isPlayerInRange);
        if (isPlayerInRange == true)
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
        }
    }

    private bool RangeInPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, LayerMask.GetMask("Red") | LayerMask.GetMask("Blue"));
        return playerCollider != null ? true : false;
    }
}
