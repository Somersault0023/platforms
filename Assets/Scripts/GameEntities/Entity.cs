using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Entity : MonoBehaviour {
    [SerializeField] public int damageAttack = 10;
    [SerializeField] protected Transform spawnPoint;
    public int DamageAttack  { get; }

    public virtual void MoveTowardsTarget(Transform target, float chaseSpeed) {}
    public virtual void PointTowardsDestiny(Transform destiny){
        transform.rotation = Quaternion.Euler(0, (destiny.position.x > transform.position.x) ? 0 : 180, 0);
    }
    public virtual void ChaseTarget(Transform target, float chaseSpeed) { GetComponentInParent<Entity>().MoveTowardsTarget(target.transform, chaseSpeed); }
    public virtual void AttackTarget(Transform target, float attackSpeed) {}

    public virtual void Reset(){
        GetComponent<FSM_Controller>().ChangeState(GetComponent<PatrolState>(), gameObject);
        transform.position = spawnPoint.position;
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("PlayerDetection")) { OnPlayerDetected(other); }
        else if (other.gameObject.CompareTag("PlayerHitBox")) { OnPlayerDamaged(other); }
    }
    
    protected virtual void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("PlayerDetection")) { OnPlayerNotDetected(other); }
        else if (other.gameObject.CompareTag("PlayerHitBox")) { OnPlayerDetected(other); }
    }
    
    protected virtual void OnPlayerDetected(Collider2D other){
        FSM_Controller ctrl = GetComponent<FSM_Controller>();
        if(ctrl) { ctrl.ChangeState(GetComponent<ChaseState>(), other.gameObject); }
    }
    
    protected virtual void OnPlayerNotDetected(Collider2D other){
        FSM_Controller ctrl = GetComponent<FSM_Controller>();
        if(ctrl) { ctrl.ChangeState(GetComponent<PatrolState>(), other.gameObject); }
    }
    
    protected virtual void OnPlayerDamaged(Collider2D other){
        FSM_Controller ctrl = GetComponent<FSM_Controller>();
        if(ctrl) { ctrl.ChangeState(GetComponent<AttackState>(), other.gameObject); }
    }
    
    protected virtual void OnPlayerDamaged(Collision2D other){
        FSM_Controller ctrl = GetComponent<FSM_Controller>();
        if(ctrl) { ctrl.ChangeState(GetComponent<AttackState>(), other.gameObject); }
    }
}
