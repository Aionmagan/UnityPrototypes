using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private GameObject m_redPortal;
    [SerializeField] private GameObject m_bluePortal;

    private GameObject[] m_portals;

    // Start is called before the first frame update
    void Start()
    {
        m_portals = new GameObject[2];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(true);
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isBlue)
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Mathf.Infinity, m_layerMask))
        {
            Vector3 dir = hit.normal;
            GameObject obj;
            var mesh = hit.transform.gameObject.GetComponent<MeshFilter>().mesh;
            //var portalMesh = null;

            if (isBlue)
            {
                //if ()
                if (m_portals[0]){Destroy(m_portals[0]);}
                obj = Instantiate(m_bluePortal, hit.point, Quaternion.LookRotation(dir));
                m_portals[0] = obj;
            }else
            {
                if (m_portals[1]){Destroy(m_portals[1]);}
                obj = Instantiate(m_redPortal, hit.point, Quaternion.LookRotation(dir));
                m_portals[1] = obj;
            }

        }
    }
}
