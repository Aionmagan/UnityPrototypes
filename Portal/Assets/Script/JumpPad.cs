using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private Transform m_point1;
    [SerializeField] private Transform m_point2;
    [SerializeField] private Transform m_point3;
    [SerializeField] private Transform m_point4;

    public GameObject player;
    public float launchSpeed; 

    [Range(0, 1)]
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        m_point1.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        m_point2.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        m_point3.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        m_point4.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    float c1 = 1 - scale;
    //    float c2 = Mathf.Pow(c1, 2);
    //    float c3 = c2 * c1;
    //    float s2 = Mathf.Pow(scale, 2);
    //    float s3 = Mathf.Pow(scale, 3);

    //    player.transform.position = c3 * m_point1.position +
    //                                3 * c2 * scale * m_point2.position +
    //                                3 * c1 * s2 * m_point3.position +
    //                                s3 * m_point4.position;
   
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_point1.position, m_point2.position);
        Gizmos.DrawLine(m_point2.position, m_point3.position);
        Gizmos.DrawLine(m_point3.position, m_point4.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            player = other.gameObject;
            StartCoroutine(Launch());
        }
    }

    IEnumerator Launch()
    {
        Vector3 oldPosition = Vector3.one;
        Vector3 velDir = Vector3.one;
        float speed = 0;
        float c1, c2, c3, s2, s3;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        while (scale < 1.0f)
        {          
            c1 = 1 - scale;
            c2 = Mathf.Pow(c1, 2);
            c3 = c2 * c1;
            s2 = Mathf.Pow(scale, 2);
            s3 = Mathf.Pow(scale, 3);

            float newspeed = (rb.position - oldPosition).magnitude / Time.fixedDeltaTime;
            //newspeed = Vector3.Distance(rb.position, oldPosition) / Time.fixedDeltaTime;
            if (newspeed > 1) { speed = newspeed; }
            rb.position = c3 * m_point1.position +
                          3 * c2 * scale * m_point2.position +
                          3 * c1 * s2 * m_point3.position +
                          s3 * m_point4.position;
            //Debug.Log(speed);
            yield return new WaitForFixedUpdate();

            velDir = m_point4.position - m_point3.position;
            scale += launchSpeed * Time.deltaTime;
            oldPosition = rb.position;
        }
        Debug.Log(speed);
        rb.velocity = velDir.normalized * speed * 1.5f * Time.deltaTime;///200;
        scale = 0;
        player = null;
    }
}
