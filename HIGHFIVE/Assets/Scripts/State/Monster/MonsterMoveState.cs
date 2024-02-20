using UnityEngine;

public class MonsterMoveState : MonsterBaseState
{
    private bool isReturnToSpawn = false;

    public MonsterMoveState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        _speedModifier = 1;
        Debug.Log("Move Enter");
    }

    public override void Exit()
    {
        base.Exit();
        _monsterStateMachine._monster.StopAnimationAll();
        Debug.Log("Move Exit");
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        DistanceCheck();
    }

    private void DistanceCheck()
    {
        _monsterStateMachine._monster.targetObject = RangeInPlayer();
        
        if (_monsterStateMachine._monster.targetObject != null)
        {
        float distance = (_monsterStateMachine._monster.targetObject.transform.position - _monsterStateMachine._monster.transform.position).magnitude;
            if (distance <= _monsterStateMachine._monster.stat.AttackRange)
            {
                _monsterStateMachine.ChangeState(_monsterStateMachine._monsterAttackState);
            }
            else
            {
                float spawnToDistance = (_monsterStateMachine._monster._spawnPoint - (Vector2)_monsterStateMachine._monster.transform.position).magnitude;
                if (spawnToDistance >= 8f || isReturnToSpawn == true) ReturnToSpawnZone();
                else
                {
                    if(isReturnToSpawn == false) MoveMonsterToTarget();
                }
            }
        }
        else
        {
            ReturnToSpawnZone();
        }
    }

    private GameObject RangeInPlayer()
    {
        LayerMask[] layersToCheck = new LayerMask[] { LayerMask.GetMask("Blue"), LayerMask.GetMask("Red") };

        GameObject closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (LayerMask layerToCheck in layersToCheck)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(_monsterStateMachine._monster.transform.position, _monsterStateMachine._monster.stat.SightRange, layerToCheck);

            if (playerCollider != null)
            {
                float distanceToPlayer = Vector2.Distance(_monsterStateMachine._monster.transform.position, playerCollider.transform.position);

                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = playerCollider.gameObject;
                }
            }
        }

        return closestPlayer;
    }

    private void MoveMonsterToTarget()
    {
        Vector2 direction = (Vector2)_monsterStateMachine._monster.targetObject.transform.position - (Vector2)_monsterStateMachine._monster.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _monsterStateMachine._monster.transform.position = Vector2.MoveTowards(
            _monsterStateMachine._monster.transform.position,
            _monsterStateMachine._monster.targetObject.transform.position,
            _monsterStateMachine._monster.stat.MoveSpeed * Time.deltaTime * _monsterStateMachine.moveSpeedModifier
        );
        Debug.Log(_objectName);
        //if(_objectName == "Tree") 
        _monsterStateMachine._monster.SetAnimation(angle);
    }

    // 스폰존으로 돌아가는 로직을 구현
    private void ReturnToSpawnZone()
    {
        isReturnToSpawn = true;
        Vector2 direction = _monsterStateMachine._monster._spawnPoint - (Vector2)_monsterStateMachine._monster.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _monsterStateMachine._monster.transform.position = Vector2.MoveTowards(
            _monsterStateMachine._monster.transform.position,
            _monsterStateMachine._monster._spawnPoint,
            _monsterStateMachine._monster.stat.MoveSpeed * Time.deltaTime * _monsterStateMachine.moveSpeedModifier);

        //if (_objectName == "Tree") 
        _monsterStateMachine._monster.SetAnimation(angle);

        //몬스터가 스폰포인트에 도착했을때
        if ((Vector2)_monsterStateMachine._monster.transform.position == _monsterStateMachine._monster._spawnPoint)
        {
            isReturnToSpawn = false;
            _monsterStateMachine.ChangeState(_monsterStateMachine._monsterIdleState);
        }
    }
}
