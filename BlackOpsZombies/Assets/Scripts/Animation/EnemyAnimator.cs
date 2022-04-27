using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

	[SerializeField] private Animator _animator;

	public void Movement(float speed)
    {
        _animator.SetFloat("Blend", speed);
    }

    public void Attack(bool isAttacking)
    {
        _animator.SetBool("Attack", isAttacking);
    }

    public void DeathFront()
    {
        _animator.SetTrigger("DeathFront");
    }

    public void DeathBack()
    {
        _animator.SetTrigger("DeathBack");
    }
}
