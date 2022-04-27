using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineAudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    private CarController _car;

    public float _pitchOffset;
    public float speedIncrease;
    public float[] gear;
    public int _currentGear;
    private float _pitch;

    // Start is called before the first frame update
    void Start()
    {
        _car = GetComponent<CarController>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var pitch = _car.Speed * _pitchOffset;
        if (pitch > 2.0f && !_car.Reverse)
        {
            _pitch += 0.2f * gear[_currentGear];
            _pitch = Mathf.Clamp(_pitch, 0.3f, pitch);

            if (_pitch > 2.0f) 
            {
                _currentGear++;
                
                if (_currentGear < gear.Length - 1) { _pitch = 0.3f; }
                else { _currentGear = gear.Length - 1; }
            };
        }
        else
        {
            _currentGear = 0;
            _pitch = Mathf.Clamp(pitch, 0.3f, pitch);
        }
        _audioSource.pitch = _pitch;
    }
}
