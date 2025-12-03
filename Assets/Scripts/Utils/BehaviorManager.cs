using System.Collections.Generic;

public class BehaviorManager : GenericSingleton<BehaviorManager>
{
    private Dictionary<int, ManagedBehavior> _behaviors = new Dictionary<int, ManagedBehavior>();
    private bool _pause = false;

    public bool Add(ManagedBehavior behavior)
    {
        if (_behaviors.ContainsKey(behavior.GetInstanceID())) return false;
        
        _behaviors.Add(behavior.GetInstanceID(), behavior);
        return true;
    }

    public void Remove(ManagedBehavior behavior)
    {
        if (_behaviors.ContainsKey(behavior.GetInstanceID())) 
            _behaviors.Remove(behavior.GetInstanceID());
    }

    public void TogglePause()
    {
        _pause = !_pause;
    }

    private void Update()
    {
        foreach (var pair in _behaviors)
        {
            pair.Value.OnUpdate(_pause);
        }
    }

    // similar for fixed update etc

    private void FixedUpdate()
    {
        foreach (var pair in _behaviors)
        {
            pair.Value.OnFixedUpdate(_pause);
        }
    }
}
