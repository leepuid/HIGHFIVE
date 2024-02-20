using System.Collections;
using UnityEngine;

public class MonsterAttackState : MonsterBaseState
{
    private float _distance;
    private double _curDelay;
    public MonsterAttackState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_animData.AttackParameterHash);
        _monsterStateMachine._monster.Animator.SetFloat("AttackSpeed", _monsterStateMachine._monster.stat.AttackSpeed);
        Debug.Log("Attack Enter");
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_animData.AttackParameterHash);
        Debug.Log("Attack Exit");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (AttackRangeCheck())
        {
            _curDelay -= Time.deltaTime;
            if (_curDelay <= 0)
            {
                _monsterStateMachine._monster.OnNormalAttack();
                _curDelay = 1.0 / _monsterStateMachine._monster.stat.AttackSpeed;
            }
            
        }
    }

    private bool AttackRangeCheck()
    {
        if (_monsterStateMachine._monster.targetObject != null && _monsterStateMachine._monster.targetObject.layer != (int)Define.Layer.Default)
        {
            _distance = (_monsterStateMachine._monster.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;
            if (_distance > _monsterStateMachine._monster.stat.AttackRange)
            {
                _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterMoveState);
            return false;
        }
    }
}
