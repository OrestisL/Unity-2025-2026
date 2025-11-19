using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _controller;

    private void Start()
    {
        if (!_controller)
            _controller = GetComponentInChildren<Animator>();
    }

    public void SetFloatValue(string name, float value)
    {
        _controller.SetFloat(name, value);
    }

    public void SetBoolValue(string name, bool value)
    {
        _controller.SetBool(name, value);
    }
}
