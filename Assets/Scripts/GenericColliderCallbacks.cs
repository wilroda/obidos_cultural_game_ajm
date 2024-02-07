using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericColliderCallbacks : MonoBehaviour
{
    public bool onlyPlayer = true;
    public bool oneTimeTrigger;

    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer && other.tag != "Player") return;


        onEnter.Invoke();
        if (oneTimeTrigger) gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (onlyPlayer && other.tag != "Player") return;
        onExit.Invoke();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (onlyPlayer && other.transform.tag != "Player") return;

        onEnter.Invoke();
        if (oneTimeTrigger) gameObject.SetActive(false);
    }

    private void OnCollisionExit(Collision other)
    {
        if (onlyPlayer && other.transform.tag != "Player") return;
        onExit.Invoke();
    }
}
