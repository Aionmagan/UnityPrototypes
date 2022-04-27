using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PersonalInputManager;

public class CameraController : MonoBehaviour
{
    private VehicleController _vehicle;
    private bool _firstPerson;
    private float _currentLerpSpeed;
    private Vector3 _shakeVector;
    private Vector3 _targetUp;
    private Transform _playerLerp; 

    public Transform target;
    public Transform firstPersonTarget;
    public float distance;
    public float maxDistance;
    public float lookAtHeight;
    public float cameraHeight;
    public float rotationAmount;
    public float shakeOffset;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _vehicle = target.GetComponent<VehicleController>();
        _playerLerp = new GameObject("Player Lerp Camera").transform;
        //QualitySettings.vSyncCount = 2;
        //Application.targetFrameRate = 30;

        //Screen.SetResolution(720, 408, true);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Control.ButtonDown(Button.Y_BUTTON)) { _firstPerson = !_firstPerson; }

        if (_firstPerson)
        {
            this.transform.position = firstPersonTarget.position;
            this.transform.rotation = firstPersonTarget.rotation;
        }
        else
        {
            _currentLerpSpeed = lerpSpeed;
            var q = Quaternion.Euler(target.up * Control.RightStick_X() * rotationAmount);
            if (Control.ButtonHold(Button.R3_BUTTON)) { q = Quaternion.Euler(target.up * 180); _currentLerpSpeed = 100.0f; }

            _playerLerp.position = target.position;
            _playerLerp.rotation = Quaternion.Lerp(_playerLerp.rotation, target.rotation, _currentLerpSpeed * Time.deltaTime);
            //var p = target.position - q * target.forward * distance + target.up * cameraHeight;
            this.transform.position = _playerLerp.position -  _playerLerp.forward * distance + _playerLerp.up * cameraHeight;
            _targetUp = Vector3.Lerp(_targetUp, target.up, lerpSpeed * Time.deltaTime);
            //this.transform.position = Vector3.Lerp(this.transform.position, p, _currentLerpSpeed * Time.deltaTime);            
            this.transform.LookAt(target.position + _targetUp * lookAtHeight + _shakeVector, _targetUp);
        }
    }

    public void ShakeCamera()
    {
        StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake()
    {
        var timer = 0.0f;
        var amountTime = 0.3f;

        while (timer < amountTime)
        {
            _shakeVector.x = Random.Range(0.0f, _vehicle.Speed * shakeOffset);
            _shakeVector.y = Random.Range(0.0f, _vehicle.Speed * shakeOffset);
            _shakeVector.z = 0;
            timer += Time.deltaTime;

            yield return null;
        }
        _shakeVector = Vector3.zero;
    }
}
