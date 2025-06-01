using UnityEngine;

public class Slime : Entity{
    public override void MoveTowardsTarget(Transform target, float speed){
        Vector2 targetPos = new Vector2(target.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}