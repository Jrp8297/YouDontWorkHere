using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFollow : MonoBehaviour {

	//The gameObject you want to overlay to follow
	public GameObject followMe;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = followMe.transform.position;
		position.z = gameObject.transform.position.z;
		gameObject.transform.position = position;
	}
}
