using UnityEngine;

public class HealthSystem : MonoBehaviour{
    [SerializeField] private int maxHealth = 100;

    public void receiveDamage(int damage){
        maxHealth -= damage;
        if (maxHealth <= 0){ Destroy(this.gameObject); }
    }
}