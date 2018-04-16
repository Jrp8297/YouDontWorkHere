using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Vector3 position;
	Vector3 velocity;
	float speed = .1f;
	public GameObject player;

	//Is the player holding an order already?
	bool hasOrder;

	//FUTURE Orders held? Like if the player can take like 5 orders at once
	//int ordersHeld = 0;

	//Is the player holding food? Assumes Player can only hold one Table order at a time.
	bool hasFood;

	bool isColliding = false;

	//If the player is found by waiter, gameOver
	//Check SightLine.cs to see how this variable is changed.
	public bool  foundByWaiter = false;

	// Use this for initialization
	void Start () {
		hasOrder = false;
		hasFood = false;
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

		//Check if player is colliding with 
		checkCollision ();

		//This was copied code, no Idea what this was originally for
		//Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		//pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
		//pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
		//transform.position = Camera.main.ViewportToWorldPoint(pos);

	}

	void checkCollision (){
		if (foundByWaiter == true) {
			Debug.Log ("I have been caught, woe me");
		}
	}
}
