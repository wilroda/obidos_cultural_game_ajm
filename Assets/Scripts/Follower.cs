using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private JamController _target;
    private Rigidbody _rb;

    private float _randomX;
    private float _randomY;

    private float _idleCountdown;
    private bool _idleWalk;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _target = FindObjectOfType<JamController>();

        _randomX = (Random.value - 0.5f) * 2f * 500;
        _randomY = (Random.value - 0.5f) * 2f * 500;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dst = Vector3.Distance(_target.transform.position, transform.position);
        Vector3 randomDir = new Vector3(
            (Mathf.PerlinNoise(Time.time * 2 + _randomX, -Time.time * 2 + _randomY) - 0.5f) * 2f,
        0,
            (Mathf.PerlinNoise(-Time.time * 2 + _randomX, Time.time * 2 + _randomY) - 0.5f) * 2f);

        if (_idleCountdown <= 0)
        {
            _idleCountdown = Random.Range(1, 5);
            _idleWalk = !_idleWalk;
        }

        transform.rotation = _target.transform.rotation;

        _idleCountdown -= Time.fixedDeltaTime;

        if (_idleWalk)
        {
            _rb.AddForce(randomDir * 2000 * Time.fixedDeltaTime);
        }
        if (dst < 2)
        {
            return;
        }


        _rb.AddForce((_target.transform.position - transform.position).normalized * 1000 * Time.fixedDeltaTime);
    }
}
