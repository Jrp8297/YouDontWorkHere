using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Vector3 position;
	Vector3 velocity;
	float speed = .1f;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	//Currently Update() only contains the functionality to move the player
	void Update () {
		//Get position and modify it by the velocity that is gotten from User's key inputs
		position = player.transform.position;
		velocity = new Vector3(0,0,0);

		if (Input.GetKey(KeyCode.A))
		{
			velocity.x = -speed;
		}
		if (Input.GetKey(KeyCode.D))
		{
			velocity.x = speed;
		}
		if (Input.GetKey(KeyCode.S))
		{
			velocity.y = -speed;
		}
		if (Input.GetKey(KeyCode.W))
		{
			velocity.y = speed;
		}

		position += velocity;
		player.transform.position = position;

		//This was copied code, no Idea what this was originally for
		//Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		//pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
		//pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
		//transform.position = Camera.main.ViewportToWorldPoint(pos);

	}
}
