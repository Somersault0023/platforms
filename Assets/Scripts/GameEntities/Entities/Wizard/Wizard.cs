using UnityEngine;
using System.Collections;

public class Wizard : Entity{
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform fireballSpawnPoint;
    [SerializeField] private float attackSpeed;
    private Animator anim;

    void Start(){ anim = GetComponent<Animator>(); }

    public override void Reset(){
        base.Reset();
        
        //StopAllCoroutines();
        //StartCoroutine(attack());
    }
    
    public override void ChaseTarget(Transform target, float attackSpeed){ StartCoroutine(attack(target)); }
    
    private IEnumerator attack(Transform target){
        anim.SetTrigger("atacar");
        yield return new WaitForSeconds(attackSpeed);
    }
    
    private void LanzarBola(){
        Instantiate(fireball, fireballSpawnPoint.position, transform.rotation);
    }
    
    override public void PointTowardsDestiny(Transform destiny){
        transform.parent.transform.rotation = Quaternion.Euler(0, (destiny.position.x > transform.position.x) ? 0 : 180, 0);
    }
}
