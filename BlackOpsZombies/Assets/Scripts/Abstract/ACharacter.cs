using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class ACharacter : MonoBehaviour, ICharacter {

	protected Rigidbody _rigidbody;
    protected bool _tookDamage;

	[Header("Character Settings")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _moveSpeed;

    public float Health
    {
        get { return _health; }
    }

    public void TakeDamage(float damage)
    {
        StopCoroutine(CoolDown());
        _health -= damage;
        _tookDamage = true;
        StartCoroutine(CoolDown());
    }

    protected void InitAbstractCharacter()
    {
		_rigidbody = GetComponent<Rigidbody>();
    }

    protected bool IsAlive()
    {
        return _health > 0.0f;
    }
    
    IEnumerator CoolDown()
    {
        while(true)
        {
            if (_tookDamage)
            {
                yield return new WaitForSeconds(5f);
                _tookDamage = false;
            }
            yield return null;
        }
    }


}
