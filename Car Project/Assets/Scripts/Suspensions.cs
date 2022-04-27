using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspensions : MonoBehaviour {

	public Transform point1;
	public Transform point2;

	public float distanceOffset;
	public float offset;
	public float offsetLenght; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = point1.position - transform.forward * offset; // * (offset *  transform.localScale.z);
		transform.LookAt(point2);
		transform.localScale = new Vector3(1, 1, Vector3.Distance(point1.position, point2.position) * distanceOffset + offsetLenght);
		//transform.LookAt(point2);
	}
}
