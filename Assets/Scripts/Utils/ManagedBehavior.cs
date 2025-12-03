using UnityEngine;

public abstract class ManagedBehavior : MonoBehaviour
{
    public virtual void OnEnable()
    {
        BehaviorManager.Instance.Add(this);
    }
    public virtual void Start()
    {
        BehaviorManager.Instance.Add(this);
    }
    public abstract void OnUpdate(bool pause);
    public abstract void OnFixedUpdate(bool pause);
    public abstract void OnLateUpdate(bool pause);
    public virtual void OnDisable()
    {
        BehaviorManager.Instance.Remove(this);
    }
}
