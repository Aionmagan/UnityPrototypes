using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSL.InputManager;

public class CameraController : MonoBehaviour
{
    private CarController car;
    private bool _firstPerson;
    private Vector3 _shakeVector;

    public Transform target;
    public Transform firstPersonTarget;
    public float distance;
    public float maxDistance;
    public float lookAtHeight;
    public float cameraHeight;
    public float rotationAmount;
    public float shakeOffset;

    // Start is called before the first frame update
    void Start()
    {
        car = target.GetComponent<CarController>();
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
            var q = Quaternion.Euler(Vector3.up * Control.RightStick_X() * rotationAmount);
            if (Control.ButtonHold(Button.R3_BUTTON)) { q = Quaternion.Euler(Vector3.up * 180); }

            var p = target.position - q * target.forward * distance + Vector3.up * cameraHeight;

            this.transform.position = Vector3.Lerp(this.transform.position, p, 5 * Time.deltaTime);
            this.transform.LookAt(target.position + Vector3.up * lookAtHeight + _shakeVector);
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
            _shakeVector.x = Random.Range(0.0f, car.Speed * shakeOffset);
            _shakeVector.y = Random.Range(0.0f, car.Speed * shakeOffset);
            _shakeVector.z = 0;
            timer += Time.deltaTime;

            yield return null;
        }
        _shakeVector = Vector3.zero;
    }
}
