using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSL.InputManager;

public class CarController : ACar 
{
	[Header("Unique Car Settings")]
	[SerializeField] private float _turboSpeed;
	[SerializeField] private float _maxTurboSpeed;

	private float _brake = 0;
	private float _gas = 0;
	private bool  _reverse = false;
	private bool _drift = false;
	public bool IsDrift
    {
		get { return _drift; }
    }
	public bool Reverse
    {
		get { return _reverse; }
    }

	private WheelFrictionCurve _originalForwardWFC;
	private WheelFrictionCurve _originalSidewaysWFC;
	private WheelFrictionCurve _driftForwardWFC;
	private WheelFrictionCurve _driftSidewaysWFC;

	public float Speed
    {
        get { return _rigidbody.velocity.magnitude; }
    }
	void Start () 
	{
		//init abstract class stuff
		Init();

		_originalForwardWFC = _colliderBL.forwardFriction;
		_originalSidewaysWFC = _colliderBL.sidewaysFriction;

		_driftForwardWFC.extremumSlip = 2;
		_driftForwardWFC.extremumValue = 5;
		_driftForwardWFC.asymptoteSlip = 5;
		_driftForwardWFC.asymptoteValue = 1;
		_driftForwardWFC.stiffness = 1;

		_driftSidewaysWFC.extremumSlip = 1;
		_driftSidewaysWFC.extremumValue = 1;
		_driftSidewaysWFC.asymptoteSlip = 0.5f;
		_driftSidewaysWFC.asymptoteValue = 0.75f;
		_driftSidewaysWFC.stiffness = 1;
	}

	void FixedUpdate () 
	{
		//SetSubStep();
		FixedInput();
	}

	void FixedInput()
    {
		_brake = 0;
		_gas = 0;

		//called from abstract class
		Steer(Control.LeftStick_X());
		Drift();

        _gas = _motorTorque * Control.RightTrigger();

        if (_gas > 0.05f)
        {
            _reverse = false;
        }
        else if (Control.LeftTrigger() > 0.3f)
        {
            if (_rigidbody.velocity.magnitude > 1f && !_reverse)
            {
                _brake = _brakeTorque;
            }
            else
            {
                _reverse = true;
                _gas = -_motorTorque * Control.LeftTrigger();
            }
        }
        else
        {
            _brake = _motorTorque * 0.5f;
        }

        Gas(_gas);
		Brake(_brake);	
	}

	void Drift()
    {
		if (Control.ButtonHold(Button.A_BUTTON))
        {
			_colliderBL.forwardFriction = _driftForwardWFC;
			_colliderBR.forwardFriction = _driftForwardWFC;
			_colliderFL.forwardFriction = _driftForwardWFC;
			_colliderFR.forwardFriction = _driftForwardWFC;

			_colliderBL.sidewaysFriction = _driftSidewaysWFC;
			_colliderBR.sidewaysFriction = _driftSidewaysWFC;
			_colliderFL.sidewaysFriction = _driftSidewaysWFC;
			_colliderFR.sidewaysFriction = _driftSidewaysWFC;

			_drift = true;
		}else
        {
			_colliderBL.forwardFriction = _originalForwardWFC;
			_colliderBR.forwardFriction = _originalForwardWFC;
			_colliderFL.forwardFriction = _originalForwardWFC;
			_colliderFR.forwardFriction = _originalForwardWFC;

			_colliderBL.sidewaysFriction = _originalSidewaysWFC;
			_colliderBR.sidewaysFriction = _originalSidewaysWFC;
			_colliderFL.sidewaysFriction = _originalSidewaysWFC;
			_colliderFR.sidewaysFriction = _originalSidewaysWFC;

			_drift = false;
		}
    }
}
