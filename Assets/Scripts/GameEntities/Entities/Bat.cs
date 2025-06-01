using UnityEngine;
using System.Collections;
public class Bat : Entity {
    public override void MoveTowardsTarget(Transform target, float speed){
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
