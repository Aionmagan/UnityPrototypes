using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGrab))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    public CameraController cameraConrtoller;
    public float moveSpeed;
    public float runSpeed;
    public float jumpeFoce;
    public float lookFrwdSpeed;

    private Vector3 m_movement;
    private Rigidbody m_rigidbody;
    private AnimatorController m_anim;
    private GroundCheck m_ground; 
    private PlayerGrab m_playerGrab; 
    private bool isGrabbing = false; 

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction runAction;

    private float curspeed;
    private float isRun;
    private bool jump = false;
    private Vector2 analog; 

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_ground = GetComponent<GroundCheck>();
        m_anim = GetComponent<AnimatorController>();
        m_playerGrab = GetComponent<PlayerGrab>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        runAction = playerInput.actions["Run"];

        m_rigidbody.position = transform.position; 
        curspeed = moveSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();

        if (analog.magnitude > 0.2f) { LookForward(); }

        Movement();

        animApply();
    }

    void UpdateInput()
    {
        analog = moveAction.ReadValue<Vector2>();
        isRun  = runAction.ReadValue<float>(); 
        jump   = jumpAction.triggered;
    }

    void Movement()
    {
        if (isRun > 0.1f) { curspeed = runSpeed * isRun; }
        else { curspeed = moveSpeed * analog.normalized.magnitude; }

        if (m_playerGrab.IsGrabbing)
        {
            curspeed = moveSpeed * 0.35f * analog.normalized.magnitude;
            jump = false; 
        }

        m_movement = new Vector3(analog.x, 0, analog.y) * curspeed * Time.deltaTime;
        m_movement = cameraConrtoller.forwardPointer.TransformDirection(m_movement);

        var jmp = m_rigidbody.velocity.y;

        if (m_ground.OnGround())
        {
            if (jump)
            {
                jmp += jumpeFoce + Physics.gravity.y * Time.deltaTime;
            }
        }

        m_rigidbody.velocity = new Vector3(0, jmp, 0);// +(Vector3)(jmp * analog);

        //if (analog.sqrMagnitude > 0.52f)
        transform.position = transform.position + m_movement;
        m_rigidbody.MovePosition(transform.position);
    }

    void LookForward()
    {
        var v = m_movement; v.y = 0; 
        //var v = new Vector3(analog.x, 0, analog.y);
        //v = cameraConrtoller.forwardPointer.TransformDirection(v);

        var q = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(v), lookFrwdSpeed * Time.deltaTime);

        transform.rotation = q;
    }

    void animApply()
    {
        //move animation
        var a = Mathf.Clamp01(Mathf.Abs(analog.x) + Mathf.Abs(analog.y));

        m_anim.AnimMove(a, isRun * a);

        //jump animation
        if (jump)
            m_anim.AnimJump();

        m_anim.AnimGrab(m_playerGrab.IsGrabbing);
    }
}
