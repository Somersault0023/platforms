using UnityEngine;

public class Player : Entity{
    private Rigidbody2D rb;
    private float inputH;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Attack System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius=0.8f;
    [SerializeField] private LayerMask whatIsDamagable;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    override protected void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){
        handleMovement();
        handleJump();
        handleAttack();        
    }

    private void handleMovement(){
        inputH = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputH * speed, rb.linearVelocity.y);
        anim.SetBool("Running", inputH != 0);
        
        if (inputH != 0) { transform.eulerAngles = inputH >= 0 ? Vector3.zero : new Vector3(0, 180, 0); }
    }

    private void handleJump(){
        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    private void handleAttack(){
        if (Input.GetMouseButtonDown(0)) { anim.SetTrigger("Attack"); }
    }

    private void Attack(){
        Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamagable);
        foreach(Collider2D item in touchedColliders){
            HealthSystem healthSystem = item.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null){ healthSystem.receiveDamage(damageAttack); }
        }
    }
    
    private void OnDrawGizmos(){
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}