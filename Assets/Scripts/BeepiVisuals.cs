using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeepiVisuals : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private JamController _movementController;

    // Update is called once per frame
    void Update()
    {
       _animator.SetBool("walking", _movementController.MovementAxis.magnitude > 0.1f); 
    }
}
