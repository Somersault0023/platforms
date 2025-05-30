using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour{
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected float patrolSpeed = 2f;
    [SerializeField] protected int damageAttack = 10;
    protected int currentWaypointIndex = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start(){ StartCoroutine(Patrol()); }

    IEnumerator Patrol(){
        while (true){
            pointTowardsDestiny();
            while (transform.position != waypoints[currentWaypointIndex].position){
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, patrolSpeed * Time.deltaTime);
                yield return null;
            }
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void pointTowardsDestiny(){
        transform.localScale = (waypoints[currentWaypointIndex].position.x > transform.position.x) ? Vector3.one : new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("PlayerDetection")){
            Debug.Log("Player detected!");
        }
        else if (other.gameObject.CompareTag("PlayerHitBox")){
            other.gameObject.GetComponentInParent<HealthSystem>().receiveDamage((int) damageAttack);
        }
    }
}
