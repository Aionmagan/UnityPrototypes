using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particles;
    public WheelCollider _Wheel;
    public AudioSource audioSource;
    public float pitchOffset; 

    private Vector3 _lastPos; 
    private CarController car;

    // Start is called before the first frame update
    void Start()
    {
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        car = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        WheelHit hit;
        _Wheel.GetGroundHit(out hit);
        

        var dir = (this.transform.position - _lastPos);
        //Debug.Log(Vector3.Angle(dir, this.transform.forward));
        if (hit.forwardSlip > 0.3f || car.IsDrift)
        {
            particles.Play();
            if (!audioSource.isPlaying) { audioSource.Play();}

            var pitch = car.Speed * pitchOffset;
            pitch = Mathf.Clamp(pitch, 0.5f, Random.Range(0.8f, 1.0f)) ;
            audioSource.pitch = pitch;

        }else
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            audioSource.Stop();
        }

        if (Vector3.Distance(_lastPos, this.transform.position) > 0.2f)
        _lastPos = this.transform.position;
    }
}
