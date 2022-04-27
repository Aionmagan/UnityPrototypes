using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PersonalInputManager;

public class VehicleController : AVehicle
{
    public Transform body;
    public float limitRotation;

    //used for camera at the moment
    public float Speed
    {
        get { return _rigidbody.velocity.magnitude; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Control.ButtonHold(Button.R_BUMPER))
        {
            Accelarate();
        }
        else if (Control.ButtonHold(Button.L_BUMPER))
        {
            Brake();
        } else if (_rigidbody.velocity.magnitude > 0.0f)
        {
            Deccelaration();
        }

        //Steer(Control.LeftStick_X());

        body.transform.rotation = Quaternion.Lerp(body.transform.rotation, this.transform.rotation * Quaternion.Euler(Vector3.forward * Control.LeftStick_X() * limitRotation), 5 * Time.deltaTime);
        body.transform.position = body.transform.position + this.transform.up * (Mathf.Sin(Time.time * 20) * 0.01f);
        //Hover();
    }

    private void FixedUpdate()
    {
        Steer(Control.LeftStick_X());
        Hover(); 
    }
}
