using UnityEngine;
using UnityEngine.InputSystem;

public class Attacks : MonoBehaviour
{
    private InputAction _punch;
    private InputAction _kick;
    private AnimationController _controller;
    private void Start()
    {
        _punch = InputSystem.actions.FindAction("Attack");
        _punch.performed += Punch;

        _kick = InputSystem.actions.FindAction("AltAttack");
        _kick.performed += Kick;

        _controller = GetComponent<AnimationController>();
    }

    private void Kick(InputAction.CallbackContext context)
    {
        _controller.SetBoolValue("Kick", true);
    }

    private void Punch(InputAction.CallbackContext context)
    {
        _controller.SetBoolValue("Punch", true);
    }
}
