using System.Collections;
using System.Collections.Generic;
using InputManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : ACharacter {

	private Vector3 _movement;

	[SerializeField] private float _rotationSpeed;
	[SerializeField] private PlayerAnimator _playerAnimator;
	[SerializeField] private GameObject _arms;

	// Use this for initialization
	void Start () 
	{
		InitAbstractCharacter();
		StartCoroutine(Regeneration());
		StartCoroutine(IsDead());
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		var x = Control.LeftStick_X();
		var y = Control.LeftStick_Y();
		var s = Control.ButtonHold(Button.L_BUMPER) ? _moveSpeed * 0.3f : _moveSpeed; 

		_movement = new Vector3(x, 0, y) * s * Time.deltaTime;
		_movement.y = _rigidbody.velocity.y;
		_movement = transform.TransformDirection(_movement);

		_rigidbody.velocity = _movement;
		transform.eulerAngles += Vector3.up * Control.RightStick_X() * _rotationSpeed * Time.deltaTime;

		if (!Control.ButtonDown(Button.R_BUMPER))
		{
			var v = Mathf.Abs(x) + Mathf.Abs(y);
			if (s != _moveSpeed) { v = 0; }
			_playerAnimator.Movement(v);
		}
	}

	IEnumerator Regeneration()
    {
		while (true)
        {
			if (!_tookDamage)
			{
				if (_health < 100)
				{
					_health += 0.6f;
					//yield return new WaitForSeconds(2);
				}
			}
			yield return null; 
        }
    }

	IEnumerator IsDead()
    {
		var isAlive = true;
		var t = transform.up * -1.2f;
		while (isAlive)
        {
			if (!IsAlive())
            {
				_arms.transform.GetChild(0).gameObject.SetActive(false);
				_arms.transform.localPosition = Vector3.MoveTowards(_arms.transform.localPosition, t, 5 * Time.deltaTime);
				if (Vector3.Distance(_arms.transform.localPosition, t) < 0.01f) { isAlive = false; }				

            }
			yield return null;
        }

		yield return new WaitForSeconds(5);
        {
			SceneManager.LoadScene(0);
        }
    }
}
