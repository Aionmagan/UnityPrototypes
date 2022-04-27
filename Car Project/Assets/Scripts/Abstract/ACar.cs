using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ACar : MonoBehaviour {

	protected Rigidbody _rigidbody;

	[Header("Car Settings")]

	//the wheel colliders for use
	[SerializeField] protected WheelCollider _colliderFL;
	[SerializeField] protected WheelCollider _colliderFR;
	[SerializeField] protected WheelCollider _colliderBL;
	[SerializeField] protected WheelCollider _colliderBR;
	[Space]

	//the physical wheels for use
	[SerializeField] protected Transform _wheelFL;
	[SerializeField] protected Transform _wheelFR;
	[SerializeField] protected Transform _wheelBL;
	[SerializeField] protected Transform _wheelBR;
	[Space]

	//simple variables for basic car use
	[SerializeField] protected float _steerAngle;
	[SerializeField] protected float _brakeTorque;
	[SerializeField] protected float _motorTorque;
	[SerializeField] protected float _maxMotorTorque;

	[Header("Car SubStep")]
	
	[SerializeField] protected float _speedThreshold;
	[SerializeField] protected int	 _stepsBelowThreshold;
	[SerializeField] protected int	 _stepsAboveThreshold;

	[Header("Car Physical Settings")]

	//this is for physical aspect only
	[SerializeField] protected float _wheelRotationSpeed;
	[SerializeField] protected float _offsetSteerAngle;

	//call on Start()
	protected void Init()
    {
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.centerOfMass = new Vector3(0.0f, -0.4f, 0.0f);
    }

	//both steers the collider wheels and the physical wheels
	protected void Steer(float dir)
    {
		_colliderFL.steerAngle = _steerAngle * dir;
		_colliderFR.steerAngle = _steerAngle * dir;

		_wheelFR.localEulerAngles = new Vector3(_wheelFR.localEulerAngles.x,
									   _colliderFR.steerAngle - this.transform.up.z + _offsetSteerAngle,
									   _wheelFR.localEulerAngles.z);

		_wheelFL.localEulerAngles = new Vector3(_wheelFL.localEulerAngles.x,
											   _colliderFL.steerAngle - this.transform.up.z - _offsetSteerAngle,
											   _wheelFL.localEulerAngles.z);

		//RotateWheels();add
    }

	//both gas on collider wheels and physical wheels
	protected void Gas(float torque)
    {
		if (_rigidbody.velocity.sqrMagnitude > _maxMotorTorque)
        {
			_colliderFL.motorTorque = 0;
			_colliderFR.motorTorque = 0;
			_colliderBL.motorTorque = 0;
			_colliderBR.motorTorque = 0;
        }
        else
        {
			_colliderFL.motorTorque = torque;
			_colliderFR.motorTorque = torque;
			_colliderBL.motorTorque = torque;
			_colliderBR.motorTorque = torque;
		}

		//RotateWheels();
    }

	//break torque on wheel colliders
	protected void Brake(float torque)
    {
		_colliderFL.brakeTorque = torque;
		_colliderFR.brakeTorque = torque;
		_colliderBL.brakeTorque = torque;
		_colliderBR.brakeTorque = torque;
	}

	//returns true if all wheels are touching (a ground) 
	protected bool OnGround()
    {
		if (_colliderFL.isGrounded
		&& _colliderFR.isGrounded
		&& _colliderBL.isGrounded
		&& _colliderBR.isGrounded)
		{ return true; }

		return false;
    }

	//rotating wheels 
	protected void RotateWheels()
    {
		_wheelFR.Rotate(_colliderFR.rpm *  _wheelRotationSpeed * Time.deltaTime, 0, 0);
		_wheelFL.Rotate(_colliderFL.rpm * -_wheelRotationSpeed * Time.deltaTime, 0, 0);
		_wheelBR.Rotate(_colliderBR.rpm *  _wheelRotationSpeed * Time.deltaTime, 0, 0);
		_wheelBL.Rotate(_colliderBL.rpm * -_wheelRotationSpeed * Time.deltaTime, 0, 0);
	}

	//non-Accessable funcions for abstract class only
	protected void SetSubStep()
    {
		_colliderBL.ConfigureVehicleSubsteps(_speedThreshold, _stepsBelowThreshold, _stepsAboveThreshold);
    }
}
