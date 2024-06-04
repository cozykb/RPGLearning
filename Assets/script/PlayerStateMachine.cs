using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
// 这玩意儿用来维护PlayerStat的变更
{   // Start is called before the first frame update

    public PlayerState currentState { get; private set; }
    
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();  
    }
}
