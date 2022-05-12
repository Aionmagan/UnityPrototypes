using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour
{
    public float offset; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var v = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * offset;
        transform.position = v;
    }
}
