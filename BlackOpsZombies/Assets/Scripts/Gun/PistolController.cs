using System.Collections;
using System.Collections.Generic;
using InputManager;
using UnityEngine;

[RequireComponent(typeof(GunController))]
public class PistolController : AGun {

	private GunController _gunController;
	private bool _lockState; 

	[SerializeField] private PlayerAnimator _playerAnimator;

	// Use this for initialization
	void Start () 
	{
		Init();
		_gunController = GetComponent<GunController>();
		StartCoroutine(Shooting());
		StartCoroutine(Reloading());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Reloading()
    {
		var b = false;
		while (true)
		{
			if (Control.ButtonDown(Button.SQUARE))
			{
				if (_currentAmo < _maxAmo)
				{
					_lockState = true;
					_playerAnimator.Reload();
					b = _playerAnimator.AnimatorIsPlaying(PlayerAnimator.AnimatorState.Reload);

					yield return new WaitForSeconds(1.6f);

					_currentAmo = _maxAmo;
					_lockState = false;
				}
			}
			yield return null; 
		}
    }

	IEnumerator Shooting()
    {
		while(true)
        {

			if (Control.ButtonDown(Button.R_BUMPER) && !_lockState)
            {
				if (_currentAmo > 0)
				{
					_currentAmo--;
					_gunController.Shoot(_damage);
					_playerAnimator.Shoot();
				}
				yield return new WaitForSeconds(_shootSpeed);
            }
            
			yield return null; 
        }
    }
}
