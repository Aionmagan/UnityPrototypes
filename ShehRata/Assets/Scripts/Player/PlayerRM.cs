/*this is not used at all was just created for testing*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerRM : MonoBehaviour
{
    public CameraController camCon; 
    private Animator animator;
    private Rigidbody rigidbody;

    public Vector2 movement;
    public float running;
    public bool isJump;
    public bool isSlide;
    public bool isVault;
    public int vaultSelect; 
    
    //input
    public PlayerInput input;
    private InputAction analog;
    private InputAction jump;
    private InputAction slide; 
    private InputAction run;

    private int hashMove;
    private int hashRun;
    private int hashJump;
    private int hashSlide; 
    private int hashVault;
    private int hashVaultSelect; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        input = GetComponent<PlayerInput>();
        analog = input.actions["Move"];
        jump = input.actions["Jump"];
        slide = input.actions["Slide"];
        run = input.actions["Run"];

        hashMove = Animator.StringToHash("Movement");
        hashRun = Animator.StringToHash("Running");
        hashJump = Animator.StringToHash("Jump");
        hashSlide = Animator.StringToHash("Slide");
        hashVault = Animator.StringToHash("Vault");
        hashVaultSelect = Animator.StringToHash("VaultSelect");
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.Lerp(movement, analog.ReadValue<Vector2>(), 5 * Time.deltaTime);
        running = Mathf.Lerp(running, run.ReadValue<float>(), 5 * Time.deltaTime);
        isJump = jump.triggered;
        isSlide = slide.triggered; 

        if (Mathf.Clamp01(movement.magnitude) > 0.3f)
        {
            var v = camCon.forwardPointer.TransformDirection(new Vector3(movement.x, 0, movement.y));
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(v),
                                 5 * Time.deltaTime);
        }else
        {
            TriggerReset(hashRun);
        }

        AnimationUpdate();
    }

    void TriggerReset(int hashValue)
    {
        foreach (var t in animator.parameters)
        {
            if (t.type == AnimatorControllerParameterType.Trigger)
            {
                if (hashValue == t.nameHash) { continue; }
                animator.ResetTrigger(t.nameHash);
            }
        }
    }

    void AnimationUpdate()
    {
        animator.SetFloat(hashMove, Mathf.Clamp01(movement.magnitude));
        animator.SetFloat(hashRun, running * Mathf.Clamp01(movement.magnitude));

        if (isVault)
        {
            animator.SetInteger(hashVaultSelect, vaultSelect);
            animator.SetTrigger(hashVault);

            isVault = false;

            TriggerReset(hashVault);
        }

        if (isJump)
        {
            animator.SetTrigger(hashJump);
            TriggerReset(hashJump);
        }

        if (isSlide)
        {
            animator.SetTrigger(hashSlide);
            TriggerReset(hashSlide);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vaultable"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump")) { Debug.Log("isJump"); return; }

            isVault = true;
            vaultSelect = Random.Range(0, 3);
        }
    }

    /* private void OnAnimatorMove()
     {
         var v = animator.deltaPosition+transform.position;

         rigidbody.MovePosition(v);
     }*/


}
