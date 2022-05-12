using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{
    Rigidbody rb; 
    Vector3 movement;

    public cameraController cam; 
    public float speed;
    public float runSpeed; 
    public float jumpforce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var s = Input.GetKey(KeyCode.LeftShift) ? runSpeed : speed;

        movement = new Vector3(x, 0, y) * speed * Time.deltaTime;
        movement.y = rb.velocity.y; 
        movement = cam.Pointer.transform.TransformDirection(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.y = jumpforce * Time.deltaTime;
        }

        rb.velocity = movement; 
    }
}
