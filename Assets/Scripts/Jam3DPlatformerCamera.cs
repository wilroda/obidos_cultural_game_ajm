using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam3DPlatformerCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;  // The player's transform
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _yOffset = 5f;

    [SerializeField] private float _minVerticalAngle = -80f;
    [SerializeField] private float _maxVerticalAngle = 80f;

    private float _mouseX;
    private float _mouseY;


    private void LateUpdate()
    {
        if (_target == null)
        {
            Debug.LogWarning("Camera target not set!");
            return;
        }

        HandleInput();

        // Rotate the camera around the player based on input
        RotateCamera();

        // Ensure the camera doesn't go beyond specified vertical angles
        _mouseY = Mathf.Clamp(_mouseY, _minVerticalAngle, _maxVerticalAngle);

        // Set the camera's position and rotation
        Vector3 rotationEulerAngles = new Vector3(-_mouseY, _mouseX, 0f);
        transform.rotation = Quaternion.Euler(rotationEulerAngles);
        transform.position = _target.position - transform.forward * _distance + (_yOffset * _target.up);
    }

    private void HandleInput()
    {
        // Mouse input
        _mouseX += Input.GetAxis("Mouse X") * _rotationSpeed;
        _mouseY += Input.GetAxis("Mouse Y") * _rotationSpeed;

        // Joystick input (Right Stick)
        //mouseX += Input.GetAxis("RightStickX") * rotationSpeed;
        //mouseY -= Input.GetAxis("RightStickY") * rotationSpeed;
    }

    private void RotateCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            float desiredAngle = _target.eulerAngles.y + horizontalInput * _rotationSpeed;
            Quaternion rotation = Quaternion.Euler(0f, desiredAngle, 0f);
            transform.position = _target.position - rotation * Vector3.forward * _rotationSpeed + (_yOffset * _target.up);
            transform.LookAt(_target.position);
        }
    }
}
