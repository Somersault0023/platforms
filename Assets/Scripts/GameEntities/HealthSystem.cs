using UnityEngine;

public class HealthSystem : MonoBehaviour{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    
    void Start(){ health = maxHealth; }

    public void receiveDamage(int damage){
        health -= damage;
        if (health <= 0){
            health = maxHealth;
            if (gameObject.CompareTag("PlayerHitBox")) { Reset(); }
            else { Destroy(this.gameObject); }
        }
    }
    
    public void Reset(){
        Entity[] entities = transform.parent.parent.GetComponentsInChildren<Entity>();
        foreach(Entity entity in entities){ entity.Reset(); }
    }
}