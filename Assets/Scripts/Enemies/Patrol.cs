using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : ManagedBehavior
{
    private NavMeshAgent _navMeshAgent;
    public List<Transform> PatrolPositions;
    public float StoppingDistance = 1.0f;
    public float Speed = 3.0f;
    private int _currentTargetIndex = 0;
    public override void Start()
    {
        base.Start();
        if (PatrolPositions.Count == 0)
        {
            Debug.LogWarning($"No patrol positions assigned for gameobject {name}.");
            Destroy(this);
        }
        _navMeshAgent = GetComponent<NavMeshAgent>();
        // set starting position to first patrol position
        transform.position = PatrolPositions[0].position;
    }

    public override void OnUpdate(bool pause)
    {
        PatrolMove(pause);
    }

    private void PatrolMove(bool pause)
    {
        _navMeshAgent.speed = pause ? 0.0f : Speed;
        if (pause) return;

        if (_navMeshAgent.remainingDistance >= StoppingDistance) return;

        // increase target index and loop back to start if at end of list
        _currentTargetIndex = ++_currentTargetIndex % PatrolPositions.Count;
        _navMeshAgent.SetDestination(PatrolPositions[_currentTargetIndex].position);

    }

    public override void OnFixedUpdate(bool pause)
    {

    }

    public override void OnLateUpdate(bool pause)
    {

    }
}
