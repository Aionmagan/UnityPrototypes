using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

	public enum AnimatorState
    {//didn't make them cap because animator naming
		Idle, 
		Walking,
		Reload,
		Shooting
    }

	[SerializeField] private Animator _animator;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Movement(float analog)
    {
		_animator.SetFloat("Blend", analog);
    }

	public void Reload()
    {
		_animator.SetTrigger("ReloadTrigger");
    }

	public void Shoot()
    {
		_animator.SetTrigger("ShootTrigger");
	}

	public bool AnimatorIsPlaying(AnimatorState animatorState)
    {
		return _animator.GetCurrentAnimatorStateInfo(0).IsName(animatorState.ToString());
    }
}
