using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPicker : MonoBehaviour
{
    public GameObject cube;
    public GameObject pointer; 
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pointer = Instantiate(pointer, Vector2.zero, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RayToBlock();
    }

    void RayToBlock()
    {
        //var destroy = false;
        //var buttonPress = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ? true : false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward * 1, transform.forward, out hit, Mathf.Infinity))
        {
            pointer.transform.position = hit.transform.position;
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(hit.transform.gameObject);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                var p = hit.normal + hit.transform.position;
                Instantiate(cube, p, Quaternion.identity);
            }
        }
        else
        {
            pointer.transform.position = transform.position - transform.forward * 10f;
        }
        
    }
}
