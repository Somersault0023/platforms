using UnityEngine;

public abstract class State : MonoBehaviour {
    protected FSM_Controller ctrl;
    protected GameObject target;

    public virtual void OnEnterState(FSM_Controller controller, GameObject t = null){
        ctrl = controller;
        target = t;
    }
    public abstract void OnUpdateState();
    public abstract void OnExitState();

    protected virtual void pointTowardsDestiny(Transform destiny){
        gameObject.GetComponent<Entity>().PointTowardsDestiny(destiny);
    }
}
