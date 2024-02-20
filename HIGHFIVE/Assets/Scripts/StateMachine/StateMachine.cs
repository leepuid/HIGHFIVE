using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected IState _currentState;

    public void ChangeState(IState newState)
    {
        // 전 상태를 해제
        _currentState?.Exit();

        // 새로운 상태를 받아온 후
        _currentState = newState;

        // 진입
        _currentState?.Enter();

        Debug.Log($"Changed state to: {newState.GetType().Name}");
    }

    public void HandleInput()
    {
        _currentState?.HandleInput();
    }

    public void StateUpdate()
    {
        _currentState?.StateUpdate();
    }

    public void PhysicsUpdate()
    {
        _currentState?.PhysicsUpdate();
    }
}
