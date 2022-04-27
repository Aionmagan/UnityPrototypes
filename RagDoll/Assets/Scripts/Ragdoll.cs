using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour, ICharacter 
{

	private Rigidbody[] ragrb;
    private Animator anim; 

	void Start()
    {
        anim = GetComponent<Animator>();
        ragrb = GetComponentsInChildren<Rigidbody>();
        SetRagdoll(false);
    }

    public void SetRagdoll(bool isRag)
    {
        anim.enabled =! isRag;
        foreach(var r in ragrb)
        {
            r.isKinematic =! isRag; 
        }
    }

    public void TakeDamage(float damage)
    {
        SetRagdoll(true);
    }
}
