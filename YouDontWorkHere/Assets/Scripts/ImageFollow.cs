using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFollow : MonoBehaviour {

	//The gameObject you want to overlay to follow
	public GameObject followMe;

	//FUTURE Orders held? Like if the player can take like 5 orders at once
	int ordersHeld = 0;
	const int MAX_ORDERS = 5;
	public int orderNum = 0; // which table he's taken an order from; 0 = not a table

	//Is the enemy holding food? Assumes Enemy can only hold one Table order at a time.
	public bool hasFood;
	//Is the enemy holding an order already?
	public bool hasOrder;

	void Start () {
		hasFood = false;
		hasOrder = false;	
	}

	void Update () {
		Vector3 position = followMe.transform.position;

		//Makes sure if it needs to overlay object in question, it will not match the Z axis of it.
		position.z = gameObject.transform.position.z;
		gameObject.transform.position = position;
	}

	//Check for collisions with Tables and Such
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Grab the object colliding with the enemy
		GameObject collided = collision.gameObject;

		//Check if they are an enemy, Table, or Kithen
		if (collided.tag == "Enemy") {
			//Never going to happen currently

		} else if (collided.tag == "Table") {
			//This is simply testing purposes
			//If player doesn't have order or food in hands, grab order, if play has food, give food
			if (!hasOrder && !hasFood && collided.GetComponentInChildren<ConsumerScript>() != null && orderNum == 0) {
				if (collided.GetComponentInChildren<ConsumerScript>().phase == 1)
				{
					hasOrder = true;
					Debug.Log("Took Order");
					orderNum = collided.GetComponent<TableScript>().tableNum;
					//collided.GetComponentInChildren<ConsumerScript>().Idling = false;
				}
			} else if (hasFood && orderNum == collided.GetComponent<TableScript>().tableNum) {
				hasFood = false;
				if (collided.GetComponentInChildren<ConsumerScript>() != null)
				{
					collided.GetComponentInChildren<ConsumerScript>().Idling = false;
				}
				Debug.Log("Gave Food");
				orderNum = 0;
			} 

		} else if (collided.tag == "Kitchen") {
			//This is assuming the player only has one order
			if (hasOrder == true) {
				hasOrder = false;
				Debug.Log("Kitchen Gave Food");
				//This is assuming the kitchen doesn't need to prepare the food currently.
				hasFood = true;
			} 
		}
	}
}
