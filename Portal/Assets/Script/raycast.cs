using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject needles;
    [SerializeField] private float _moveToSpeed; 

    private Camera m_cam;
    private GameObject currentNeedle;



    // Start is called before the first frame update
    void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos();
        }else if (!currentNeedle)
        {
            //currentNeelde = Instantiate(needles) as GameObject;
        }
    }

    private void mousePos()
    {
        RaycastHit hit;
        var smp = m_cam.WorldToScreenPoint(Input.mousePosition);

        if (Physics.Raycast(smp, this.transform.forward, out hit, _distance, _layerMask))
        {
            if (hit.point != null)
            {
                //needle move towards hit.point
                StartCoroutine(moveTo(hit.point, hit.transform.gameObject));
            }
        }
    }

    IEnumerator moveTo(Vector3 position, GameObject obj)
    {
        float scale = 0;
        var lastPos = currentNeedle.transform.position;

        while(scale < 1.0f)
        {
            currentNeedle.transform.position = lastPos + scale * (position - lastPos);

            yield return null;

            scale += _moveToSpeed;
        }

        currentNeedle.transform.parent = obj.transform;
        currentNeedle = null;
    }
}
