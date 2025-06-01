using UnityEngine;

public class ChaseState : State{
    [SerializeField] private float chaseSpeed = 2.0f;
    override public void OnEnterState(FSM_Controller controller, GameObject target = null){
        base.OnEnterState(controller, target);
    }

    override public void OnUpdateState(){
        pointTowardsDestiny(target.transform);
        if (transform.position != target.transform.position){
            GetComponentInParent<Entity>().ChaseTarget(target.transform, chaseSpeed);
        }
    }

    override public void OnExitState(){
        // Clean up patrol behavior, reset variables, etc.
    }
}
