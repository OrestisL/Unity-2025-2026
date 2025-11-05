using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerMovement : MonoBehaviour
{
    public float WalkSpeed = 3.0f;
    public float RunSpeed = 8.0f;
    private float _currentSpeed;

    public float Gravity = -10.0f;
    public float JumpHeight = 3.0f;
    public float RunJumpHeight = 5.0f;
    private float _currentJumpHeight;
    private bool _canDoubleJump = true;

    private InputAction _move;
    private InputAction _jump;
    private InputAction _run;

    private CharacterController _controller;
    private float turnSmoothVelocity = 0;
    private Vector3 _velocity;

    private void Start()
    {
        _move = InputSystem.actions.FindAction("Move");
        _run = InputSystem.actions.FindAction("Run");

        _run.started += Run;
        _run.canceled += Run;

        _jump = InputSystem.actions.FindAction("Jump");
        _jump.started += Jump;

        _controller = GetComponent<CharacterController>();

        _currentSpeed = WalkSpeed;
        _currentJumpHeight = JumpHeight;
    }

    private void Update()
    {
        Vector2 input = _move.ReadValue<Vector2>();
        _velocity = new Vector3(input.x, _velocity.y, input.y);
        ApplyGravity();
        Move(_velocity);
    }

    private void Move(Vector3 moveVector)
    {
        // move player
        _controller.Move(_currentSpeed * Time.deltaTime * moveVector);

        if (moveVector.sqrMagnitude <= 0.1f)
            return;

        // rotate player
        // calculate rotation angle
        float θ = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;

        // smooth the transition between angles
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, θ, ref turnSmoothVelocity, 0.1f);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void Run(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            _currentSpeed = RunSpeed;
            _currentJumpHeight = RunJumpHeight;
        }
        else
        {
            _currentSpeed = WalkSpeed;
            _currentJumpHeight = JumpHeight;
        }
    }

    private void ApplyGravity()
    {
        if (!_controller.isGrounded)
            _velocity.y += Gravity * Time.deltaTime;

        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
            _canDoubleJump = true;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_controller.isGrounded)

            _velocity.y = Mathf.Sqrt(2 * Mathf.Abs(Gravity) * _currentJumpHeight);
        else
        {
            // already in the air
            if (_canDoubleJump)
            { 
                _canDoubleJump = false;
                _velocity.y = Mathf.Sqrt(Mathf.Abs(Gravity) * _currentJumpHeight);
            }

        }
    }

    private void OnDisable()
    {
        _run.started -= Run;
        _run.canceled -= Run;

        _jump.started -= Jump;
    }

}
