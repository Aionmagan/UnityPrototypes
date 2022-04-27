using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using carPrototype.InputManager;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private GameObject _lastPosRot; //pivot pointer for cam
    private bool _reverse = false;

    [SerializeField]
    private WheelCollider[] wheelsCol; //first 2 wheels are assumes front left and right;
    [SerializeField]
    private GameObject[] wheels; //assuming same order as wheelcolliders
    [SerializeField]
    private float maxTorque;
    
    public float torque;
    public float steering;

    public GameObject Pivot
    {
        get
        {
            return _lastPosRot;
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return _rigidbody.velocity;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0, -0.6f, 0);
        _lastPosRot = new GameObject("Car Pivot");

        if(wheels.Length == wheelsCol.Length)
        {
            StartCoroutine(WheelSuspention());
        }
    }

    private void Update()
    {
        _lastPosRot.transform.position = this.transform.position;
        _lastPosRot.transform.eulerAngles = Vector3.up * this.transform.eulerAngles.y;

        if (OnGround()) { Gravity(); }
        WheelSuspention();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float motor = 0;
        float brake = 0;
        float steer = Control.HorizontalAxis() * steering; //change input (left and right) 

        //change input (this is gas) 
        if (Control.Gas())
        {
            motor = torque;
            _reverse = false;
        }

        //change input (this is break)
        if (Control.Brake())
        {
            if (_rigidbody.velocity.magnitude > 1f && !_reverse)
            {
                brake = torque;
            }
            else
            {
                _reverse = true;
                motor = -torque;
            }
        }

        //limiting max speed
        if (_rigidbody.velocity.magnitude > maxTorque) { motor = 0; }


        //accelerate all four wheels for better traction
        for (int i = 0; i < wheelsCol.Length; ++i)
        {
            //check if it's the first two (front) wheels
            if (i < wheelsCol.Length / 2)
            {
                wheelsCol[i].steerAngle = steer;
            }

            wheelsCol[i].brakeTorque = brake;
            wheelsCol[i].motorTorque = motor;
        }
    }

    private void Gravity()
    {
        //pointing downwards in object coordinate and applying graviy force
        //used to stick to everything
        _rigidbody.AddForce((this.transform.up * Physics.gravity.y * 
                            (_rigidbody.mass * 1.7f))/50/50, ForceMode.Acceleration);
    }

    private bool OnGround()
    {
        if (wheelsCol[0].isGrounded)
        {
            _rigidbody.useGravity = false;
            return true;
        }

        _rigidbody.useGravity = true;
        return false;
    }

    IEnumerator WheelSuspention()
    {
        //whole code is to make wheels follow wheelcoliders
        var q = Quaternion.identity;
        var v = Vector3.zero;

        for (int i = 0; i < wheels.Length; ++i)
        {
            wheels[i].transform.parent = wheelsCol[i].transform;
        }

        while (true)
        {
            for (int i = 0; i < wheels.Length; ++i)
            {
                wheelsCol[i].GetWorldPose(out v, out q);

                wheels[i].transform.position = v;
                wheels[i].transform.rotation = q;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
