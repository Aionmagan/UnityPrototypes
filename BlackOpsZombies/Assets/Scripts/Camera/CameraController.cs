using System.Collections;
using System.Collections.Generic;
using InputManager;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float x, y;
	public bool _isLerping = false;
	public Vector3 _currentPosition;
	public Vector3 _currentEularAngles;

	[SerializeField] private Transform _movingContainer;
	[SerializeField] private Transform _camera;
	[SerializeField] private Transform _downSightPoint;
	[SerializeField] private Transform _sightPoint;

	[SerializeField] private float _rotationSpeed;
	[SerializeField] private float _lerpSpeed;
	[SerializeField] private float _min, _max;
	
	// Use this for initialization
	void Start () {
		_currentPosition = _sightPoint.position;
		_currentEularAngles = _sightPoint.eulerAngles;
		StartCoroutine(LerpSight());
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (!_isLerping) 
		{ 
			_camera.localPosition = _currentPosition;
			_camera.localEulerAngles = _currentEularAngles;
		}

		//x -= Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
		y += Control.RightStick_Y() * _rotationSpeed * Time.deltaTime;

		y = Mathf.Clamp(y, _min, _max);
		_movingContainer.eulerAngles = new Vector3(y, transform.eulerAngles.y, 0);
	}

	IEnumerator LerpSight()
    {
		var v = _currentPosition;
		var e = _currentEularAngles;

		while(true)
        {
			if (Control.ButtonHold(Button.L_BUMPER))
            {
				v = _downSightPoint.localPosition;
				e = _downSightPoint.localEulerAngles;
            }
			else
            {
				v = _sightPoint.localPosition;
				e = _sightPoint.localEulerAngles;
            }

			if (Control.ButtonDown(Button.L_BUMPER) || Control.ButtonUP(Button.L_BUMPER)) { _isLerping = true; }

			while (Vector3.Distance(_currentPosition, v) > 0.1f && _isLerping)
            {
				//_isLerping = true;
				//_currentPosition = Vector3.Lerp(_currentPosition, v, _lerpSpeed * Time.deltaTime);
				_currentEularAngles = Vector3.Lerp(_currentEularAngles, e, _lerpSpeed * Time.deltaTime);

				_currentPosition = Vector3.MoveTowards(_currentPosition, v, _lerpSpeed * Time.deltaTime);			
				_camera.localPosition = _currentPosition;
				_camera.localEulerAngles = _currentEularAngles;
				yield return null;
            }

			_isLerping = false;
			_currentPosition = v;
			_currentEularAngles = e;

			yield return null;
        }
    }
}
