using UnityEngine;
using System;

public class FSM_Controller : MonoBehaviour{
    private State currentState;
    private ChaseState chaseState;
    private PatrolState patrolState;
    private AttackState attackState;

    private void Awake(){
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();

        currentState = patrolState;
        if (currentState != null){ currentState.OnEnterState(this); }
    }
    
    private void Update(){
        if (currentState != null){ currentState.OnUpdateState(); }
    }
    
    public void ChangeState(State newState, GameObject target = null){
        if (currentState != null){ currentState.OnExitState(); }
        
        currentState = newState;
        if (currentState != null){ currentState.OnEnterState(this, target); }
    }
}
