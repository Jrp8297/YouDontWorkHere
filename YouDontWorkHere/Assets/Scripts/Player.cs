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
	void Update () {
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
		/*if (Input.GetKey("A") && Input.GetKey("D"))
		{
			velocity.x = 0;
		} */

		position += velocity;
		player.transform.position = position;

		//Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
		//pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
		//pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
		//transform.position = Camera.main.ViewportToWorldPoint(pos);

	}
}
