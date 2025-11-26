using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public List<Transform> PatrolPositions;
    public float StoppingDistance = 1.0f;
    private int _currentTargetIndex = 0;
    private void Start()
    {
        if (PatrolPositions.Count == 0)
        {
            Debug.LogWarning($"No patrol positions assigned for gameobject {name}.");
            Destroy(this);
        }
        _navMeshAgent = GetComponent<NavMeshAgent>();
        // set starting position to first patrol position
        transform.position = PatrolPositions[0].position;
    }

    private void Update()
    {
        PatrolMove();
    }

    private void PatrolMove()
    {
        if (_navMeshAgent.remainingDistance >= StoppingDistance) return;

        // increase target index and loop back to start if at end of list
        _currentTargetIndex = ++_currentTargetIndex % PatrolPositions.Count;
        _navMeshAgent.SetDestination(PatrolPositions[_currentTargetIndex].position);
    }
}
