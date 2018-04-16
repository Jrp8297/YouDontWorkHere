using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFollow : MonoBehaviour {

	//The gameObject you want to overlay to follow
	public GameObject followMe;


	void Start () {
		
	}

	void Update () {
		Vector3 position = followMe.transform.position;

		//Makes sure if it needs to overlay object in question, it will not match the Z axis of it.
		position.z = gameObject.transform.position.z;
		gameObject.transform.position = position;
	}
}
