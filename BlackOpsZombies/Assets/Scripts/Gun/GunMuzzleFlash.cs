using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMuzzleFlash : MonoBehaviour
{
	private bool _isMuzzling = false;
    private SpriteRenderer[] _spriteRenderer;

	[SerializeField] private float _flashSpeed;
	[SerializeField] private Transform[] _muzzleFlash;
	[SerializeField] private Light _light;

	// Use this for initialization
	void Start()
    {
		_spriteRenderer = new SpriteRenderer[_muzzleFlash.Length];
		for(int i = 0; i < _muzzleFlash.Length; ++i)
        {
			_spriteRenderer[i] = _muzzleFlash[i].GetComponent<SpriteRenderer>();
			_spriteRenderer[i].enabled = false;
			
        }
		_light.enabled = false;
    }
	
	//void Update()
 //   {
	//	if (Input.GetKeyDown(KeyCode.Mouse0))
 //       {
	//		MuzzleFlashStart();
 //       }
 //   }

	public void MuzzleFlashStart()
    {
		StartCoroutine(MuzzleFlash());
    }

	IEnumerator MuzzleFlash()
    {
		_isMuzzling = true;
		_light.enabled = true;
		for(int i = 0; i < _muzzleFlash.Length; ++i)
        {
			_muzzleFlash[i].localEulerAngles = Vector3.forward * Random.Range(0f, 360f);
			_spriteRenderer[i].enabled = true;
        }
		
		yield return new WaitForSeconds(_flashSpeed);

		for(int i = 0; i < _muzzleFlash.Length; ++i)
        {
			_spriteRenderer[i].enabled = false;
        }
		_light.enabled = false;
		_isMuzzling = false;
    }
}
