using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortalController : MonoBehaviour
{
    [SerializeField] private Camera m_camera1;
    [SerializeField] private string m_tagOtherPortal;

    private Camera m_camera2;
    private Vector3 m_direction = Vector3.one;
    private GameObject m_player;
    private float m_distance;
    float distanceOffset;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        distanceOffset = m_camera1.fieldOfView;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //if ((m_distance = Vector3.Distance(m_player.transform.position, m_camera.transform.position)) < 15.0f)
        //{
        m_distance = Vector3.Distance(m_player.transform.position, m_camera1.transform.position);
        //m_camera1.fieldOfView = distanceOffset + m_distance;
        m_direction = m_player.transform.position - m_camera1.transform.position;

        if (m_camera2 != null)
        {
            //m_camera2.transform.LookAt(-m_direction);
            m_camera2.fieldOfView = distanceOffset + m_distance;
            m_camera2.transform.rotation = Quaternion.LookRotation(m_direction.normalized);
        }
        else 
        {
            GetOtherPortalCamera();
        }
        //m_camera2.transform.LookAt(-m_direction);
            //m_camera.transform.eulerAngles = m_direction;
        //m_camera1.transform.LookAt(m_direction);
            //Debug.Log(m_direction);
        //}
    }

    private void GetOtherPortalCamera()
    {
        var portal = GameObject.FindGameObjectWithTag(m_tagOtherPortal);
        if (portal)
        {         
            m_camera2 = portal.GetComponentInChildren<Camera>();
            distanceOffset = m_camera2.fieldOfView;
        }
    }
}
