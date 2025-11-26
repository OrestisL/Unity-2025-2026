using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent Agent;
    public LayerMask ValidNavigationLayers;
    public float StoppingDistance = 1.0f;

    private InputAction _mousePos;
    private GameObject _clickParticlePrefab;

    Queue<Vector3> _targetPositions = new Queue<Vector3>();

    private void Start()
    {
        _mousePos = InputSystem.actions.FindAction("Point");

        // get left click action
        InputSystem.actions.FindAction("Attack").performed += SetTargetPosition;

        _clickParticlePrefab = Resources.Load<GameObject>("Particles/ClickParticle");
        if (!_clickParticlePrefab)
        {
            Debug.LogError("Click particle prefab not found in Resources/Particles/ClickParticle");
        }
    }

    private void SetTargetPosition(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = _mousePos.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, ValidNavigationLayers))
        {
            if (_clickParticlePrefab)
            {
                var particle = Instantiate(_clickParticlePrefab, hitInfo.point + hitInfo.normal, Quaternion.identity);
                particle.transform.forward = hitInfo.normal;
            }
            if (NavMesh.SamplePosition(hitInfo.point, out NavMeshHit navMeshHit, 1.0f, NavMesh.AllAreas))
            {

                _targetPositions.Enqueue(navMeshHit.position);
            }
        }
    }

    private void Update()
    {
        if (_targetPositions.Count == 0) return;

        if (Agent.remainingDistance >= StoppingDistance) return;

        Vector3 _targetPos = _targetPositions.Dequeue();
        Agent.SetDestination(_targetPos);
    }


}
