using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public List<string> TargetableTags = new();

    private PlayerStats _stats;
    private AnimationController _controller;
    private void Start()
    {
        _stats = GetComponentInParent<PlayerStats>();
        _controller = GetComponentInParent<AnimationController>();
    }
    public void CancelPunch()
    {
        _controller.SetBoolValue("Punch", false);
    }
    public void CancelKick()
    {
        _controller.SetBoolValue("Kick", false);
    }

    public void PunchDamage()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 3.0f);
        if (targets.Length == 0)
            return;

        foreach (Collider target in targets)
        {
            if (!TargetableTags.Contains(target.tag))
                continue;

            if (target.gameObject.TryGetComponent(out Rigidbody body))
            {
                // get direction to target
                Vector3 dir = (target.transform.position - transform.position).normalized;
                body.AddForce(dir * _stats.PunchStrength, ForceMode.Force);

                // also play sound
                if (target.TryGetComponent(out ObjectHitSFX sfx))
                {
                    sfx.PlaySFX();
                }
            }
        }
    }

    public void KickDamage()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 3.0f);
        if (targets.Length == 0)
            return;

        foreach (Collider target in targets)
        {
            if (!TargetableTags.Contains(target.tag))
                continue;

            if (target.gameObject.TryGetComponent(out Rigidbody body))
            {
                // get direction to target
                Vector3 dir = (target.transform.position - transform.position).normalized;
                body.AddForce(dir * _stats.KickStrength, ForceMode.Force);
            }
        }
    }
}
