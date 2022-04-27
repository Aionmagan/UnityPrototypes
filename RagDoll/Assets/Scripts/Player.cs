using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;

public class Player : MonoBehaviour 
{
	public float moveForce;
	public float rotationForce;
	public Transform cam;
	public Transform pointer;
	public Animator anim;
	public LayerMask layerMask; 
	public float min, max;

	private Rigidbody rb; 
	private Vector3 movement;
	private float mag; 
	private float x, y;
	private float lx, ly;

	private int hashShoot;
	private int hashWalk;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();

		hashShoot = Animator.StringToHash("Shoot");
		hashWalk = Animator.StringToHash("Walk");
	}
	
	// Update is called once per frame
	void Update ()
	{

		movement = Control.MainStick_3D() * moveForce * Time.deltaTime;
		movement.y = rb.velocity.y;
		movement = transform.TransformDirection(movement);

		var v = transform.position + movement;
		rb.velocity = Vector3.zero; // new Vector3(0, movement.y, 0);
		rb.MovePosition(v);

		mag = movement.sqrMagnitude;

		IsShoot();
		Anim();
	}

	void LateUpdate()
    {
		lx = Mathf.Lerp(lx, Control.RightStick_X(), 10 * Time.deltaTime);
		ly = Mathf.Lerp(ly, Control.RightStick_Y(), 10 * Time.deltaTime);
		y += ly * rotationForce * Time.deltaTime;
		x += lx * rotationForce * Time.deltaTime;
	
		y = Mathf.Clamp(y, min, max);
		cam.localRotation = Quaternion.Euler(y, transform.rotation.y, 0);
		rb.rotation = Quaternion.Euler(0, x ,0);
    }

	void IsShoot()
    {
		if (Control.ButtonDown(Button.R_BUMPER) && mag < 0.01f)
        {
			anim.SetTrigger(hashShoot);

			RaycastHit hit;
			
			if (Physics.Raycast(pointer.position, pointer.forward, out hit, Mathf.Infinity, layerMask))
            {
				ICharacter ic = hit.collider.gameObject.GetComponentInParent<ICharacter>();
				Debug.Log(hit.collider.gameObject.name);
				if (ic != null)
                {
					ic.TakeDamage(1);
					hit.rigidbody.AddForce(cam.forward * 2555);
                }
            }
        }
    }

	void Anim()
    {
		anim.SetBool(hashWalk, mag > 0.01f);
    }
}
