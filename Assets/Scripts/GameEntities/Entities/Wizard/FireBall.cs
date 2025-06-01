using UnityEngine;

public class FireBall : Entity{
    Rigidbody2D rb;
    [SerializeField] private float shotForce;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shotForce, ForceMode2D.Impulse);
    }

    override public void Reset(){
        base.Reset();
        Destroy(gameObject);
    }
    
    protected void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("PlayerHitBox")) { OnPlayerDamaged(other); }
        if (other.gameObject.CompareTag("Map") || other.gameObject.CompareTag("PlayerHitBox")){ Destroy(gameObject); }
    }
    
    override protected void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("PlayerHitBox")) {
            OnPlayerDamaged(other);
            Destroy(gameObject);
        }
    }
    
    override protected void OnTriggerExit2D(Collider2D other){}

    override protected void OnPlayerDamaged(Collision2D other) {
        other.gameObject.GetComponentInParent<HealthSystem>().receiveDamage((int) gameObject.GetComponent<Entity>().damageAttack);
    }
}
