using UnityEngine;

public class Player : Entity{
    [Header("Movement System")]
    [SerializeField] private Transform feet;
    [SerializeField] private float speed = 5f;

    [SerializeField] private const int MAX_JUMPS = 2;
    [SerializeField] private const int JUMP_COOLDOWN = 10;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float floorDetectionDist = .01f;
    [SerializeField] private LayerMask whatIsJumpable;

    [Header("Attack System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius=0.8f;
    [SerializeField] private LayerMask whatIsDamagable;


    private Rigidbody2D rb;
    private float inputH;
    private Animator anim;
    private int jumpsRemaining = MAX_JUMPS;
    private int jumpCooldown = 0;
    private bool jumped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public override void Reset(){
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        jumpsRemaining = MAX_JUMPS;
        jumpCooldown = 0;
        jumped = false;
        transform.position = spawnPoint.position; // Reset position to origin
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
        //bool tg = touchingGround();
        if (Mathf.Round(rb.linearVelocity.y) == 0f) { jumpsRemaining = MAX_JUMPS; }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0 && jumpCooldown == 0){
            jumped = true;
            jumpsRemaining--;
            //Debug.Log("Jumps remaining: " + jumpsRemaining);
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
        
        if(jumped) {
            jumpCooldown = (jumpCooldown + 1) % JUMP_COOLDOWN;
            jumped = jumpCooldown > 0;
        }
        //else { jumpsRemaining = MAX_JUMPS; }
    }

    private bool touchingGround(){
        return Physics2D.Raycast(feet.position, Vector2.down, floorDetectionDist, whatIsJumpable);
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