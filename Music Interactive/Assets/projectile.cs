using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*trash code, too lazy to fix*/ 

public class projectile : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float angle;
    public float distance;
    public GameObject cam;

    public float wPHeight;
    public float wPForward;
    public GameObject worldParticle;
    Camera _cam;

    public float speedOfChange;

    // Start is called before the first frame update
    void Start()
    {
        _cam = cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var time = Time.time;
        var timeMove = Mathf.PingPong(Time.time * (speedOfChange), 25);

        this.transform.position = new Vector3(timeMove * Mathf.Cos(time),
                                              timeMove * Mathf.Sin(time),
                                              Time.time * speed);
    }

    private void LateUpdate()
    {
        worldParticle.transform.position = cam.transform.position + Vector3.up * wPHeight + 
                                           cam.transform.forward  * wPForward;

        var q = Quaternion.Euler(0, Time.time * rotSpeed, 0);
        var v = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        cam.transform.position = v - q * Vector3.forward * distance;
        cam.transform.LookAt(this.transform.position);
    }

}
