using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AVehicle : MonoBehaviour
{
    protected Rigidbody _rigidbody;

    private Transform _normalsTransform;
    private float _turning;
    private float _deccelarate;
    private Quaternion _currentRotation;

    [Header("Vehicle Settings")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _speed;
    [SerializeField] private float _brake;
    [SerializeField] private float _steerAngleSpeed;
    [SerializeField] private float _gravity;
    
    [Space]
    [SerializeField] private float _hoverHeight;
    [SerializeField] private float _hoverForce;
    [SerializeField] private float _hoverRotation;
    [Space]
    [SerializeField] private float _rayCastDistance;
    [SerializeField] private LayerMask _rayCastLayerMask; 
    
    [Space]
    [Header("Vehicle Physical Settings")]
    [SerializeField] private float _minRotationLimit;
    [SerializeField] private float _maxRotationLimit;

    protected void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _normalsTransform = new GameObject("Normal Direciton").transform;
    }

    protected void Accelarate()
    {
        _deccelarate = _acceleration;
        _rigidbody.velocity = this.transform.forward * _acceleration * Time.deltaTime;
    }

    protected void Brake()
    {
        _deccelarate -= (_decceleration * _brake) * 5 * Time.deltaTime;     
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, Vector3.zero, _brake * Time.deltaTime);
    }

    protected void Deccelaration()
    {
        _rigidbody.velocity = this.transform.forward * _deccelarate * Time.deltaTime;
        _deccelarate -= _decceleration * Time.deltaTime;
        _deccelarate = Mathf.Clamp(_deccelarate, 0, _acceleration);
    }

    protected void Steer(float turn)
    {
        _turning = turn * _steerAngleSpeed * Time.deltaTime;
        //var q = _rigidbody.rotation;
        // _rigidbody.rotation = new Quaternion(q.x, (_turning * Mathf.Deg2Rad), q.z,  q.w).normalized;
        //_rigidbody.rotation = Quaternion.Euler(this.transform.up * _turning);
        //var q = Quaternion.AngleAxis(_turning, _normalsTransform.forward);
        //var v =  q * Quaternion.FromToRotation(Vector3.up, _normalsTransform.forward);

        //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, v, _hoverRotation * Time.deltaTime);
        //_currentRotation = Quaternion.Euler(this.transform.up * _turning);
        //this.transform.rotation = _currentRotation;

        var q = Quaternion.AngleAxis(_turning, _normalsTransform.forward);
        var v = q * Quaternion.FromToRotation(this.transform.up, _normalsTransform.forward);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, v * this.transform.rotation, _hoverRotation * Time.deltaTime);
    }

    protected void Hover()
    {
        RaycastHit hit; 

        if (Physics.Raycast(this.transform.position + this.transform.up * 2, -this.transform.up, 
                            out hit, _rayCastDistance, _rayCastLayerMask))
        {
            _normalsTransform.position = hit.point;
            _normalsTransform.rotation = Quaternion.LookRotation(hit.normal);
            //_normalsTransform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);


            //var v = hit.point;
            //var q = Quaternion.LookRotation(hit.point);
            //q.y = _rigidbody.rotation.y;

            //Debug.DrawRay(v, q * Vector3.forward);

            //rotate to normals orientation
            //_rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, q, _hoverForce * Time.deltaTime);
            //var n = _normalsTransform.eulerAngles - Vector3.left * 90;
            //n.y = this.transform.eulerAngles.y;
            //this.transform.LookAt(_normalsTransform.position + this.transform.forward * _hoverForce, _normalsTransform.forward);
            //var q = Quaternion.AngleAxis(_turning, _normalsTransform.forward);
            //var v = q * Quaternion.FromToRotation(this.transform.up, _normalsTransform.forward);
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, v * this.transform.rotation, _hoverRotation * Time.deltaTime);
            //this.transform.rotation *= Quaternion.LookRotation(-_normalsTransform.up, _normalsTransform.forward);
            //this.transform.rotation = Quaternion.LookRotation(_normalsTransform.position + _normalsTransform.forward * hit.distance + this.transform.forward * 10, _normalsTransform.forward);
            //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, v, _hoverForce * Time.deltaTime);// * Quaternion.Euler(Vector3.right * this.transform.eulerAngles.x);
            //this.transform.rotation = Quaternion.AngleAxis(this.transform.rotation.x, Vector3.left) * v;
            //this.transform.eulerAngles = n;// _normalsTransform.eulerAngles - Vector3.left * 90;

            //var up = hit.normal;
            //var fw = Vector3.Cross(this.transform.up, up);
            //this.transform.rotation = Quaternion.LookRotation(fw, up);
            //hover to desired position          
            //float h = Mathf.Abs(_hoverHeight - hit.distance);// _hoverForce ;
            //Debug.Log(h);
            var p = _normalsTransform.position + _normalsTransform.forward * _hoverHeight;
            _rigidbody.position = Vector3.Lerp(_rigidbody.position, p, _hoverForce * Time.deltaTime);


        }else
        {
            _rigidbody.velocity += -this.transform.up * _gravity * Time.deltaTime;
        }
    }
}
