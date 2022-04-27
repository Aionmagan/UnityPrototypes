using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private Vector3 m_movement;
    private Transform m_camPointer;

    [SerializeField] private GameObject m_cam;
    [SerializeField] private float m_jump;
    [SerializeField] private float m_moveSpeed;

    public GameObject PlayerCamera
    {
        get
        {
            return m_cam;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_camPointer = new GameObject("m_camPointer").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        m_camPointer.transform.eulerAngles = Vector3.up * m_cam.transform.eulerAngles.y;

        m_movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_moveSpeed;
        m_movement.y = m_rigidbody.velocity.y;
        m_movement = m_camPointer.transform.TransformDirection(m_movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_movement.y = m_jump;
        }
       
        m_rigidbody.velocity = m_movement;
    }

    private void LateUpdate()
    {
        this.transform.eulerAngles = new Vector3(0, m_cam.transform.eulerAngles.y, 0);
    }

}
