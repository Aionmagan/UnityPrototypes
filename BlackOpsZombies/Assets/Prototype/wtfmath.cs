using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wtfmath : MonoBehaviour 
{
	public Transform point1;
	public Transform point2;
	public Transform ai;
	public float scale; 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		LookForward();
	}

	void LookForward()
    {
		var d = Mathf.Clamp(Vector3.Distance(ai.position, point2.position), 0, 5) / 5;
		//next point - current point; 
		scale = (1.0f - d);

		var v = (point2.position + scale * (point1.position - point2.position));
		v = v - transform.position; 
		v.y = 0;

		transform.rotation = Quaternion.Slerp(transform.rotation, 
							 Quaternion.LookRotation(v), 5 * Time.deltaTime);
    }
}
