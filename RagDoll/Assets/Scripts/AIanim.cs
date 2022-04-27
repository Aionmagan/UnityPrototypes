using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIanim : MonoBehaviour 
{
	public bool isRun;
	public bool isJump;
	public bool isDance;
	public bool isCrouch; 
	public float move;

	private Animator anim;

	private int hashRun;
	private int hashJump;
	private int hashDance;
	private int hashCrouch; 
	private int hashMove; 

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

		hashRun = Animator.StringToHash("IsRun");
		hashJump = Animator.StringToHash("IsJump");
		hashDance = Animator.StringToHash("IsDance");
		hashCrouch = Animator.StringToHash("IsCrouch");
		hashMove = Animator.StringToHash("Move");

		if (isRun)
        {
			anim.SetBool(hashRun, true);
        }
		else if (isJump)
        {
			anim.SetBool(hashJump, true);
        }
		else if (isDance)
        {
			anim.SetBool(hashDance, true);
        }
		else if (isCrouch)
        {
			anim.SetBool(hashCrouch, true);
        }
		else if (move > 0.0f)
        {
			anim.SetFloat(hashMove, move);
        }

	}
	
}
