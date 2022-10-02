using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, ISwitch
{
    public Transform point1;
    public Transform point2;
    public Transform water; 

    public float moveSpeed;
    private float scale = 0;
    private bool isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = point1.position * (moveSpeed + 1) - point2.position;
        water.position = point1.position + scale * (point2.position - point1.position);
    }

    public void TriggerSwitch()
    {
        if (isMove) { return; }
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        isMove = true;

        var s = scale >= 1.0f ? 0.0f : 1.0f ;

        while (s != scale)
        {
            scale = Mathf.MoveTowards(scale, s, moveSpeed * Time.deltaTime);

            yield return new WaitForFixedUpdate(); 
        }

        isMove = false;
    }
}
