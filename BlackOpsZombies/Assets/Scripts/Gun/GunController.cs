using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	private GunMuzzleFlash _GMFComponent;

	[SerializeField] private Transform _gunMuzzleFlash;
	[SerializeField] private LayerMask _layerMask;

	// Use this for initialization
	void Start()
	{
		_GMFComponent = _gunMuzzleFlash.GetComponent<GunMuzzleFlash>();
	}

	public void Shoot(float damage)
	{
		RaycastHit hit;
		_GMFComponent.MuzzleFlashStart();

		Debug.DrawRay(_gunMuzzleFlash.position, _gunMuzzleFlash.forward * Mathf.Infinity);
		if (Physics.Raycast(_gunMuzzleFlash.position, _gunMuzzleFlash.forward, out hit, Mathf.Infinity, _layerMask))
		{
			ICharacter ic = hit.collider.gameObject.GetComponentInParent<ICharacter>();
			
			if (ic != null) { ic.TakeDamage(damage); }
			Debug.Log(hit.collider.name);
		}
	}
}

