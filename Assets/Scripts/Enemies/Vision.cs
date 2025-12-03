using UnityEngine;

public class Vision : ManagedBehavior
{
    public float ViewRadius = 10.0f;
    public float ViewDegrees = 60.0f;
    public LayerMask ValidLayers;
    public LayerMask ObstacleLayers;
    public Transform VisionStartingPoint;
    public Transform _target;

    public override void OnFixedUpdate(bool pause)
    {
        See(pause);
    }

    private void See(bool pause)
    {
        if (pause) return;

        Collider[] potentialTargets = Physics.OverlapSphere(VisionStartingPoint.position, ViewRadius, ValidLayers);
        if (potentialTargets.Length == 0)
        {
            _target = null;
            return;
        }

        foreach (Collider target in potentialTargets)
        {
            Vector3 directionToTarget = (target.transform.position - VisionStartingPoint.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (Mathf.Abs(angle) > ViewDegrees)
            {
                // target is outside of vision 
                Debug.DrawLine(VisionStartingPoint.position, target.transform.position, Color.red);
                _target = null;
                continue;
            }
            if (Physics.Linecast(VisionStartingPoint.position, target.transform.position, ObstacleLayers))
            {

                // Target is obstructed
                Debug.DrawLine(VisionStartingPoint.position, target.transform.position, Color.red);
                _target = null;
                continue;
            }

            _target = target.transform;
        }
    }

#if DEBUG
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ViewRadius);
        if (_target)
            Gizmos.DrawLine(VisionStartingPoint.position, _target.position);
    }
#endif
    public override void OnUpdate(bool pause)
    {
        
    }

    public override void OnLateUpdate(bool pause)
    {
        
    }

}
