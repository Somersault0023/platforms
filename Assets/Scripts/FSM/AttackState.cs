using UnityEngine;

public class AttackState : State{
    [SerializeField] private float attackSpeed = 1f;
    override public void OnEnterState(FSM_Controller controller, GameObject target = null){
        base.OnEnterState(controller, target);
        
        target.GetComponentInParent<HealthSystem>().receiveDamage((int) gameObject.GetComponent<Entity>().damageAttack);
    }

    override public void OnUpdateState(){
        pointTowardsDestiny(target.transform);
        GetComponentInParent<Entity>().AttackTarget(target.transform, attackSpeed);
    }

    override public void OnExitState(){}
}