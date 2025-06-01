using UnityEngine;
using System.Collections.Generic;

public class PatrolState : State{
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private Transform patrolRoute;
    protected int currentWaypointIndex = 1;
    private List<Transform> waypoints = new();
    
    override public void OnEnterState(FSM_Controller controller, GameObject target = null){
        base.OnEnterState(controller, target);
        if (patrolRoute != null) {
            foreach (Transform child in patrolRoute) { waypoints.Add(child); }
            currentWaypointIndex = 1;  
        }
    }

    override public void OnUpdateState(){
        if (waypoints.Count > 0) {
            pointTowardsDestiny(waypoints[currentWaypointIndex].transform);
            if (transform.position != waypoints[currentWaypointIndex].position){
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, patrolSpeed * Time.deltaTime);
            }
            else { currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; }
        }
    }

    override public void OnExitState(){ waypoints.Clear(); }
}
