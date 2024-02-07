using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneVisuals : MonoBehaviour
{
    [SerializeField] private JamController _controller;
    [SerializeField] private Animator _animator;

    [SerializeField] private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_controller == default)
        {
            _animator.SetBool("moving", _rb.velocity.magnitude > 0.01f);
            return;
        }

        _animator.speed = _controller.RunFactor;
        _animator.SetBool("moving", _controller.IsMoving);
    }
}
