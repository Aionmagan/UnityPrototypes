using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_fpsPosition;
    [SerializeField] private float m_rotationSpeed;
    [SerializeField] private float m_maxRot, m_minRot;
    
    [SerializeField] private GameObject m_arm;
    [SerializeField] private Transform m_armPos;
    [SerializeField] private float m_armLerpSpeed; 

    private Vector3 m_rotation = Vector3.one;
    public Vector3 CameraRotation
    {
        get
        {
            return m_rotation;
        }
        set
        {
            m_rotation = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_rotation   += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * m_rotationSpeed * Time.deltaTime;
        m_rotation.x = Mathf.Clamp(m_rotation.x, m_minRot, m_maxRot);

        this.transform.eulerAngles = m_rotation;
        this.transform.position    = m_fpsPosition.position;

        moveArm();
    }

    private void moveArm()
    {
        m_arm.transform.position = m_armPos.position;
        //m_arm.transform.position = Vector3.Lerp(m_arm.transform.position, m_armPos.position, m_armLerpSpeed * Time.deltaTime);
        m_arm.transform.rotation = Quaternion.Slerp(m_arm.transform.rotation, m_armPos.rotation, m_armLerpSpeed * Time.deltaTime);
        
    }
}
