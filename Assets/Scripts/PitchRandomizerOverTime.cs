using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PitchRandomizerOverTime : MonoBehaviour
{
    public Vector2 _minMax = new Vector2(0.9f, 1.1f);
    public float _pitchShiftDelay = 0.5f;
    AudioSource _source;
    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            _source.pitch = Random.Range(_minMax.x, _minMax.y);
            countdown = _pitchShiftDelay;
        }

        countdown -= Time.deltaTime;
    }
}
