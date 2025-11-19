using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController Controller;
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpHeight;
    public float Gravity = 10;
    private bool _running;

    private InputAction _moveAction;
    private InputAction _runAction;
    
    [SerializeField]
    private Camera _mainCam;

    private Vector3 _velocity;
    private float turnSmoothVelocity, turnSmoothTime = 0.1f;
    private AnimationController _animController;

    private void Start()
    {
        PlayerData.Instance.Init();

        Controller = GetComponent<CharacterController>();

        _moveAction = InputSystem.actions.FindAction("Move");

        _runAction = InputSystem.actions.FindAction("Run");
        _runAction.started += ToggleRun;
        _runAction.canceled += ToggleRun;

        _mainCam = Camera.main;

        _animController = GetComponent<AnimationController>();

        InputSystem.actions.FindAction("Jump").started += Jump;

        InputSystem.actions.FindAction("Crouch").started += CrouchStart;
        InputSystem.actions.FindAction("Crouch").canceled += CrouchEnd;
    }

    private void CrouchEnd(InputAction.CallbackContext context)
    {
        _animController.SetBoolValue("Crouch", false);
    }

    private void CrouchStart(InputAction.CallbackContext obj)
    {
        _animController.SetBoolValue("Crouch", true);
    }

    private void ToggleRun(InputAction.CallbackContext context)
    {
        _running = context.started == true;
    }

    private void Update()
    {
        Vector2 move = _moveAction.ReadValue<Vector2>();
        Move(move);

        ApplyGravity();

        PlayerData.Instance.AddScore(0.00001f);
    }

    private void Move(Vector2 input)
    {
        if (input.sqrMagnitude == 0)
        {
            _animController.SetFloatValue("Speed", 0);
            return;
        }

        Vector3 inputDirection = new Vector3(input.x, 0, input.y).normalized;

        //rotate player to where the camera is looking
        float targetAngle = _mainCam.transform.eulerAngles.y + Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.localRotation = Quaternion.Euler(0, angle, 0);

        float currentSpeed = _running ? RunSpeed : WalkSpeed;
        _animController.SetFloatValue("Speed", currentSpeed);
        //rotate forward to look direction
        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * currentSpeed;

        Controller.Move(Time.deltaTime * moveDirection);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!Controller.isGrounded) return;

        _velocity.y = Mathf.Sqrt(-2 * Gravity * JumpHeight);
    }
    private void ApplyGravity()
    {
        Controller.Move(_velocity * Time.deltaTime);

        if (Controller.isGrounded && _velocity.y < 0)
            _velocity.y = Gravity;
        else if (!Controller.isGrounded)
            _velocity.y += Gravity * Time.deltaTime;
    }

    private void OnDisable()
    {
        InputSystem.actions.FindAction("Crouch").started -= CrouchStart;
        InputSystem.actions.FindAction("Crouch").canceled -= CrouchEnd;
        InputSystem.actions.FindAction("Jump").started -= Jump;
        _runAction.started -= ToggleRun;
        _runAction.canceled -= ToggleRun;
    }
}
