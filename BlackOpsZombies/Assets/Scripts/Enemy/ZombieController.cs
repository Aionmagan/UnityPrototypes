using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieController : ACharacter {

	enum EnemyState
    {
		MOVING, 
		ATTACK, 
		DEATH
    }
	EnemyState _enemyState;

	private Transform _player;
	private NavMeshAgent _nma;
	private ICharacter _iCharacer;
	private CapsuleCollider _capsuleCollider;

	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private EnemyAnimator _animator;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_iCharacer = _player.GetComponent<ICharacter>();
		_nma = GetComponent<NavMeshAgent>();
		_animator = GetComponent<EnemyAnimator>();

		_enemyState = EnemyState.MOVING;
		_moveSpeed = (int)Random.Range(0, 2);
		_nma.speed = _moveSpeed < 1 ? 0.7f : 5;

		InitAbstractCharacter();
		_capsuleCollider = GetComponentInChildren<CapsuleCollider>();
		StartCoroutine(Attact());
	}
	
	// Update is called once per frame
	void Update () {

		if (IsAlive())
        {
			Active();
        }else
        {
			InActive();
        }

		Animate();

	}

	void Active()
    {
		_nma.SetDestination(_player.position);
		if (Vector3.Distance(transform.position, _player.position) < 2.2f)
		{
			_enemyState = EnemyState.ATTACK;
		}
		else
		{
			_enemyState = EnemyState.MOVING;
		}
	}

	void InActive()
    {
		_nma.isStopped = true;

		_enemyState = EnemyState.DEATH;
		StartCoroutine(Death());
    }

	void Animate()
    {
		switch(_enemyState)
        {
			case EnemyState.MOVING:
				_animator.Attack(false);
				_animator.Movement(_moveSpeed);
				break;
			case EnemyState.ATTACK:
				_animator.Attack(true);
				break;
			case EnemyState.DEATH:
				_animator.Attack(false);

				if (Vector3.Dot(transform.forward, _player.forward) < 0.0f)
				{
					_animator.DeathBack();
				}
				else
				{ 
					_animator.DeathFront();
				}
				break;
        }
    }

	IEnumerator Attact()
    {
		while(true)
        {
			if (_enemyState == EnemyState.ATTACK)
            {
				_nma.isStopped = true;

				_iCharacer.TakeDamage(10.5f);

				yield return new WaitForSeconds(1);
            }
			_nma.isStopped = false;
			yield return null;
        }
    }

	IEnumerator Death()
    {
		_nma.isStopped = true;
		_rigidbody.useGravity = false;
		_capsuleCollider.enabled = false;
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
		//gameObject.SetActive(false);
    }
}
