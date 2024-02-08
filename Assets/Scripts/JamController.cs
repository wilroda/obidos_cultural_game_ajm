using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _addedGravity = 9;

    private Rigidbody _rb;
    private Transform _playerTransform;
    private Camera _playerCamera;
    public Vector3 Movement { get; private set; }
    public Vector2 MovementAxis { get; private set; }
    public float RunFactor { get; private set; }
    public bool IsMoving => _rb.velocity.magnitude + Movement.magnitude > 0.01f;
    public PlayerTeam Team { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Team = GetComponent<PlayerTeam>();
        _playerTransform = transform;
        _playerCamera = Camera.main;
        RunFactor = 1;
    }

    private void Update()
    {
        HandleJumpInput();
        GetMovementInput();
        RotatePlayerTowardsMovement();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
        _rb.AddForce(transform.up * -_addedGravity * Time.fixedDeltaTime);
    }

    private void GetMovementInput()
    {
        MovementAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunFactor = 2;
        }
        else
        {
            RunFactor = 1;
        }
    }

    private void HandleMovementInput()
    {
        Movement = new Vector3(MovementAxis.x, 0f, MovementAxis.y).normalized;
        Vector3 newPosition = _playerTransform.position + (_playerTransform.TransformDirection(Movement) * _movementSpeed * Time.fixedDeltaTime * RunFactor);

        _rb.MovePosition(newPosition);
    }

    private void RotatePlayerTowardsMovement()
    {
        Vector3 movementDirection = Vector3.Scale(_playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized; // Camera looking at

        if (MovementAxis.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection.normalized, Vector3.up);
            _playerTransform.rotation = Quaternion.RotateTowards(_playerTransform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void Suppress()
    {
        enabled = false;
        Movement = Vector3.zero;
        MovementAxis = Vector2.zero;
        _rb.velocity = Vector3.zero;
    }
    public void Unsupress()
    {
        enabled = true;
    }
}
