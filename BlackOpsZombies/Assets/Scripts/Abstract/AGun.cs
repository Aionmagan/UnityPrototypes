using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGun : MonoBehaviour {

	protected int _currentAmo; 

	[Header("Weapon Settings")]
	[SerializeField] protected float _shootSpeed;
	[SerializeField] protected int _maxAmo;
	[SerializeField] protected float _damage;

	protected void Init()
    {
		_currentAmo = _maxAmo;
    }
}
