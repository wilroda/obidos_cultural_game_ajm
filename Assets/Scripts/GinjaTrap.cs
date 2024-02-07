using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinjaTrap : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _ginjaSurprise;

    private Transform _lookAtTarget;

    public void PlayerEnteredCollider()
    {
        _anim.SetBool("opened", true);
        _anim.SetTrigger("ginjaSurprise");
        _lookAtTarget = FindObjectOfType<JamController>().transform;
    }
    public void PlayerExitedCollider()
    {
        _anim.SetBool("opened", false);
        _anim.SetTrigger("ginjaGoAway");
    }
    private void LateUpdate()
    {
        if (_lookAtTarget != null)
        {
            _ginjaSurprise.LookAt(_lookAtTarget.position + Vector3.up);
        }
    }
}
